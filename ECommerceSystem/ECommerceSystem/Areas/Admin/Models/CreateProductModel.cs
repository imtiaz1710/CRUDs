using Autofac;
using ECommerceSystem.ProductManager.BusinessObjects;
using ECommerceSystem.ProductManager.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.Areas.Admin.Models
{
    public class CreateProductModel
    {
        [Required, MaxLength(200, ErrorMessage = "Product Name should be less than 200 charcaters")]
        public string Name { get; set; }
        [Required, Range(1, 10000000)]
        public int Price { get; set; }

        private readonly IProductService _productService;

        public CreateProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }

        public CreateProductModel(IProductService productService)
        {
            _productService = productService;
        }

        internal void CreateProduct()
        {
            var product = new Product
            {
                Name = Name,
                Price = Price
            };

            _productService.CreateProduct(product);
        }
    }
}
