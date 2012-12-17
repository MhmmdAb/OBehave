using System;
using NUnit.Framework;

namespace OBehave.Tests
{
    [TestFixture]
    public class BehaviorTreeTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Calling_Update_on_unconfigured_tree_throws()
        {
            var tree = new BehaviorTree();
            tree.Update();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Calling_Configure_more_than_once_throws()
        {
            var tree = new BehaviorTree();
            tree.Configure().Action(() => true);
            tree.Configure().Action(() => true);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Calling_Configure_after_calling_Update_throws()
        {
            var tree = new BehaviorTree();
            tree.Configure().Condition(() => true);
            tree.Update();
            tree.Configure().Condition(() => true);
        }
        
        [Test]
        public void Can_call_Update_on_tree_with_single_action()
        {
            var actionWasCalled = false;
            
            var tree = new BehaviorTree();
            tree.Configure().Action(() => actionWasCalled = true);            
            tree.Update();
            Assert.That(actionWasCalled);
        }

        [Test]
        public void Can_call_Update_on_tree_with_single_condition()
        {
            var conditionWasCalled = false;            
            
            var tree = new BehaviorTree();
            tree.Configure().Condition(() => 
            {
                conditionWasCalled = true;
                return false;
            });
            tree.Update();
            Assert.That(conditionWasCalled);
        }

        [Test]
        public void Sequence_runs_steps_in_order()
        {
            var tree = new BehaviorTree();
            var actionCount = 0;
            tree.Configure().BeginSequence()
                .Action(() => Assert.That(++actionCount == 1))
                .Action(() => Assert.That(++actionCount == 2))
                .Action(() => Assert.That(++actionCount == 3))
            .End();            
            tree.Update();
        }

        [Test]
        public void Condition_returning_true_does_not_stop_sequence()
        {
            var tree = new BehaviorTree();
            tree.Configure().BeginSequence()
                .Condition(() => true)
                .Action(() =>
                {
                    Assert.That(true);
                    return true;
                })
            .End();
            tree.Update();
        }

        [Test]
        public void Condition_returning_false_stops_sequence()
        {
            var tree = new BehaviorTree();
            tree.Configure().BeginSequence()
                .Action(() => true)
                .Condition(() => false)
                .Action(() =>
                {
                    Assert.Fail();
                    return true;
                })
            .End();
            tree.Update();
        }

        [Test]
        public void Nested_sequence_runs()
        {            
            var nestedActionWasCalled = false;

            var tree = new BehaviorTree();
            tree.Configure().BeginSequence()
                .BeginSequence()
                    .Action(() => nestedActionWasCalled = true)                        
                .End()
                .Action(() => true)
            .End();
            tree.Update();
            Assert.That(nestedActionWasCalled);
        }

        [Test]
        public void Condition_returning_false_blocks_nested_sequence()
        {
            var tree = new BehaviorTree();
            tree.Configure().BeginSequence()
                .Condition(() => false)
                .BeginSequence()
                    .Action(() =>
                    {
                        Assert.Fail();
                        return true;
                    })
                .End()
            .End();
            tree.Update();
        }

        [Test]
        public void Selector_runs_steps_in_order()
        {
            var callCount = 0;

            var tree = new BehaviorTree();
            tree.Configure().BeginSelector()
                .Action(() =>
                {
                    Assert.That(++callCount == 1);
                    return true;
                })
                .Action(() =>
                {
                    Assert.That(++callCount == 2);
                    return true;
                })
                .Action(() =>
                {
                    Assert.That(++callCount == 3);
                    return true;
                })
            .End();
            tree.Update();
        }

        [Test]
        public void Selector_runs_nested_sequence()
        {
            var nestedActionWasCalled = false;

            var tree = new BehaviorTree();
            tree.Configure().BeginSelector()
                .BeginSequence()
                    .Action(() => nestedActionWasCalled = true)
                .End()
            .End();
            tree.Update();
            Assert.That(nestedActionWasCalled);            
        }

        [Test]
        public void Selector_skips_failed_nested_sequence()
        {
            var tree = new BehaviorTree();
            tree.Configure().BeginSelector()
                .BeginSequence()
                    .Condition(() => false)
                    .Action(() =>
                    {
                        Assert.Fail();
                        return true;
                    })
                .End()
            .End();
            tree.Update();
        }

        [Test]
        public void Tree_skips_to_running_action_on_update()
        {
            var firstActionCallCount = 0;
            var runningActionCallCount = 0;
            var lastActionCallCount = 0;

            var tree = new BehaviorTree();
            tree.Configure().BeginSequence()
                .Action(() =>
                {
                    ++firstActionCallCount;
                    return true;
                })
                .Action(() =>
                {
                    ++runningActionCallCount;
                    if (runningActionCallCount >= 2)
                        return true;
                    return false;
                })
                .Action(() =>
                {
                    ++lastActionCallCount;
                    return true;
                })
            .End();

            tree.Update();
            tree.Update();
            
            Assert.That(firstActionCallCount == 1);
            Assert.That(lastActionCallCount == 1);
            Assert.That(runningActionCallCount == 2);
        }

        [Test]
        public void Selector_restarts_on_success()
        {
            var condition = false;
            var firstSequenceWasCalled = false;
            var secondSequenceWasCalled = false;

            var tree = new BehaviorTree();
            tree.Configure().BeginSelector()
                .BeginSequence()
                    .Condition(() => !condition)
                    .Action(() => firstSequenceWasCalled = true)
                .End()
                .BeginSequence()
                    .Condition(() => condition)
                    .Action(() =>
                    {
                        condition = false;
                        secondSequenceWasCalled = true;
                    })
                .End()
            .End();

            condition = true;
            tree.Update();
            Assert.That(!firstSequenceWasCalled);
            Assert.That(secondSequenceWasCalled);
            Assert.That(!condition);
            firstSequenceWasCalled = false;
            secondSequenceWasCalled = false;
            tree.Update();
            Assert.That(firstSequenceWasCalled);
            Assert.That(!secondSequenceWasCalled);
        }
    }
}

