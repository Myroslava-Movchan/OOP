using Microsoft.EntityFrameworkCore;
using Online_Store_Management.Models;

namespace Online_Store_Management.DataAccess
{
    public class OrderInfoDbContext : DbContext
    {
        public OrderInfoDbContext(DbContextOptions<OrderInfoDbContext> options) : base(options)
        { 
        }

        public DbSet<OrderInfo> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderInfo>(entity =>
            {
                entity.Property(e => e.OrderNumber);
            });
        }
    }
}
