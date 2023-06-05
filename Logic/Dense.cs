using System.Runtime.CompilerServices;

namespace NttSharp.Logic
{
    public static class Dense
    {
        public static ntt Peek(ntt[] data, ntt offset)
        {
            if (offset < data[0])
            {
                return data[(offset + 1) * Sparse.ROW_LENGTH];
            }
            throw new IndexOutOfRangeException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ntt PeekUnchecked(ntt* data, ntt offset) => data[offset * Sparse.ROW_LENGTH];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ntt PokeUnsafe(ntt[] data, ntt offset, ntt value)
        {
            return data[(offset + 1) * Sparse.ROW_LENGTH] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ntt PokeLength(ntt[] data, ntt value)
        {
            return data[0] = value;
        }

        public static ntt Add(ref ntt[] data, ntt value)
        {
            // Get the index the next
            // value will be places at.
            ntt slot = PeekLength(data);
            // Get the size of the set
            // after this value is added.
            ntt size = PeekLength(data) + 1;

            // Check if the size is greater
            // then the size of the array.
            if (size >= Sparse.GetLength(data))
            {
                // If so reszize the set
                // so we can fit the value.
                Sparse.ResizeSet(ref data, (int)((size + 1) * 2));

                // Restart the process...
                return Add(ref data, value);
            }
            // Write the new length
            // to the header.
            PokeLength(data, size);
            // Write the value to 
            // the set.
            PokeUnsafe(data, slot, value);
            // Return the index
            // of the value.
            return slot;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ntt PeekLength(ntt[] data) => data[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ntt PeekLength(ntt* data) => data[-2];
    }
}