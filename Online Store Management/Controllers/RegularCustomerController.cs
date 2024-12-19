using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;
using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RegularCustomerController(IRegularCustomer regularCustomerService) : ControllerBase
    {
        private readonly IRegularCustomer regularCustomerService = regularCustomerService ?? throw new ArgumentNullException(nameof(regularCustomerService));

        [HttpGet("create")]
        public async Task<RegularCustomer> CreateRegularCustomerAsync(CancellationToken cancellationToken)
        {
            return await regularCustomerService.GetRegularCustomerAsync(cancellationToken);
        }
    }
}
