using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Services;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class EditCustomerModel
    {
        [Required, Range(1, 50000)]
        public int? Id { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 charcaters")]
        public string Name { get; set; }
        [Required, Range(10, 150)]
        public int? Age { get; set; }
        [Required, MaxLength(500, ErrorMessage = "Address should be less than 500 charcaters")]
        public string Address { get; set; }

        private readonly ICustomerService _CustomerService;

        public EditCustomerModel()
        {
            _CustomerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        public void LoadModelData(int id)
        {
            var Customer = _CustomerService.GetCustomer(id);
            Id = Customer?.Id;
            Name = Customer?.Name;
            Age = Customer?.Age;
            Address = Customer?.Address;
        }

        internal void Update()
        {
            var customer = new Customer
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                Age = Age.HasValue ? Age.Value : 0,
                Address = Address
            };

            _CustomerService.UpdateCustomer(customer);
        }

    }
}
