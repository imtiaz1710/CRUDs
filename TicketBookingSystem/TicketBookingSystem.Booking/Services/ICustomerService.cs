using System.Collections.Generic;
using TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Booking.Services
{
    public interface ICustomerService
    {
        void CreateCustomer(Customer customer);
        void DeleteCustomer(int id);
        IList<Customer> GetAllCustomers();
        Customer GetCustomer(int id);

        (IList<Customer> records, int total, int totalDisplay) GetCustomers(
            int pageIndex, int pageSize, string searchText, string sortText);

        void UpdateCustomer(Customer customer);
    }
}