using NttSharp.Collections;
using NttSharp.Extensions;
using NttSharp.Logic;

namespace NttSharp.Entities
{
    public sealed partial class World
    {
        static int _unique = 0;

        List<int> free = new List<int>();
        Pool[] pools = Array.Empty<Pool>();
        SparseSet entities = SparseSet.Create(512);

        public int Create()
        {
            int entity = _GetEntityId();

            SparseSet.Add(ref entities, entity);

            return entity;
        }

        public void Destroy(int entity)
        {
            if (entities.Contains(entity))
            {
                SparseSet.Remove(entities, entity);

                free.Add(entity);

                RemoveAll(entity);
            }
        }

        public bool Contains(int entity) => entities.Contains(entity);

        public void Add<T>(int entity, T initial) where T : unmanaged
        {
            int unique = TypeID<T>.Unique;

            if (pools.Length <= unique)
            {
                Array.Resize(ref pools, unique + 1);

                pools[unique] = Pool.Create<T>(256);
            }
            PoolEx.AddComponent(pools[unique], entity, initial);
        }

        public void Remove<T>(int entity) where T : unmanaged
        {
            PoolEx.RemoveComponent(pools[TypeID<T>.Unique], entity);
        }

        public void RemoveAll(int entity)
        {
            for (int y = 0; y < pools.Length; y++)
            {
                Pool? pool = pools[y];

                if (pool is not null)
                {
                    if (pool.Contains(entity))
                    {
                        PoolEx.RemoveComponent(pool, entity);
                    }
                }
            }
        }

        public DenseEnumerable AllEntites() => entities.EnumerateDense();

        internal Pool GetPool<T>() where T : unmanaged
        {
            int unique = TypeID<T>.Unique;

            if (unique >= pools.Length)
            {
                Array.Resize(ref pools, unique + 1);

                return pools[unique] = Pool.Create<T>(512);
            }
            return pools[unique];
        }

        internal Pool? TryGetPool<T>() where T : unmanaged
        {
            int unique = TypeID<T>.Unique;

            if (unique >= pools.Length)
            {
                return null;
            }
            return pools[unique];
        }

        internal Pool? TryGetPool(int unique)
        {
            if (unique >= pools.Length)
            {
                return null;
            }
            return pools[unique];
        }

        int _GetEntityId()
        {
            if (free.Count > 0)
            {
                int i = free.Count - 1;
                int e = free[i];
                free.RemoveAt(i);
                return e;
            }
            return ++_unique;
        }
    }
}
