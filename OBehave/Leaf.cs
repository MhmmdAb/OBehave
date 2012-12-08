using System;

namespace OBehave
{
    public class Leaf<TContext>
        : Node<TContext>
    {
        private Func<TContext, bool> onUpdate;

        public Leaf(Func<TContext, bool> onUpdate)
        {
            if (onUpdate == null)
                throw new ArgumentNullException(BehaviorTreeResource.OnUpdateCannotBeNull);

            this.onUpdate = onUpdate;
        }
        
        public bool Update(TContext context)
        {
            return onUpdate(context);
        }
    }
}
