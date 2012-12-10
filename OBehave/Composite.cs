using System;
using System.Collections.Generic;

namespace OBehave
{
    abstract class Composite<TContext>
        : NodeBase<TContext>
    {
        private List<Node<TContext>> nodes = new List<Node<TContext>>();

        protected IList<Node<TContext>> Nodes
        { 
            get
            {
                return nodes;
            }
        }
        
        public Composite(System.Action<TContext> entryAction   = null,
                         System.Action<TContext> successAction = null,
                         System.Action<TContext> failureAction = null,
                         System.Action<TContext> exitAction    = null)
            : base(entryAction, successAction, failureAction, exitAction)
        {
        }

        public void AddChildNode(Node<TContext> child)
        {
            EnsureUpdateWasNotCalled();
            nodes.Add(child);
        }
    }
}
