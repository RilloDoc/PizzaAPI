using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PizzaDB.Tabs
{
    public class Customer(string? Name, string? PhoneNumber, string? Address)
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; } = Name ?? string.Empty;
        public string PhoneNumber { get; set; } = PhoneNumber ?? string.Empty;
        public string Address { get; set; } = Address ?? string.Empty;

        //navigation properties
        [ValidateNever]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
