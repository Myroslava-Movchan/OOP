using Moq;
using Online_Store_Management.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Store_Management.Models;
using Online_Store_Management.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Unit_Tests
{
    [TestClass]
    public sealed class UserServiceTest
    {
        private Mock<IUserRepository> userRepositoryMock;
        private UserService userService;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userService = new UserService(userRepositoryMock.Object);
        }

        [TestMethod]
        public void GetUserByEmailTest_ShouldReturnUser()
        {
            //Arrange
            var email = "user@gmail.com";
            var expectedUser = new User
            {
                Email = email,
                PasswordHash = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f"
            };
            userRepositoryMock
                .Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .Returns(expectedUser);

            //Act
            var result = userService.GetUserByEmail(email);

            //Assert
            Assert.AreEqual(expectedUser, result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserByEmailTest_ShouldReturnNull()
        {
            //Arrange
            var email1 = "user@gmail.com";
            var expectedUser = new User
            {
                Email = "test@gmail.com",
                PasswordHash = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f"
            };
            userRepositoryMock
                .Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .Returns((User)null);

            //Act
            var result = userService.GetUserByEmail(email1);

            //Assert
            Assert.AreNotEqual(expectedUser.Email, email1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void VerifyHashedPasswordTest_ShouldReturnFalse()
        {
            //Arrange
            var plain = "hellouser";
            var hash = "qwertyuiopvjnle46367cbiv";

            //Act
            var result = userService.VerifyHashedPassword(plain, hash);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyHashedPasswordTest_ShouldReturnTrue()
        {
            //Arrange
            var plain = "password123";
            var hash = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f";

            //Act
            var result = userService.VerifyHashedPassword(plain, hash);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
