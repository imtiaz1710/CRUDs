using System.Collections.Generic;
using TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Booking.Services
{
    public interface ITicketService
    {
        void CreateTicket(Ticket Ticket);
        void DeleteTicket(int id);
        IList<Ticket> GetAllTickets();
        Ticket GetTicket(int id);

        (IList<Ticket> records, int total, int totalDisplay) GetTickets(
            int pageIndex, int pageSize, string searchText, string sortText);

        void UpdateTicket(Ticket Ticket);
    }
}