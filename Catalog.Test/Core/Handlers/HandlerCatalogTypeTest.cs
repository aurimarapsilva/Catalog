using Catalog.Core.Commands;
using Catalog.Core.Commands.Contracts;
using Catalog.Core.Handlers;
using Catalog.Core.Repositories;
using catalog.test.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Handlers
{
    [TestClass]
    public class HandlerCatalogTypeTest
    {
        private CommandAddType _commandAdd;
        private CommandUpdateType _commandUpdate;
        private ICatalogTypeRepository _repository;
        private HandlerCatalogType _handler;

        [TestInitialize]
        public void Initialize() => Constructor();

        [TestCleanup]
        public void Cleanup() => Dispose();

        [TestMethod]
        public void TestCommandAddTypeInvalidArguments()
        {
            _commandAdd = new CommandAddType("Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres");
            var result = _handler.handle(_commandAdd);
            Assert.IsTrue(result.HasError());
        }

        [TestMethod]
        public void TestCommandAddTypeValid()
        {
            _commandAdd = new CommandAddType("Teste com menos de 100 caracteres");
            var result = _handler.handle(_commandAdd);
            Assert.IsFalse(result.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateTypeInvalidArguments()
        {
            _commandUpdate = new CommandUpdateType(0, "Teste com menos de 100 caracteres");
            var result = _handler.handle(_commandUpdate);
            Assert.IsTrue(result.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateTypeErrorLocateType()
        {
            _commandUpdate = new CommandUpdateType(2, "Teste com menos de 100 caracteres");
            var result = _handler.handle(_commandUpdate);
            Assert.IsTrue(result.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateTypeValid()
        {
            _commandUpdate = new CommandUpdateType(1, "Teste com menos de 100 caracteres");
            var result = _handler.handle(_commandUpdate);
            Assert.IsFalse(result.HasError());
        }
        private void Constructor()
        {
            _repository = new CatalogTypeRepository();
            _handler = new HandlerCatalogType(_repository);
        }

        private void Dispose()
        {
            _commandAdd = null;
            _commandUpdate = null;
            _repository = null;
            _handler = null;
        }
    }
}
