using NttSharp.Entities;
using NttSharp.Models;

namespace NttSharp.Extensions
{
    public delegate void EachDelegate<A>(ref A arg);
    public delegate void EachDelegate<A, B>(ref A arg1, ref B arg2);
    public delegate void EachDelegate<A, B, C>(ref A arg1, ref B arg2, ref C arg3);
    public delegate void EachDelegate<A, B, C, D>(ref A arg1, ref B arg2, ref C arg3, ref D arg4);

    public delegate void EachDelegateWithEntity<A>(int entity, ref A arg);
    public delegate void EachDelegateWithEntity<A, B>(int entity, ref A arg1, ref B arg2);
    public delegate void EachDelegateWithEntity<A, B, C>(int entity, ref A arg1, ref B arg2, ref C arg3);
    public delegate void EachDelegateWithEntity<A, B, C, D>(int entity, ref A arg1, ref B arg2, ref C arg3, ref D arg4);

    public static class WorldEach
    {
        public static void Each<A>(this World world, EachDelegate<A> action) where A : unmanaged
        {
            View<A> view = world.View<A>();

            foreach(var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);

                action(ref arg1);
            }
        }

        public static void Each<A, B>(this World world, EachDelegate<A, B> action)
            where A : unmanaged
            where B : unmanaged
        {
            View<A, B> view = world.View<A, B>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);

                action(ref arg1, ref arg2);
            }
        }

        public static void Each<A, B, C>(this World world, EachDelegate<A, B, C> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            View<A, B, C> view = world.View<A, B, C>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);

                action(ref arg1, ref arg2, ref arg3);
            }
        }

        public static void Each<A, B, C, D>(this World world, EachDelegate<A, B, C, D> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            View<A, B, C, D> view = world.View<A, B, C, D>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);
                ref D arg4 = ref view.Get4(entity);

                action(ref arg1, ref arg2, ref arg3, ref arg4);
            }
        }

        public static void Each<A>(this World world, EachDelegateWithEntity<A> action) where A : unmanaged
        {
            View<A> view = world.View<A>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);

                action(entity, ref arg1);
            }
        }

        public static void Each<A, B>(this World world, EachDelegateWithEntity<A, B> action)
            where A : unmanaged
            where B : unmanaged
        {
            View<A, B> view = world.View<A, B>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);

                action(entity, ref arg1, ref arg2);
            }
        }

        public static void Each<A, B, C>(this World world, EachDelegateWithEntity<A, B, C> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            View<A, B, C> view = world.View<A, B, C>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);

                action(entity, ref arg1, ref arg2, ref arg3);
            }
        }

        public static void Each<A, B, C, D>(this World world, EachDelegateWithEntity<A, B, C, D> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            View<A, B, C, D> view = world.View<A, B, C, D>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);
                ref D arg4 = ref view.Get4(entity);

                action(entity, ref arg1, ref arg2, ref arg3, ref arg4);
            }
        }
    }
}
