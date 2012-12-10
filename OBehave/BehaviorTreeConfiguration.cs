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
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException
                        (BehaviorTreeResource.SelectorMustBeNestedUnderCompositeNodes
                        );

                var selector = new Selector<TContext>();

                if (stack.Count != 0)
                {
                    var parent = stack.Peek() as Composite<TContext>;
                    Debug.Assert(parent != null);
                    parent.AddChildNode(selector);
                }

                if (behaviorTree.node == null)
                    behaviorTree.node = selector;

                stack.Push(selector);
                return this;
            }

            public BehaviorTreeConfiguration BeginSequence()
            {
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException
                        (BehaviorTreeResource.SequenceMustBeNestedUnderCompositeNodes
                        );

                var sequence = new Sequence<TContext>();

                if (stack.Count != 0)
                {
                    var parent = stack.Peek() as Composite<TContext>;
                    Debug.Assert(parent != null);
                    parent.AddChildNode(sequence);
                }

                if (behaviorTree.node == null)
                    behaviorTree.node = sequence;

                stack.Push(sequence);
                return this;
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
                return Action(c => onUpdate());
            }
            
            public BehaviorTreeConfiguration Action(System.Action<TContext> onUpdate)
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
                    
                    if (parent is Selector<TContext>)
                        parent.AddChildNode(new Not<TContext>(action));
                    else
                        parent.AddChildNode(action);                
                }

                return this;
            }

            public BehaviorTreeConfiguration Condition(System.Func<bool> onUpdate)
            {
                return Condition(c => onUpdate());
            }

            public BehaviorTreeConfiguration Condition(System.Func<TContext, bool> onUpdate)
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

            public BehaviorTreeConfiguration Condition(System.Func<bool> onUpdate)
            {
                config.Condition(onUpdate);
                return this;
            }
        }
    }

}
