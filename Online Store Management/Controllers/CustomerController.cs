using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CustomerController : ControllerBase
    {
        private static readonly string[] LastNamesNew = new[]
        {
            "Snow", "Goth", "White", "Jeffry", "Smith", "Brown"
        };
        private static readonly string[] LastNamesRegular = new[]
        {
            "Garcia", "Lee", "Patel", "Johnson", "Wilson", "Kim"
        };

        [HttpGet ("new")]
        public ActionResult<object> GetNewCustomer() // синтаксис actionresult взято в інтернеті
        {
            var lastName = LastNamesNew[Random.Shared.Next(LastNamesNew.Length)];
            var customer = new NewCustomer()
            {
                LastName = lastName,
                Id = Random.Shared.Next(1, 6)
            };
            var productController = new ProductController();
            var product = productController.GetProduct();
            customer.SetProduct(product);
            var discountedPrice = customer.GetDiscount();

            return new
            {
                Customer = customer,
                DiscountedPrice = discountedPrice
            };
        }

        [HttpGet("regular")]
        public ActionResult<object> GetRegularCustomer() // синтаксис actionresult взято в інтернеті
        {
            var lastName = LastNamesRegular[Random.Shared.Next(LastNamesRegular.Length)];
            var customer = new RegularCustomer()
            {
                LastName = lastName,
                Id = Random.Shared.Next(7, 12)
            };
            var productController = new ProductController();
            var product = productController.GetProduct();
            customer.SetProduct(product);
            var discountedPrice = customer.GetDiscount();

            return new
            {
                Customer = customer,
                DiscountedPrice = discountedPrice
            };

        }

    }
}
