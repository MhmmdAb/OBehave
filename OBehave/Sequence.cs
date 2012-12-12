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

        protected override Composite<TContext>.CompositeNodeStatus ProcessNode(Node<TContext> node, TContext context)
        {
            var status = node.Update(context);

            switch (status)
            {
                case NodeStatus.Succeeded:
                    return CompositeNodeStatus.MoveNext;

                case NodeStatus.Failed:
                    return CompositeNodeStatus.Failed;

                default:
                    return CompositeNodeStatus.Running;
            }            
        }

        protected override NodeStatus EndProcessingNodesStatus(TContext context)
        {
            return NodeStatus.Succeeded;
        }
    }
}
