using System;
using Catalog.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catalog.Test.Entities
{
    [TestClass]
    public class CatalogItemTest
    {
        public CatalogItemTest()
        {
            _item = new CatalogItem("Teste", "Realizando teste na camada de entidade", 0.85m, 1, 1, 5, 10);
        }

        private CatalogItem _item;

        [TestMethod]
        [TestCategory("Entities")]
        public void RemoveStockInvalidForNotHavingAvailableStock()
        {
            var item = new CatalogItem("Teste", "Realizando teste na camada de entidade", 0.85m, 1, 1, 5);

            item.RemoveStock(5);

            Assert.IsTrue(item.Invalid);
        }

        [TestMethod]
        [TestCategory("Entities")]
        public void RemoveStockInvalidForQuantityLessthanOrEqualTo0()
        {

            _item.AddStock(2);

            _item.RemoveStock(0);

            Assert.IsTrue(_item.Invalid);
        }

        [TestMethod]
        [TestCategory("Entites")]
        public void RemoveStockSuccess()
        {
            _item.AddStock(10);
            Assert.AreEqual(5, _item.RemoveStock(5));
        }

        [TestMethod]
        [TestCategory("Entites")]
        public void RemoveStockFailed()
        {
            _item.AddStock(4);
            Assert.AreEqual(4, _item.RemoveStock(5));
        }

        [TestMethod]
        [TestCategory("Entities")]
        public void AddStockTestSuccess()
        {
            Assert.AreEqual(4, _item.AddStock(4));
        }


        [TestMethod]
        [TestCategory("Entities")]
        public void AddStockTestFailed()
        {
            Assert.AreNotEqual(12.5m, _item.AddStock(12.5m));
        }

        [TestCleanup]
        public void CleanUp()
        {
            _item = null;
        }
    }
}