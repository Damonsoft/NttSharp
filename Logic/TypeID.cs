using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NttSharp.Logic
{
    public readonly struct TypeDetails
    {
        public readonly Type Handle;
        public readonly int Unique;
        public readonly int Size;

        public TypeDetails(Type handle, int unique, int size)
        {
            Handle = handle;
            Unique = unique;
            Size = size;
        }
    }

    internal static class _Internal_Type_Increment
    {
        //
        internal static int _Increment = 0;
        //
        internal static Dictionary<int, TypeDetails> _Types = new Dictionary<int, TypeDetails>();
    }

    public static class TypeID<T>
    {
        public readonly static int Unique;

        unsafe static TypeID()
        {
            //
            Unique = Interlocked.Increment(ref _Internal_Type_Increment._Increment) + 1;
            //
            Dictionary<int, TypeDetails> copy = new Dictionary<int, TypeDetails>(_Internal_Type_Increment._Types);
            //
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
            copy[Unique] = new TypeDetails(typeof(T), Unique, sizeof(T));
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
                              //
            Interlocked.Exchange(ref _Internal_Type_Increment._Types, copy);
        }
    }

    public static class TypeID
    {
        public static int Count => _Internal_Type_Increment._Increment;
        public static TypeDetails Lookup(int unique) => _Internal_Type_Increment._Types[unique];
    }
}
