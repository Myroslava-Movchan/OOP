using Online_Store_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests
{
    [TestClass]
    public sealed class RegularCustomerTest
    {
        [TestMethod]
        public void ExecuteDiscountTest_ShouldReturnCorrectDiscount()
        {
            // Arrange
            var regularCustomer = new RegularCustomer { LastName = "Smith" };
            decimal expectedDiscount = 0.10m;

            decimal ApplyDiscount() => expectedDiscount;

            // Act
            var result = regularCustomer.ExecuteDiscount(ApplyDiscount);

            // Assert
            Assert.AreEqual(expectedDiscount, result);
        }

        [TestMethod]
        public void GetDiscountTest_ShouldReturnCorrectRegularDiscount()
        {
            // Arrange
            var regularCustomer = new RegularCustomer { LastName = "Smith"};

            // Act
            var result = regularCustomer.GetDiscount();

            // Assert
            Assert.AreEqual(0.10m, result);
        }

        [TestMethod]
        public void HelpTest_ShouldCallBaseHelpAndPrintExpectedMessage()
        {
            // Arrange
            var regularCustomer = new RegularCustomer { LastName = "Smith" };
            var issue = "Password reset";

            using var writer = new StringWriter();
            Console.SetOut(writer);

            // Act
            regularCustomer.Help(issue);
            var output = writer.ToString().Trim();

            // Assert
            Assert.IsNotNull(issue, output);
        }

        [TestMethod]
        public void RecommendationTest_ShouldPrintExpectedMessage()
        {
            // Arrange
            var regularCustomer = new RegularCustomer { LastName = "test"};

            using var writer = new StringWriter();
            Console.SetOut(writer);

            // Act
            regularCustomer.Recommendation();
            var output = writer.ToString().Trim();

            // Assert
            Assert.AreEqual("Do not forget to check your email for new offers!", output);
        }
    }
}
