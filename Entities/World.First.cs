using NttSharp.Models;

namespace NttSharp.Entities
{
    public sealed partial class World
    {
        public bool TryFirst<A>(out Single<A> result) where A : unmanaged
		{
			ref Pool pool_a = ref GetPool<A>();

			foreach(ntt entity in AllEntites())
			{
				if (pool_a.Contains(entity))
				{
					result = new Single<A>(entity, in pool_a);

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
			ref Pool pool_a = ref GetPool<A>();
			ref Pool pool_b = ref GetPool<B>();

			foreach (ntt entity in AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity))
				{
					result = new Single<A, B>(entity, in pool_a, in pool_b);

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
            ref Pool pool_a = ref GetPool<A>();
            ref Pool pool_b = ref GetPool<B>();
			ref Pool pool_c = ref GetPool<C>();

            foreach (ntt entity in AllEntites())
            {
                if (pool_a.Contains(entity) &&
                    pool_b.Contains(entity))
                {
                    result = new Single<A, B, C>(entity, in pool_a, in pool_b, in pool_c);

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
			ref Pool pool_a = ref GetPool<A>();

			foreach (ntt entity in AllEntites())
			{
				if (pool_a.Contains(entity))
				{
					return new Single<A>(entity, in pool_a);
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
			ref Pool pool_a = ref GetPool<A>();
			ref Pool pool_b = ref GetPool<B>();

			foreach (ntt entity in AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity))
				{
					return new Single<A, B>(entity, in pool_a, in pool_b);
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
			ref Pool pool_a = ref GetPool<A>();
			ref Pool pool_b = ref GetPool<B>();
			ref Pool pool_c = ref GetPool<C>();

			foreach (ntt entity in AllEntites())
			{
				if (pool_a.Contains(entity) &&
					pool_b.Contains(entity) &&
					pool_c.Contains(entity))
				{
					return new Single<A, B, C>(entity, in pool_a, in pool_b, in pool_c);
				}
			}
			throw new Exception();
		}
	}
}
