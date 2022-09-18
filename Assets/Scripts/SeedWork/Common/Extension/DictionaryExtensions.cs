using System.Collections.Generic;
using System;

namespace Yaroyan.SeedWork.Common.Extension
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// If the key does not exist, invoke default funciton.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="defaultFunction"></param>
        /// <returns></returns>
        public static TValue GetValueOrDefalut<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> defaultFunction) => dictionary.TryGetValue(key, out var result) ? result : defaultFunction(key);
    }
}