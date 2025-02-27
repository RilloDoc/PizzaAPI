using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaDB.Tabs;

namespace PizzaDB
{
    public class OrdiniContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cities> Cities { get; set; }

        public OrdiniContext(DbContextOptions<OrdiniContext> options)
                 : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }


    }
}
