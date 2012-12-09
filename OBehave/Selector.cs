using System;
using System.Collections.Generic;

namespace OBehave
{
    class Selector<TContext>
        : Composite<TContext>
    {
        public Selector(IList<Node<TContext>> nodes,
                        Action                onEnter                = null,
                        Action<TContext>      onEnterWithContext     = null,
                        Action                onSucceeded            = null,
                        Action<TContext>      onSucceededWithContext = null,
                        Action                onFailed               = null,
                        Action<TContext>      onFailedWithContext    = null,
                        Action                onExit                 = null,
                        Action<TContext>      onExitWithContext      = null)
            : base(nodes,
                   onEnter,     onEnterWithContext,
                   onSucceeded, onSucceededWithContext,
                   onFailed,    onFailedWithContext,
                   onExit,      onExitWithContext)
        {
            // Do nothing.
        }

        protected override bool UpdateImplementation(TContext context)
        {
            foreach (var node in Nodes)
            {
                if (node.Update(context))
                    return true;
            }

            return false;
        }
    }
}
