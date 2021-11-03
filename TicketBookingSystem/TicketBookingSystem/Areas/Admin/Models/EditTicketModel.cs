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
    public class EditTicketModel
    {
        [Required, Range(1, 1000000)]
        public int? Id { get; set; }
        [Required, Range(1, 100000)]
        public int? CustomerId { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Destination should be less than 200 characters")]
        public string Destination { get; set; }
        [Required, Range(5, 100000)]
        public int? TicketFee { get; set; }

        private readonly ITicketService _ticketService;

        public EditTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        public void LoadModelData(int id)
        {
            var ticket = _ticketService.GetTicket(id);
            Id = ticket?.Id;
            CustomerId = ticket?.CustomerId;
            Destination = ticket?.Destination;
            TicketFee = ticket?.TicketFee;
        }

        internal void Update()
        {
            var ticket = new Ticket
            {
                Id = Id.HasValue ? Id.Value : 0,
                CustomerId = CustomerId.HasValue ? CustomerId.Value : 0,
                Destination = Destination,
                TicketFee = TicketFee.HasValue ? TicketFee.Value : 0
            };

            _ticketService.UpdateTicket(ticket);
        }

    }
}
