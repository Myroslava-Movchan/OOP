using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Store_Management.Services;

namespace Unit_Tests
{
    [TestClass]
    public sealed class RegularCustomerServiceTest
    {
        private RegularCustomerService service;
        [TestInitialize]
        public void TestInitialize()
        {
            service = new RegularCustomerService();
        }

        [TestMethod]
        public async Task GetRegularCustomerAsyncTest_ShouldReturnRegularCustomer()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;

            //Act
            var result = service.GetRegularCustomerAsync(cancellationToken);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
