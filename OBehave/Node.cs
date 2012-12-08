using System;

namespace OBehave
{
    public interface Node<TContext>
    {
        bool Update(TContext context);
    }
}
