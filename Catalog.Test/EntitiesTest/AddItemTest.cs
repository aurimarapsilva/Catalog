using Catalog.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace catalog.test.EntitiesTest
{
    [TestClass]
    [TestCategory("AddItem")]
    public class AddItemTest
    {
        public AddItemTest()
        {
            _item = new CatalogItem(
                name: "test Add Item",
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
        public void ExceptionQuantityGreaterThanTheStock()
        {
            Assert.ThrowsException<NotImplementedException>(() =>
            {
                _item.AddStock(35);
            });
        }

        [TestMethod]
        public void ExceptionAmountLessdThan0()
        {
            Assert.ThrowsException<NotImplementedException>(() =>
            {
                _item.AddStock(-2);
            });
        }

        [TestMethod]
        public void InformValidQuantity()
        {
            Assert.AreEqual(30, _item.AddStock(30));
        }

        [TestMethod]
        public void ValidatesRightAmountInAttack()
        {
            _item.AddStock(25);
            Assert.AreEqual(25, _item.AvailableStock);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _item = null;
        }
    }
}
