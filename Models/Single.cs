using NttSharp.Entities;
using System.Runtime.CompilerServices;

namespace NttSharp.Models
{
    public readonly ref struct Single<A>
        where A : unmanaged
    {
        public readonly ntt Entity => entity;

        readonly ntt entity;
        readonly ref readonly Pool pool_a;

        public Single(ntt entity, in Pool pool_a)
        {
            this.entity = entity;
            this.pool_a = ref pool_a;
        }

        public readonly ref A Get1() => ref pool_a.GetComponent<A>(entity);

        public readonly void Set1(in A value) => pool_a.SetComponent(entity, in value);
    }

    public readonly ref struct Single<A, B>
        where A : unmanaged
        where B : unmanaged
    {
        public readonly ntt Entity => entity;

        readonly ntt entity;
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;

        public Single(ntt entity, in Pool pool_a, in Pool pool_b)
        {
            this.entity = entity;
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
        }

        public readonly ref A Get1() => ref pool_a.GetComponent<A>(entity);
        public readonly ref B Get2() => ref pool_b.GetComponent<B>(entity);

        public readonly void Set1(in A value) => pool_a.SetComponent(entity, in value);
        public readonly void Set2(in B value) => pool_b.SetComponent(entity, in value);
    }

    public readonly ref struct Single<A, B, C>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        readonly ref ntt entity;
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;
        readonly ref readonly Pool pool_c;

        public Single(ntt entity, in Pool pool_a, in Pool pool_b, in Pool pool_c)
        {
            this.entity = entity;
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
            this.pool_c = ref pool_c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref A Get1() => ref pool_a.GetComponent<A>(entity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref B Get2() => ref pool_b.GetComponent<B>(entity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref C Get3() => ref pool_c.GetComponent<C>(entity);

        public readonly void Set1(in A value) => pool_a.SetComponent(entity, in value);
        public readonly void Set2(in B value) => pool_b.SetComponent(entity, in value);
        public readonly void Set3(in C value) => pool_c.SetComponent(entity, in value);
    }

    public readonly ref struct Single<A, B, C, D>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
        where D : unmanaged
    {
        readonly ntt entity;
        readonly ref readonly Pool pool_a;
        readonly ref readonly Pool pool_b;
        readonly ref readonly Pool pool_c;
        readonly ref readonly Pool pool_d;

        public Single(ntt entity, in Pool pool_a, in Pool pool_b, in Pool pool_c, in Pool pool_d)
        {
            this.entity = entity;
            this.pool_a = ref pool_a;
            this.pool_b = ref pool_b;
            this.pool_c = ref pool_c;
            this.pool_d = ref pool_d;
        }

        public readonly ref A Get1() => ref pool_a.GetComponent<A>(entity);
        public readonly ref B Get2() => ref pool_b.GetComponent<B>(entity);
        public readonly ref C Get3() => ref pool_c.GetComponent<C>(entity);
        public readonly ref D Get4() => ref pool_d.GetComponent<D>(entity);

        public readonly void Set1(in A value) => pool_a.SetComponent(entity, in value);
        public readonly void Set2(in B value) => pool_b.SetComponent(entity, in value);
        public readonly void Set3(in C value) => pool_c.SetComponent(entity, in value);
        public readonly void Set4(in D value) => pool_d.SetComponent(entity, in value);
    }
}
