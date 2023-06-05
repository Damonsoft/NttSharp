using NttSharp.Collections;
using NttSharp.Logic;

namespace NttSharp.Entities
{
    /// <summary>
    /// Provides methods to search, store, delete,
    /// and modify both entities and their components.
    /// </summary>
    public sealed partial class World
    {
        static ntt _unique = 0;

        List<ntt> free = new List<ntt>();
        Chunker<Pool> pools = Chunker<Pool>.Create(32);
        SparseSet entities = SparseSet.Create(512);

        /// <summary>
        /// Creates an empty entity.
        /// </summary>
        public ntt Create()
        {
            ntt entity = _GetEntityId();

            SparseSet.Add(ref entities, entity);
   
            return entity;
        }

        /// <summary>
        /// Deletes all components associated with an entity
        /// then recycles the entity id.
        /// </summary>
        /// <param name="entity">the entity id</param>
        public void Destroy(ntt entity)
        {
            if (entities.Contains(entity))
            {
                SparseSet.Remove(ref entities, entity);

                free.Add(entity);

                RemoveAll(entity);
            }
        }


        /// <summary>
        /// Checks if the entity is valid.
        /// </summary>
        /// <param name="entity">the entity id</param>
        public bool Contains(ntt entity) => entities.Contains(entity);

        /// <summary>
        /// Assigns a component of type
        /// <typeparamref name="T"/>
        /// to an entity.
        /// </summary>
        /// <param name="entity">the entity id</param>
        /// <param name="initial">the initial value of the component</param>
        public void Assign<T>(ntt entity, T initial) where T : unmanaged
        {
            ref Pool pool = ref pools[Chunker<Pool>.Ensure(ref pools, TypeID<T>.Unique)];

            if (pool.NetType is null)
            { 
                pool = Pool.Create<T>(32);
            }
            pool.AddComponent(entity, in initial);
        }

        /// <summary>
        /// Removes a component of type
        /// <typeparamref name="T"/>
        /// from an entity.
        /// </summary>
        /// <param name="entity">the entity id</param>
        public void Remove<T>(ntt entity) where T : unmanaged
        {
            pools[TypeID<T>.Unique].RemoveComponent(entity);
        }

        /// <summary>
        /// Removes all components
        /// from an entity.
        /// </summary>
        /// <param name="entity">the entity id</param>
        public void RemoveAll(ntt entity)
        {
            for (int y = 0; y < pools.Length; y++)
            {
                Pool pool = pools[y];

                if (pool.NetType is not null)
                {
                    if (pool.Contains(entity))
                    {
                        pool.RemoveComponent(entity);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a reference
        /// to a component of type
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entity">the entity id</param>
        public ref T Get<T>(ntt entity) where T : unmanaged
        {
            return ref GetPool<T>().GetComponent<T>(entity);
        }

        /// <summary>
        /// Returns an enumeration of
        /// all entities registered.
        /// </summary>
        public DenseEnumerable AllEntites() => entities.EnumerateDense();

        /// <summary>
        /// Registers a type with
        /// the world. 
        /// </summary>
        public void Register<T>() where T : unmanaged => GetPool<T>();


        internal ref Pool GetPool<T>() where T : unmanaged
        {
            int unique = Chunker<Pool>.Ensure(ref pools, TypeID<T>.Unique);

            if (pools[unique].NetType is null)
            {
                Chunker<Pool>.Resize(ref pools, unique + 1);

                pools[unique] = Pool.Create<T>(512);
            }
            return ref pools[unique];
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

        ntt _GetEntityId()
        {
            if (free.Count > 0)
            {
                int i = free.Count - 1;
                ntt e = free[i];
                free.RemoveAt(i);
                return e;
            }
            return ++_unique;
        }
    }
}
