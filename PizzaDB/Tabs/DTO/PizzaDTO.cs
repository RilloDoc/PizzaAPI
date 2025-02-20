using static PizzaDB.Tabs.Pizza;

namespace PizzaDB.Tabs.DTO
{
    public class PizzaDTO
    {
        public int Lunghezza { get; set; }
        public int OrderId { get; set; }
        public required List<GustoDTO> Gusto { get; set; }
    }
}
