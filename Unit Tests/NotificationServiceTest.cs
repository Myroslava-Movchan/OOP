using System;
using System.Collections.Generic;
using System.Linq;
using Online_Store_Management.Services;
using Online_Store_Management.DataAccess;

namespace Unit_Tests
{
    [TestClass]
    public sealed class NotificationServiceTest
    {
        public NotificationService notificationService;

        [TestMethod]
        public void NotificationTest_ShouldWriteLine()
        {
            //Arrange
            notificationService = new NotificationService();
            var update = new CustomerDbModel
            {
                Id = 1,
                LastName = "Test",
                PostIndex = 12345
            };
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //Act
            notificationService.Notification(update);

            //Assert
            var output = stringWriter.ToString().Trim();
            Assert.AreEqual(output, "Customer has been updated");
        }
    }
}
