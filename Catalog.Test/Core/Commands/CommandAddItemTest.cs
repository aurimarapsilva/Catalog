using Catalog.Core.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Commands
{
    [TestClass]
    public class CommandAddItemTest
    {
        private CommandAddItem _command;

        [TestInitialize]
        public void Initialize() =>   _command = new CommandAddItem();

        [TestCleanup]
        public void CleanUp() => _command = null;

        [TestMethod]
        public void CommandNullTest()
        {
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandIdInvalid()
        {
            InsertValue(2,0);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandQuantityInvalid()
        {
            InsertValue(2,0);
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void CommandValid()
        {
            InsertValue(2,2);
            _command.Validate();
            Assert.IsTrue(_command.Valid);
        }

        private void InsertValue(int id, decimal quantity)
        {
            _command.Id = id;
            _command.Quantity = quantity;
        }
    }
}
