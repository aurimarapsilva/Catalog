using Catalog.Core.Commands;
using Catalog.Core.Handlers;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using catalog.test.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Handlers
{
    [TestClass]
    public class HandlerCatalogItemTest
    {
        private HandlerCatalogItem _handler;
        private ICatalogItemRepository _repository;
        private IResponse _response;

        [TestInitialize]
        public void Initialize() => Constructor();

        [TestCleanup]
        public void Cleanup() => Dispose();

        [TestMethod]
        public void TestCommandAddItemArgumentsInvalid()
        {
            _response = ResultHandlerAdd(0, 5);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandAddErrorLocateItem()
        {
            _response = ResultHandlerAdd(2, 5);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandAddErrorAddStock()
        {
            _response = ResultHandlerAdd(1, 31);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandAddValid()
        {
            _response = ResultHandlerAdd(1, 30);
            Assert.IsFalse(_response.HasError());
        }

        [TestMethod]
        public void TestCommandRemoveItemArgumentsInvalid()
        {
            _response = ResultHandlerRemove(0, 5);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandRemoveItemErrorLocateItem()
        {
            _response = ResultHandlerRemove(2, 5);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandRemoveItemErrorRemoveItem()
        {
            _response = ResultHandlerRemove(1, 5);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandRemoveItemValid()
        {
            _response = ResultHandlerRemove(5, 5);
            Assert.IsFalse(_response.HasError());
        }

        [TestMethod]
        public void TestCommandRegisterItemArgumentsInvalid()
        {
            _response = ResultHandlerRegister("Teste", "descrição Teste", -1, 1, 1, 15, 30);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandRegisterItemValid()
        {
            _response = ResultHandlerRegister("Teste", "descrição Teste", 1, 1, 1, 15, 30);
            Assert.IsFalse(_response.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateItemInvalid()
        {
            _response = ResultHandlerUpdate(1,"Teste", "descrição Teste", -1, 1, 1, 15, 30);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateItemErrorLocateItem()
        {
            _response = ResultHandlerUpdate(2, "Teste", "descrição Teste", 1, 1, 1, 15, 30);
            Assert.IsTrue(_response.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateItemValid()
        {
            _response = ResultHandlerUpdate(1, "Testes", "descrição Teste", 1, 1, 1, 15, 30);
            Assert.IsFalse(_response.HasError());
        }

        #region Métodos base para teste
        private void Constructor()
        {
            _repository = new CatalogItemRepository();
            _handler = new HandlerCatalogItem(_repository);
        }

        private void Dispose()
        {
            _handler = null;
            _repository = null;
            _response = null;
        }


        private CommandAddItem ResultCommandAdd(int id, decimal quantity) => new CommandAddItem(id, quantity);

        private CommandRemoveItem ResultCommandRemove(int id, decimal quantity) => new CommandRemoveItem(id, quantity);

        private CommandRegisterItem ResultCommandRegister
            (string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold) =>
                new CommandRegisterItem(name, description, price, catalogTypeId, catalogBrandId, restockThreshold, maxStockThreshold);

        private CommandUpdateItem ResultCommandUpdate
            (int id, string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold) =>
                new CommandUpdateItem(id, name, description, price, catalogTypeId, catalogBrandId, restockThreshold, maxStockThreshold);

        private IResponse ResultHandlerAdd(int id, decimal quantity) => _handler.handle(ResultCommandAdd(id, quantity));

        private IResponse ResultHandlerRemove(int id, decimal quantity) => _handler.handle(ResultCommandRemove(id, quantity));

        private IResponse ResultHandlerRegister
            (string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold) =>
                _handler.handle(ResultCommandRegister(name, description, price, catalogTypeId, catalogBrandId, restockThreshold, maxStockThreshold));

        private IResponse ResultHandlerUpdate
            (int id, string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold) =>
                _handler.handle(ResultCommandUpdate(id, name, description, price, catalogTypeId, catalogBrandId, restockThreshold, maxStockThreshold));

        #endregion

    }
}
