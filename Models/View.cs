using NttSharp.Entities;
using NttSharp.Logic;
using System.Runtime.CompilerServices;

namespace NttSharp.Models
{
    public unsafe ref struct View_Enumerator1
    {
        public readonly int Current => value;

        int index;
        int value;
        ref readonly int* pool_a;

        public View_Enumerator1(int index, in Pool pool_a)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = ref pool_a.Map.Body;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = pool_a;

            while (index >= 0)
            {
                value = Dense.GetUnchecked(ptr_a, index--);

                return true;
            }
            return false;
        }
    }

    public unsafe ref struct View_Enumerator2
    {
        public readonly int Current => value;

        int value;
        int index;
        ref readonly int* pool_a;
        ref readonly int* pool_b;

        public View_Enumerator2(int index, in Pool pool_a, in Pool pool_b)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = ref pool_a.Map.Body;
            this.pool_b = ref pool_b.Map.Body;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = pool_a;
            int* ptr_b = pool_b;

            while (index >= 0)
            {
                value = Dense.GetUnchecked(ptr_a, index--);

                if (Sparse.Contains(ptr_b, value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public unsafe ref struct View_Enumerator3
    {
        public readonly int Current => value;

        int value;
        int index;
        ref readonly int* pool_a;
        ref readonly int* pool_b;
        ref readonly int* pool_c;

        public View_Enumerator3(int index, in Pool pool_a, in Pool pool_b, in Pool pool_c)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = ref pool_a.Map.Body;
            this.pool_b = ref pool_b.Map.Body;
            this.pool_c = ref pool_c.Map.Body;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = pool_a;
            int* ptr_b = pool_b;
            int* ptr_c = pool_c;

            while (index >= 0)
            {
#if DEBUG
                //Debug.Assert(pool_a.Map.IsBody(ptr_a));
                //Debug.Assert(pool_b.Map.IsBody(ptr_b));
                //Debug.Assert(pool_c.Map.IsBody(ptr_c));
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

    public unsafe ref struct View_Enumerator4
    {
        public readonly int Current => value;

        int value;
        int index;
        ref readonly int* pool_a;
        ref readonly int* pool_b;
        ref readonly int* pool_c;
        ref readonly int* pool_d;

        public View_Enumerator4(int index, in Pool pool_a, in Pool pool_b, in Pool pool_c, in Pool pool_d)
        {
            this.value = 0;
            this.index = pool_a.GetDenseLength() - 1;
            this.pool_a = ref pool_a.Map.Body;
            this.pool_b = ref pool_b.Map.Body;
            this.pool_c = ref pool_c.Map.Body;
            this.pool_d = ref pool_d.Map.Body;
        }

        public unsafe bool MoveNext()
        {
            int* ptr_a = pool_a;
            int* ptr_b = pool_b;
            int* ptr_c = pool_c;
            int* ptr_d = pool_d;

            while (index >= 0)
            {
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

    public readonly ref struct View<A>
        where A : unmanaged
    {
        readonly ref readonly Pool pool_a;

        public View(in Pool pool_a)
        {
            this.pool_a = ref pool_a;
        }

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="A"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(int entity) => ref Component.GetComponent<A>(entity, in pool_a.Map, in pool_a.Bytes);
        
        public View_Enumerator1 GetEnumerator() => new View_Enumerator1(0, pool_a);
    }

    public readonly ref struct View<A, B>
        where A : unmanaged
        where B : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;

        public View(in Pool pool_a, in Pool pool_b)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
        }

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="A"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(int entity) => ref Component.GetComponent<A>(entity, in pool_a.Map, in pool_a.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="B"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(int entity) => ref Component.GetComponent<B>(entity, in pool_b.Map, in pool_b.Bytes);

        public View_Enumerator2 GetEnumerator() => new View_Enumerator2(0, pool_a, pool_b);
    }

    public readonly ref struct View<A, B, C>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;
        readonly ref readonly Pool pool_c;

        public View(in Pool pool_a, in Pool pool_b, in Pool pool_c)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
            this.pool_c = ref pool_c;
        }

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="A"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(int entity) => ref Component.GetComponent<A>(entity, in pool_a.Map, in pool_a.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="B"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(int entity) => ref Component.GetComponent<B>(entity, in pool_b.Map, in pool_b.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="C"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref C Get3(int entity) => ref Component.GetComponent<C>(entity, in pool_c.Map, in pool_c.Bytes);

        public View_Enumerator3 GetEnumerator() => new View_Enumerator3(0, in pool_a, in pool_b, in pool_c);
    }

    public readonly ref struct View<A, B, C, D>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
        where D : unmanaged
    {
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;
        readonly ref readonly Pool pool_c;
        readonly ref readonly Pool pool_d;

        public View(in Pool pool_a, in Pool pool_b, in Pool pool_c, in Pool pool_d)
        {
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
            this.pool_c = ref pool_c;
            this.pool_d = ref pool_d;
        }

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="A"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1(int entity) => ref Component.GetComponent<A>(entity, in pool_a.Map, in pool_a.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="B"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2(int entity) => ref Component.GetComponent<B>(entity, in pool_b.Map, in pool_b.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="C"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref C Get3(int entity) => ref Component.GetComponent<C>(entity, in pool_c.Map, in pool_c.Bytes);

        /// <summary>
        /// Gets a reference to
        /// the component of type: 
        /// <typeparamref name="D"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref D Get4(int entity) => ref Component.GetComponent<D>(entity, in pool_d.Map, in pool_d.Bytes);

        public View_Enumerator4 GetEnumerator() => new View_Enumerator4(0, in pool_a, in pool_b, in pool_c, in pool_d);
    }
}
