using Autofac;
using System.Linq;
using TicketBookingSystem.Booking.Services;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class CustomerListModel
    {
        public ICustomerService _customerService;

        public CustomerListModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        public CustomerListModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        internal object GetCustomer(DataTablesAjaxRequestModel tableModel)
        {
            var data = _customerService.GetCustomers(
                tableModel.PageIndex, tableModel.PageSize, 
                tableModel.SearchText, 
                tableModel.GetSortText(new string[] {"Id", "Name", "Age", "Address"}));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Id.ToString(),
                            record.Name,
                            record.Age.ToString(),
                            record.Address,
                            record.Id.ToString()
                        }
                ).ToArray()
            };
        }

        public void Delete(int id)
        {
            _customerService.DeleteCustomer(id);
        }
    }
}
