using Catalog.Core.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Commands
{
    [TestClass]
    public class CommandUpdateTypeTest
    {
        private CommandUpdateType _command;

        [TestInitialize]
        public void Initialize() => _command = new CommandUpdateType();

        [TestCleanup]
        public void CleanUp() => _command = null;

        [TestMethod]
        public void BrandNullTest()
        {
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void BrandValidTest()
        {
            InsertValue(2, "Teste com menos de 100 caracteres");
            _command.Validate();
            Assert.IsTrue(_command.Valid);
        }

        [TestMethod]
        public void BrandIdInvalidTest()
        {
            InsertValue(0, "Teste com menos de 100 caracteres");
            _command.Validate();
            Assert.IsTrue(_command.Valid);
        }

        [TestMethod]
        public void BrandBrandInvalidTest()
        {
            InsertValue(2, "Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres");
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        private void InsertValue(int id, string type)
        {
            _command.Id = id;
            _command.Type = type;
        }
    }
}
