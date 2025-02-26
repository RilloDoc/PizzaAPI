using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace PizzaDB.Tabs
{
    public class Address(string Address, string City, int OrderId)
    {
        [Key]
        public int Id { get; set; }
        public string _Address { get; set; } = Address ?? string.Empty;

        public string City { get; set; } = City ?? string.Empty;


        [ForeignKey("OrderId")]
        [JsonIgnore]
        public int OrderId { get; set; } = OrderId ;

        public virtual Order order { get; set; }
    }
}
