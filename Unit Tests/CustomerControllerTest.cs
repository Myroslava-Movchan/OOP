using Microsoft.AspNetCore.Mvc;
using Moq;
using Online_Store_Management.Controllers;
using Online_Store_Management.DataAccess;
using Online_Store_Management.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests
{
    [TestClass]
    public sealed class CustomerControllerTest
    {
        private  Mock<ICustomer> _customerServiceMock;
        private  Mock<INotificationService> _notificationServiceMock;
        private  CustomerController _controller;
        [TestInitialize]
        public void TestInitialize()
        {
            _customerServiceMock = new Mock<ICustomer>();
            _notificationServiceMock = new Mock<INotificationService>();
            _controller = new CustomerController(_customerServiceMock.Object, _notificationServiceMock.Object);
        }

        [TestMethod]
        public async Task AddNewCustomerAsync_ShouldCallCustomerServiceAddCustomer()
        {
            // Arrange
            var customer = new CustomerDbModel { Id = 1, LastName = "Doe", PostIndex = 12345};
            _customerServiceMock.Setup(service => service.AddCustomerAsync(customer, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.AddNewCustomerAsync(customer, CancellationToken.None);

            // Assert
            _customerServiceMock.Verify(service => service.AddCustomerAsync(customer, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest_ShouldReturnCustomer()
        {
            // Arrange
            int customerId = 1;
            var customer = new CustomerDbModel { Id = customerId, LastName = "Doe", PostIndex = 12345 };
            _customerServiceMock.Setup(service => service.GetCustomerByIdAsync(customerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            // Act
            var result = await _controller.GetByIdAsync(customerId, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType<OkObjectResult>(result.Result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateAsyncTest_ShouldCallCustomerUpdate()
        {
            // Arrange
            var customer = new CustomerDbModel { Id = 1, LastName = "Updated Name" };
            _customerServiceMock.Setup(service => service.UpdateAsync(customer, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _notificationServiceMock.Setup(service => service.Notification(customer));

            // Act
            await _controller.UpdateAsync(customer, CancellationToken.None);

            // Assert
            _customerServiceMock.Verify(service => service.UpdateAsync(customer, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest_ShouldCallCustomerServiceDelete()
        {
            // Arrange
            int customerId = 1;
            _customerServiceMock.Setup(service => service.DeleteAsync(customerId, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _controller.DeleteAsync(customerId, CancellationToken.None);

            // Assert
            _customerServiceMock.Verify(service => service.DeleteAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
