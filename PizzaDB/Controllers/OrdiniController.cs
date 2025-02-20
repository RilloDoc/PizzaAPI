using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PizzaDB.Tabs;
using PizzaDB.Tabs.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;



namespace PizzaDB.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class OrdiniController : Controller
    {
        private readonly OrdiniContext _context;

        public OrdiniController(OrdiniContext context) => _context = context;

        [HttpGet("GetAllOrder")]
        public async Task<ActionResult<List<Order>>> GetAllOrder()
        {
            var order = await _context.Orders.Include("Pizzas").FirstOrDefault();
            return order;
        }
        [HttpPost("AddOrder")]
        public async Task<ActionResult> AddOrder([FromBody]OrderDTO orderDTO)
        {
            var order = new Order(orderDTO.time, orderDTO.CustomerId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Created(string.Empty,order);
        }

        //--------------------------------------------------------------------------------------------
        [HttpPost("AddCustomer")]
        public async Task<ActionResult> AddCustomer([FromBody] CustomerDTO customerDTO)
        {
            var customer = new Customer(customerDTO.Name, customerDTO.PhoneNumber, customerDTO.Address);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Created(string.Empty,customer);
        }

        [HttpGet("GetCustomer/{id:int}")]
        public async Task<ActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer is null) return NotFound("Customer Not Found");
            return Ok(customer);
        }
        [HttpPost("AddPizza")]
        public async Task<ActionResult> AddPizzas([FromBody] PizzaDTO pizzaDTO)
        {
            var pizza = new Pizza(pizzaDTO.Lunghezza, pizzaDTO.OrderId,pizzaDTO.Gusto);
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();
            return Created(string.Empty, pizza);

        }

    }
}


