using System.ComponentModel.DataAnnotations;

namespace PizzaDB.Tabs
{
    public class Cities
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public int PriceDelivery { get; set; }
    }
}
