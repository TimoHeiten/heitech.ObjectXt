using System;
using System.Collections.Generic;
using heitech.ObjectExpander.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace heitech.ObjectExpander.Tests.Util
{
    [TestClass]
    public class MappedPropertyManagerTests 
    {
        private MapperSpy propManager;
        private readonly PropertyClass inputObj = new PropertyClass(new PropertyClass(null));

        [TestInitialize]
        public void Init()
        {
            propManager = new MapperSpy(inputObj);
        }

        [TestMethod]
        public void MappedPropertyManager_DictionaryNamesAreConservedFromProperty()
        {
            IDictionary<string, object> dict = propManager.AllProperties();
            Assert.ThrowsException<KeyNotFoundException>(() => dict["number"]);
            Assert.AreEqual(42, dict["Number"]);
            Assert.ThrowsException<KeyNotFoundException>(() => dict["text"]);
            Assert.AreEqual("text", dict["Text"]);
        }

        [TestMethod]
        public void MappedPropertyManager_IndexerThrowsKeyNotFoundOnIncorrectKey()
            => Assert.ThrowsException<KeyNotFoundException>(() => propManager["nokey"]);

        [TestMethod]
        public void MappedPropertyManager_IndexerGetReturnsCurrentValueOnCorrectKey()
        {
            Assert.AreEqual(42, propManager["Number"]);
            Assert.AreEqual("text", propManager["Text"]);
        }

        [TestMethod]
        public void MappedPropertyManager_IndexerSetThrowsArgumentExceptionOnWrongType()
            => Assert.ThrowsException<ArgumentException>(() => propManager["Number"] = "text");

        [TestMethod]
        public void MappedPropertyManager_IndexerSetTHrowsKeyNotFoundExceptionOnIncorrectKey()
            => Assert.ThrowsException<KeyNotFoundException>(() => propManager["incorrect"] = 112);

        [TestMethod]
        public void MappedPropertyManager_IndexerSetsOnCorrectValue()
        {
            Assert.AreEqual(42, propManager["Number"]);
            propManager["Number"] = 112;
            Assert.AreEqual(112, propManager["Number"]);
        }

        [TestMethod]
        public void MappedPropertyManager_CreationGeneratesDictionaryWithAllProperties()
            => Assert.AreEqual(typeof(PropertyClass).GetProperties(ObjectUtils.GetFlags()).Length, propManager.AllProperties().Count);

        [TestMethod]
        public void MappedPropertyManager_TryGetPropertyReturnsFalseOnFalseName()
        {
            bool isSuccess = propManager.TryGetProperty("incorrect", out int i);
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void MappedPropertyManager_TryGetPropertyReturnsFalseOnCorrectNameButIncorrectType()
        {
            bool isSuccess = propManager.TryGetProperty("Number", out string s);
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void MappedPropertyManager_TryGetPropertyReturnsTrueOnCorrectNameAndType()
        {
            bool isSuccess = propManager.TryGetProperty("Number", out int i);
            Assert.AreEqual(42, i);
            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        public void MappedPropertyManager_TrySetValueReturnsFalseOnIncorrectNameAndIncorrectType()
        {
            bool isSuccess = propManager.TrySetProperty("number", "text");
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void MappedPropertyManager_TrySetValueReturnsFalseOnIncorrectNameAndCorrectType()
        {
            bool isSuccess = propManager.TrySetProperty("NUMBER", 42);
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void MappedPropertyManager_TrySetValueReturnsFalseOnCorrectNameAndIncorrectType()
        {
            bool isSuccess = propManager.TrySetProperty("Number", "abcaffeschnee");
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void MappedPropertyManager_TrySetAndTryGetWorkWithInterfaces()
        {
            var nestedNext = new PropertyClass(null) { Number = 112, Text ="newOne" };

            Assert.IsTrue(propManager.TrySetProperty("Nested", nestedNext));
            Assert.IsTrue(propManager.TryGetProperty("Nested", out IPropertyClass @class));

            Assert.AreSame(nestedNext, @class);
        }

        [TestMethod]
        public void MappedPropertyManager_TrySetValueReturnsTrueOnCorrectNameAndCorrectType_AlsoSetsValueOnOriginalObject()
        {
            bool isSuccess = propManager.TrySetProperty("Number", 1000);

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(1000, propManager["Number"]);
            Assert.AreEqual(1000, inputObj.Number);
        }

        [TestMethod]
        public void MappedPropertyManager_WorksWithInternalProperty()
        {
            string name = nameof(PropertyClass.SomeText);
            Assert.IsTrue(propManager.TryGetProperty(name, out string s));
            Assert.IsTrue(propManager.TrySetProperty(name, "someOther"));

            Assert.AreEqual("someOther", inputObj.SomeText);
            Assert.AreEqual("someOther", propManager[name]);
        }

        [TestMethod]
        public void MappedPropertyManager_WorksWithStaticProperty()
        {
            string name = nameof(PropertyClass.AnyStatic);
            string expected = "abcaffeschnee";
            Assert.IsTrue(propManager.TrySetProperty(name, expected));
            Assert.IsTrue(propManager.TryGetProperty(name, out string s));

            Assert.AreEqual(expected, s);
        }

        [TestMethod]
        public void MappedPropertyManager_WorksWithInitialNullProperty()
        {
            string name = nameof(PropertyClass.AnyInternal);
            string expected = "abcaffeschnee";
            Assert.IsTrue(propManager.TrySetProperty(name, expected));
            Assert.IsTrue(propManager.TryGetProperty(name, out string s));

            Assert.AreEqual(expected, s);
        }

        [TestMethod]
        public void MappedPropertyManager_GetWorksWithLambdaSyntaxProperty()
        {
            string key = nameof(PropertyClass.SomeAutoProp);
            Assert.IsTrue(propManager.TryGetProperty(key, out string s));
        }

        [TestMethod]
        public void MappedPropertyManager_SetWorksWithLambdaSyntaxProperty()
        {
            string key = nameof(PropertyClass.SomeAutoProp);
            Assert.ThrowsException<NullReferenceException>(() => propManager.TrySetProperty(key, "next"));
        }

        [TestMethod]
        public void MappedPropertyManager_ToString_G_ReturnsJustTypeName()
        {
            propManager = new MapperSpy(new Item());
            Assert.AreEqual(typeof(MapperSpy).Name, propManager.ToString("g", null));
        }

        [TestMethod]
        public void MappedPropertyManager_ToString_A_ReturnsAllItemsListedAsKeyValuePair()
        {
            propManager = new MapperSpy(new Item());
            string expected = "MappedProperties: 'key:Property, value:Object'\n";
            Assert.AreEqual(expected, propManager.ToString("a", null));
        }

        [TestMethod]
        public void MappedPropertyManager_ToString_O_ReturnsOrigin()
        {
            propManager = new MapperSpy("abc");
            string expected = "MapperSpy: Origin 'abc', OriginType 'System.String'";
            Assert.AreEqual(expected, propManager.ToString("o", null));
        }

        [TestMethod]
        public void MappedPropertyManager_ToString_NoArgsReturnsTypeName()
        {
            Assert.AreEqual(typeof(MapperSpy).Name, propManager.ToString("was falsches", null));
        }

        private class Item
        {
            internal object Property { get; } = new object();
        }

        private class MapperSpy : MappedPropertyManager
        {
            internal MapperSpy(object obj) 
                : base(obj)
            {
            }

            internal object Origin => this.origin;
        }

        private class PropertyClass : IPropertyClass
        {
            public int Number { get; set; }
            public string Text { get; set; }

            internal string SomeText { get; } = "internaltext";

            public static string AnyStatic { get; } = "start";
            internal string AnyInternal { get; }

            public IPropertyClass Nested { get; }

            public string SomeAutoProp => "Auto";

            public void Method() { }

            public PropertyClass(IPropertyClass nested)
            {
                Number = 42;
                Text = "text";
                Nested = nested;
            }
        }

        private interface IPropertyClass
        {
            int Number { get; }
            string Text { get; }
            IPropertyClass Nested { get; }
        }
    }
}
