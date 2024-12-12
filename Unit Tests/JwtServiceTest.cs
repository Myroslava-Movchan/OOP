using Microsoft.Extensions.Options;
using Moq;
using Online_Store_Management.Services.Auth;
using Online_Store_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Unit_Tests
{
    [TestClass]
    public sealed class JwtServiceTest
    {
        private Mock<IOptions<JwtOptions>> _jwtOptionsMock;
        private JwtService _jwtService;
        [TestInitialize]
        public void TestInitialize()
        {
            _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();
            _jwtOptionsMock.SetupGet(o => o.Value).Returns(new JwtOptions
            {
                Key = "ThisIsASecretKeyForTestingPurpose!",
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                ExpiresInMinutes = 30
            });

            _jwtService = new JwtService(_jwtOptionsMock.Object);
        }

        [TestMethod]
        public void GenerateToken_ShouldGenerateValidToken()
        {
            // Arrange
            var username = "testuser";

            // Act
            var token = _jwtService.GenerateToken(username);

            // Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            Assert.IsNotNull(jwtToken);
            Assert.AreEqual(_jwtOptionsMock.Object.Value.Issuer, jwtToken.Issuer);
            Assert.IsTrue(jwtToken.ValidTo > DateTime.UtcNow);
        }

        [TestMethod]
        public void ValidateToken_ShouldThrowExceptionForInvalidToken()
        {
            // Arrange
            var invalidToken = "invalid.token.string";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _jwtService.ValidateToken(invalidToken));
        }
    }
}
