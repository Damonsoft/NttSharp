using NttSharp.Models;

namespace NttSharp.Entities
{
    public sealed partial class World
    {
        public bool TryFirst<A>(out Single<A> result) where A : unmanaged
		{
			Pool pool_a = GetPool<A>();

			foreach(int entity in AllEntites())
			{
				if (pool_a.Contains(entity))
				{
					result = new Single<A>(entity, pool_a);

					return true;
				}
			}
			result = default;

			return false;
		}

        public bool TryFirst<A, B>(out Single<A, B> result)
			where A : unmanaged
			where B : unmanaged
		{
			Pool pool_a = GetPool<A>();
			Pool pool_b = GetPool<B>();

			foreach (int entity in AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity))
				{
					result = new Single<A, B>(entity, pool_a, pool_b);

					return true;
				}
			}
			result = default;

			return false;
		}

        public bool TryFirst<A, B, C>(out Single<A, B, C> result)
			where A : unmanaged
			where B : unmanaged
            where C : unmanaged
        {
            Pool pool_a = GetPool<A>();
            Pool pool_b = GetPool<B>();
			Pool pool_c = GetPool<C>();

            foreach (int entity in AllEntites())
            {
                if (pool_a.Contains(entity) &&
                    pool_b.Contains(entity))
                {
                    result = new Single<A, B, C>(entity, pool_a, pool_b, pool_c);

                    return true;
                }
            }
            result = default;

            return false;
        }

        /// <summary>
        /// Will return the first
		/// entity that contains
		/// the following components: 
        /// <typeparamref name="A"/>.
        /// </summary>
        public Single<A> First<A>() where A : unmanaged
		{
			Pool pool_a = GetPool<A>();

			foreach (int entity in AllEntites())
			{
				if (pool_a.Contains(entity))
				{
					return new Single<A>(entity, pool_a);
				}	
			}
			throw new Exception();
		}

        /// <summary>
        /// Will return the first
        /// entity that contains
        /// the following components: 
        /// <typeparamref name="A"/>,
		/// <typeparamref name="B"/>.
        /// </summary>
        public Single<A, B> First<A, B>()
			where A : unmanaged
			where B : unmanaged
		{
			Pool pool_a = GetPool<A>();
			Pool pool_b = GetPool<B>();

			foreach (int entity in AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity))
				{
					return new Single<A, B>(entity, pool_a, pool_b);
				}
			}
			throw new Exception();
		}

        /// <summary>
        /// Will return the first
        /// entity that contains
        /// the following components: 
        /// <typeparamref name="A"/>,
		/// <typeparamref name="B"/>,
        /// <typeparamref name="C"/>.
        /// </summary>
        public Single<A, B, C> First<A, B, C>()
			where A : unmanaged
			where B : unmanaged
			where C : unmanaged
		{
			Pool pool_a = GetPool<A>();
			Pool pool_b = GetPool<B>();
			Pool pool_c = GetPool<C>();

			foreach (int entity in AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity) &&
					pool_c.Contains(entity))
				{
					return new Single<A, B, C>(entity, pool_a, pool_b, pool_c);
				}
			}
			throw new Exception();
		}
	}
}
