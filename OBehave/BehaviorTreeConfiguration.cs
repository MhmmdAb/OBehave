using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBehave
{
    public partial class BehaviorTree<TContext>
    {
        public class BehaviorTreeConfiguration
        {       
            internal BehaviorTreeConfiguration()
            {
                //var bt = new BehaviorTree<object>();
                //bt.Configure()
                //    .Selector
                //        .OnEnter()
                //        .OnExit()
                //        .Sequence.OnEnter().OnExit()
                //            .Leaf(()=> return true).OnEnter()
                //            .Leaf(()=> return true).OnExit()
                //            .Selector
                //                .Leaf()
                //                .Leaf();
            }
            
            public BehaviorTreeConfiguration Selector
            {
                get
                {                    
                    return this;
                }
            }

            public BehaviorTreeConfiguration Sequence
            {
                get
                {
                    return this;
                }
            }

            public BehaviorTreeConfiguration Leaf(Func<bool> updateAction)
            {
                return Leaf(c => updateAction());
            }

            public BehaviorTreeConfiguration Leaf(Func<TContext, bool> updateAction)
            {
                return this;
            }

            public BehaviorTreeConfiguration OnEnter(Action enterAction)
            {
                return OnEnter(c => enterAction());
            }

            public BehaviorTreeConfiguration OnEnter(Action<TContext> enterAction)
            {
                return this;
            }

            public BehaviorTreeConfiguration OnExit(Action exitAction)
            {
                return OnExit(c => exitAction());
            }

            public BehaviorTreeConfiguration OnExit(Action<TContext> exitAction)
            {
                return this;
            }

            public BehaviorTreeConfiguration OnSucceeded(Action successAction)
            {
                return OnSucceeded(c => successAction());
            }

            public BehaviorTreeConfiguration OnSucceeded(Action<TContext> successAction)
            {
                return this;
            }

            public BehaviorTreeConfiguration OnFailed(Action failureAction)
            {
                return OnFailed(c => failureAction());
            }

            public BehaviorTreeConfiguration OnFailed(Action<TContext> failureAction)
            {
                return this;
            }
        }
    }
}
