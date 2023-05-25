using NttSharp.Logic;
using System.Runtime.CompilerServices;

namespace NttSharp.Collections
{
    public readonly unsafe struct DenseWrap
    {
        public readonly ref int Length => ref _pointer[0];
        public readonly ref int this[int index] => ref _pointer[(index + 1) * 2];

        readonly int* _pointer;

        internal DenseWrap(int* pointer)
        {
            _pointer = pointer;
        }

        public static DenseWrap Create(long[] set)
        {
            return new DenseWrap((int*)Unsafe.AsPointer(ref set[0]));
        }
    }
    public readonly unsafe struct SparseWrap
    {
        public readonly ref int Length => ref _pointer[1];
        public readonly ref int this[int index] => ref _pointer[(index + 1) * 2 + 1];

        readonly int* _pointer;

        internal SparseWrap(int* pointer)
        {
            _pointer = pointer;
        }

        public static SparseWrap Create(long[] set)
        {
            return new SparseWrap((int*)Unsafe.AsPointer(ref set[0]));
        }
    }

    public readonly unsafe struct DenseEnumerable
    {
        private readonly int* Body;

        public DenseEnumerable(int* body)
        {
            Body = body;
        }

        public DenseEnumerator GetEnumerator() => new DenseEnumerator(Body);
    }
    public unsafe struct DenseEnumerator
    {
        public int Current => current;

        int index;
        int current;
        int* pointer;

        public DenseEnumerator(int* pointer)
        {
            index = 0;
            current = 0;
            this.pointer = pointer;
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            while (index < pointer[-2])
            {
                current = pointer[index++ * 2];

                return true;
            }
            return false;
        }

        public void Reset() { }
    }

    public readonly unsafe struct SparseSet
    {
        public readonly int SparseLength => Body[-1];
        public readonly int DenseLength => Body[-2];

        internal readonly int* Body;

        internal readonly int[] Reference;

        private SparseSet(int* Body, int[] Reference)
        {
            this.Body = Body;
            this.Reference = Reference;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly DenseEnumerable EnumerateDense() => new DenseEnumerable(Body);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IsWithinSparse(int index) => index < Body[-1];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IsWithinDense(int index) => index < Body[-2];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(int index)
        {
            if (index < Body[-1])
            {
                int dense_index = Body[index * 2 + 1];

                if (dense_index < Body[-2])
                {
                    return Body[dense_index * 2] == index;
                }
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetSparse(int index)
        {
            if (index < Body[-1])
            {
                return Body[index * 2 + 1];
            }
            throw new IndexOutOfRangeException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetDense(int index)
        {
            if (index < Body[-2])
            {
                return Body[index * 2];
            }
            throw new IndexOutOfRangeException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetSparseUnsafe(int index) => Body[index * 2 + 1];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetDenseUnsafe(int index) => Body[index * 2];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly SparseWrap WrapSparse() => new SparseWrap(&Body[-2]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly DenseWrap WrapDense() => new DenseWrap(&Body[-2]);

#if DEBUG
        public readonly bool IsBody(int* pointer) => (int*)Unsafe.AsPointer(ref Reference[2]) == pointer;
#endif

        public static SparseSet Create(int length)
        {
            int[] set = Sparse.Create(length, true);

            return new SparseSet((int*)Unsafe.AsPointer(ref set[2]), set);
        }

        public static int Add(ref SparseSet set, int offset)
        {
            int[] array = set.Reference;

            int result = Sparse.Add(ref array, offset);

            set = new SparseSet((int*)Unsafe.AsPointer(ref array[2]), array);

            return result;
        }

        public static void Remove(SparseSet set, int offset)
        {
            Sparse.Remove(set.Reference, offset);
        }

        public static void Resize(ref SparseSet set, int length)
        {
            int[] _internal = set.Reference;

            // Resize the array and preserve
            // the pinned aspect of the next array.
            Helpers.ResizeArray(ref _internal, length, pinned: true);

#if DEBUG
            Array.Fill(_internal, 0x0dead); 
#endif
            // Write the new length of the
            // array to the header of the
            // buffer while preserving the
            // length of the dense set.
            _internal[0] = Sparse.Write(_internal, -1, length - 1);

            // Overwrite the original set
            // from the caller with the data.
            set = new SparseSet((int*)Unsafe.AsPointer(ref _internal[1]), _internal); ;
        }
    }
}
