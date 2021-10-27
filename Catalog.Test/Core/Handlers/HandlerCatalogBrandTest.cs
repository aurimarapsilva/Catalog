using Catalog.Core.Commands;
using Catalog.Core.Handlers;
using Catalog.Core.Repositories;
using catalog.test.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Handlers
{
    [TestClass]
    public class HandlerCatalogBrandTest
    {
        private CommandAddBrand _commandAdd;
        private CommandUpdateBrand _commandUpdate;
        private HandlerCatalogBrand _handler;
        private ICatalogBrandRepository _repository;

        [TestInitialize]
        public void Initialize() => Constructor();

        [TestCleanup]
        public void Cleanup() => Dispose();

        [TestMethod]
        public void TestCommandAddBrandArgumentsInvalid()
        {
            _commandAdd = new CommandAddBrand("Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres");
            var result = _handler.handle(_commandAdd);
            Assert.IsTrue(result.HasError());
        }

        [TestMethod]
        public void TestCommandAddBrandValid()
        {
            _commandAdd = new CommandAddBrand("Teste com menos de 100 caracteres");
            var result = _handler.handle(_commandAdd);
            Assert.IsFalse(result.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateTypeInvalidArguments()
        {
            _commandUpdate = new CommandUpdateBrand(0, "Teste com menos de 100 caracteres");
            var result = _handler.handle(_commandUpdate);
            Assert.IsTrue(result.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateTypeErrorLocateType()
        {
            _commandUpdate = new CommandUpdateBrand(2, "Teste com menos de 100 caracteres");
            var result = _handler.handle(_commandUpdate);
            Assert.IsTrue(result.HasError());
        }

        [TestMethod]
        public void TestCommandUpdateTypeValid()
        {
            _commandUpdate = new CommandUpdateBrand(1, "Teste com menos de 100 caracteres");
            var result = _handler.handle(_commandUpdate);
            Assert.IsFalse(result.HasError());
        }

        private void Constructor()
        {
            _repository = new CatalogBrandRepository();
            _handler = new HandlerCatalogBrand(_repository);
        }

        private void Dispose()
        {
            _commandAdd = null;
            _commandUpdate = null;
            _handler = null;
            _repository = null;
        }
    }
}
