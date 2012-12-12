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

        protected override Composite<TContext>.CompositeNodeStatus ProcessNode(Node<TContext> node, TContext context)
        {
            var status = node.Update(context);

            switch (status)
            {
                case NodeStatus.Succeeded:
                    return CompositeNodeStatus.Succeeded;

                case NodeStatus.Failed:
                    return CompositeNodeStatus.MoveNext;

                default:
                    return CompositeNodeStatus.Running;
            }                        
        }

        protected override NodeStatus EndProcessingNodesStatus(TContext context)
        {
            return NodeStatus.Failed;
        }
    }
}
