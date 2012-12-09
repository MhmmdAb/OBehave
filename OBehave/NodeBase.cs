using System;

namespace OBehave
{
    public abstract class NodeBase<TContext>
        : Node<TContext>
    {        
        private Action<TContext> entryAction;
        private Action<TContext> successAction;
        private Action<TContext> failureAction;
        private Action<TContext> exitAction;

        protected NodeBase(Action<TContext> enteryAction,
                           Action<TContext> successAction,
                           Action<TContext> failureAction,
                           Action<TContext> exitAction)
        {            
            this.entryAction   = enteryAction;
            this.successAction = successAction;
            this.failureAction = failureAction;
            this.exitAction    = exitAction;
        }        

        public bool Update(TContext context)
        {            
            CallIfNotNull(entryAction, context);
            
            var status = UpdateImplementation(context);

            if (status)
                CallIfNotNull(successAction, context);
            else
                CallIfNotNull(failureAction, context);

            CallIfNotNull(exitAction, context);

            return status;
        }

        protected abstract bool UpdateImplementation(TContext context);

        private void CallIfNotNull(Action<TContext> a, TContext context)
        {
            if (a != null)
                a(context);
        }
    }
}
