using Microsoft.EntityFrameworkCore;
using PizzaDB.Tabs;

namespace PizzaDB
{
    public class OrdiniContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }

        public OrdiniContext(DbContextOptions<OrdiniContext> options)
                 : base(options)
        {
        }




    }
}
