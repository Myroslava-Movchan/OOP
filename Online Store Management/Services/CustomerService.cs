using Online_Store_Management.Models;
using Online_Store_Management.Interfaces;
using Online_Store_Management.DataAccess;
namespace Online_Store_Management.Services
{
    public class CustomerService : ICustomer
    {
        private readonly IRepository<CustomerDbModel> customerRepository;
        private static readonly string[] LastNamesNew = new[]
        {
            "Snow", "Goth", "White", "Jeffry", "Smith", "Brown"
        };
        private static readonly string[] LastNamesRegular = new[]
        {
            "Garcia", "Lee", "Patel", "Johnson", "Wilson", "Kim"
        };
        private static readonly string[] Products = new[]
        {
            "T-Shirt", "Jeans",
            "Sweater", "Jacket",
            "Dress", "Skirt",
            "Shorts", "Blouse",
            "Suit", "Coat",
            "Hoodie", "Pajamas",
            "Belt", "Scarf",
            "Hat", "Socks",
            "Boots", "Sneakers"
        };
        private static readonly int[] PostIndexes = new[]
        {
            03115, 22567, 89088, 12345, 54321, 65678
        };

        private FileStream _transactionLogFileStream;

        public void LogAction(string message)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes($"{DateTime.UtcNow}: {message}\n");
            _transactionLogFileStream.Write(messageBytes, 0, messageBytes.Length);
        }

        public void SetCustomerLogFileStream(FileStream customerLogFileStream)
        {
            this._transactionLogFileStream = customerLogFileStream;
        }
        public async Task<Discount> GetNewCustomerAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            var productName = Products[Random.Shared.Next(Products.Length)];
            var lastName = LastNamesNew[Random.Shared.Next(LastNamesNew.Length)];
            var customer = new NewCustomer()
            {
                LastName = lastName,
                Id = Random.Shared.Next(1, 6),
                PostIndex = PostIndexes[Random.Shared.Next(PostIndexes.Length)]
            };
            var product = new Product()
            {
                ProductId = Random.Shared.Next(1, 18),
                ProductName = productName,
                ProductPrice = Random.Shared.Next(8, 230)
            };
            customer.SetProduct(product);
            var discountedPrice = customer.GetDiscount();

            return new Discount
            {
                Customer = customer,
                DiscountedPrice = discountedPrice
            };
        }

        public async Task<Discount> GetRegularCustomerAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            var productName = Products[Random.Shared.Next(Products.Length)];
            var lastName = LastNamesRegular[Random.Shared.Next(LastNamesRegular.Length)];
            var postIndex = PostIndexes[Random.Shared.Next(PostIndexes.Length)];
            var customer = new RegularCustomer()
            {
                LastName = lastName,
                Id = Random.Shared.Next(7, 12),
                PostIndex = postIndex
            };
            var product = new Product()
            {
                ProductId = Random.Shared.Next(1, 18),
                ProductName = productName,
                ProductPrice = Random.Shared.Next(8, 230)
            };
            customer.SetProduct(product);
            RegularCustomer.RegularCustomerDiscount getDiscount = customer.GetDiscount;

            return new Discount
            {
                Customer = customer,
                DiscountedPrice = customer.ExecuteDiscount(getDiscount)
            };

        }
        public CustomerService(IRepository<CustomerDbModel> customerRepository)
        {
            this.customerRepository = customerRepository
            ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<CustomerDbModel?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await customerRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task AddCustomerAsync(CustomerDbModel customer, CancellationToken cancellationToken)
        {
            await customerRepository.AddAsync(customer, cancellationToken);
        }

        public delegate void CustomerUpdateHandler(CustomerDbModel customerUpdate);
        public event CustomerUpdateHandler? CustomerUpdate;
        public async Task UpdateAsync(CustomerDbModel customer, CancellationToken cancellationToken)
        {
            await customerRepository.UpdateAsync(customer, cancellationToken);
             CustomerUpdate.Invoke(customer);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await customerRepository.DeleteAsync(id, cancellationToken);
        }

        public IEnumerable<string> GetLastNamesStartingWithS()
        {
            IEnumerable<string> lastNameNewQuery =
                from name in LastNamesNew
                where name.StartsWith("S")
                select name;

            return lastNameNewQuery;
        }

        public IEnumerable<int> GetIndexesDesc()
        {
            IEnumerable<int> postIndexQuery =
                from index in PostIndexes
                orderby index descending
                select index;
            return postIndexQuery;
        }
    }
}
