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
    public class CreateTicketModel
    {
        [Required, Range(1,100000)]
        public int CustomerId { get; set; }
        [Required, MaxLength(200, ErrorMessage ="Destination should be less than 200 characters")]
        public string Destination { get; set; }
        [Required, Range(5,100000)]
        public int TicketFee { get; set; }

        private readonly ITicketService _ticketService;

        public CreateTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        public CreateTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        internal void CreateTicket()
        {
            var ticket = new Ticket
            {
                CustomerId = CustomerId,
                Destination = Destination,
                TicketFee = TicketFee
            };

            _ticketService.CreateTicket(ticket);
        }
    }
}
