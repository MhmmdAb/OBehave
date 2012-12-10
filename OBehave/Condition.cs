using System;

namespace OBehave
{
    class Condition<TContext>
        : Leaf<TContext>
    {
        private System.Func<TContext, bool> updateAction;

        public Condition(System.Func<TContext, bool> updateAction,
                         System.Action<TContext> onEnter   = null,
                         System.Action<TContext> onSuccess = null,
                         System.Action<TContext> onFailure = null,
                         System.Action<TContext> onExit    = null)
            : base(onEnter, onSuccess, onFailure, onExit)
        {
            if (updateAction == null)
                throw new ArgumentNullException(BehaviorTreeResource.UpdateActionCannotBeNull);

            this.updateAction = updateAction;
        }

        protected override bool UpdateImplementation(TContext context)
        {
            return updateAction(context);
        }
    }
}
