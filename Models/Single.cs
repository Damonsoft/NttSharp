using NttSharp.Entities;
using NttSharp.Extensions;

namespace NttSharp.Models
{
    public readonly struct Single<A>
        where A : unmanaged
    {
        public readonly int Entity => entity;

        readonly int entity;
        readonly Pool pool_a;

        public Single(int entity, Pool pool_a)
        {
            this.entity = entity;
            this.pool_a = pool_a;
        }

        public readonly ref A Get1() => ref PoolEx.GetComponent<A>(pool_a, entity);

        public readonly void Set1(in A value) => PoolEx.WriteComponent(pool_a, entity, in value);
    }

    public readonly struct Single<A, B>
        where A : unmanaged
        where B : unmanaged
    {
        public readonly int Entity => entity;

        readonly int entity;
        readonly Pool pool_a;
        readonly Pool pool_b;

        public Single(int entity, Pool pool_a, Pool pool_b)
        {
            this.entity = entity;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
        }

        public readonly ref A Get1() => ref PoolEx.GetComponent<A>(pool_a, entity);
        public readonly ref B Get2() => ref PoolEx.GetComponent<B>(pool_b, entity);

        public readonly void Set1(in A value) => PoolEx.WriteComponent(pool_a, entity, in value);
        public readonly void Set2(in B value) => PoolEx.WriteComponent(pool_b, entity, in value);
    }

    public readonly struct Single<A, B, C>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
    {
        readonly int entity;
        readonly Pool pool_a;
        readonly Pool pool_b;
        readonly Pool pool_c;

        public Single(int entity, Pool pool_a, Pool pool_b, Pool pool_c)
        {
            this.entity = entity;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_b;
        }

        public readonly ref A Get1() => ref PoolEx.GetComponent<A>(pool_a, entity);
        public readonly ref B Get2() => ref PoolEx.GetComponent<B>(pool_b, entity);
        public readonly ref C Get3() => ref PoolEx.GetComponent<C>(pool_c, entity);

        public readonly void Set1(in A value) => PoolEx.WriteComponent(pool_a, entity, in value);
        public readonly void Set2(in B value) => PoolEx.WriteComponent(pool_b, entity, in value);
        public readonly void Set3(in C value) => PoolEx.WriteComponent(pool_c, entity, in value);
    }

    public readonly struct Single<A, B, C, D>
        where A : unmanaged
        where B : unmanaged
        where C : unmanaged
        where D : unmanaged
    {
        readonly int entity;
        readonly Pool pool_a;
        readonly Pool pool_b;
        readonly Pool pool_c;
        readonly Pool pool_d;

        public Single(int entity, Pool pool_a, Pool pool_b, Pool pool_c, Pool pool_d)
        {
            this.entity = entity;
            this.pool_a = pool_a;
            this.pool_b = pool_b;
            this.pool_c = pool_c;
            this.pool_d = pool_d;
        }

        public readonly ref A Get1() => ref PoolEx.GetComponent<A>(pool_a, entity);
        public readonly ref B Get2() => ref PoolEx.GetComponent<B>(pool_b, entity);
        public readonly ref C Get3() => ref PoolEx.GetComponent<C>(pool_c, entity);
        public readonly ref D Get4() => ref PoolEx.GetComponent<D>(pool_d, entity);

        public readonly void Set1(in A value) => PoolEx.WriteComponent(pool_a, entity, in value);
        public readonly void Set2(in B value) => PoolEx.WriteComponent(pool_b, entity, in value);
        public readonly void Set3(in C value) => PoolEx.WriteComponent(pool_c, entity, in value);
        public readonly void Set4(in D value) => PoolEx.WriteComponent(pool_d, entity, in value);

    }
}
