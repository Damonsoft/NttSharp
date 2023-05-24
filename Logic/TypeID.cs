using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NttSharp.Logic
{
    internal static class _Internal_Type_Increment
    {
        //
        internal static int _Increment = 0;
        //
        internal static Dictionary<int, Type> _Types = new Dictionary<int, Type>();
    }

    public static class TypeID<T>
    {
        public readonly static int Unique;

        static TypeID()
        {
            //
            Unique = Interlocked.Increment(ref _Internal_Type_Increment._Increment) + 1;
            //
            Dictionary<int, Type> copy = new Dictionary<int, Type>(_Internal_Type_Increment._Types);
            //
            copy[Unique] = typeof(T);
            //
            Interlocked.Exchange(ref _Internal_Type_Increment._Types, copy);
        }
    }

    public static class TypeID
    {
        public static Type Lookup(int unique) => _Internal_Type_Increment._Types[unique];
    }
}
