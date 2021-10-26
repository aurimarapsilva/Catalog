using Catalog.Core.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Commands
{
    [TestClass]
    public class CommandRegisterItemTest
    {
        private CommandRegisterItem _command;

        [TestInitialize]
        public void Initialize() => _command = new CommandRegisterItem();

        [TestCleanup]
        public void CleanUp() => _command = null;

        [TestMethod]
        public void CommandNullTest()
        {
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandNameInvalidTest()
        {
            InsertValue(null, "Teste descrição", 2, 1, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }
        
        [TestMethod]
        public void CommandPriceInvalidTest()
        {
            InsertValue("Teste nome", "Teste descrição", 0, 1, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandCatalogTypeIdInvalidTest()
        {
            InsertValue("Teste nome", "Teste descrição", 2, 0, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandCatalogBrandIdInvalidTest()
        {
            InsertValue("Teste nome", "Teste descrição", 2, 1, 0, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandRestockThresholdInvalidTest()
        {
            InsertValue("Teste nome", "Teste descrição", 2, 1, 1, -1, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandMaxStockThresholdInvalidTest()
        {
            InsertValue("Teste nome", "Teste descrição", 2, 1, 1, 20, -1);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandValidTest()
        {
            InsertValue("Teste nome", "Teste descrição", 2, 1, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Valid);
        }

        private void InsertValue(string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold)
        {
            _command.Name = name;
            _command.Description = description;
            _command.Price = price;
            _command.CatalogTypeId = catalogTypeId;
            _command.CatalogBrandId = catalogBrandId;
            _command.RestockThreshold = restockThreshold;
            _command.MaxStockThreshold = maxStockThreshold;
        }
    }
}
