using System;
using System.Collections.Generic;
using System.Text;

namespace P1x3lc0w.Common
{
    interface IProvider<T>
    {
        public T Get();
    }
}
