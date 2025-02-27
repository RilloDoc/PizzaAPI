using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PizzaDB.Tabs;
using PizzaDB.Tabs.DTO;
using PizzaDB.utils;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;



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
        [HttpPost("AddMoreAddress")]

        public async Task<ActionResult<List<AddressResDTO>>> AddMoreAddress([FromBody] List<AddressDTO> addressDTOs)
        {
            var resList = new List<AddressResDTO>();
            foreach (var addressDTO in addressDTOs)
            {
                var address = new Address(addressDTO.Address, addressDTO.City, addressDTO.OrderId);
                resList.Add(new AddressResDTO
                {
                    Id = address.Id,
                    Address = address._Address,
                    City = address.City,
                    OrderId = address.OrderId
                });
                _context.Addresses.Add(address);
            }
            await _context.SaveChangesAsync();



            return Created(string.Empty, resList);
        }

        [HttpGet("SearchAddresses/{address}")]
        public async Task<ActionResult<List<AddressResDTO>>> SearchAddresses(string address)
        {
            if (address.Length == 0) return BadRequest();
            var terms = address;
            var _address = await _context.Addresses
                .OrderByDescending(x => x._Address.Contains(terms))
                .ThenBy(a => a._Address)
                .ToListAsync();
            List<AddressResDTO> _addressDTO = new List<AddressResDTO>();
            foreach (var item in _address)
            {
                _addressDTO.Add(new AddressResDTO
                {
                    Address = item._Address,
                    City = item.City,
                    Id = item.Id
                });
            }
            Console.WriteLine(_addressDTO.Count);

            var res = LevenshteinTest.testc(new AddressDTO { Address = address, }, _addressDTO.ToArray()).Take(10);

            var addressDtos = _address.Select(a => new AddressResDTO
            {
                Id = a.Id,
                Address = a._Address,
                City = a.City,
            }).ToList();

            return Ok(res);
        }
     
    }
  
}


