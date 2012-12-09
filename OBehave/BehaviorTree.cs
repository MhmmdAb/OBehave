using System;

namespace OBehave
{
    public partial class BehaviorTree<TContext>
    {
        public BehaviorTreeConfiguration Configure
        {
            get
            {
                return new BehaviorTreeConfiguration();
            }            
        }

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
