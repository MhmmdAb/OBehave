using System;
using System.Collections.Generic;

namespace OBehave
{
    class Sequence<TContext>
        : Composite<TContext>
    {
        public Sequence()
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
