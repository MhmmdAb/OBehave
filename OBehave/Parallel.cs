using System;

namespace OBehave
{
    public enum ParallelCompletionBehavior
    {
        SucceedAlways,
        SucceedWhenAllChildrenSucceed,
        SucceedWhenAllChildrenFail,
    }

    class Parallel<TContext>
        : Composite<TContext>
    {
        ParallelCompletionBehavior completionBehavior;
        NodeStatus runningStatus;

        public Parallel()
            : this(ParallelCompletionBehavior.SucceedAlways)
        {            
        }

        public Parallel(ParallelCompletionBehavior completionBehavior)
        {
            this.completionBehavior = completionBehavior;
            this.runningStatus = NodeStatus.Succeeded;
        }
        
        protected override Composite<TContext>.CompositeNodeStatus ProcessNode(Node<TContext> node, TContext context)
        {
            var status = node.Update(context);

            if (completionBehavior == ParallelCompletionBehavior.SucceedWhenAllChildrenSucceed
                && status == NodeStatus.Failed)
            {
                runningStatus = NodeStatus.Failed;
            }

            if (completionBehavior == ParallelCompletionBehavior.SucceedWhenAllChildrenFail
                && status == NodeStatus.Succeeded)
            {
                runningStatus = NodeStatus.Failed;
            }
            
            switch (status)
            {
                case NodeStatus.Running:
                    return CompositeNodeStatus.Running;
                default:
                    return CompositeNodeStatus.MoveNext;
            }
        }

        protected override NodeStatus EndProcessingNodesStatus(TContext context)
        {
            var returnStatus = runningStatus;
            runningStatus = NodeStatus.Succeeded;
            return returnStatus;
        }
    }
}
