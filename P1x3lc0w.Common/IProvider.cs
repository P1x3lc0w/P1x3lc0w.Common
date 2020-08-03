using System;
using System.Collections.Generic;
using System.Text;

namespace P1x3lc0w.Common
{
    public interface IProvider<T>
    {
        T Get();
    }
}
