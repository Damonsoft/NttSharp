using NttSharp.Logic;
using System.Runtime.CompilerServices;

namespace NttSharp.Collections
{
    public readonly unsafe struct DenseEnumerable
    {
        private readonly ntt* Body;

        public DenseEnumerable(ntt* body)
        {
            Body = body;
        }

        public DenseEnumerator GetEnumerator() => new DenseEnumerator(Body);
    }
    public unsafe struct DenseEnumerator
    {
        public ntt Current => current;

        int index;
        ntt current;
        ntt* pointer;

        public DenseEnumerator(ntt* pointer)
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
        public readonly ntt SparseLength => Body[-1];
        public readonly ntt DenseLength => Body[-2];

        internal readonly ntt* Body;
        internal readonly ntt[] Head;
        internal readonly int _SparseLength; // cached
        internal readonly int _DenseLength; // cached

        private SparseSet(ntt* Body, ntt[] Head, int sparseLength, int denseLength)
        {
            this.Body = Body;
            this.Head = Head;
            this._SparseLength = sparseLength;
            this._DenseLength = denseLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly DenseEnumerable EnumerateDense() => new DenseEnumerable(Body);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IsWithinSparse(ntt index) => index < Body[-1];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IsWithinDense(ntt index) => index < Body[-2];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(ntt index)
        {
            if (index < Body[-1])
            {
                ntt dense_index = Body[index * 2 + 1];

                if (dense_index < Body[-2])
                {
                    return Body[dense_index * 2] == index;
                }
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ntt GetSparse(ntt index)
        {
            if (index < Body[-1])
            {
                return Body[index * 2 + 1];
            }
            return Fault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ntt GetDense(ntt index)
        {
            if (index < Body[-2])
            {
                return Body[index * 2];
            }
            return Fault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ntt GetSparseUnsafe(ntt index) => Body[index * 2 + 1];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ntt GetDenseUnsafe(ntt index) => Body[index * 2];

        public static SparseSet Create(int length)
        {
            ntt[] set = GC.AllocateArray<ntt>(length, pinned: true);

            Sparse.Init(set);

            return new SparseSet((ntt*)Unsafe.AsPointer(ref set[2]), set, set[1], set[0]);
        }

        public static ntt Add(ref SparseSet set, ntt offset)
        {
            ntt[] head = set.Head;

            ntt result = Sparse.Add(ref head, offset);

            set = new SparseSet((ntt*)Unsafe.AsPointer(ref head[2]), head, head[1], head[0]);

            return result;
        }

        public static void Remove(ref SparseSet set, ntt offset)
        {
            int[] head = set.Head;

            Sparse.Remove(head, offset);

            set = new SparseSet((ntt*)Unsafe.AsPointer(ref head[2]), head, head[1], head[0]);
        }

        public static void Resize(ref SparseSet set, int length)
        {
            ntt[] head = set.Head;

            // Resize the array and preserve
            // the pinned aspect of the next array.
            Helpers.ResizeArray(ref head, length, pinned: true);

            // Write the new length of the
            // array to the header of the
            // buffer while preserving the
            // length of the dense set.
            Sparse.Poke(head, -1, length - 1);

            // Overwrite the original set
            // from the caller with the data.
            set = new SparseSet((ntt*)Unsafe.AsPointer(ref head[2]), head, head[1], head[0]);
        }

        static int Fault() => throw new IndexOutOfRangeException();
    }
}