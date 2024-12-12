using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;
using Online_Store_Management.Services;

namespace Unit_Tests
{
    [TestClass]
    public sealed class OrderInfoServiceTest
    {
        private Mock<IRepository<OrderInfo>>? orderInfoRepositoryMock;
        private Mock<IConfiguration> configuration;
        private OrderInfoService? orderInfoService;
        [TestInitialize]
        public void TestInitialize()
        {
            orderInfoRepositoryMock = new Mock<IRepository<OrderInfo>>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["Encryption:PublicKey"]).Returns("your-public-key");
            configurationMock.Setup(c => c["Encryption:PrivateKey"]).Returns("your-private-key");
            orderInfoService = new OrderInfoService(orderInfoRepositoryMock.Object, configurationMock.Object);
        }
        [TestMethod]
        public async Task PostAsyncTest_ShouldReturnOrderInfoObj()
        {
            //Arrange
            var gift = "sticker";
            var product = new Product
            {
                ProductId = 11,
                ProductName = "Hat",
                ProductPrice = 50,
                Category = "accessories",
                Availability = true,
                Rating = 5
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await orderInfoService.PostAsync(product, cancellationToken);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(product, result.Product);
        }

        [TestMethod]
        public async Task CompareOrdersTest_ShouldReturnBoolFalse()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Hat",
                ProductPrice = 50,
                Category = "accessories",
                Availability = true,
                Rating = 5
            };
            var order = new OrderInfo
            {
                OrderNumber = 10,
                Gift = "sticker",
                ProductId = 3,
                Product = product,
                Status = "shipped"
            };
            var cancellationToken = CancellationToken.None;


            //Act
            var result = await orderInfoService.CompareOrdersAsync(order, cancellationToken);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CompareOrdersAsyncTest_ShouldReturnFalseWhenOrderIsNull()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await orderInfoService.CompareOrdersAsync(null, cancellationToken);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AddToTableAsyncTest_ShouldReturnBoolTrue()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Hat",
                ProductPrice = 50,
                Category = "accessories",
                Availability = true,
                Rating = 5
            };
            var order = new OrderInfo
            {
                OrderNumber = 10,
                Gift = "sticker",
                ProductId = 3,
                Product = product,
                Status = "shipped"
            };
            var cancellationToken = CancellationToken.None;

            //Act
            var result = await orderInfoService.AddToTableAsync(order, cancellationToken);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task EstimateDelieveryAsyncTest_ShouldReturnInt()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;

            //Act
            var result = await orderInfoService.EstimateDeliveryAsync(cancellationToken);

            //Assert
            Assert.IsInstanceOfType<int>(result);
        }

        [TestMethod]
        public async Task EstimateDeliveryAsyncTest_ShouldReturnCorrectDeliveryTime()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await orderInfoService.EstimateDeliveryAsync(cancellationToken);

            // Assert
            Assert.AreEqual(120, result); 
        }

        [TestMethod]
        public void GetTotalAsync_ShouldReturnDecimal()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Hat",
                ProductPrice = 50,
                Category = "accessories",
                Availability = true,
                Rating = 5
            };
            var order = new OrderInfo
            {
                OrderNumber = 10,
                Gift = "sticker",
                ProductId = 3,
                Product = product,
                Status = "shipped"
            };

            orderInfoService.CalculateTotal = order =>
            {
                return order.Product?.ProductPrice ?? 0m;
            };

            // Act
            var result = orderInfoService.GetTotal(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(decimal));
            Assert.AreEqual(50m, result);
        }

        [TestMethod]
        public async Task GetOrderByIdTest_ShouldReturnOrder()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Hat",
                ProductPrice = 50,
                Category = "accessories",
                Availability = true,
                Rating = 5
            };
            var expectedOrder = new OrderInfo
            {
                OrderNumber = 10,
                Gift = "sticker",
                ProductId = 3,
                Product = product,
                Status = "shipped"
            };
            var cancellationToken = CancellationToken.None;
            orderInfoRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedOrder);
            var number = 10;

            //Act
            var order = await orderInfoService.GetOrderByIdAsync(number, cancellationToken);

            //Assert
            Assert.IsNotNull(order);
            Assert.AreEqual(expectedOrder.OrderNumber, order.OrderNumber);
        }

        [TestMethod]
        public async Task GetOrderByIdAsyncTest_ShouldReturnNullIfOrderNotFound()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            orderInfoRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((OrderInfo)null); 

            // Act
            var result = await orderInfoService.GetOrderByIdAsync(99, cancellationToken);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddOrderAsyncTest_ShouldInvokeRepository()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Hat",
                ProductPrice = 50,
                Category = "accessories",
                Availability = true,
                Rating = 5
            };
            var orderToAdd = new OrderInfo
            {
                OrderNumber = 10,
                Gift = "sticker",
                ProductId = 3,
                Product = product,
                Status = "shipped"
            };
            var cancellationToken = CancellationToken.None;

            //Act
            await orderInfoService.AddOrderAsync(orderToAdd, cancellationToken);

            //Assert
            orderInfoRepositoryMock.Verify(r => r.AddAsync(orderToAdd, cancellationToken), Times.Once());
        }

        [TestMethod]
        public async Task UpdateAsyncTest_ShouldInvokeRepository()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Hat",
                ProductPrice = 50,
                Category = "accessories",
                Availability = true,
                Rating = 5
            };
            var orderToUpdate = new OrderInfo
            {
                OrderNumber = 10,
                Gift = "sticker",
                ProductId = 3,
                Product = product,
                Status = "shipped"
            };
            var cancellationToken = CancellationToken.None;
            orderInfoRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<OrderInfo>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            await orderInfoService.UpdateAsync(orderToUpdate, cancellationToken);

            //Assert
            orderInfoRepositoryMock.Verify(r => r.UpdateAsync(orderToUpdate, cancellationToken), Times.Once());
        }

        [TestMethod]
        public async Task DeleteAsyncTest_ShouldInvokeRepository()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            orderInfoRepositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            var id = 10;

            //Act
            await orderInfoService.DeleteAsync(id, cancellationToken);

            //Assert
            orderInfoRepositoryMock.Verify(r => r.DeleteAsync(id, cancellationToken), Times.Once());
        }

        [TestMethod]
        public async Task GetAllAsyncTest_ShouldReturnIEnumerable()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 3,
                ProductName = "Hat",
                ProductPrice = 50,
                Category = "accessories",
                Availability = true,
                Rating = 5
            };

            var expectedOrders = new List<OrderInfo>
                {
                    new OrderInfo
                    {
                        OrderNumber = 10,
                        Gift = "sticker",
                        ProductId = 3,
                        Product = product,
                        Status = "shipped"
                    },
                    new OrderInfo
                    {
                        OrderNumber = 11,
                        Gift = "card",
                        ProductId = 4,
                        Product = new Product
                    {
                        ProductId = 4,
                        ProductName = "Shirt",
                        ProductPrice = 100,
                        Category = "clothing",
                        Availability = true,
                        Rating = 4
                    },
                        Status = "pending"
                    }
                };

            var cancellationToken = CancellationToken.None;

            orderInfoRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedOrders);

            // Act
            var result = await orderInfoService.GetAllAsync(cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<OrderInfo>));
            Assert.AreEqual(expectedOrders.Count, result.Count());
            Assert.AreEqual(expectedOrders[0].OrderNumber, result.First().OrderNumber);
            Assert.AreEqual(expectedOrders[1].OrderNumber, result.ElementAt(1).OrderNumber);
        }

    }

}
