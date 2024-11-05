using Microsoft.EntityFrameworkCore;

namespace Online_Store_Management.DataAccess
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
        {
        }

        public DbSet<CustomerDbModel> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDbModel>(entity =>
            {
                entity.Property(e => e.PostIndex);
            });
        }
    }
}
