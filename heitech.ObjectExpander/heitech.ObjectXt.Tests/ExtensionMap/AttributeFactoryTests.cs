using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using heitech.ObjectXt.ExtensionMap;
using heitech.ObjectXt.Interfaces;
using heitech.ObjectXt.ExtensionMap.Attributes;
using Moq;

namespace heitech.ObjectXt.Tests.ExtensionMap
{
    [TestClass]
    public class AttributeFactoryTests
    {
        private IAttributeFactory factory;
        private readonly AttrMap map = new AttrMap();
        [TestInitialize]
        public void Init()
        {
            factory =  AttributeFactory.Create(map);
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeActionAttribute()
        {
            IExtensionAttribute attribute = factory.CreateActionAttribute("key",() => { });
            Assert.AreEqual(typeof(ActionAttribute), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeActionParamAttribute()
        {
            IExtensionAttribute attribute = factory.CreateActionAttribute<string, int>("key", (i) => { });
            Assert.AreEqual(typeof(ActionParamAttribute<int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeActionAttributeDualParam()
        {
            IExtensionAttribute attribute = factory.CreateActionAttribute<string, int, int>("key", (i,j) => { });
            Assert.AreEqual(typeof(ActionDualParameter<int,int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeFunc()
        {
            IExtensionAttribute attribute = factory.CreateFuncAttribute<string, int>("key", () => 42);
            Assert.AreEqual(typeof(FuncAttribute<int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeFuncParam()
        {
            IExtensionAttribute attribute = factory.CreateFuncAttribute<string, int, int>("key", (i) => 42);
            Assert.AreEqual(typeof(FuncAttributeParam<int, int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeFuncDualParam()
        {
            IExtensionAttribute attribute = factory.CreateFuncAttribute<string, int, int, int>("key", (i,j) => 42);
            Assert.AreEqual(typeof(FuncAttributeDualParam<int,int, int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateReturnsAttributeMapGlobalIfNotSet()
        {
            var map = factory.GetMap();
            Assert.AreEqual(typeof(AttrMap), map.GetType());
        }
    }

    internal class AttrMap : IAttributeMap
    {
        internal bool _CanInvoke { get; set; }
        internal bool _HasKey { get; set; }
        internal bool WasAdded { get; private set; }
        internal bool WasInvoked { get; private set; }
        public void Add<TKey>(object extended, TKey key, IExtensionAttribute func)
        {
            WasAdded = true;
        }

        public bool CanInvoke<TKey>(object extended, TKey key, Type expectedReturnType, params object[] parameters)
        {
            return _CanInvoke;
        }

        public bool HasKey<TKey>(object extended, TKey key)
        {
            return _HasKey;
        }

        public object Invoke<TKey>(object extended, TKey key, params object[] parameters)
        {
            WasInvoked = true;
            return null;
        }
    }
}
