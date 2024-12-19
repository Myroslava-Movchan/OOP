using Microsoft.EntityFrameworkCore;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

namespace Online_Store_Management.DataAccess
{ 
    public class CustomerRepository(CustomerDBContext context) : IRepository<CustomerDbModel>
    {
        private readonly CustomerDBContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task AddAsync(CustomerDbModel entity, CancellationToken cancellationToken)
        {
            if (_context.Customers == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await _context.Customers.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<CustomerDbModel?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (_context.Customers == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return await _context.Customers.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<CustomerDbModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            if(_context.Customers == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return await _context.Customers.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(CustomerDbModel entity, CancellationToken cancellationToken)
        {
            if (_context.Customers == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var existingEntity = await _context.Customers.FindAsync([entity.Id, cancellationToken], cancellationToken: cancellationToken) ?? throw new InvalidOperationException($"Customer with id {entity.Id} does not exist");
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (_context.Customers == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var entity = await _context.Customers.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
            if (entity != null)
            {
                _context.Customers.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
