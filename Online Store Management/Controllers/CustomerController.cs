using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;
using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CustomerController : ControllerBase
    {
        private readonly CustomerService customerService;
        public CustomerController()
        {
            customerService = new CustomerService();
        }

        [HttpGet("new")]
        public Discount GetNewCustomer()
        {
            var newCustomer = customerService.GetNewCustomer();
            return newCustomer;
        }

        [HttpGet("regular")]
        public Discount GetRegularCustomer()
        {
            var regularCustomer = customerService.GetRegularCustomer();
            return regularCustomer;
        }

    }
}
