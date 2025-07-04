using System;
using heitech.ObjectXt.ExtensionMap;
using heitech.ObjectXt.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace heitech.ObjectXt.Tests.Extender
{
    [TestClass]
    public class ObjectExtenderCallTests
    {
        IMarkedExtendable extendable = new MarkedObject();
        readonly AttributeMapMock map = new AttributeMapMock();
        readonly Mock<IAttributeFactory> factory = new Mock<IAttributeFactory>();

        [TestInitialize]
        public void Init()
        {
            factory.Setup(x => x.GetMap()).Returns(map);
            ObjectExtender.SetFactory(factory.Object);
        }

        [TestCleanup]
        public void Teardown()
        {
            ObjectExtender.SetFactory(null);
        }

        [TestMethod]
        public void ExtensionCaller_CallNotRegisteredThrowsattributeNotFoundException()
        {
            Assert.ThrowsException<AttributeNotFoundException>(() => extendable.Call("key"));
            Assert.ThrowsException<AttributeNotFoundException>(() => extendable.Call("key",42));
            Assert.ThrowsException<AttributeNotFoundException>(() => extendable.Call("key",42,43));

            Assert.ThrowsException<AttributeNotFoundException>(() => extendable.Invoke<string, int>("key"));
            Assert.ThrowsException<AttributeNotFoundException>(() => extendable.Invoke<string, int, int>("key",42));
            Assert.ThrowsException<AttributeNotFoundException>(() => extendable.Invoke<string, int,int,int>("key", 42,42));
        }

        [TestMethod]
        public void ExtensionCller_CallUsesAttributeMapCanInvokeAndInvokes()
        {
            extendable.RegisterAction("key", () => { });
            map.CanInvokeIt = true;

            extendable.Call("key");

            Assert.IsTrue(map.WasInvoked);
        }

        [TestMethod]
        public void ExtensionCaller_InvokeUsesAttributeMapWithCanInvokeAndInvoke()
        {
            extendable.RegisterFunc("key", () => 42);

            map.CanInvokeIt = true;
            map.Result = 42;

            int result = extendable.Invoke<string, int>("key");

            Assert.AreEqual(42, result);
            Assert.IsTrue(map.WasInvoked);
        }

        [TestMethod]
        public void ExtensionCaller_AsyncThrowNotIMplemented()
        {
            Assert.ThrowsException<NotImplementedException>(() => extendable.CallAsync("key"));
            Assert.ThrowsException<NotImplementedException>(() => extendable.InvokeAsync<string, int>("key"));
        }

        [TestMethod]
        public void ExtensionCaller_EachMethodCallsInvokeOnMapWithCorrectParameters()
        {
            map.CanInvokeIt = true;
            AssertInvoke(() => extendable.Call("key"), "key");
            AssertInvoke(() => extendable.Call("key", 42), "key", null, new object[] { 42 });
            AssertInvoke(() => extendable.Call("key", 42, 43), "key", null, new object[] { 42, 42 });

            map.Result = 112;
            AssertInvoke(() => extendable.Invoke<string, int>("key"),"key", typeof(int));
            AssertInvoke(() => extendable.Invoke<string, int, int>("key", 42), "key", typeof(int), new object[] { 42 });
            AssertInvoke(() => extendable.Invoke<string, int, int,int>("key", 42, 43), "key", typeof(int), new object[] { 42, 42 });

        }

        private void AssertInvoke(Action call, string key, Type type = null, params object[] parameters)
        {
            call();
            Assert.AreEqual(key, map.Input.key);
            Assert.AreEqual(type, map.Input.expectedType);
            Assert.AreEqual(parameters.Length, map.Input.parameters.Length);
            Assert.AreSame(extendable, map.Input.input);
        }

        private class AttributeMapMock : IAttributeMap
        {
            public void Add<TKey>(object extended, TKey key, IExtensionAttribute func)
            {
                //do nothing
            }
            internal bool CanInvokeIt { get; set; }
            public bool CanInvoke<TKey>(object extended, TKey key, Type expectedReturnType, params object[] parameters)
            {
                Input = (extended, key, expectedReturnType, parameters);
                return CanInvokeIt;
            }

            internal (object input, object key, Type expectedType, object[] parameters) Input { get; private set; }

            public bool HasKey<TKey>(object extended, TKey key) => false;

            internal bool WasInvoked { get; private set; }
            internal object Result { get; set; }
            public object Invoke<TKey>(object extended, TKey key, params object[] parameters)
            {
                WasInvoked = true;
                return Result;
            }
        }
    }
}
