using heitech.ObjectXt.ExtensionMap.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace heitech.ObjectXt.Tests.ExtensionMap
{
    [TestClass]
    public class ExtensionAttributeInvokeTests
    {
        bool wasInvoked = false;
        [TestCleanup]
        public void TearDown()
        {
            Assert.IsTrue(wasInvoked);
        }

        [TestMethod]
        public void ExtensionAttribute_InvokeAction()
        {
            var action = new ActionAttribute("key", () => wasInvoked = true);
            action.Invoke();
        }

        [TestMethod]
        public void ExtensionAttribute_InvokeActionWithParam()
        {
            var action = new ActionParamAttribute<int>("key", i => wasInvoked = true);
            action.Invoke(42);
        }

        [TestMethod]
        public void ExtensionAttribute_InvokeActionWithWronParamThrowsException()
        {
            wasInvoked = true;
            Assert.ThrowsException<TargetParameterCountException>(() => new ActionAttribute("key", () => { }).Invoke(112));
            Assert.ThrowsException<TargetParameterCountException>(() => new ActionDualParameter<int, string>("key", (i, s) => { }).Invoke("abc", "def", 12));
            Assert.ThrowsException<TargetParameterCountException>(() => new ActionDualParameter<int, string>("key", (i, s) => { }).Invoke());
            Assert.ThrowsException<TargetParameterCountException>(() => new ActionDualParameter<int, string>("key", (i, s) => { }).Invoke("123"));
            Assert.ThrowsException<TargetParameterCountException>(() => new ActionAttribute("key", () => { }).Invoke(112));

            Assert.ThrowsException<ArgumentException>(() => new ActionParamAttribute<int>("key", (i) => { }).Invoke("abc"));
            Assert.ThrowsException<ArgumentException>(() => new ActionDualParameter<int, string>("key", (i, s) => { }).Invoke("abc", "def"));
        }

        [TestMethod]
        public void ExtensionAttribute_InvokeActionWithTwoParams()
        {
            var action = new ActionDualParameter<int, string>("key", (i,s) => wasInvoked = true);
            action.Invoke(42, "string");
        }

        [TestMethod]
        public void ExtensionAttribute_InvokeFunc()
        {
            var func = new FuncAttribute<int>("key", () => 42);
            wasInvoked = true;
            Assert.AreEqual(42, func.Invoke());
        }

        [TestMethod]
        public void ExtensionAttribute_InvokeFuncWithParam()
        {
            var func = new FuncAttributeParam<int, int>("key", i => i);
            wasInvoked = true;
            Assert.AreEqual(42, func.Invoke(42));
        }

        [TestMethod]
        public void ExtensionAttribute_InvokeFuncWithTwoParams()
        {
            var func = new FuncAttributeDualParam<int, int, int>("key", (j,i) => i+j);
            wasInvoked = true;
            Assert.AreEqual(42, func.Invoke(21,21));
        }
    }
}
