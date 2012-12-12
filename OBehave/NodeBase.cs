using System;

namespace OBehave
{
    public abstract class NodeBase<TContext>
        : Node<TContext>
    {
        private bool wasUpdateCalled = false;
      
        protected NodeBase()
        {            
        }        

        public NodeStatus Update(TContext context)
        {
            wasUpdateCalled = true;          
            return UpdateImplementation(context);
        }

        protected abstract NodeStatus UpdateImplementation(TContext context);

        protected void EnsureUpdateWasNotCalled()
        {
            if (wasUpdateCalled)
                throw new InvalidOperationException();
        }
    }
}
