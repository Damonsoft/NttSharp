using NttSharp.Entities;
using NttSharp.Logic;

namespace NttSharp.Extensions
{
    public static class WorldHas
    {
        public static bool Has<A>(this World world, int entity) => Has(world, entity, TypeID<A>.Unique);

        public static bool Has(this World world, int entity, int type)
        {
            Pool? pool = world.TryGetPool(type);

            if (pool is not null)
            {
                return pool.Contains(entity);
            }
            return false;
        }

        public static bool HasAny<A, B>(this World world, int entity)
            where A : unmanaged
            where B : unmanaged
        {
            Pool? pool_a = world.TryGetPool<A>();
            if (pool_a is null) return false;
            Pool? pool_b = world.TryGetPool<B>();
            if (pool_b is null) return false;
            return pool_a.Contains(entity) || pool_b.Contains(entity);
        }

        public static bool HasAny<A, B, C>(this World world, int entity)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            Pool? pool_a = world.TryGetPool<A>();
            if (pool_a is null) return false;
            Pool? pool_b = world.TryGetPool<B>();
            if (pool_b is null) return false;
            Pool? pool_c = world.TryGetPool<C>();
            if (pool_c is null) return false;
            return
                pool_a.Contains(entity) ||
                pool_b.Contains(entity) ||
                pool_c.Contains(entity);
        }

        public static bool HasAny<A, B, C, D>(this World world, int entity)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            Pool? pool_a = world.TryGetPool<A>();
            if (pool_a is null) return false;
            Pool? pool_b = world.TryGetPool<B>();
            if (pool_b is null) return false;
            Pool? pool_c = world.TryGetPool<C>();
            if (pool_c is null) return false;
            Pool? pool_d = world.TryGetPool<C>();
            if (pool_d is null) return false;
            return
                pool_a.Contains(entity) ||
                pool_b.Contains(entity) ||
                pool_c.Contains(entity) ||
                pool_d.Contains(entity);
        }
    }
}
