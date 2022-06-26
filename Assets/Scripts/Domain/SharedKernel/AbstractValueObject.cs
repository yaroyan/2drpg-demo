using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Yaroyan.Game.DDD.SharedKernel
{
    /// <summary>
    /// 値オブジェクトの抽象クラス
    /// @see https://docs.microsoft.com/ja-jp/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
    /// </summary>
    public abstract class AbstractValueObject<T> : IEquatable<AbstractValueObject<T>>
    {
        protected static bool EqualOperator(AbstractValueObject<T> left, AbstractValueObject<T> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(AbstractValueObject<T> left, AbstractValueObject<T> right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj) || obj.GetType() != this.GetType()) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((AbstractValueObject<T>)obj);
        }

        public bool Equals(AbstractValueObject<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(AbstractValueObject<T> one, AbstractValueObject<T> two)
        {
            return EqualOperator(one, two);
        }

        public static bool operator !=(AbstractValueObject<T> one, AbstractValueObject<T> two)
        {
            return NotEqualOperator(one, two);
        }
        // Other utility methods
    }
}