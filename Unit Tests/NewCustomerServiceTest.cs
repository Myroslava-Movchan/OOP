using Online_Store_Management.Services;

namespace Unit_Tests
{
    [TestClass]
    public sealed class NewCustomerServiceTest
    {
        private NewCustomerService service;

        [TestMethod]
        public async Task GetNewCustomerAsyncTest_ShouldReturnNewCustomer()
        {
            //Arrange
            service = new NewCustomerService();
            var cancellationToken = CancellationToken.None;

            //Act
            var result = await service.GetNewCustomerAsync(cancellationToken);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
