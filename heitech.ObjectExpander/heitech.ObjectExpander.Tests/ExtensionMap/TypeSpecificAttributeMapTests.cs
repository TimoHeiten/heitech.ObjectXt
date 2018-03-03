using heitech.ObjectExpander.ExtensionMap;
using heitech.ObjectExpander.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new AttrMap();
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
            public bool CanInvoke<TKey>(TKey key, Type expectedReturnType = null, params object[] parameters)
            {
                throw new NotImplementedException();
            }

            public object Invoke(params object[] parameters)
            {
                throw new NotImplementedException();
            }
        }
    }
}
