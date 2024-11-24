using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;
using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegularCustomerController : ControllerBase
    {
        private readonly IRegularCustomer regularCustomerService;

        public RegularCustomerController(IRegularCustomer regularCustomerService)
        {
            this.regularCustomerService = regularCustomerService ?? throw new ArgumentNullException(nameof(regularCustomerService));
        }

        [HttpGet("create")]
        public async Task<RegularCustomer> CreateRegularCustomerAsync(CancellationToken cancellationToken)
        {
            return await regularCustomerService.GetRegularCustomerAsync(cancellationToken);
        }
    }
}
