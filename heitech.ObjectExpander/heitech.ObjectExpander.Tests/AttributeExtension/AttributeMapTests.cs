using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using heitech.ObjectExpander.AttributeExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace heitech.ObjectExpander.Tests.AttributeExtension
{
    [TestClass]
    public class AttributeMapTests
    {
        private readonly Spy map = new Spy();

        // AttributeMap_
        [TestMethod]
        public void AttributeMap_Add_ExtendsItem_InMap_ByKeyAndValue()
        {
            Assert.AreEqual(0, map.CountItems);
            map.Add("key", new object());

            Assert.AreEqual(1, map.CountItems);
        }

        [TestMethod]
        public void AttributeMap_AddSameKeyTwicwe_ThrowsArgumentException()
        {
            map.Add("key", new object());
            Assert.ThrowsException<ArgumentException>(() => map.Add("key", 42));
        }

        [TestMethod]
        public void AttributeMap_Remove_RemovesItemAtKey()
        {
            map.Add("key", new object());
            Assert.AreEqual(1, map.CountItems);

            map.Remove("key");
            Assert.AreEqual(0, map.CountItems);
        }

        [TestMethod]
        public void AttributeMap_Remove_ThrowsKeyNotFoundException_IFKeyIsNotRegistered()
            => Assert.ThrowsException<KeyNotFoundException>(() => map.Remove("key"));

        [TestMethod]
        public void AttributeMap_TryGetAttribute_ReturnsFalse_IfKeyIsNotRegistered()
        {

        }

        [TestMethod]
        public void AttributeMap_TryGetAttribute_ReturnsTrue_IfKeyIsRegistered()
        {

        }

        [TestMethod]
        public void AttributeMap_TryGetAttribute_ReturnsExpectedAttribute()
        {
            string expectedAttr = "abcaffeschnee";
            string key = "key";

            map.Add(key, expectedAttr);

            map.TryGetAttribute(key, out string s);
            
        }

        [TestMethod]
        public void AttributeMap_ThrowsArgumentException_IfKeyOrValIsNull()
        {
            Assert.ThrowsException<ArgumentException>(() => map.Add(null, 42));
            Assert.ThrowsException<ArgumentException>(() => map.Add("k", null));
        }

        [TestMethod]
        public void AttributeMap_IsRegisteredKey_ReturnsFalse_IfKeyIsNotRegistered()
                => Assert.IsFalse(map.HasKey("key"));

        [TestMethod]
        public void AttributeMap_IsRegisteredKey_ReturnsTrue_IfKeyIsRegistered()
        {
            map.Add("key", new object());
            Assert.IsTrue(map.HasKey("key"));
        }

        [TestMethod]
        public void AttributeMap_HasTypeAttribute_ReturnsFalse_IfNoTypeIsFound()
            => Assert.IsFalse(map.HasAttributeOfType<string>());

        [TestMethod]
        public void AttributeMap_HasTypeAttribute_ReturnsTrue_ifTypeIsFound_AndReturnsTupleOfKeysAndValues()
        {
            map.Add("key", "attribute");
            Assert.IsTrue(map.HasAttributeOfType<string>());
        }

        [TestMethod]
        public void AttributeMap_IndexerGet_ReturnsObject_IfKeyIsRegistered()
        {
            map.Add("key", "attribute");
            object attr = map["key"];

            Assert.AreEqual("attribute", attr);
        }

        [TestMethod]
        public void AttributeMap_IndexerGet_ThrowsAttributeNotFoundException_IfKeyIsNotRegistered()
            => Assert.ThrowsException<KeyNotFoundException>(() => map["key"]);

        [TestMethod]
        public void AttributeMap_IndexerSet_SetsNewObject_IfNotRegistered()
        {
            map["key"] = "attribute";

            Assert.AreEqual(1, map.CountItems);
        }

        [TestMethod]
        public void AttributeMap_IndexerSet_OverwritesObject_ifKeyIsRegistered()
        {
            map.Add("key", "attribute");
            map["key"] = "otherAttr";

            Assert.AreEqual("ohterAttr", map["key"]);
        }
    }

    internal class Spy : AttributeExtender<string>
    {
        internal int CountItems => this.Attributes.Count;
    }
}
