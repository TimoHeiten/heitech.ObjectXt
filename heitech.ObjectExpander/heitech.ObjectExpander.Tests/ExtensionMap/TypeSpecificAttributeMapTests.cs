using heitech.ObjectExpander.ExtensionMap;
using heitech.ObjectExpander.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace heitech.ObjectExpander.Tests.ExtensionMap
{
    [TestClass]
    public class TypeSpecificAttributeMapTests
    {
        private Spy map;
        private bool wasCreated = false;
        private IAttributeMap Factory()
        {
            wasCreated = true;
            return new GlobalAttributeMap();
        }

        [TestInitialize]
        public void Init()
        {
            map = new Spy(Factory);
        }

        [TestMethod]
        public void TypeSpecific_AddCreatesNewIfNotContained()
        {
            map.Add("string", "key", new AnyAttribute());

            Assert.IsTrue(wasCreated);
            Assert.AreEqual(1, map.Count);
        }

        [TestMethod]
        public void TypeSpecific_AddAddsToAlreadyContainedMap()
        {
            var attrMap = new AttrMap();
            map.Add(typeof(string), attrMap);

            Assert.AreEqual(1, map.Count);
            map.Add("string", "key", new AnyAttribute());
            Assert.AreEqual(1, map.Count);
            Assert.IsTrue(attrMap.WasAdded);
            Assert.IsFalse(wasCreated);
        }

        [TestMethod]
        public void TypeSpecific_CanInvokeReturnsTrueIfKeyIsFound()
        {
            map.Add("string", "key", new AnyAttribute { _Can = true });
            Assert.IsTrue(map.CanInvoke("string", "key", null));
        }

        [TestMethod]
        public void TypeSpecific_CanInvokeReturnsTrue_ForAllRegistered_withType()
        {
            map.Add("string", "key1", new AnyAttribute { _Can = true });
            map.Add("string", "key2", new AnyAttribute { _Can = true });

            Assert.IsTrue(map.CanInvoke("string", "key1", null));
            Assert.IsTrue(map.CanInvoke("string", "key2", null));
        }

        [TestMethod]
        public void TypeSpecific_CanInvoke_ReturnsFalse_IfKeyIsNotFound()
            => Assert.IsFalse(map.CanInvoke("string", "key", null));

        private class Spy : TypeSpecificAttributeMap
        {
            internal Spy(Func<IAttributeMap> factory) : base(factory)
            {
            }

            internal int Count => this.dictionary.Count;
            internal void Add(Type type, IAttributeMap map) => dictionary.Add(type, map);
        }

        private class AnyAttribute : IExtensionAttribute
        {
            internal bool _Can { get; set; }
            public bool CanInvoke<TKey>(TKey key, Type expectedReturnType = null, params object[] parameters)
                => _Can;

            public object Invoke(params object[] parameters)
            {
                throw new NotImplementedException();
            }
        }
    }
}
