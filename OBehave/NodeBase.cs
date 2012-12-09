using System;

namespace OBehave
{
    public abstract class NodeBase<TContext>
        : Node<TContext>
    {
        private Action onEnter;
        private Action<TContext> onEnterWithContext;
        private Action<bool, TContext> onEnterWithStatusAndContext;

        private Action onSucceeded;
        private Action<TContext> onSucceededWithContext;

        private Action onFailed;
        private Action<TContext> onFailedWithContext;

        private Action onExit;
        private Action<TContext> onExitWithContext;

        protected NodeBase(Action           onEnter,
                           Action<TContext> onEnterWithContext,
                           Action           onSucceeded,
                           Action<TContext> onSucceededWithContext,
                           Action           onFailed,
                           Action<TContext> onFailedWithContext,
                           Action           onExit,
                           Action<TContext> onExitWithContext)
        {            
            if (onEnter != null && onEnterWithContext != null)
                throw new ArgumentException();

            if (onSucceeded != null && onSucceededWithContext != null)
                throw new ArgumentException();

            if (onFailed != null && onFailedWithContext != null)
                throw new ArgumentException();

            if (onExit != null && onExitWithContext != null)
                throw new ArgumentException();            

            this.onEnter                = onEnter;
            this.onEnterWithContext     = onEnterWithContext;
            this.onSucceeded            = onSucceeded;
            this.onSucceededWithContext = onSucceededWithContext;
            this.onFailed               = onFailed;
            this.onFailedWithContext    = onFailedWithContext;
            this.onExit                 = onExit;
            this.onExitWithContext      = onExitWithContext;
        }        

        public bool Update(TContext context)
        {
            CallIfNotNull(onEnter);
            CallIfNotNull(onEnterWithContext, context);
            
            if (onEnterWithContext != null)
                onEnterWithContext(context);

            var status = UpdateImplementation(context);

            if (status)
            {
                CallIfNotNull(onSucceeded);
                CallIfNotNull(onSucceededWithContext, context);
            }
            else
            {
                CallIfNotNull(onFailed);
                CallIfNotNull(onFailedWithContext, context);
            }

            CallIfNotNull(onExit);
            CallIfNotNull(onExitWithContext, context);

            return status;
        }

        protected abstract bool UpdateImplementation(TContext context);

        private void CallIfNotNull(Action a)
        {
            if (a != null)
                a();
        }

        private void CallIfNotNull(Action<TContext> a, TContext context)
        {
            if (a != null)
                a(context);
        }
    }
}
