using System;

namespace OBehave
{
    class Action<TContext>
        : Leaf<TContext>
    {
        private System.Action<TContext> updateAction;

        private Action()
        {
        }

        public Action(System.Action<TContext> updateAction,
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
            updateAction(context);
            return true;
        }
    }
}
