using System;
using System.Collections.Generic;

namespace OBehave
{
    class Sequence<TContext>
        : Composite<TContext>
    {
        public Sequence(IList<Node<TContext>> nodes,
                        Action<TContext>      entryAction   = null,
                        Action<TContext>      successAction = null,
                        Action<TContext>      failureAction = null,
                        Action<TContext>      exitAction    = null)
            : base(nodes, entryAction, successAction, failureAction, exitAction)
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
