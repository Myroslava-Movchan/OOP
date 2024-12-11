using Online_Store_Management.Models;
using Online_Store_Management.Interfaces;
using Online_Store_Management.DataAccess;
namespace Online_Store_Management.Services
{
    public class CustomerService : ICustomer
    {
        public delegate void CustomerUpdateHandler(CustomerDbModel customerUpdate);
        public event CustomerUpdateHandler? CustomerUpdate;
        private readonly IRepository<CustomerDbModel> customerRepository;
        private static readonly string[] LastNamesNew = new[]
        {
            "Snow", "Goth", "White", "Jeffry", "Smith", "Brown"
        };
        private static readonly int[] PostIndexes = new[]
        {
            03115, 22567, 89088, 12345, 54321, 65678
        };

        private FileStream _transactionLogFileStream;

        public async Task LogActionAsync(Customer customer, string message, CancellationToken cancellationToken)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes($"{DateTime.UtcNow}: {message}\n");
            _transactionLogFileStream.Write(messageBytes, 0, messageBytes.Length);
            await Infrastructure.Logger.LogToConsole(customer, cancellationToken);
        }

        public void SetCustomerLogFileStream(FileStream customerLogFileStream)
        {
            this._transactionLogFileStream = customerLogFileStream;
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

        public async Task UpdateAsync(CustomerDbModel customer, CancellationToken cancellationToken)
        {
            await customerRepository.UpdateAsync(customer, cancellationToken);
            CustomerUpdate.Invoke(customer);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID provided.");
            }
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
