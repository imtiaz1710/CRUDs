using Autofac;
using ECommerceSystem.Models;
using ECommerceSystem.ProductManager.Services;
using System.Linq;

namespace ECommerceSystem.Areas.Admin.Models
{
    public class ProductListModel
    {
        private IProductService _productService;

        public ProductListModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }

        public ProductListModel(IProductService productService)
        {
            _productService = productService;
        }

        internal object GetProducts(DataTablesAjaxRequestModel tableModel)
        {
            var data = _productService.GetProducts(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] {"Id", "Name", "Price" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Id.ToString(),
                                record.Name,
                                record.Price.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
