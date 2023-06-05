using NttSharp.Logic;
using System.Runtime.CompilerServices;

namespace NttSharp.Entities
{
    public sealed partial class World
    {

        /// <summary>
        /// Checks if an entity
        /// contains a component of
        /// type: <typeparamref name="A"/>.
        /// </summary>
        /// <param name="entity">the entity id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has<A>(ntt entity) => Has(entity, TypeID<A>.Unique);

        /// <summary>
        /// Checks if an entity
        /// contains a component of
        /// the give type id.
        /// </summary>
        /// <param name="entity">the entity id</param>
        /// <param name="type">the type id of the component</param>
        public bool Has(ntt entity, int type)
        {
            Pool? pool = TryGetPool(type);

            if (pool is not null)
            {
                return pool.Value.Contains(entity);
            }
            return false;
        }

        /// <summary>
        /// Checks if an entity
        /// contains any of the
        /// components types:
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>.
        /// </summary>
        /// <param name="entity">the entity id</param>
        public bool HasAny<A, B>(ntt entity)
            where A : unmanaged
            where B : unmanaged
        {
            Pool? pool_a = TryGetPool<A>();
            if (pool_a is null) return false;
            Pool? pool_b = TryGetPool<B>();
            if (pool_b is null) return false;
            return pool_a.Value.Contains(entity) || pool_b.Value.Contains(entity);
        }

        /// <summary>
        /// Checks if an entity
        /// contains any of the
        /// components types:
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>,    /// <param name="entity">the entity id</param>
        public bool HasAny<A, B, C>(ntt entity)
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
                pool_a.Value.Contains(entity) ||
                pool_b.Value.Contains(entity) ||
                pool_c.Value.Contains(entity);
        }


        /// <summary>
        /// Checks if an entity
        /// contains any of the
        /// components types:
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>,
        /// <typeparamref name="C"/>,
        /// <typeparamref name="D"/>.
        /// </summary>
        /// <param name="entity">the entity id</param>
        public bool HasAny<A, B, C, D>(ntt entity)
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
                pool_a.Value.Contains(entity) ||
                pool_b.Value.Contains(entity) ||
                pool_c.Value.Contains(entity) ||
                pool_d.Value.Contains(entity);
        }

        /// <summary>
        /// Checks if an entity
        /// contains all of the
        /// components types:
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        public bool HasAll<A, B>(ntt entity) =>
                Has(entity, TypeID<A>.Unique) &&
                Has(entity, TypeID<B>.Unique);

        /// <summary>
        /// Checks if an entity
        /// contains all of the
        /// components types:
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>,
        /// <typeparamref name="C"/>,
        /// </summary>
        /// <param name="entity">the entity id</param>
        public bool HasAll<A, B, C>(ntt entity) =>
                Has(entity, TypeID<A>.Unique) &&
                Has(entity, TypeID<B>.Unique) &&
                Has(entity, TypeID<C>.Unique);

        /// <summary>
        /// Checks if an entity
        /// contains all of the
        /// components types:
        /// <typeparamref name="A"/>,
        /// <typeparamref name="B"/>,
        /// <typeparamref name="C"/>,
        /// <typeparamref name="D"/>.
        /// </summary>
        /// <param name="entity">the entity id</param>
        public bool HasAll<A, B, C, D>(ntt entity) =>
                Has(entity, TypeID<A>.Unique) &&
                Has(entity, TypeID<B>.Unique) &&
                Has(entity, TypeID<C>.Unique) &&
                Has(entity, TypeID<D>.Unique); 
    }
}
