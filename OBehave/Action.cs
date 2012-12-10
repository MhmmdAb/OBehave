using System;

namespace OBehave
{
    class Action<TContext>
        : Leaf<TContext>
    {
        private System.Action<TContext> updateAction;

        public Action(System.Action<TContext> updateAction)
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
