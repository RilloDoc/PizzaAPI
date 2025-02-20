using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PizzaDB.Tabs;
using PizzaDB.Tabs.DTO;
using System.Reflection.Metadata.Ecma335;



namespace PizzaDB.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class OrdiniController : Controller
    {
        private readonly OrdiniContext _context;

        public OrdiniController(OrdiniContext context) => _context = context;

        [HttpGet("GetAllOrder")]
        public async Task<ActionResult<List<OrderResDTO>>> GetAllOrder()
        {
            var orders = await _context.Orders.ToListAsync();

            // Map to DTOs
            var orderDtos = orders.Select(o => new OrderResDTO
            {
                Id = o.Id,
                Time = o.time,
                Customer = new CustomerDTO
                {
                    Name = o.Customer.Name,
                    PhoneNumber = o.Customer.PhoneNumber,
                    Address = o.Customer.Address
                },
                Pizzas = o.Pizzas.Select(p => new PizzaResDTO
                {
                    Id = p.Id,
                    Lunghezza = p.lunghezza,
                    Gusto = p.Gusto
                    // OrderId is excluded from the DTO
                }).ToList()
            });
            return Ok(orderDtos);

        }
        [HttpPost("AddOrder")]
        public async Task<ActionResult> AddOrder([FromBody] OrderDTO orderDTO)
        {
            var order = new Order(orderDTO.time, orderDTO.CustomerId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Created(string.Empty, order);
        }

        //--------------------------------------------------------------------------------------------
        [HttpPost("AddCustomer")]
        public async Task<ActionResult> AddCustomer([FromBody] CustomerDTO customerDTO)
        {
            var customer = new Customer(customerDTO.Name, customerDTO.PhoneNumber, customerDTO.Address);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Created(string.Empty, customer);
        }

        [HttpGet("GetCustomer")]
        public async Task<ActionResult> GetCustomer(int id)
        {
            Customer? customer = await _context.Customers.FirstOrDefaultAsync(c=>c.Id==id);
            if (customer is null) return NotFound("Customer Not Found");
            var customerDto = new CustomerResDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
            };

            return Ok(customerDto);
        }
        [HttpPost("AddPizza")]
        public async Task<ActionResult> AddPizzas([FromBody] PizzaListDTO pizzaListDTO)
        {
            foreach (var pizzaDTO in pizzaListDTO.PizzaList)
            {
                var pizza = new Pizza(pizzaDTO.Lunghezza, pizzaListDTO.OrderId, pizzaDTO.Gusto);
                _context.Pizzas.Add(pizza);
            }
            await _context.SaveChangesAsync();
            return Created(string.Empty, pizzaListDTO);

        }

    }
}


