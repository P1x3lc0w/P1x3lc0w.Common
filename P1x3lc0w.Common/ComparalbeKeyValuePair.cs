using System;
using System.Diagnostics.CodeAnalysis;

namespace P1x3lc0w.Common
{
    public struct ComparalbeKeyValuePair<TKey, TValue> : IComparable<TKey>, IComparable<ComparalbeKeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
    {
        public TKey key;
        public TValue value;

        public ComparalbeKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public int CompareTo(TKey other) => key.CompareTo(other);

        public int CompareTo(ComparalbeKeyValuePair<TKey, TValue> other) => key.CompareTo(other.key);
    }
}