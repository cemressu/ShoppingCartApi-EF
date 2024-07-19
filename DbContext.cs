using Microsoft.EntityFrameworkCore;
using Npgsql;
using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;


namespace ShoppingCartApi
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;  

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public DbSet<BasketModel> BasketModel{ get; set; }
        public DbSet<CustomerModel> CustomerModel{ get; set; }
        public DbSet <ProductModel> ProductModel{ get; set; }
        public DbSet<OrderModel> OrderModel { get; set; }


    }
}

