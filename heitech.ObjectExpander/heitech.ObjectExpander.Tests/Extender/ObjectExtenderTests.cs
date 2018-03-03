using heitech.ObjectExpander.Extender;
using heitech.ObjectExpander.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace heitech.ObjectExpander.Tests.Extender
{
    [TestClass]
    public class ObjectExtenderTests
    {
        readonly IMarkedExtendable extender = new MarkedObject();
        private readonly Mock<IAttributeMap> map = new Mock<IAttributeMap>();

        [TestInitialize]
        public void Init()
        {
            var mock = new Mock<IAttributeFactory>();
            mock.Setup(x => x.GetMap()).Returns(map.Object);
            ObjectExtender.SetFactory(mock.Object);
        }

        [TestMethod]
        public void ObjectExtender_RegisterActionDelegatesToAttributeMap()
        {
            bool wasInvoked = false;
            map.Setup(x => x.Add(extender, "key", It.IsAny<IExtensionAttribute>()))
                .Callback(() => wasInvoked = true);

            extender.RegisterAction("key", () => { });

            Assert.IsTrue(wasInvoked);
        }

        [TestMethod]
        public void ObjectExtender_RegisterFuncDelegatesToAttributesMap()
        {
            bool wasInvoked = false;
            map.Setup(x => x.Add(extender, "key", It.IsAny<IExtensionAttribute>()))
                .Callback(() => wasInvoked = true);

            extender.RegisterFunc("key", () => 42);

            Assert.IsTrue(wasInvoked);
        }

        [TestCleanup]
        public void TearDown()
        {
            ObjectExtender.SetFactory(null);
        }

        [TestMethod]
        public void ObjectExtender_AsyncThrowsNotImplementedException()
        {
            Assert.ThrowsException<NotImplementedException>(() => extender.RegisterAsyncAction("string", () => Task.CompletedTask));
            Assert.ThrowsException<NotImplementedException>(() => extender.RegisterFuncAsync<string, int>("string", () => Task.FromResult(0)));
        }
    }
}
