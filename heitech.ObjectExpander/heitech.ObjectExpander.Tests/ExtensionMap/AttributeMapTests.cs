using heitech.ObjectExpander.ExtensionMap;
using heitech.ObjectExpander.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace heitech.ObjectExpander.Tests.ExtensionMap
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


        private class AttrSpy : GlobalAttributeMap
        {
            internal int Count => MappedAttributes.Count;
        }
    }
}
