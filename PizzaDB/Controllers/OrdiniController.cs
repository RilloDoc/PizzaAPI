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

        [HttpGet("GetAllOrders")]
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
                },
                Pizzas = o.Pizzas.Select(p => new PizzaResDTO
                {
                    Id = p.Id,
                    Lunghezza = p.lunghezza,
                    Gusto = p.Gusto
                }).ToList(),
                Address = new AddressResDTO
                {
                    Address=o.Address._Address,
                    City=o.Address.City,
                    Id = o.Address.Id
                }

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
        public async Task<ActionResult<CustomerResDTO>> AddCustomer([FromBody] CustomerDTO customerDTO)
        {
            var customer = new Customer(customerDTO.Name, customerDTO.PhoneNumber);
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
            };

            return Ok(customerDto);
        }
        [HttpPost("AddPizzas")]
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
        [HttpPost("AddAddress")]
        public async Task<ActionResult<AddressResDTO>> AddAddress([FromBody] AddressDTO addressDTO)
        {
            var address = new Address(addressDTO.Address, addressDTO.City, addressDTO.OrderId);
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            var addressResDTO = new AddressResDTO
            {
                Id = address.Id,
                Address = address._Address,
                City = address.City,
                OrderId = address.OrderId
            };

            return Created(string.Empty, addressResDTO);
        }

        [HttpGet("SearchAddresses")]
        public async Task<ActionResult<List<AddressResDTO>>> SearchAddresses(string query)
        {

            var addresses = await _context.Addresses
                .Where(a => EF.Functions.Like(a._Address, $"%{query}%") || EF.Functions.Like(a.City, $"%{query}%"))
                .ToListAsync();

            var addressDtos = addresses.Select(a => new AddressResDTO
            {
                Id = a.Id,
                Address = a._Address,
                City = a.City,
                OrderId = a.OrderId
            }).ToList();

            return Ok(addressDtos);
        }
    }
  
}


