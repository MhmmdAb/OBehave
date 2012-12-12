using System;

namespace OBehave
{
    public partial class BehaviorTree<TContext>
    {
        private Node<TContext> node = null;

        public BehaviorTreeConfiguration Configure()
        {
            if (node != null)
                throw new InvalidOperationException(BehaviorTreeResource.ConfigureMustBeCalledOnce);
            return new BehaviorTreeConfiguration(this);
        }

        public BehaviorTree()
        {
        }

        public void Update(TContext context)
        {
            if (node == null)
                throw new InvalidOperationException(BehaviorTreeResource.ConfigureMustBeCalledBeforeUpdate);
            node.Update(context);
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

        public void Update()
        {
            implementation.Update(null);
        }

    }
}
