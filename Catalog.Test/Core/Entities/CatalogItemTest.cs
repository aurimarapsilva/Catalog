using System;
using Catalog.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace catalog.test.Core.Entities
{
    [TestClass]
    public class CatalogItemTest
    {
        private CatalogItem _addItem;
        private CatalogItem _removeItem;

        [TestInitialize]
        public void Initialize()
        {
            _addItem = new CatalogItem(
                name: "test Add Item",
                description: "Testando camanda entity",
                price: 0.01m,
                catalogTypeId: 1,
                catalogBrandId: 1,
                restockThreshold: 10,
                maxStockThreshold: 30
            );
            _removeItem = new CatalogItem(
                name: "test removed Item",
                description: "Testando camanda entity",
                price: 0.01m,
                catalogTypeId: 1,
                catalogBrandId: 1,
                restockThreshold: 10,
                maxStockThreshold: 30
            );
        }


        [TestMethod]
        public void ExceptionQuantityGreaterThanTheStock()
        {
            Assert.ThrowsException<NotImplementedException>(() =>
            {
                _addItem.AddStock(35);
            });
        }

        [TestMethod]
        public void ExceptionAmountLessdThan0()
        {
            Assert.ThrowsException<NotImplementedException>(() =>
            {
                _addItem.AddStock(-2);
            });
        }

        [TestMethod]
        public void InformValidQuantity()
        {
            Assert.AreEqual(30, _addItem.AddStock(30));
        }

        [TestMethod]
        public void ValidatesRightAmountInAttack()
        {
            _addItem.AddStock(25);
            Assert.AreEqual(25, _addItem.AvailableStock);
        }


        [TestMethod]
        public void ExceptionForNotHavingProductInStock()
        {
            Assert.ThrowsException<NotImplementedException>(() =>
            {
                _removeItem.RemoveStock(5);
            });
        }

        [TestMethod]
        public void ExceptionForQuantityReportedLessThan0()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                _removeItem.AddStock(25);
                _removeItem.RemoveStock(0);
            });
        }

        [TestMethod]
        public void ExceptionForReportedQuantityGreaterThanStock()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                _removeItem.AddStock(25);
                _removeItem.RemoveStock(26);
            });
        }

        [TestMethod]
        public void InformValidQuantityRemove()
        {
            _removeItem.AddStock(25);
            Assert.AreEqual(10, _removeItem.RemoveStock(10));
        }

        [TestMethod]
        public void ValidatesRightAmountInAttackRemove()
        {
            _removeItem.AddStock(25);
            _removeItem.RemoveStock(24);
            Assert.AreEqual(1, _removeItem.AvailableStock);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _addItem = null;
            _removeItem = null;
        }
    }
}
