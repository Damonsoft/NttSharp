using NttSharp.Models;

namespace NttSharp.Entities
{
    public delegate void EachDelegate<A>(ref A arg);
    public delegate void EachDelegate<A, B>(ref A arg1, ref B arg2);
    public delegate void EachDelegate<A, B, C>(ref A arg1, ref B arg2, ref C arg3);
    public delegate void EachDelegate<A, B, C, D>(ref A arg1, ref B arg2, ref C arg3, ref D arg4);

    public delegate void EachDelegateWithEntity<A>(int entity, ref A arg);
    public delegate void EachDelegateWithEntity<A, B>(int entity, ref A arg1, ref B arg2);
    public delegate void EachDelegateWithEntity<A, B, C>(int entity, ref A arg1, ref B arg2, ref C arg3);
    public delegate void EachDelegateWithEntity<A, B, C, D>(int entity, ref A arg1, ref B arg2, ref C arg3, ref D arg4);

    public sealed partial class World
    {
        public unsafe void Each<A>(delegate* managed<ref A, void> action) where A : unmanaged
        {
            View<A> view = View<A>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);

                action(ref arg1);
            }
        }

        public unsafe void Each<A, B>(delegate* managed<ref A, ref B, void> action)
            where A : unmanaged
            where B : unmanaged
        {
            View<A,B> view = View<A, B>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);

                action(ref arg1, ref arg2);
            }
        }

        public unsafe void Each<A, B, C>(delegate* managed<ref A, ref B, ref C, void> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            View<A, B, C> view = View<A, B, C>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);

                action(ref arg1, ref arg2, ref arg3);
            }
        }

        public unsafe void Each<A, B, C, D>(delegate* managed<ref A, ref B, ref C, ref D, void> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            View<A, B, C, D> view = View<A, B, C, D>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);
                ref D arg4 = ref view.Get4(entity);

                action(ref arg1, ref arg2, ref arg3, ref arg4);
            }
        }

        public void Each<A>(EachDelegate<A> action) where A : unmanaged
        {
            View<A> view = this.View<A>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);

                action(ref arg1);
            }
        }

        public void Each<A, B>(EachDelegate<A, B> action)
            where A : unmanaged
            where B : unmanaged
        {
            View<A, B> view = this.View<A, B>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);

                action(ref arg1, ref arg2);
            }
        }

        public void Each<A, B, C>(EachDelegate<A, B, C> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            View<A, B, C> view = this.View<A, B, C>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);

                action(ref arg1, ref arg2, ref arg3);
            }
        }

        public void Each<A, B, C, D>(EachDelegate<A, B, C, D> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            View<A, B, C, D> view = this.View<A, B, C, D>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);
                ref D arg4 = ref view.Get4(entity);

                action(ref arg1, ref arg2, ref arg3, ref arg4);
            }
        }

        public void Each<A>(EachDelegateWithEntity<A> action) where A : unmanaged
        {
            View<A> view = this.View<A>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);

                action(entity, ref arg1);
            }
        }

        public void Each<A, B>(EachDelegateWithEntity<A, B> action)
            where A : unmanaged
            where B : unmanaged
        {
            View<A, B> view = this.View<A, B>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);

                action(entity, ref arg1, ref arg2);
            }
        }

        public void Each<A, B, C>(EachDelegateWithEntity<A, B, C> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
        {
            View<A, B, C> view = this.View<A, B, C>();

            foreach (var entity in view)
            {
                ref A arg1 = ref view.Get1(entity);
                ref B arg2 = ref view.Get2(entity);
                ref C arg3 = ref view.Get3(entity);

                action(entity, ref arg1, ref arg2, ref arg3);
            }
        }

        public void Each<A, B, C, D>(EachDelegateWithEntity<A, B, C, D> action)
            where A : unmanaged
            where B : unmanaged
            where C : unmanaged
            where D : unmanaged
        {
            View<A, B, C, D> view = this.View<A, B, C, D>();

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
