using System;

namespace OBehave
{
    public partial class BehaviorTree<TContext>
    {
        private Node<TContext> node = null;

        public BehaviorTreeConfiguration Configure()
        {
            if (node != null)
                throw new InvalidOperationException(); // TODO: Add message
            return new BehaviorTreeConfiguration(this);
        }

        

        public BehaviorTree()
        {
        }

        public bool Update(TContext context)        
        {
            if (node == null)
                throw new InvalidOperationException(); // TODO: Add message
            return node.Update(context);
        }
    }
}
