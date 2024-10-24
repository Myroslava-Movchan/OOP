using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

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
                await transactionLogFileStream.WriteAsync(messageBytes, 0, messageBytes.Length, cancellationToken); // це треба перенести в сервіс
            }
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
