namespace OrderManagementSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using OrderManagementSystem.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<OrderHeader>? OrderHeader { get; set; }
        public DbSet<OrderLine>? OrderLine { get; set; }
    }
}
