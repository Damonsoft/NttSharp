using NttSharp.Logic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NttSharp.Collections
{
    public readonly struct Chunker<T>
    {
        const int CHUNK_SIZE = 1024;

        public readonly ref T this[int index]
        {
            get => ref pools[index / CHUNK_SIZE][index % CHUNK_SIZE];
        }
        public readonly int Length => (pools.Length * CHUNK_SIZE);
        
        private readonly T[][] pools;

        private Chunker(T[][] pools)
        {
            this.pools = pools;
        }

        public readonly ref T Get(int index)
        {
            return ref pools[index / CHUNK_SIZE][index % CHUNK_SIZE];
        }

        public static int Ensure(ref Chunker<T> chunks, int length)
        {
            if (length >= chunks.pools.Length * CHUNK_SIZE)
            {
                Resize(ref chunks, length);
            }
            return length;
        }

        public static void Resize(ref Chunker<T> chunks, int length)
        {
            T[][] _old = chunks.pools;
            T[][] _temp = chunks.pools;

            Array.Resize(ref _temp, (length / CHUNK_SIZE) + (length % CHUNK_SIZE != 0 ? 1 : 0));

            for(int i = _old.Length; i < _old.Length + (_temp.Length - _old.Length); i++)
            {
                _temp[i] = new T[CHUNK_SIZE];
            }
            chunks = new Chunker<T>(_temp);
        }

        public bool IsWithin(int index) => index < Length;

        public static Chunker<T> Create(int capacity)
        {
            T[][] data = new T[capacity][];

            for (int i = 0; i < capacity; i++)
                data[i] = new T[CHUNK_SIZE];

            return new Chunker<T>(data);
        }
    }
}
