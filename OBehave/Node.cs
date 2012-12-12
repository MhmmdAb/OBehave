using System;

namespace OBehave
{
    public interface Node<TContext>
    {
        NodeStatus Update(TContext context);
    }
}
