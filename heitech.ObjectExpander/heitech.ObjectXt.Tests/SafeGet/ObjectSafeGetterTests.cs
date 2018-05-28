using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using heitech.ObjectXt.SafeGet;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace heitech.ObjectXt.Tests.SafeGet
{
    [TestClass]
    public class ObjectSafeGetterTests
    {
        private ObjectSafeGetter _getter;

        private object[] GetItems()
        => new object[]
        {
            "name",
            42,
            new ObjectResult(),
            new ObjectResult_2(),
            new StructResult()
        };

        [TestInitialize]
        public void Init()
        {
            _getter = new ObjectSafeGetter(GetItems());
        }

        [TestMethod]
        public void SafeGetter_TryGet_Returns_True_If_Type_Is_Once_In_Collection()
        {
            bool isInCollection = _getter.TryGet(out int i);

            Assert.IsTrue(isInCollection);
            Assert.AreEqual(42, i);
        }

        [TestMethod]
        public void SafeGetter_TryGet_Returns_False_If_Type_Is_More_Than_Once_In_Collection()
        {
            bool isInCollection = _getter.TryGet(out IObjectResult obj);

            Assert.IsFalse(isInCollection);
            Assert.IsNull(obj);
        }


        [TestMethod]
        public void SafeGetter_TryGet_Returns_True_If_Subclass_of_Type_Is_Once_In_Collection()
        {
            bool isInCollection = _getter.TryGet(out ObjectResult_2 obj);

            Assert.IsTrue(isInCollection);
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void SafeGetter_TryGet_Returns_True_If_Type_Assignable_and_Is_Once_In_Collection()
        {
            bool isInCollection = _getter.TryGet(out IObjectResult2 obj);

            Assert.IsTrue(isInCollection);
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void SafeGetter_TryGetAll_Returns_EmptyCollection_And_False_If_Is_not_In_Collection()
        {
            bool isInCollection = _getter.TryGetAll(out IEnumerable<ObjectSafeGetter> objs);

            Assert.IsFalse(isInCollection);
            Assert.AreEqual(0, objs.Count());
        }

        [TestMethod]
        public void SafeGetter_TryGetAll_Returns_True_If_Type_Is_at_Least_Once_In_Collection()
        {
            bool isInCollection = _getter.TryGetAll(out IEnumerable<IObjectResult> objs);

            Assert.IsTrue(isInCollection);
            Assert.AreEqual(2, objs.Count());
        }

        private interface IObjectResult { }
        private class ObjectResult : IObjectResult { }
        private class ObjectResult_2 : ObjectResult { }

        private interface IObjectResult2 { }
        private struct StructResult : IObjectResult2 { }
    }
}
