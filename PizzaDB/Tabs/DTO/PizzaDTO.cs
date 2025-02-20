using static PizzaDB.Tabs.Pizza;

namespace PizzaDB.Tabs.DTO
{
    public class PizzaListDTO
    {

        public required int OrderId { get; set; }
        public required List<PizzaDTO> PizzaList { get; set; }
    }
    public class PizzaDTO

    {

        public required int Lunghezza { get; set; }
        public required List<GustoDTO> Gusto { get; set; }
    }
    public class PizzaResDTO

    {
        public int Id { get; set; }
        public required int Lunghezza { get; set; }
        public required string Gusto { get; set; }
    }
}
