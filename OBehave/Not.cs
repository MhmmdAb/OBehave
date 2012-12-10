using System;

namespace OBehave
{
    class Not<TContext>
        : Leaf<TContext>
    {
        private Node<TContext> node;

        public Not(Node<TContext> node)
        {
            if (node == null)
                throw new ArgumentNullException();
            this.node = node;
        }

        protected override bool UpdateImplementation(TContext context)
        {
            return !node.Update(context);
        }
    }
}
