using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDB.Tabs
{
    public class Order(string time, int customerId)
    {
        [Key]
        public int Id { get; set; }
        public string time { get; set; } = time;
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; } = customerId;

        //navigation properties
        public Customer? Customer { get; set; }
        public ICollection<Pizza>? Pizzas { get; set; }

    }
}
