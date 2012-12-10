using System;

namespace OBehave
{
    public abstract class Leaf<TContext>
        : NodeBase<TContext>
    {
        protected Leaf(System.Action<TContext> entryAction   = null,
                       System.Action<TContext> successAction = null,
                       System.Action<TContext> failureAction = null,
                       System.Action<TContext> exitAction    = null)
            : base(entryAction, successAction, failureAction, exitAction)
        {
        }
    }
}
