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

        public bool Update(TContext context)
        {
            wasUpdateCalled = true;          
            return UpdateImplementation(context);
        }

        protected abstract bool UpdateImplementation(TContext context);

        protected void EnsureUpdateWasNotCalled()
        {
            if (wasUpdateCalled)
                throw new InvalidOperationException(); // TODO: Add message.
        }
    }
}
