using System;

namespace OBehave
{
    public class BehaviorTree<TContext>
    {
        private Node<TContext> node;

        public BehaviorTree(Node<TContext> startNode)
        {
            if (startNode == null)
                throw new ArgumentNullException();

            this.node = startNode;
        }

        public bool Update(TContext context)        
        {
            return node.Update(context);
        }
    }
}
