using System;
using System.Collections.Generic;

namespace OBehave
{
    class Sequence<TContext>
        : Composite<TContext>
    {
        public Sequence(System.Action<TContext> onEnter   = null,
                        System.Action<TContext> onSuccess = null,
                        System.Action<TContext> onFailure = null,
                        System.Action<TContext> onExit    = null)
            : base(onEnter, onSuccess, onFailure, onExit)
        {
        }

        protected override bool UpdateImplementation(TContext context)
        {
            foreach (var node in Nodes)
            {
                if (!node.Update(context))
                    return false;
            }

            return true;
        }
    }
}
