using System;
using System.Collections.Generic;

namespace OBehave
{
    abstract class Composite<TContext>
        : NodeBase<TContext>
    {
        protected IList<Node<TContext>> Nodes { get; private set; }
        
        public Composite(IList<Node<TContext>> nodes,
                         Action           onEnter                = null,
                         Action<TContext> onEnterWithContext     = null,
                         Action           onSucceeded            = null,
                         Action<TContext> onSucceededWithContext = null,
                         Action           onFailed               = null,
                         Action<TContext> onFailedWithContext    = null,
                         Action           onExit                 = null,
                         Action<TContext> onExitWithContext      = null)
            : base(onEnter,     onEnterWithContext,
                   onSucceeded, onSucceededWithContext,
                   onFailed,    onFailedWithContext,
                   onExit,      onExitWithContext)
        {
            if (nodes == null)
                throw new ArgumentNullException(BehaviorTreeResource.NodesCannotBeNull);

            if (nodes.Count < 1)
                throw new ArgumentException(BehaviorTreeResource.NodesCannotBeEmpty);

            this.Nodes = nodes;
        }
    }
}
