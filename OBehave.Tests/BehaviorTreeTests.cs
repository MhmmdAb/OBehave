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
            tree.Configure().Action(() => Assert.True(true));
            tree.Configure().Action(() => Assert.True(true));            
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
            var wasCalled = false;
            
            var tree = new BehaviorTree();
            tree.Configure().Action(() => wasCalled = true);            
            tree.Update();
            Assert.True(wasCalled);
        }

        [Test]
        public void Can_call_Update_on_tree_with_single_condition()
        {
            var wasCalled = false;            
            
            var tree = new BehaviorTree();
            tree.Configure().Condition(() => 
            {
                wasCalled = true;
                return false;
            });
            tree.Update();
            Assert.True(wasCalled);
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
                .Action(() => Assert.True(true))
            .End();
            tree.Update();
        }

        [Test]
        public void Condition_returning_false_stops_sequence()
        {
            var tree = new BehaviorTree();
            tree.Configure().BeginSequence()
                .Action(() => Assert.True(true))
                .Condition(() => false)
                .Action(() => Assert.Fail())
            .End();
            tree.Update();
        }

        [Test]
        public void Nested_sequence_runs()
        {            
            var nestedActionCalled = false;

            var tree = new BehaviorTree();
            tree.Configure().BeginSequence()
                .BeginSequence()
                    .Action(() => nestedActionCalled = true)                        
                .End()
                .Action(() => Assert.True(true))
            .End();
            tree.Update();
            Assert.True(nestedActionCalled);
        }

        [Test]
        public void Condition_returning_false_blocks_nested_sequence()
        {
            var tree = new BehaviorTree();
            tree.Configure().BeginSequence()
                .Condition(() => false)
                .BeginSequence()
                    .Action(() => Assert.Fail())
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
                .Action(() => Assert.That(++callCount == 1))
                .Action(() => Assert.That(++callCount == 2))
                .Action(() => Assert.That(++callCount == 3))
            .End();
            tree.Update();
        }

        [Test]
        public void Selector_runs_nested_sequence()
        {
            var wasNestedActionCalled = false;

            var tree = new BehaviorTree();
            tree.Configure().BeginSelector()
                .BeginSequence()
                    .Action(() => wasNestedActionCalled = true)
                .End()
            .End();
            tree.Update();
            Assert.True(wasNestedActionCalled);            
        }

        [Test]
        public void Selector_skips_failed_nested_sequence()
        {
            var tree = new BehaviorTree();
            tree.Configure().BeginSelector()
                .BeginSequence()
                    .Condition(() => false)
                    .Action(() => Assert.Fail())
                .End()
                .Action(() => Assert.True(true))
            .End();
            tree.Update();
        }
    }
}
