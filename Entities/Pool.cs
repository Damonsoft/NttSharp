using NttSharp.Collections;
using NttSharp.Logic;
using System.Runtime.CompilerServices;

namespace NttSharp.Entities
{
    public sealed unsafe class Pool
    {
        public readonly Type Type;
        public readonly int Unique;
        public readonly int Step;
        public int Count;
        public byte[] Bytes;
        public byte* BytePtr;
        public SparseSet Set;

        private Pool(Type _type, int type, int step, int count,SparseSet set, byte[] bytes)
        {
            this.Type = _type;
            this.Unique = type;
            this.Step = step;
            this.Count = count;
            this.Bytes = bytes;
            this.BytePtr = (byte*)Unsafe.AsPointer(ref bytes[0]);
            this.Set = set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(int entity) => Set.Contains(entity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetDenseLength() => Set.DenseLength;

        public static unsafe Pool Create<T>(int capacity) where T : unmanaged
        {
            return new Pool(typeof(T), TypeID<T>.Unique, sizeof(T), capacity, SparseSet.Create(512), GC.AllocateArray<byte>(capacity * sizeof(T), pinned: true));
        }
    }
}
