using Online_Store_Management.Services;

namespace Unit_Tests
{
    [TestClass]
    public sealed class NewCustomerServiceTest
    {
        private readonly NewCustomerService service;
        public NewCustomerServiceTest()
        {
            service = new NewCustomerService();
        }
        [TestMethod]
        public async Task GetNewCustomerAsyncTest_ShouldReturnNewCustomer()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;

            //Act
            var result = await service.GetNewCustomerAsync(cancellationToken);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
