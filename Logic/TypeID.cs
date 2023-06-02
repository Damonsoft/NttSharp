using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NttSharp.Logic
{
    public static class TypeID
    {
        internal static int _Increment = 0;
        public static int Count => TypeID._Increment;
    }

    public static class TypeID<T>
    {
        public readonly static int Unique;

        static TypeID()
        {
            Unique = Interlocked.Increment(ref TypeID._Increment) + 1;
        }
    }
}
