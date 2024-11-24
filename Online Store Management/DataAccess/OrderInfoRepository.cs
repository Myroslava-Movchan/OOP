using Microsoft.EntityFrameworkCore;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

namespace Online_Store_Management.DataAccess
{
    public class OrderInfoRepository : IRepository<OrderInfo>
    {
        private static readonly List<OrderInfo> _order = new();
        private readonly OrderInfoDbContext _context;

        public OrderInfoRepository(OrderInfoDbContext context)
        { 
            _context = context;
        }
        public async Task AddAsync(OrderInfo entity, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<OrderInfo?> GetByIdAsync(int orderNumber, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.Product) 
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber, cancellationToken);
        }
        public async Task<IEnumerable<OrderInfo>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Orders.ToListAsync(cancellationToken);
        }
        public async Task UpdateAsync(OrderInfo entity, CancellationToken cancellationToken)
        {
            var existingEntity = await _context.Orders.FindAsync(entity.OrderNumber, cancellationToken);
            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Customer with id {entity.OrderNumber} does not exist");
            }
            existingEntity.Status = entity.Status;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int orderNumber, CancellationToken cancellationToken)
        {
            var entity = await _context.Orders.FindAsync(orderNumber, cancellationToken);
            if (entity != null)
            {
                _context.Orders.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
