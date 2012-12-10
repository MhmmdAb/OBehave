using System;
using System.Collections.Generic;

namespace OBehave
{
    class Selector<TContext>
        : Composite<TContext>
    {
        public Selector()
        {
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
