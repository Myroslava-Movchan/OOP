using Microsoft.EntityFrameworkCore;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

namespace Online_Store_Management.DataAccess
{
    public class ProductRepository : IRepository<Product>
    {
        private static readonly List<Product> _product = new();
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product entity, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(id, cancellationToken);
        }
        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
        public async Task UpdateAsync(Product entity, CancellationToken cancellationToken)
        {
            var existingEntity = await _context.Products.FindAsync(entity.ProductId, cancellationToken);
            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Customer with id {entity.ProductId} does not exist");
            }
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int orderNumber, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(orderNumber, cancellationToken);
            if (entity != null)
            {
                _context.Products.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
