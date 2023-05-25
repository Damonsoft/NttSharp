using NttSharp.Logic;

namespace NttSharp.Entities
{
    public sealed partial class World
    {
        public bool Has<A>(int entity) => Has(entity, TypeID<A>.Unique);

        public bool Has(int entity, int type)
        {
            Pool? pool = TryGetPool(type);

            if (pool is not null)
            {
                return pool.Contains(entity);
            }
            return false;
        }

        public bool HasAny<A, B>(int entity)
            where A : unmanaged
            where B : unmanaged
        {
            Pool? pool_a = TryGetPool<A>();
            if (pool_a is null) return false;
            Pool? pool_b = TryGetPool<B>();
            if (pool_b is null) return false;
            return pool_a.Contains(entity) || pool_b.Contains(entity);
        }

        public bool HasAny<A, B, C>(int entity)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            Pool? pool_a = TryGetPool<A>();
            if (pool_a is null) return false;
            Pool? pool_b = TryGetPool<B>();
            if (pool_b is null) return false;
            Pool? pool_c = TryGetPool<C>();
            if (pool_c is null) return false;
            return
                pool_a.Contains(entity) ||
                pool_b.Contains(entity) ||
                pool_c.Contains(entity);
        }

        public bool HasAny<A, B, C, D>(int entity)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            Pool? pool_a = TryGetPool<A>();
            if (pool_a is null) return false;
            Pool? pool_b = TryGetPool<B>();
            if (pool_b is null) return false;
            Pool? pool_c = TryGetPool<C>();
            if (pool_c is null) return false;
            Pool? pool_d = TryGetPool<C>();
            if (pool_d is null) return false;
            return
                pool_a.Contains(entity) ||
                pool_b.Contains(entity) ||
                pool_c.Contains(entity) ||
                pool_d.Contains(entity);
        }

        public bool HasAll<A, B>(int entity) =>
                Has(entity, TypeID<A>.Unique) &&
                Has(entity, TypeID<B>.Unique);

        public bool HasAll<A, B, C>(int entity) =>
                Has(entity, TypeID<A>.Unique) &&
                Has(entity, TypeID<B>.Unique) &&
                Has(entity, TypeID<C>.Unique);

        public bool HasAll<A, B, C, D>(int entity) =>
                Has(entity, TypeID<A>.Unique) &&
                Has(entity, TypeID<B>.Unique) &&
                Has(entity, TypeID<C>.Unique) &&
                Has(entity, TypeID<D>.Unique); 
    }
}
