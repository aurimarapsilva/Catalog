using Catalog.Core.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Commands
{
    [TestClass]
    public class CommandAddBrandTest
    {
        private CommandAddBrand _command;

        [TestInitialize]
        public void Initialize() =>  _command = new CommandAddBrand();

        [TestCleanup]
        public void CleanUp() => _command = null;
        
        [TestMethod]
        public void BrandNullTest()
        {
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }

        [TestMethod]
        public void BrandWith100CharacterTest()
        {
            _command.Brand = "Teste com menos de 100 caracteres";
            _command.Validate();
            Assert.IsTrue(_command.Valid);
        }

        [TestMethod]
        public void BrandWithMoreThan100CharacterTest()
        {
            _command.Brand = "Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres";
            _command.Validate();
            Assert.IsTrue(_command.Invalid);
        }
    }
}
