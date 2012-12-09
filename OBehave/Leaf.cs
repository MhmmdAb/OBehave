using System;

namespace OBehave
{
    public class Leaf<TContext>
        : NodeBase<TContext>
    {
        private Func<TContext, bool> onUpdate;

        public Leaf(Func<TContext, bool> onUpdate,
                    Action               onEnter,
                    Action<TContext>     onEnterWithContext,
                    Action               onSucceeded,
                    Action<TContext>     onSucceededWithContext,
                    Action               onFailed,
                    Action<TContext>     onFailedWithContext,
                    Action               onExit,
                    Action<TContext>     onExitWithContext)
            : base(onEnter, onEnterWithContext,
                   onSucceeded, onSucceededWithContext,
                   onFailed, onFailedWithContext,
                   onExit, onExitWithContext)

        {
            if (onUpdate == null)
                throw new ArgumentNullException(BehaviorTreeResource.OnUpdateCannotBeNull);

            this.onUpdate = onUpdate;
        }

        protected override bool UpdateImplementation(TContext context)
        {
            return onUpdate(context);
        }
    }
}
