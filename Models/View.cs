using NttSharp.Entities;
using NttSharp.Logic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NttSharp.Models
{
    public struct View_Enumerator1
    {
        public readonly int Current => value;

        int index;
        int value;
        Pool pool_a;

        public View_Enumerator1(int index, Pool pool_a)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = pool_a;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = pool_a.Map.Body;

            while (index >= 0)
            {
#if DEBUG
                Debug.Assert(pool_a.Map.IsBody(ptr_a));
#endif
                value = Dense.GetUnchecked(ptr_a, index--);

                return true;
            }
            return false;
        }
    }

    public struct View_Enumerator2
    {
        public readonly int Current => value;

        int value;
        int index;
        Pool pool_a;
        Pool pool_b;

        public View_Enumerator2(int index, Pool pool_a, Pool pool_b)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = pool_a.Map.Body;
            int* ptr_b = pool_b.Map.Body;

            while (index >= 0)
            {
#if DEBUG
                Debug.Assert(pool_a.Map.IsBody(ptr_a));
                Debug.Assert(pool_b.Map.IsBody(ptr_b));
#endif
                value = Dense.GetUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public struct View_Enumerator3
    {
        public readonly int Current => value;

        int value;
        int index;
        Pool pool_a;
        Pool pool_b;
        Pool pool_c;

        public View_Enumerator3(int index, Pool pool_a, Pool pool_b, Pool pool_c)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = pool_a.Map.Body;
            int* ptr_b = pool_b.Map.Body;
            int* ptr_c = pool_c.Map.Body;

            while (index >= 0)
            {
#if DEBUG
                Debug.Assert(pool_a.Map.IsBody(ptr_a));
                Debug.Assert(pool_b.Map.IsBody(ptr_b));
                Debug.Assert(pool_c.Map.IsBody(ptr_c));
#endif
                value = Dense.GetUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value) &&
                    Sparse.Contains(ptr_c, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public struct View_Enumerator4
    {
        public readonly int Current => value;

        int value;
        int index;
        Pool pool_a;
        Pool pool_b;
        Pool pool_c;
        Pool pool_d;

        public View_Enumerator4(int index, Pool pool_a, Pool pool_b, Pool pool_c, Pool pool_d)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
            this.pool_d = pool_d;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = pool_a.Map.Body;
            int* ptr_b = pool_b.Map.Body;
            int* ptr_c = pool_c.Map.Body;
            int* ptr_d = pool_d.Map.Body;

            while (index >= 0)
            {
#if DEBUG
                Debug.Assert(pool_a.Map.IsBody(ptr_a));
                Debug.Assert(pool_b.Map.IsBody(ptr_b));
                Debug.Assert(pool_c.Map.IsBody(ptr_c));
                Debug.Assert(pool_d.Map.IsBody(ptr_d));
#endif

                value = Dense.GetUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value) &&
                    Sparse.Contains(ptr_c, value) &&
                    Sparse.Contains(ptr_d, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public readonly unsafe struct View<A>
        where A : unmanaged
    {
        readonly Pool pool_a;

        public View(Pool pool_a)
        {
            this.pool_a = pool_a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(int entity) => ref Component.GetComponent<A>(entity, in pool_a.Map, in pool_a.Bytes);
        public View_Enumerator1 GetEnumerator() => new View_Enumerator1(0, pool_a);
    }

    public readonly unsafe struct View<A, B>
        where A : unmanaged
        where B : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;

        public View(Pool pool_a, Pool pool_b)
        {
            this.pool_a = pool_a;
            this.pool_b = pool_b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(int entity) => ref Component.GetComponent<A>(entity, in pool_a.Map, in pool_a.Bytes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(int entity) => ref Component.GetComponent<B>(entity, in pool_b.Map, in pool_b.Bytes);

        public View_Enumerator2 GetEnumerator() => new View_Enumerator2(0, pool_a, pool_b);
    }

    public readonly unsafe struct View<A, B, C>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;
        readonly Pool pool_c;

        public View(Pool pool_a, Pool pool_b, Pool pool_c)
        {
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(int entity) => ref Component.GetComponent<A>(entity, in pool_a.Map, in pool_a.Bytes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(int entity) => ref Component.GetComponent<B>(entity, in pool_b.Map, in pool_b.Bytes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref C Get3(int entity) => ref Component.GetComponent<C>(entity, in pool_c.Map, in pool_c.Bytes);

        public View_Enumerator3 GetEnumerator() => new View_Enumerator3(0, pool_a, pool_b, pool_c);
    }

    public readonly unsafe struct View<A, B, C, D>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
        where D : unmanaged
    {
        readonly Pool pool_a;
        readonly Pool pool_b;
        readonly Pool pool_c;
        readonly Pool pool_d;

        public View(Pool pool_a, Pool pool_b, Pool pool_c, Pool pool_d)
        {
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
            this.pool_d = pool_d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(int entity) => ref Component.GetComponent<A>(entity, in pool_a.Map, in pool_a.Bytes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(int entity) => ref Component.GetComponent<B>(entity, in pool_b.Map, in pool_b.Bytes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref C Get3(int entity) => ref Component.GetComponent<C>(entity, in pool_c.Map, in pool_c.Bytes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref D Get4(int entity) => ref Component.GetComponent<D>(entity, in pool_d.Map, in pool_d.Bytes);

        public View_Enumerator4 GetEnumerator() => new View_Enumerator4(0, pool_a, pool_b, pool_c, pool_d);
    }
}
