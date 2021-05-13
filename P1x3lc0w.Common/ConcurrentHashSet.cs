using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace P1x3lc0w.Common
{
    /// <summary>
    /// A Thread-Safe wrapper for HashSet&lt;T&gt;
    /// </summary>
    /// <remarks>
    /// Modified, Originally written by https://stackoverflow.com/users/344143/ben-mosher
    /// (https://stackoverflow.com/questions/4306936/how-to-implement-concurrenthashset-in-net)
    /// Licenced under Creative Commons Attribution-ShareAlike 3.0 Unported (CC BY-SA 3.0)(https://creativecommons.org/licenses/by-sa/3.0/)
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentHashSet<T> : IDisposable, ISet<T>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly HashSet<T> _hashSet = new HashSet<T>();

        #region Implementation of ICollection<T> ...ish
        public bool Add(T item)
        {
            try
            {
                _lock.EnterWriteLock();
                return _hashSet.Add(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            try
            {
                _lock.EnterWriteLock();
                _hashSet.Clear();
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public bool Contains(T item)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.Contains(item);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }

        public bool Remove(T item)
        {
            try
            {
                _lock.EnterWriteLock();
                return _hashSet.Remove(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public int Count
        {
            get
            {
                try
                {
                    _lock.EnterReadLock();
                    return _hashSet.Count;
                }
                finally
                {
                    if (_lock.IsReadLockHeld) _lock.ExitReadLock();
                }
            }
        }

        public bool IsReadOnly => false;
        #endregion

        #region ISet<T>
        public void ExceptWith(IEnumerable<T> other)
        {
            try
            {
                _lock.EnterWriteLock();
                _hashSet.ExceptWith(other);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }
        public void IntersectWith(IEnumerable<T> other)
        {
            try
            {
                _lock.EnterWriteLock();
                _hashSet.IntersectWith(other);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.IsProperSubsetOf(other);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.IsProperSupersetOf(other);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }
        public bool IsSubsetOf(IEnumerable<T> other) 
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.IsSubsetOf(other);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.IsSupersetOf(other);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }
        public bool Overlaps(IEnumerable<T> other)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.Overlaps(other);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }
        public bool SetEquals(IEnumerable<T> other)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.SetEquals(other);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            try
            {
                _lock.EnterWriteLock();
                _hashSet.SymmetricExceptWith(other);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }
        public void UnionWith(IEnumerable<T> other) 
        {
            try
            {
                _lock.EnterWriteLock();
                _hashSet.UnionWith(other);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }
        void ICollection<T>.Add(T item)
        {
            Add(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            try
            {
                _lock.EnterReadLock();
                _hashSet.CopyTo(array, arrayIndex);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            try
            {
                _lock.EnterReadLock();
                T[] items = new T[_hashSet.Count];
                _hashSet.CopyTo(items);
                return ((IEnumerable<T>)items).GetEnumerator();
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
            
        }
        IEnumerator IEnumerable.GetEnumerator() 
        {
            return GetEnumerator();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_lock != null) _lock.Dispose();
        }
        #endregion
    }
}