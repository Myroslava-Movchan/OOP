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
        public CustomerController(CustomerService customerService)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService);
        }

        [HttpGet("new")]
        public Discount GetNewCustomer()
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.UtcNow.Ticks}_transcations.log");
            using (CustomerService service = new(filepath))
            {
                var newCustomer = customerService.GetNewCustomer();
                service.LogAction($"Create new customer: {newCustomer}");
                return newCustomer;
            }
        }

        [HttpGet("regular")]
        public Discount GetRegularCustomer()
        {
            var regularCustomer = customerService.GetRegularCustomer();
            return regularCustomer;
        }

    }
}
