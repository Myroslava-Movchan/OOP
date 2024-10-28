using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CustomerController : ControllerBase
    {
        private readonly ICustomer customerService;
        public CustomerController(ICustomer customerService)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        

        [HttpGet("new")]
        public async Task<Discount>  GetNewCustomer(CancellationToken cancellationToken)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.UtcNow.Ticks}_transcations.log");
            
            var newCustomer = customerService.GetNewCustomer();
            using (var transactionLogFileStream = new FileStream("transaction.log", FileMode.Append))
            {
                byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes($"{DateTime.UtcNow.Ticks}, {newCustomer}");
                await transactionLogFileStream.WriteAsync(messageBytes, 0, messageBytes.Length, cancellationToken);
            }
            return newCustomer;
        }

        [HttpGet("regular")]
        public Discount GetRegularCustomer()
        {
            var regularCustomer = customerService.GetRegularCustomer();
            return regularCustomer;
        }

        [HttpGet]
        public async Task<Customer?> GetAsync(int id, CancellationToken cancellationToken)
        {
            var result =  await customerService.GetCustomerAsync(id, cancellationToken);
            if(result == null || result.Id == 0)
            {
                return null;
            }
            return result;
        }

    }
}
