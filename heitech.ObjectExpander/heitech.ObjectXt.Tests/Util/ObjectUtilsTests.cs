using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using heitech.ObjectXt.Util;
using System.Linq;

namespace heitech.ObjectXt.Tests.Util
{
    [TestClass]
    public class ObjectUtilsTests
    {
        readonly PropertyTester obj = new PropertyTester();
        readonly string numb = nameof(PropertyTester.Number);

        [TestMethod]
        public void ObjectUtils_ReturnsDictionaryOfProperties()
        {
            int expected = obj.GetType().GetProperties().Count();
            Dictionary<string, object> result = obj.AllProperties();
            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void ObjectUtils_DictionaryOfPropertiesHasCurrentValues()
        {
            obj.Number = 42;
            Dictionary<string, object> result = obj.AllProperties();
            Assert.AreEqual(42, result[numb]);
        }

        [TestMethod]
        public void ObjectUtils_TryGetPropertyValueReturnsFalseIfPropertyIsNull()
        {
            bool result = obj.TryGetPropertyValue(numb, out int number);
            Assert.IsFalse(result);

            result = true;
            result = obj.TryGetPropertyValue("Text", out string text);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ObjectUtils_TryGetPropertyReturnsFalseIfPropertyTypeDoesNotMatch()
        {
            bool result = obj.TryGetPropertyValue(numb, out string number);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ObjectUtils_TryGetPropertyReturnsFalseIfPropertyNotFound()
        {
            string incorrectPropName = "Numba";
            bool result = obj.TryGetPropertyValue(incorrectPropName, out int number);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ObjectUtils_TryGetPropertyReturnsTrueIfValueFoundAndNonNull()
        {
            obj.Number = 42;
            bool result = obj.TryGetPropertyValue(nameof(PropertyTester.Number), out int number);
            Assert.IsTrue(result);
            Assert.AreEqual(42, number);

            obj.Text = "text";
            result = obj.TryGetPropertyValue(nameof(PropertyTester.Text), out string text);
            Assert.IsTrue(result);
            Assert.AreEqual("text", text);
        }

        [TestMethod]
        public void ObjectUtils_TryGetPropertyReturnsTrueIfTypeHasRelation()
        {
            obj.Nested = new AnotherTester();
            bool result = obj.TryGetPropertyValue("Nested", out IPropTester another);

            Assert.IsTrue(result);
            Assert.AreSame(another, obj.Nested);

            obj.Nested = new PropertyTester();
            result = obj.TryGetPropertyValue("Nested", out AnotherTester tester);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ObjectUtils_IsCastableReturnsFalseIfUpCast()
        {
            Assert.IsFalse(typeof(PropertyTester).IsUpCastable(typeof(IPropTester)));
            Assert.IsFalse(typeof(AnotherTester).IsUpCastable(typeof(PropertyTester)));

            Assert.IsFalse(typeof(IPropTester).IsDownCastable(typeof(PropertyTester)));
            Assert.IsFalse(typeof(PropertyTester).IsDownCastable(typeof(AnotherTester)));
        }

        [TestMethod]
        public void ObjectUtils_IsCastableReturnsTrueIfDownCast()
        {
            Assert.IsTrue(typeof(PropertyTester).IsDownCastable(typeof(IPropTester)));
            Assert.IsTrue(typeof(AnotherTester).IsDownCastable(typeof(PropertyTester)));

            Assert.IsTrue(typeof(IPropTester).IsUpCastable(typeof(PropertyTester)));
            Assert.IsTrue(typeof(PropertyTester).IsUpCastable(typeof(AnotherTester)));
        }

        [TestMethod]
        public void ObjectUtils_TryGetParameterCatchesTargetParameterCountException()
        {
            string _s = "abc";
            _s.AllProperties();
        }

        private class PropertyTester : IPropTester
        {
            public int Number { get; set; }
            public string Text { get; set; }
            public PropertyTester Nested { get; set; }
            public void Method() { }

            public PropertyTester()
            { }

            public int Function() => 0;
        }

        private class AnotherTester : PropertyTester
        {

        }

        private interface IPropTester
        {
            int Number { get; set; }
        }
    }
}
