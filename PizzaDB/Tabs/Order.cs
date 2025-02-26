using Newtonsoft.Json;
using PizzaDB.Tabs.DTO;
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
        [JsonIgnore]
        public int CustomerId { get; set; } = customerId;

        //navigation properties
        public virtual Customer? Customer { get; set; }
        public virtual Address? Address { get; set; }

        public virtual ICollection<Pizza>? Pizzas { get; set; }

    }
    public class OrderResDTO
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<PizzaResDTO> Pizzas { get; set; }

        public AddressResDTO Address { get; set; }
    }
}
