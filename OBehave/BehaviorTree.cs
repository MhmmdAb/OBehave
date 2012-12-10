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

    public partial class BehaviorTree
    {
        private BehaviorTree<object> implementation;

        public BehaviorTreeConfiguration Configure()
        {
            return new BehaviorTreeConfiguration(implementation.Configure());
        }

        public BehaviorTree()
        {
            implementation = new BehaviorTree<object>();
        }

        public bool Update()
        {
            return implementation.Update(null);
        }

    }
}
