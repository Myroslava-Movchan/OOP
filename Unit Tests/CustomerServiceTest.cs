using Online_Store_Management.Services;
using Moq;
using Online_Store_Management.DataAccess;
using Online_Store_Management.Interfaces;
using static Online_Store_Management.Services.CustomerService;

namespace Unit_Tests
{

    [TestClass]
    public sealed class CustomerServiceTest
    {
        private readonly Mock<IRepository<CustomerDbModel>> customerRepositoryMock;
        private readonly CustomerService service;
        public CustomerServiceTest()
        {
            customerRepositoryMock = new Mock<IRepository<CustomerDbModel>>();
            service = new CustomerService(customerRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetCustomerByIdAsyncTest_ShouldReturnCustomer_CustomerNotNull()
        {
            // Arrange
            var expectedCustomer = new CustomerDbModel
            {
                Id = 1,
                LastName = "Goth",
                PostIndex = 11111
            };

            customerRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCustomer);

            var id = 1;
            var cancellationToken = CancellationToken.None;

            // Act
            var customer = await service.GetCustomerByIdAsync(id, cancellationToken);

            // Assert
            Assert.IsNotNull(customer);
            Assert.AreEqual(expectedCustomer.Id, customer?.Id);
            Assert.AreEqual(expectedCustomer.LastName, customer?.LastName);
        }

        [TestMethod]
        public async Task AddCustomerAsyncTest_ShouldInvokeRepository()
        {
            //Arrange
            var customerToAdd = new CustomerDbModel
            {
                Id = 5,
                LastName = "White",
                PostIndex = 12345
            };
            var cancellationToken = CancellationToken.None;

            //Act
            await service.AddCustomerAsync(customerToAdd, cancellationToken);

            //Assert
            customerRepositoryMock.Verify(r => r.AddAsync(customerToAdd, cancellationToken), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsyncTest_ShouldInvokeRepositoryAndDelegate()
        {
            //Arrange
            var customerToUpdate = new CustomerDbModel
            {
                Id = 5,
                LastName = "White",
                PostIndex = 12345
            };
            customerRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<CustomerDbModel>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            bool isCustomerUpdateEventCalled = false;
            void customerUpdateAction(CustomerDbModel _)
            {
                isCustomerUpdateEventCalled = true;
            }

            service.CustomerUpdate += customerUpdateAction;
            var cancellationToken = CancellationToken.None;

            //Act
            await service.UpdateAsync(customerToUpdate, cancellationToken);

            //Assert
            customerRepositoryMock.Verify(r => r.UpdateAsync(customerToUpdate, cancellationToken), Times.Once);
            Assert.IsTrue(isCustomerUpdateEventCalled, "CustomerUpdate event was not invoked.");
        }

        [TestMethod]
        public async Task DeleteAsyncTest_ShouldInvokeRepository()
        {
            //Arrange
            var id = 1;
            var cancellationToken = CancellationToken.None;
            customerRepositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            await service.DeleteAsync(id, cancellationToken);

            //Assert
            customerRepositoryMock.Verify(r => r.DeleteAsync(id, cancellationToken), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest_ShouldThrowExceptionInvalidId()
        {
            //Arrange
            var id = -2;
            var cancellationToken = CancellationToken.None;
            customerRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<CustomerDbModel>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            try
            {
                await service.DeleteAsync(id, cancellationToken);
                Assert.Fail("Expected exception not thrown.");
            }
            catch (ArgumentException ex)
            {
                // Assert
                Assert.AreEqual("Invalid ID provided.", ex.Message);
            }
        }

        [TestMethod]
        public void GetLastNamesStartingWithSTest_ShouldReturnNamesStartingWithS()
        {
            // Arrange
            var expectedLastNames = new[] { "Snow", "Smith" };

            // Act
            var lastNames = service.GetLastNamesStartingWithS();

            // Assert
            CollectionAssert.AreEqual(expectedLastNames, lastNames.ToList());
        }

        [TestMethod]
        public void GetIndexesDescTest_ShouldReturnIndexesInDescendingOrder()
        {
            // Arrange
            var expectedIndexes = new[] { 89088, 65678, 54321, 22567, 12345, 3115 };

            // Act
            var indexes = service.GetIndexesDesc();

            // Assert
            CollectionAssert.AreEqual(expectedIndexes, indexes.ToList());
        }

        [TestMethod]
        public async Task AddCustomerAsyncTest_ShouldNotAddCustomerIfInvalidData()
        {
            // Arrange
            var invalidCustomer = new CustomerDbModel
            {
                Id = 0,
                LastName = "",
                PostIndex = 0
            };
            var cancellationToken = CancellationToken.None;

            // Act
            customerRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<CustomerDbModel>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ArgumentException("Invalid customer data."));

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                service.AddCustomerAsync(invalidCustomer, cancellationToken)
            );
            Assert.AreEqual("Invalid customer data.", exception.Message);
        }


        [TestMethod]
        public async Task UpdateAsyncTest_ShouldNotUpdateIfCustomerNotFound()
        {
            // Arrange
            var nonExistingCustomer = new CustomerDbModel
            {
                Id = 9999,
                LastName = "NonExistent",
                PostIndex = 99999
            };
            var cancellationToken = CancellationToken.None;

            // Act
            customerRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<CustomerDbModel>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Customer not found"));

            // Assert
            var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                service.UpdateAsync(nonExistingCustomer, cancellationToken)
            );
            Assert.AreEqual("Customer not found", exception.Message);
        }

        [TestMethod]
        public async Task DeleteAsyncTest_ShouldNotDeleteIfCustomerNotFound()
        {
            // Arrange
            var nonExistingCustomerId = 9999;
            var cancellationToken = CancellationToken.None;

            // Act
            customerRepositoryMock
            .Setup(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Customer not found"));

            //Assert
            var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                service.DeleteAsync(nonExistingCustomerId, cancellationToken)
            );
            Assert.AreEqual("Customer not found", exception.Message);
        }
    }
}
