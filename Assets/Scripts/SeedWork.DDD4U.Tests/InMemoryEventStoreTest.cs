using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Yaroyan.SeedWork.DDD4U.Test
{
    public class InMemoryEventStoreTest
    {
        [Test]
        public void dynamicTest()
        {
            TestInterface test = new TestClass();
            Assert.AreEqual(test.Test() + "Foo", Mutate(test));
        }
        public string Mutate(TestInterface test) => ((dynamic)this).Test((dynamic)test);

        public string Test(TestClass test) => test.Test() + "Foo";
        public string Test(Test2Class test) => test.Test() + "Bar";
    }

    public interface TestInterface
    {
        public string Test();
    }

    public class TestClass : TestInterface
    {
        public string Test()
        {
            return nameof(TestClass);
        }
    }

    public class Test2Class : TestInterface
    {
        public string Test()
        {
            return nameof(Test2Class);
        }
    }
}

