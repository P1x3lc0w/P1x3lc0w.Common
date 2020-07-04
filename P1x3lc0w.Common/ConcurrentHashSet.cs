﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace P1x3lc0w.Common
{
    /// <summary>
    /// A Thread-Safe wrapper for HashSet&lt;T&gt;
    /// </summary>
    /// <remarks>
    /// Originally written by https://stackoverflow.com/users/344143/ben-mosher
    /// (https://stackoverflow.com/questions/4306936/how-to-implement-concurrenthashset-in-net)
    /// Licenced under Creative Commons Attribution-ShareAlike 3.0 Unported (CC BY-SA 3.0)(https://creativecommons.org/licenses/by-sa/3.0/)
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentHashSet<T> : IDisposable
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
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_lock != null) _lock.Dispose();
        }
        #endregion
    }
}