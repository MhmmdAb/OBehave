using System;

namespace OBehave
{
    public abstract class NodeBase<TContext>
        : Node<TContext>
    {
        private bool wasUpdateCalled = false;

        private System.Action<TContext> entryAction;
        private System.Action<TContext> successAction;
        private System.Action<TContext> failureAction;
        private System.Action<TContext> exitAction;
        
        protected NodeBase(System.Action<TContext> enteryAction  = null,
                           System.Action<TContext> successAction = null,
                           System.Action<TContext> failureAction = null,
                           System.Action<TContext> exitAction    = null)
        {            
            this.entryAction   = enteryAction;
            this.successAction = successAction;
            this.failureAction = failureAction;
            this.exitAction    = exitAction;
        }        

        public bool Update(TContext context)
        {
            wasUpdateCalled = true;

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

        protected void EnsureUpdateWasNotCalled()
        {
            if (wasUpdateCalled)
                throw new InvalidOperationException(); // TODO: Add message.
        }

        private void CallIfNotNull(System.Action<TContext> a, TContext context)
        {
            if (a != null)
                a(context);
        }
    }
}
