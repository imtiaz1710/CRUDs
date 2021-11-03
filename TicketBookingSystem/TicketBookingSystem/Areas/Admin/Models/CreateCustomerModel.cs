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
    public class CreateCustomerModel
    {
        [Required, MaxLength(200, ErrorMessage ="Name should be less than 200 characters")]
        public string Name { get; set; }
        [Required, Range(10, 150)]
        public int Age { get; set; }
        [Required, MaxLength(500, ErrorMessage ="Address should be less than 500 characters")]
        public string Address { get; set; }

        private readonly ICustomerService _customerService;

        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public CreateCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        internal void CreateCustomer()
        {
            var customer = new Customer
            {
                Name = Name,
                Age = Age,
                Address = Address
            };
            _customerService.CreateCustomer(customer);
        }
    }
}
