using Quicker.EntityFrameworkCore;
using QuickerAspNetCoreDemo.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace QuickerAspNetCoreDemo.Db
{
    public class MyDbContext : QuickerDbContext
    {
        public DbSet<Product> Products { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product("Test product", 100)
            {
                Id = 1
            });
        }
    }
}