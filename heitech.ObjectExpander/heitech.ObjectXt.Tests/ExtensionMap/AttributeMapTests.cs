using heitech.ObjectXt.ExtensionMap;
using heitech.ObjectXt.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace heitech.ObjectXt.Tests.ExtensionMap
{
    [TestClass]
    public class AttributeMapTests
    {
        private AttrSpy globalAttr = new AttrSpy();
        private readonly Mock<IExtensionAttribute> moq = new Mock<IExtensionAttribute>();
        readonly object obj = new object();

        [TestMethod]
        public void GlobalAttributeMap_AddAddsMap()
        {
            Assert.AreEqual(0, globalAttr.Count);
            globalAttr.Add(obj, "key", moq.Object);
            Assert.AreEqual(1, globalAttr.Count);
        }

        [TestMethod]
        public void GlobalAttributeMap_CanInvokeReturnsFalseIfValueIsNotInDictionary()
            => Assert.IsFalse(globalAttr.CanInvoke(obj, "key", null));

        [TestMethod]
        public void GlobalAttributeMap_CanInvokeReturnsTrueIfItemIsInValueOfDictionary()
        {
            Assert.IsFalse(globalAttr.CanInvoke(obj, "key"), null);
            var mock = new Mock<IExtensionAttribute>();
            globalAttr.Dictionary.Add("key", mock.Object);
            mock.Setup(x => x.CanInvoke("key", null)).Returns(true);
            Assert.IsTrue(globalAttr.CanInvoke(obj, "key", null));
        }

        [TestMethod]
        public void GlobalAttributeMap_HasKeyReturnsTrueIfUnderlyingDictionaryContainsKey()
        {
            Assert.IsFalse(globalAttr.HasKey(obj, "key"));
            globalAttr.Dictionary.Add("key", null);

            Assert.IsTrue(globalAttr.HasKey(obj, "key"));
        }

        [TestMethod]
        public void GlobalAttributeMap_InvokeReturnsNull_IfKeyIsNotFound()
        {
            object result = globalAttr.Invoke(obj, "key");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GlobalAttributeMap_InvokeRetrunsExtensionattributesInvokeReturnValueIfKeyIsFound()
        {
            object result = "abcaffeschnee";
            var map = new Mock<IExtensionAttribute>();
            map.Setup(x => x.Invoke()).Returns(result);
            globalAttr.Dictionary.Add("key", map.Object);

            Assert.AreSame(result, globalAttr.Invoke(obj, "key"));
        }

        private class AttrSpy : GlobalAttributeMap
        {
            internal int Count => MappedAttributes.Count;
            internal Dictionary<object, IExtensionAttribute> Dictionary => this.MappedAttributes;
        }
    }
}
