using heitech.ObjectExpander.AttributeExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace heitech.ObjectExpander.Tests.AttributeExtension
{
    [TestClass]
    public class AttributeMapTests
    {
        private readonly Spy map = new Spy();

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
            => Assert.IsFalse(map.TryGetAttribute("key", out string val));

        [TestMethod]
        public void AttributeMap_TryGetAttribute_ReturnsTrue_IfKeyIsRegistered()
        {
            map.Add("key", "string");
            Assert.IsTrue(map.TryGetAttribute("key", out string s));
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
        public void AttributeMap_HasKey_ReturnsFalse_IfKeyIsNotRegistered()
                => Assert.IsFalse(map.HasAttribute("key"));

        [TestMethod]
        public void AttributeMap_HasKey_ReturnsTrue_IfKeyIsRegistered()
        {
            map.Add("key", new object());
            Assert.IsTrue(map.HasAttribute("key"));
        }

        [TestMethod]
        public void AttributeMap_HasTypeAttribute_ReturnsFalse_IfNoTypeIsFound()
            => Assert.IsFalse(map.HasAttributeOfType<string>(out string key));

        [TestMethod]
        public void AttributeMap_HasTypeAttribute_ReturnsTrue_ifTypeIsFound_AndReturnsOutKey()
        {
            map.Add("key", "attribute");
            Assert.IsTrue(map.HasAttributeOfType<string>(out string key));
            Assert.AreEqual("key", key);
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

            Assert.AreEqual("otherAttr", map["key"]);
        }

        [TestMethod]
        public void AttributeMap_GetKeyAttributeMap_returnsTupleWithBoolean_IsFalse_IfKeyIsNotThere()
        {
            (bool b, string key, string attr) result = map.GetKeyAttributePair<string>("key");
            Assert.IsFalse(result.b);
            Assert.IsNull(result.attr);
        }

        [TestMethod]
        public void AttributeMap_GetKeyAttributeMap_returnsTupleWithBoolean_IsTrue_IfKeyIsThere()
        {
            map.Add("key", "result");
            (bool b, string key, string attr) result = map.GetKeyAttributePair<string>("key");
            Assert.IsTrue(result.b);
            Assert.IsNotNull(result.attr);
            Assert.AreEqual("result", result.attr);
        }
    }

    internal class Spy : AttributeExtender<string>
    {
        internal int CountItems => this.Attributes.Count;
    }
}
