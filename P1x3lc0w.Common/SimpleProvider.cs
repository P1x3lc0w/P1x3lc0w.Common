using System;
using System.Collections.Generic;
using System.Text;

namespace P1x3lc0w.Common
{
    public class SimpleProvider<T> : IProvider<T>
    {
        public T ProvidedObject { get; protected set; }

        public SimpleProvider(T providedObject)
        {
            ProvidedObject = providedObject;
        }
        public T Get() => ProvidedObject;

        public static implicit operator SimpleProvider<T>(T obj) 
            => new SimpleProvider<T>(obj);
    }
}
