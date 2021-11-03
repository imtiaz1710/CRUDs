using System;

namespace TicketBookingSystem.Booking.BusinessObjects
{
    public class Ticket
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Destination { get; set; }
        public int TicketFee { get; set; }
    }
}
