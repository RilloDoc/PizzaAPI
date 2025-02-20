using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PizzaDB.Tabs
{
    public class Pizza
    {
        public Pizza() { }
        public Pizza(int lunghezza_pizza, int orderId,List<GustoDTO> gustoListDTO)
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
        if (gustoList.Sum(g => g.length) != lunghezza)
        {
            throw new InvalidOperationException("La somma dei valori di GustoDTO non può superare la lunghezza della pizza.");
        }

        Gusto = JsonConvert.SerializeObject(gustoList);
    }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        //navigation properties
        public Order? Order { get; set; }
    }
    public class GustoDTO
    {
        public required int length { get; set; }
        public required string note { get; set; }
        public int price { get; set; }
    }
}
