using System;
using System.Collections.Generic;

namespace OBehave
{
    abstract class Composite<TContext>
        : NodeBase<TContext>
    {
        protected IList<Node<TContext>> Nodes { get; private set; }
        
        public Composite(IList<Node<TContext>> nodes,
                         Action<TContext> entryAction,
                         Action<TContext> successAction,
                         Action<TContext> failureAction,
                         Action<TContext> exitAction)
            : base(entryAction, successAction, failureAction, exitAction)
        {
            if (nodes == null)
                throw new ArgumentNullException(BehaviorTreeResource.NodesCannotBeNull);

            if (nodes.Count < 1)
                throw new ArgumentException(BehaviorTreeResource.NodesCannotBeEmpty);

            this.Nodes = nodes;
        }
    }
}
