using Online_Store_Management.Controllers;
using Online_Store_Management.Models;
namespace Online_Store_Management.Services
{
    public class CustomerService : IDisposable
    {
        private static readonly string[] LastNamesNew = new[]
        {
            "Snow", "Goth", "White", "Jeffry", "Smith", "Brown"
        };
        private static readonly string[] LastNamesRegular = new[]
        {
            "Garcia", "Lee", "Patel", "Johnson", "Wilson", "Kim"
        };

        private readonly FileStream _transactionLogFileStream;
        private bool _disposed = false;

        public CustomerService()
        {
        }
        public CustomerService(string filepath)
        {
            _transactionLogFileStream = new FileStream(filepath, FileMode.OpenOrCreate);
        }

        public void LogAction(string message)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(CustomerService));
            }
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes($"{DateTime.UtcNow}: {message}\n");
            _transactionLogFileStream.Write(messageBytes, 0, messageBytes.Length);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing) 
            {
                _transactionLogFileStream.Close();
                _transactionLogFileStream.Dispose();
            }
            _disposed = true;
        }

        public Discount GetNewCustomer()
        {
            var lastName = LastNamesNew[Random.Shared.Next(LastNamesNew.Length)];
            var customer = new NewCustomer()
            {
                LastName = lastName,
                Id = Random.Shared.Next(1, 6)
            };
            var productController = new ProductController();
            var product = productController.GetProduct();
            customer.SetProduct(product);
            var discountedPrice = customer.GetDiscount();

            return new Discount
            {
                Customer = customer,
                DiscountedPrice = discountedPrice
            };
        }

        public Discount GetRegularCustomer()
        {
            var lastName = LastNamesRegular[Random.Shared.Next(LastNamesRegular.Length)];
            var customer = new RegularCustomer()
            {
                LastName = lastName,
                Id = Random.Shared.Next(7, 12)
            };
            var productController = new ProductController();
            var product = productController.GetProduct();
            customer.SetProduct(product);
            var discountedPrice = customer.GetDiscount();

            return new Discount
            {
                Customer = customer,
                DiscountedPrice = discountedPrice
            };

        }

        ~CustomerService()
        {
            Dispose(false);
        }
    }
}
