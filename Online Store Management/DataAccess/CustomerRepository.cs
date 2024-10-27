using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

namespace Online_Store_Management.DataAccess
{
    public class CustomerRepository : IRepository<Customer>
    {
        private static readonly List<Customer> _customer = new();
        public async Task AddAsync(Customer entity, CancellationToken cancellationToken)
        {
            await Task.Run(() => _customer.Add(entity), cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _customer?.Find(a => a.Id == id), cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => _customer, cancellationToken);
        }

        public async Task UpdateAsync(Customer entity, CancellationToken cancellationToken)
        {
            if (entity == null || entity.Id == 0)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var existingCustomer = await GetByIdAsync(entity.Id, cancellationToken);
            if (existingCustomer != null)
            {
                existingCustomer.LastName = entity.LastName;
                existingCustomer.PostIndex = entity.PostIndex;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var customerDelete = await GetByIdAsync(id, cancellationToken);
            if (customerDelete != null)
            { 
                await Task.Run(() => _customer.Remove(customerDelete), cancellationToken);
            }
        }
    }
}
