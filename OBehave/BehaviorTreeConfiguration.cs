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
                Debug.Assert(behaviorTree.node == null);
                this.behaviorTree = behaviorTree;
            }

            public BehaviorTreeConfiguration BeginSelector(System.Action<TContext> onEnter   = null,
                                                           System.Action<TContext> onSuccess = null,
                                                           System.Action<TContext> onFailure = null,
                                                           System.Action<TContext> onExit    = null)
            {
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException(); // TODO: Add message

                var selector = new Selector<TContext>(onEnter, onSuccess, onFailure, onExit);

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

            public BehaviorTreeConfiguration BeginSequence(System.Action<TContext> onEnter = null,
                                                           System.Action<TContext> onSuccess = null,
                                                           System.Action<TContext> onFailure = null,
                                                           System.Action<TContext> onExit = null)
            {
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException(); // TODO: Add message

                var sequence = new Sequence<TContext>(onEnter, onSuccess, onFailure, onExit);

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
                    throw new InvalidOperationException(); // TODO: Add message
                stack.Pop();
                return this;
            }

            public BehaviorTreeConfiguration Action(System.Action<TContext> onUpdate,
                                                    System.Action<TContext> onEnter   = null,
                                                    System.Action<TContext> onSuccess = null,
                                                    System.Action<TContext> onFailure = null,
                                                    System.Action<TContext> onExit    = null)
            {
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException(); // TODO: Add message
                
                var action = new OBehave.Action<TContext>(onUpdate, onEnter, onSuccess, onFailure, onExit);

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

            public BehaviorTreeConfiguration Condition(System.Func<TContext, bool> onUpdate,
                                                       System.Action<TContext> onEnter   = null,
                                                       System.Action<TContext> onSuccess = null,
                                                       System.Action<TContext> onFailure = null,
                                                       System.Action<TContext> onExit    = null)

            {
                if (stack.Count != 0 && !(stack.Peek() is Composite<TContext>))
                    throw new InvalidOperationException(); // TODO: Add message
                
                var condition = new Condition<TContext>(onUpdate, onEnter, onSuccess, onFailure, onExit);

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
}
