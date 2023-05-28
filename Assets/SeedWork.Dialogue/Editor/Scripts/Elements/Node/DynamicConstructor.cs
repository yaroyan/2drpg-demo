using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    public class DynamicConstructor<T> where T : class
    {
        // Typeごとのコンストラクタを格納
        static readonly Dictionary<Type, Dictionary<string, Func<object[], object>>> s_cachedConstructor;
        readonly Dictionary<(Type, string), Func<object[], object>> _cachedConstructor;
        public static readonly string Connector = " ";

        public DynamicConstructor()
        {
            _cachedConstructor = BuildConstructorCache(typeof(T));
        }

        static string GenerateArgsKey(params object[] args) => GenerateArgsKey(args.Select(e => e.GetType()));

        static string GenerateArgsKey(ConstructorInfo constructorInfo) => GenerateArgsKey(constructorInfo.GetParameters().Select(e => e.ParameterType));

        static string GenerateArgsKey(IEnumerable<Type> types) => string.Join(Connector, types.Select(e => e.ToString()));

        static (Type, string) GenerateKey(Type type, params object[] args) => (type, GenerateArgsKey(args));

        /// <summary>
        /// 指定のクラスとサブクラスのコンストラクタのキャッシュを生成します。
        /// </summary>
        /// <param name="baseType">キャッシュ生成対象のクラスの型</param>
        /// <returns>コンストラクタのキャッシュ</returns>
        static Dictionary<(Type, string), Func<object[], object>> BuildConstructorCache(Type baseType)
        {
            Dictionary<(Type, string), Func<object[], object>> cache = new();
            foreach (var type in baseType.Assembly.GetTypes().Where(e => e.IsSubclassOf(baseType) && !e.IsAbstract))
            {
                foreach (var entry in CompileConstructorSequentially(type))
                {
                    var key = (type, entry.Key);
                    cache[key] = entry.Expression;
                }
            }
            return cache;
        }

        /// <summary>
        /// 逐次的にコンストラクタをコンパイルします。
        /// </summary>
        /// <param name="type">コンパイル対象の型</param>
        /// <returns>コンストラクタのタプル</returns>
        static IEnumerable<(string Key, Func<object[], object> Expression)> CompileConstructorSequentially(Type type)
        {
            var args = Expression.Parameter(typeof(object[]), "args");
            foreach (var ctor in type.GetConstructors())
            {
                var key = GenerateArgsKey(ctor);
                var parameters = ctor.GetParameters()
                    .Select((x, index) => Expression.Convert(Expression.ArrayIndex(args, Expression.Constant(index)), x.ParameterType))
                    .ToArray();
                var body = Expression.New(ctor, parameters);
                var expression = Expression.Lambda<Func<object[], object>>(Expression.Convert(body, typeof(object)), args).Compile();
                yield return (key, expression);
            }
        }

        public object Construct(Type type, params object[] args) => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(T))
            ? _cachedConstructor[GenerateKey(type, args)].Invoke(args)
            : throw new ArgumentException($"{typeof(T).Name}のサブクラスではありません。");

        public U Construct<U>(params object[] args) where U : class, T => _cachedConstructor[GenerateKey(typeof(U), args)].Invoke(args) as U;
    }
}