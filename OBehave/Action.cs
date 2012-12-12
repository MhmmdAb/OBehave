using System;

namespace OBehave
{
    class Action<TContext>
        : Simple<TContext>
    {
        private Func<TContext, bool> updateAction;

        public Action(Func<TContext, bool> updateAction)
        {
            if (updateAction == null)
                throw new ArgumentNullException(BehaviorTreeResource.UpdateActionCannotBeNull);

            this.updateAction = updateAction;
        }

        protected override NodeStatus UpdateImplementation(TContext context)
        {
            return updateAction(context) ? NodeStatus.Succeeded : NodeStatus.Running;
        }
    }
}
