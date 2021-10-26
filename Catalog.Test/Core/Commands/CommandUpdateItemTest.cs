using Catalog.Core.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Commands
{
    [TestClass]
    public class CommandUpdateItemTest
    {
        private CommandUpdateItem _command;

        [TestInitialize]
        public void Initialize() => _command = new CommandUpdateItem();

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
            InsertValue(1,null, "Teste descrição", 2, 1, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandPriceInvalidTest()
        {
            InsertValue(1,"Teste nome", "Teste descrição", 0, 1, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandCatalogTypeIdInvalidTest()
        {
            InsertValue(1,"Teste nome", "Teste descrição", 2, 0, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandCatalogBrandIdInvalidTest()
        {
            InsertValue(1,"Teste nome", "Teste descrição", 2, 1, 0, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandRestockThresholdInvalidTest()
        {
            InsertValue(1,"Teste nome", "Teste descrição", 2, 1, 1, -1, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandMaxStockThresholdInvalidTest()
        {
            InsertValue(1,"Teste nome", "Teste descrição", 2, 1, 1, 20, -1);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandValidTest()
        {
            InsertValue(1, "Teste nome", "Teste descrição", 2, 1, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Valid);
        }

        [TestMethod]
        public void CommandIdInvalidTest()
        {
            InsertValue(0, "Teste nome", "Teste descrição", 2, 1, 1, 20, 20);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        private void InsertValue(int id, string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold)
        {
            _command.Id = id;
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
