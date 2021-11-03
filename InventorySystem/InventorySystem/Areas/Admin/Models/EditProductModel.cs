using Autofac;
using InventorySystem.Inventory.BusinessObjects;
using InventorySystem.Inventory.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Models
{
    public class EditProductModel
    {
        [Required, Range(1, 10000000)]
        public int? Id { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Product Name should be less than 200 charcaters")]
        public string Name { get; set; }
        [Required, Range(1, 10000000)]
        public int? Price { get; set; }

        private readonly IProductService _productService;

        public EditProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }

        public void LoadModelData(int id)
        {
            var product = _productService.GetProduct(id);
            Id = product?.Id;
            Name = product.Name;
            Price = product?.Price;
        }

        internal void Update()
        {
            var product = new Product
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                Price = Price.HasValue ? Price.Value : 0
            };

            _productService.UpdateProduct(product);
        }
    }
}
