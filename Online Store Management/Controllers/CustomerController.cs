using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;
using Online_Store_Management.DataAccess;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CustomerController : ControllerBase
    {
        private readonly ICustomer customerService;
        private readonly INotificationService notificationService;
        private readonly Func<CancellationToken, Task<Discount>> getNewCustomerFunc;
        public CustomerController(ICustomer customerService, INotificationService notificationService)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            this.getNewCustomerFunc = customerService.GetNewCustomerAsync;
        }

        [HttpGet("new")]
        public async Task<Discount> CreateNewCustomerAsync(CancellationToken cancellationToken)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.UtcNow.Ticks}_transcations.log");

            var newCustomer = await getNewCustomerFunc(cancellationToken);
            using (var transactionLogFileStream = new FileStream("transaction.log", FileMode.Append))
            {
                byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes($"{DateTime.UtcNow.Ticks}, {newCustomer}");
                await transactionLogFileStream.WriteAsync(messageBytes, 0, messageBytes.Length, cancellationToken);
            }
            return newCustomer;
        }

        [HttpGet("regular")]
        public async Task<Discount> CreateRegularCustomerAsync(CancellationToken cancellationToken)
        {
            var regularCustomer = customerService.GetRegularCustomerAsync(cancellationToken);
            return await regularCustomer;
        }

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
