using System;
using System.Collections.Generic;

namespace OBehave
{
    abstract class Composite<TContext>
        : NodeBase<TContext>
    {
        protected enum CompositeNodeStatus
        {
            MoveNext,
            Running,
            Succeeded,
            Failed,
        }

        private List<Node<TContext>> nodes = new List<Node<TContext>>();
        private int currentNodeIndex = 0;

        protected abstract CompositeNodeStatus ProcessNode(Node<TContext> node, TContext context);
        protected abstract NodeStatus EndProcessingNodesStatus(TContext context);

        public Composite()
        {
        }

        protected override NodeStatus UpdateImplementation(TContext context)
        {
            for (int i = currentNodeIndex; i < nodes.Count; ++i)
            {
                var status = ProcessNode(nodes[i], context);
                switch (status)
                {
                    case CompositeNodeStatus.MoveNext:
                        break;
                        
                    case CompositeNodeStatus.Running:
                        currentNodeIndex = i;
                        return NodeStatus.Running;

                    case CompositeNodeStatus.Succeeded:
                        currentNodeIndex = i;
                        return NodeStatus.Succeeded;
                        
                    case CompositeNodeStatus.Failed:
                        currentNodeIndex = 0;
                        return NodeStatus.Failed;
                }
            }

            return EndProcessingNodesStatus(context);
        }

        public void AddChildNode(Node<TContext> child)
        {
            EnsureUpdateWasNotCalled();
            nodes.Add(child);
        }
    }
}
