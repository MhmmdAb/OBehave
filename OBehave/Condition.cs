using System;

namespace OBehave
{
    class Condition<TContext>
        : Leaf<TContext>
    {
        private System.Func<TContext, bool> updateAction;

        public Condition(System.Func<TContext, bool> updateAction)
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
