using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Store_Management.Services;
using Online_Store_Management.Models;
using static Online_Store_Management.Models.ProductTypeEnum;

namespace Unit_Tests
{
    [TestClass]
    public sealed class ProductPriceServiceTest
    {
        private ProductPriceService _service;
        [TestInitialize]
        public void TestInitialize()
        {
            _service = new ProductPriceService();
        }

        [TestMethod]
        public void CategorizeGiftsNumberTest_ShouldReturnProductPriceTypeLow()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                ProductPrice = 10
            };
            var order = new OrderInfo
            {
                Product = product,
                OrderNumber = 1,
                Gift = "test",
                ProductId = 1,
                Status = "test",
                OrderDate = DateTime.Now,
            };

            //Act
            var result = _service.CategorizeGiftsNumber(order, product);

            //Assert
            Assert.AreEqual(ProductPriceType.Low, result);
        }

        [TestMethod]
        public void CategorizeGiftsNumberTest_ShouldReturnProductPriceTypeMedium()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 2,
                ProductName = "Test",
                ProductPrice = 80
            };
            var order = new OrderInfo
            {
                Product = product,
                OrderNumber = 2,
                Gift = "test",
                ProductId = 1,
                Status = "test",
                OrderDate = DateTime.Now,
            };

            //Act
            var result = _service.CategorizeGiftsNumber(order, product);

            //Assert
            Assert.AreEqual(ProductPriceType.Medium, result);

        }
        [TestMethod]
        public void CategorizeGiftsNumberTest_ShouldReturnProductPriceTypeHigh()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Test",
                ProductPrice = 200
            };
            var order = new OrderInfo
            {
                Product = product,
                OrderNumber = 3,
                Gift = "test",
                ProductId = 1,
                Status = "test",
                OrderDate = DateTime.Now,
            };

            //Act
            var result = _service.CategorizeGiftsNumber(order, product);

            //Assert
            Assert.AreEqual(ProductPriceType.High, result);
        }
        [TestMethod]
        public void CategorizeGiftsNumberTest_ShouldReturnProductPriceTypeInvalidPrice()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Test",
                ProductPrice = -2
            };
            var order = new OrderInfo
            {
                Product = product,
                OrderNumber = 3,
                Gift = "test",
                ProductId = 1,
                Status = "test",
                OrderDate = DateTime.Now,
            };

            //Act
            var result = _service.CategorizeGiftsNumber(order, product);

            //Assert
            Assert.AreEqual(ProductPriceType.InvalidPrice, result);
        }
    }
}
