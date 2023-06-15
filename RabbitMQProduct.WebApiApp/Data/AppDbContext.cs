using Microsoft.EntityFrameworkCore;
using RabbitMQProduct.WebApiApp.Models;

namespace RabbitMQProduct.WebApiApp.Data
{
    public class AppDbContext : DbContext 
    {
        public IConfiguration _configuration { get; }
        public DbSet<Product> Products { get; set; }
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DevConnection"));
        }
    }
}
