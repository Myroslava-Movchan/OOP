using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Interfaces;
using Online_Store_Management.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace Online_Store_Management.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class CustomerController(ICustomer customerService, INotificationService notificationService) : ControllerBase
    {
        private readonly ICustomer customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        private readonly INotificationService notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));

        [HttpPost("Add new customer")]
        public async Task AddNewCustomerAsync(CustomerDbModel customer, CancellationToken cancellationToken)
        {
            await customerService.AddCustomerAsync(customer, cancellationToken);
        }

        [HttpGet("Get customer by ID")]

        public async Task<ActionResult<CustomerDbModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await customerService.GetCustomerByIdAsync(id, cancellationToken);
            if (result == null || result.Id == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("Update customer")]

        public async Task UpdateAsync(CustomerDbModel customer, CancellationToken cancellationToken)
        {
            customerService.CustomerUpdate += notificationService.Notification;
            await customerService.UpdateAsync(customer, cancellationToken);
            customerService.CustomerUpdate -= notificationService.Notification;
        }

        [HttpDelete("Delete customer")]
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await customerService.DeleteAsync(id, cancellationToken);
        }
    }
}
