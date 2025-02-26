using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaDB.Tabs
{
    public class Pizza
    {
        public Pizza() { }
        public Pizza(int lunghezza_pizza, int orderId, List<GustoDTO> gustoListDTO)
        {
            this.lunghezza = lunghezza_pizza;
            SetGusto(gustoListDTO);
            this.OrderId = orderId;
        }

        [Key]
        public int Id { set; get; } = 0;
        public enum Gusti
        {
            Margherita,
            Wurstel
        }
        private int _lunghezza = 100;
        public int lunghezza
        {
            get => _lunghezza;
            set => _lunghezza = (value > 100) ? 100 : value;
        }

        public string Gusto { get; set; } = string.Empty;
        public void SetGusto(List<GustoDTO> gustoList)
        {
            int sum = gustoList.Sum(g => g.length);
            if (sum != lunghezza)
            {
                throw new InvalidOperationException($"La somma della lunghezza dei singoli gusti ({sum}cm) non corresponde alla lunghezza della pizza ({_lunghezza}cm).");
            }


            Gusto = JsonConvert.SerializeObject(gustoList);
        }

        [ForeignKey("OrderId")]
        [JsonIgnore]
        public int OrderId { get; set; }

        //navigation properties
        public virtual Order order { get; set; }
    }
    public class GustoDTO
    {
        public required int length { get; set; }
        public required string name { get; set; }
        public required string note { get; set; }
       
    }
}
