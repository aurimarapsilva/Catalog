using Catalog.Core.Response;
using catalog.infra.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Worker
{
    [TestClass]
    public class GatewayCatalogTest
    {
        private IServiceWorker _service;
        private RabbitMQPersistentConnection _rabbit;

        [TestInitialize]
        public void Initialize() => Constructor();

        [TestCleanup]
        public void Cleanup() => Dispose();

        [TestMethod]
        public void TestAddBrandValid() => Assert.IsFalse(Response("catalog1").HasError());

        [TestMethod]
        public void TestAddItemValid() => Assert.IsFalse(Response("catalog2").HasError());

        [TestMethod]
        public void TestAddTypeValid() => Assert.IsFalse(Response("catalog3").HasError());

        [TestMethod]
        public void TestRegisterItemValid() => Assert.IsFalse(Response("catalog4").HasError());

        [TestMethod]
        public void TestRemoveItemValid() => Assert.IsFalse(Response("catalog5").HasError());

        [TestMethod]
        public void TestUpdateBrandValid() => Assert.IsFalse(Response("catalog6").HasError());

        [TestMethod]
        public void TestUpdateItemValid() => Assert.IsFalse(Response("catalog7").HasError());

        [TestMethod]
        public void TestUpdateTypeValid() => Assert.IsFalse(Response("catalog8").HasError());

        [TestMethod]
        public void TestAddBrandInvalid() => Assert.IsTrue(Response("catalog9").HasError());

        [TestMethod]
        public void TestAddItemInvalid() => Assert.IsTrue(Response("catalog10").HasError());

        [TestMethod]
        public void TestAddTypeInvalid() => Assert.IsTrue(Response("catalog11").HasError());

        [TestMethod]
        public void TestRegisterItemInvalid() => Assert.IsTrue(Response("catalog12").HasError());

        [TestMethod]
        public void TestRemoveItemInvalid() => Assert.IsTrue(Response("catalog13").HasError());

        [TestMethod]
        public void TestUpdateBrandInvalid() => Assert.IsTrue(Response("catalog14").HasError());

        [TestMethod]
        public void TestUpdateItemInvalid() => Assert.IsTrue(Response("catalog15").HasError());

        [TestMethod]
        public void TestUpdateTypeInvalid() => Assert.IsTrue(Response("catalog16").HasError());

        private string Message(string queue) => _rabbit.Receivement(queue, false, false, false);

        private IResponse Response(string queue) => _service.BeginTransaction(Message(queue));

        private void Constructor()
        {
            _service = new ServiceWorker();
            _rabbit = new RabbitMQPersistentConnection();
        }

        private void Dispose()
        {
            _service = null;
            _rabbit = null;
        }
    }
}
