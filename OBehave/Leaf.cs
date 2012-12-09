using System;

namespace OBehave
{
    public class Leaf<TContext>
        : NodeBase<TContext>
    {
        private Func<TContext, bool> updateAction;

        public Leaf(Func<TContext, bool> updateAction,
                    Action<TContext>     entryAction,
                    Action<TContext>     successAction,
                    Action<TContext>     failureAction,
                    Action<TContext>     exitAction)
            : base(entryAction, successAction, failureAction, exitAction)
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
