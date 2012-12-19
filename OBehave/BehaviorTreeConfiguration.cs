using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OBehave
{
    public partial class BehaviorTree<TContext>
    {
        public class BehaviorTreeConfiguration
        {
            private Stack<Node<TContext>> stack = new Stack<Node<TContext>>();
            private BehaviorTree<TContext> behaviorTree;

            internal BehaviorTreeConfiguration(BehaviorTree<TContext> behaviorTree)
            {
                this.behaviorTree = behaviorTree;
            }

            public BehaviorTreeConfiguration BeginSelector()
            {
                return BeginImplementation(new Selector<TContext>());
            }

            public BehaviorTreeConfiguration BeginSequence()
            {
                return BeginImplementation(new Sequence<TContext>());
            }

            public BehaviorTreeConfiguration BeginParallel()
            {
                return BeginImplementation(new Parallel<TContext>());
            }

            public BehaviorTreeConfiguration BeginParallel(ParallelCompletionBehavior parallelBehavior)
            {
                return BeginImplementation(new Parallel<TContext>(parallelBehavior));
            }

            public BehaviorTreeConfiguration End()
            {
                if (stack.Count == 0 || !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException
                        (BehaviorTreeResource.EndCanOnlyBeCalledOnCompositeNodes
                        );

                stack.Pop();
                return this;
            }

            public BehaviorTreeConfiguration Action(System.Action onUpdate)
            {
                return Action(c => { onUpdate(); return true; });
            }

            public BehaviorTreeConfiguration Action(Func<bool> onUpdate)
            {
                return Action(c => onUpdate());
            }

            public BehaviorTreeConfiguration Action(System.Action<TContext> onUpdate)
            {
                return Action(c => { onUpdate(c); return true; });
            }

            public BehaviorTreeConfiguration Action(Func<TContext, bool> onUpdate)
            {
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException
                        (BehaviorTreeResource.ActionMustBeNestedUnderCompositeNodes
                        );
                
                var action = new OBehave.Action<TContext>(onUpdate);

                if (stack.Count == 0)
                {
                    stack.Push(action);
                    Debug.Assert(behaviorTree.node == null);
                    behaviorTree.node = action;
                }
                else
                {
                    var parent = stack.Peek() as Composite<TContext>;                    
                    System.Diagnostics.Debug.Assert(parent != null);                    
                    parent.AddChildNode(action);                
                }

                return this;
            }

            public BehaviorTreeConfiguration Condition(Func<bool> onUpdate)
            {
                return Condition(c => onUpdate());
            }

            public BehaviorTreeConfiguration Condition(Func<TContext, bool> onUpdate)
            {
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException
                        (BehaviorTreeResource.ConditionsMustBeNesterUnderCompositeNodes
                        );
                
                var condition = new Condition<TContext>(onUpdate);

                if (stack.Count == 0)
                {
                    stack.Push(condition);
                    Debug.Assert(behaviorTree.node == null);
                    behaviorTree.node = condition;
                }
                else
                {
                    var parent = stack.Peek() as Composite<TContext>;
                    System.Diagnostics.Debug.Assert(parent != null);
                    parent.AddChildNode(condition);                
                }

                return this;
            }

            private BehaviorTreeConfiguration BeginImplementation(Composite<TContext> node)
            {
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException
                        (BehaviorTreeResource.CompositeMustBeNestedUnderCompositeNodes
                        );

                if (stack.Count != 0)
                {
                    var parent = stack.Peek() as Composite<TContext>;
                    Debug.Assert(parent != null);
                    parent.AddChildNode(node);
                }

                if (behaviorTree.node == null)
                    behaviorTree.node = node;

                stack.Push(node);
                return this;
            }
        }
    }

    public partial class BehaviorTree
    {
        public class BehaviorTreeConfiguration
        {
            private BehaviorTree<object>.BehaviorTreeConfiguration config;
            internal BehaviorTreeConfiguration(BehaviorTree<object>.BehaviorTreeConfiguration config)
            {
                this.config = config;
            }

            public BehaviorTreeConfiguration BeginSelector()
            {
                config.BeginSelector();
                return this;
            }

            public BehaviorTreeConfiguration BeginSequence()
            {
                config.BeginSequence();
                return this;
            }

            public BehaviorTreeConfiguration BeginParallel()
            {
                config.BeginParallel();
                return this;
            }

            public BehaviorTreeConfiguration BeginParallel(ParallelCompletionBehavior parallelBehavior)
            {
                config.BeginParallel(parallelBehavior);
                return this;
            }

            public BehaviorTreeConfiguration End()
            {
                config.End();
                return this;
            }

            public BehaviorTreeConfiguration Action(System.Action onUpdate)
            {
                config.Action(onUpdate);
                return this;
            }

            public BehaviorTreeConfiguration Action(Func<bool> onUpdate)
            {
                config.Action(onUpdate);
                return this;
            }

            public BehaviorTreeConfiguration Condition(Func<bool> onUpdate)
            {
                config.Condition(onUpdate);
                return this;
            }
        }
    }

}
