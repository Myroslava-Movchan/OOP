using Moq;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;
using Online_Store_Management.Services;

namespace Unit_Tests
{
    [TestClass]
    public sealed class ProductServiceTest
    {
        private Mock<IRepository<Product>>? productRepositoryMock;
        private ProductService? productService;
        [TestInitialize]
        public void TestInitialize()
        {
            productRepositoryMock = new Mock<IRepository<Product>>();
            productService = new ProductService(productRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetProductAsyncTest_ShouldReturnProduct()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;

            //Act
            var result = await productService.GetProductAsync(cancellationToken);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Product));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ProductId >= 1 && result.ProductId <= 17);
            Assert.IsTrue(result.ProductPrice >= 8 && result.ProductPrice <= 230);
            Assert.IsNotNull(result.ProductName);
            Assert.IsTrue(result.Rating >= 0 && result.Rating <= 4);
            Assert.IsNotNull(result.Category);
        }

        [TestMethod]
        public async Task GetProducByIdAsyncTest_ShouldReturnProduct()
        {
            //Arrange
            var expectedProduct = new Product
            {
                ProductId = 1,
                ProductName = "a",
                ProductPrice = 20,
                Availability = false,
                Category = "b",
                Rating = 3
            };
            var cancellationToken = CancellationToken.None;
            var id = 1;
            productRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedProduct);

            //Act
            var result = await productService.GetProductByIdAsync(id, cancellationToken);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedProduct.ProductId, result.ProductId);
        }

        [TestMethod]
        public async Task AddProductAsyncTest_ShouldInvokeRepository()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var productToAdd = new Product
            {
                ProductId = 1,
                ProductName = "a",
                ProductPrice = 20,
                Availability = false,
                Category = "b",
                Rating = 3
            };
            productRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            await productService.AddProductAsync(productToAdd, cancellationToken);

            //Assert
            productRepositoryMock.Verify(r => r.AddAsync(productToAdd, cancellationToken), Times.Once());
        }

        [TestMethod]
        public async Task UpdateAsyncTest_ShouldInvokeRepository()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var productToUpdate = new Product
            {
                ProductId = 1,
                ProductName = "a",
                ProductPrice = 20,
                Availability = false,
                Category = "b",
                Rating = 3
            };
            productRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            await productService.UpdateAsync(productToUpdate, cancellationToken);

            //Assert
            productRepositoryMock.Verify(r => r.UpdateAsync(productToUpdate, cancellationToken), Times.Once());
        }

        [TestMethod]
        public async Task DeleteAsyncTest_ShoyldInvokeRepository()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var id = 1;
            productRepositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            await productService.DeleteAsync(id, cancellationToken);

            //Assert
            productRepositoryMock.Verify(r => r.DeleteAsync(id, cancellationToken), Times.Once());
        }

        [TestMethod]
        public void GetListOfProductsTest_ShouldReturnIEnumerable()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "abcd",
                    ProductPrice = 20,
                    Availability = false,
                    Category = "b",
                    Rating = 3
                },
                new Product
                {
                    ProductId = 6,
                    ProductName = "efgh",
                    ProductPrice = 75,
                    Availability = true,
                    Category = "d",
                    Rating = 4
                }
            };

            //Act
            var result = productService.GetListOfProducts();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
        }

        [TestMethod]
        public async Task SearchProductCategoryTest_ShouldReturnList()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "abcd",
                    ProductPrice = 20,
                    Availability = false,
                    Category = "pants",
                    Rating = 3
                },
                new Product
                {
                    ProductId = 6,
                    ProductName = "efgh",
                    ProductPrice = 75,
                    Availability = true,
                    Category = "pants",
                    Rating = 4
                }
            };
            productRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);
            var category = "pants";

            //Act
            var result = await productService.SearchProductCategory(category, cancellationToken);

            //Assert
            Assert.IsNotInstanceOfType(result, typeof(List<string>));
            Assert.AreEqual(category, result[0].Category, result[1].Category);
        }

        [TestMethod]
        public async Task SearchProductAvailabilityTest_ShouldReturnList()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "abcd",
                    ProductPrice = 20,
                    Availability = false,
                    Category = "pants",
                    Rating = 3
                },
                new Product
                {
                    ProductId = 6,
                    ProductName = "efgh",
                    ProductPrice = 75,
                    Availability = true,
                    Category = "pants",
                    Rating = 4
                }
            };
            productRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            //Act
            var result = await productService.SearchProductAvailability(cancellationToken);

            //Assert
            Assert.IsTrue(result[0].Availability);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SearchProductPriceRangeTest_ShouldReturnList()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "abcd",
                    ProductPrice = 20,
                    Availability = false,
                    Category = "pants",
                    Rating = 3
                },
                new Product
                {
                    ProductId = 6,
                    ProductName = "efgh",
                    ProductPrice = 75,
                    Availability = true,
                    Category = "pants",
                    Rating = 4
                }
            };
            productRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);
            var min = 15;
            var max = 50;

            //Act
            var result = await productService.SearchProductPriceRange(min, max, cancellationToken);

            //Assert
            Assert.IsTrue(result[0].ProductPrice <= 50 && result[0].ProductPrice >= 15);
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public async Task GetBestProductsRatingTest_ShouldReturnList()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "abcd",
                    ProductPrice = 20,
                    Availability = false,
                    Category = "pants",
                    Rating = 5
                },
                new Product
                {
                    ProductId = 6,
                    ProductName = "efgh",
                    ProductPrice = 75,
                    Availability = true,
                    Category = "pants",
                    Rating = 4
                }
            };
            productRepositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            //Act
            var result = await productService.GetBestProductsRating(cancellationToken);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result[0].Rating >= 4 && result[1].Rating >= 4);
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }
    }
}
