using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Services;
using Microsoft.AspNetCore.Authorization;

namespace Online_Store_Management.Controllers
{
    [Authorize]
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
