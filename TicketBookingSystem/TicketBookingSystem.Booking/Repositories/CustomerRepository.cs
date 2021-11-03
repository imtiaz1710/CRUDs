using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Booking.Entities;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Repositories
{
    public class CustomerRepository : Repository<Customer, int> , ICustomerRepository
    {
        public CustomerRepository(IBookingDbContext context) : 
            base((DbContext)context)
        { 
        }
    }
}
