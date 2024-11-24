using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewCustomerController : ControllerBase
    {
        private readonly INewCustomer newCustomerService;

        public NewCustomerController(INewCustomer newCustomerService)
        {
            this.newCustomerService = newCustomerService ?? throw new ArgumentNullException(nameof(newCustomerService));
        }

        [HttpGet("create")]
        public async Task<NewCustomer> CreateNewCustomerAsync(CancellationToken cancellationToken)
        {
            return await newCustomerService.GetNewCustomerAsync(cancellationToken);
        }
    }
}
