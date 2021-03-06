using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Exceptions;
using TicketBookingSystem.Booking.UnitOfWorks;

namespace TicketBookingSystem.Booking.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IBookingUnitOfWork _bookingUnitOfWork;

        public CustomerService(IBookingUnitOfWork bookingUnitOfWork)
        {
            _bookingUnitOfWork = bookingUnitOfWork;
        }

        public IList<Customer> GetAllCustomers()
        {
            var customerEntities = _bookingUnitOfWork.Customers.GetAll();
            var customers = new List<Customer>();

            foreach (var entity in customerEntities)
            {
                var customer = new Customer()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Age = entity.Age,
                    Address = entity.Address
                };

                customers.Add(customer);
            }

            return customers;
        }

        public (IList<Customer> records, int total, int totalDisplay) GetCustomers(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var customerData = _bookingUnitOfWork.Customers.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from customer in customerData.data
                              select new Customer
                              {
                                  Id = customer.Id,
                                  Name = customer.Name,
                                  Age = customer.Age,
                                  Address = customer.Address
                              }).ToList();
            return (resultData, customerData.total, customerData.totalDisplay);
        }

        public Customer GetCustomer(int id)
        {
            var customer = _bookingUnitOfWork.Customers.GetById(id);

            if (customer == null) return null;

            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Age = customer.Age,
                Address = customer.Address
            };
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer == null)
                throw new InvalidParameterException("Customer was not provided");

            _bookingUnitOfWork.Customers.Add(
                new Entities.Customer
                {
                    Name = customer.Name,
                    Age = customer.Age,
                    Address = customer.Address
                }
            );

            _bookingUnitOfWork.Save();
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new InvalidParameterException("Customer is not given");

            var customerEntity = _bookingUnitOfWork.Customers.GetById(customer.Id);

            if (customerEntity != null)
            {
                customerEntity.Name = customer.Name;
                customerEntity.Age = customer.Age;
                customerEntity.Address = customer.Address;
                _bookingUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find customer");

        }

        public void DeleteCustomer(int id)
        {
            _bookingUnitOfWork.Customers.Remove(id);
            _bookingUnitOfWork.Save();
        }
    }
}
