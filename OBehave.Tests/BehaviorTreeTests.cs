using System;
using NUnit.Framework;

namespace OBehave.Tests
{
    [TestFixture]
    public class BehaviorTreeTests
    {
        public static int callCount = 0;
        public static void method()
        {
            ++callCount;
        }

        [Test]
        public void Can_call_update_on_tree_with_single_action()
        {
            var wasCalled = false;
            
            var tree = new BehaviorTree<object>();
            tree.Configure()
                .Action(onUpdate: c => wasCalled = true);
            tree.Update(null);

            Assert.That(wasCalled == true);
        }

        [Test]
        public void Can_call_update_on_tree_with_single_condition()
        {
            var wasCalled = false;            
            
            var tree = new BehaviorTree<object>();
            tree.Configure()
                .Condition(onUpdate: c => 
                {
                    wasCalled = true;
                    return false;
                });
            tree.Update(null);

            Assert.That(wasCalled);
        }
    }
}
