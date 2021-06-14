using System;
using Catalog.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.EntitiesTest
{
    [TestClass]
    [TestCategory("RemovedItem")]
    public class RemoveItemTest
    {
        public RemoveItemTest()
        {
            _item = new CatalogItem(
                name: "test removed Item",
                description: "Testando camanda entity",
                price: 0.01m,
                catalogTypeId: 1,
                catalogBrandId: 1,
                restockThreshold: 10,
                maxStockThreshold: 30
            );
        }
        private CatalogItem _item;

        [TestMethod]
        public void ExceptionForNotHavingProductInStock()
        {
            Assert.ThrowsException<NotImplementedException>(() =>
            {
                _item.RemoveStock(5);
            });
        }

        [TestMethod]
        public void ExceptionForQuantityReportedLessThan0()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                _item.AddStock(25);
                _item.RemoveStock(0);
            });
        }

        [TestMethod]
        public void ExceptionForReportedQuantityGreaterThanStock()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                _item.AddStock(25);
                _item.RemoveStock(26);
            });
        }

        [TestMethod]
        public void InformValidQuantity()
        {
            _item.AddStock(25);
            Assert.AreEqual(10, _item.RemoveStock(10));
        }

        [TestMethod]
        public void ValidatesRightAmountInAttack()
        {
            _item.AddStock(25);
            _item.RemoveStock(24);
            Assert.AreEqual(1, _item.AvailableStock);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _item = null;
        }
    }
}
