using System;
using System.Collections.Generic;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Core
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object?> GetValues();

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            return obj is ValueObject other && GetValues().SequenceEqual(other.GetValues());
        }

        public bool Equals(ValueObject? other)
        {
            return !ReferenceEquals(other, null) && GetValues().SequenceEqual(other.GetValues());
        }

        public override int GetHashCode() =>
            GetValues()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x * 21 + y);

        protected static bool EqualOperator(ValueObject? left, ValueObject? right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
        {
            return !(EqualOperator(left, right));
        }

        public static bool operator ==(ValueObject? first, ValueObject? second)
        {
            return !ReferenceEquals(first, null) && first.Equals(second);
        }

        public static bool operator !=(ValueObject? first, ValueObject? second)
        {
            return ReferenceEquals(first, null) || !(first == second);
        }
    }
}
