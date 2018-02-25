using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using heitech.ObjectExpander.ExtensionMap;
using heitech.ObjectExpander.Interfaces;
using heitech.ObjectExpander.ExtensionMap.Attributes;

namespace heitech.ObjectExpander.Tests.ExtensionMap
{
    [TestClass]
    public class AttributeFactoryTests
    {
        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeActionAttribute()
        {
            IExtensionAttribute attribute = AttributeFactory.CreateActionAttribute("key",() => { });
            Assert.AreEqual(typeof(ActionAttribute), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeActionParamAttribute()
        {
            IExtensionAttribute attribute = AttributeFactory.CreateActionAttribute<string, int>("key", (i) => { });
            Assert.AreEqual(typeof(ActionParamAttribute<int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeActionAttributeDualParam()
        {
            IExtensionAttribute attribute = AttributeFactory.CreateActionAttribute<string, int, int>("key", (i,j) => { });
            Assert.AreEqual(typeof(ActionDualParameter<int,int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeFunc()
        {
            IExtensionAttribute attribute = AttributeFactory.CreateFuncAttribute<string, int>("key", () => 42);
            Assert.AreEqual(typeof(FuncAttribute<int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeFuncParam()
        {
            IExtensionAttribute attribute = AttributeFactory.CreateFuncAttribute<string, int, int>("key", (i) => 42);
            Assert.AreEqual(typeof(FuncAttributeParam<int, int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateActionReturnsTypeFuncDualParam()
        {
            IExtensionAttribute attribute = AttributeFactory.CreateFuncAttribute<string, int, int, int>("key", (i,j) => 42);
            Assert.AreEqual(typeof(FuncAttributeDualParam<int,int, int>), attribute.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateReturnsAttributeMapGlobalIfNotSet()
        {
            var map = AttributeFactory.CreateMap();
            Assert.AreEqual(typeof(GlobalAttributeMap), map.GetType());
        }

        [TestMethod]
        public void AttributeFactory_CreateReturnsAttributeMapThatWasSetInConfig()
        {
            AttributeFactory.SetMap(() => new AttrMap());
            var map = AttributeFactory.CreateMap();
            Assert.AreEqual(typeof(AttrMap), map.GetType());
        }

        private class AttrMap : IAttributeMap
        {
            public void Add<TKey>(object extended, TKey key, IExtensionAttribute func)
            {
                throw new NotImplementedException();
            }

            public bool CanInvoke<TKey>(TKey key, Type expectedReturnType, params object[] parameters)
            {
                throw new NotImplementedException();
            }

            public bool HasKey<TKey>(TKey key)
            {
                throw new NotImplementedException();
            }

            public object Invoke<TKey>(TKey key, params object[] parameters)
            {
                throw new NotImplementedException();
            }
        }
    }
}
