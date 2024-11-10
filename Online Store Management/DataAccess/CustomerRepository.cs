using Microsoft.EntityFrameworkCore;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

namespace Online_Store_Management.DataAccess
{ 
    public class CustomerRepository : IRepository<CustomerDbModel>
    {
        private static readonly List<Customer> _customer = new();
        private readonly CustomerDBContext _context;

        public CustomerRepository(CustomerDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(CustomerDbModel entity, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<CustomerDbModel?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Customers.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<CustomerDbModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Customers.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(CustomerDbModel entity, CancellationToken cancellationToken)
        {
            var existingEntity = await _context.Customers.FindAsync(entity.Id, cancellationToken);
            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Customer with id {entity.Id} does not exist");
            }
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers.FindAsync(id, cancellationToken);
            if (entity != null)
            {
                _context.Customers.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
