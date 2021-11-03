using InventorySystem.Inventory.BusinessObjects;
using InventorySystem.Inventory.Exceptions;
using InventorySystem.Inventory.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Inventory.Services
{
    public class ProductService : IProductService
    {
        private readonly IInventoryUnitOfWork _trainingUnitOfWork;

        public ProductService(IInventoryUnitOfWork trainingUnitOfWork)
        {
            _trainingUnitOfWork = trainingUnitOfWork;
        }

        public IList<Product> GetAllProducts()
        {
            var productEntities = _trainingUnitOfWork.Products.GetAll();
            var products = new List<Product>();

            foreach (var entity in productEntities)
            {
                var product = new Product()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Price = entity.Price,
                };

                products.Add(product);
            }

            return products;
        }

        public void CreateProduct(Product product)
        {
            if (product == null)
                throw new InvalidParameterException("Product was not provided");

            if (IsNameAlreadyUsed(product.Name))
                throw new DuplicateNameException("Product Name already exists");

            _trainingUnitOfWork.Products.Add(
                new Entities.Product
                {
                    Name = product.Name,
                    Price = product.Price
                }
            );

            _trainingUnitOfWork.Save();
        }

        public (IList<Product> records, int total, int totalDisplay) GetProducts(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var productData = _trainingUnitOfWork.Products.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from product in productData.data
                              select new Product
                              {
                                  Id = product.Id,
                                  Name = product.Name,
                                  Price = product.Price
                              }).ToList();

            return (resultData, productData.total, productData.totalDisplay);
        }

        public Product GetProduct(int id)
        {
            var product = _trainingUnitOfWork.Products.GetById(id);

            if (product == null) return null;

            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new InvalidOperationException("Product is missing");

            if (IsNameAlreadyUsed(product.Name, product.Id))
                throw new DuplicateNameException("Product Name already used in other product.");

            var productEntity = _trainingUnitOfWork.Products.GetById(product.Id);

            if (productEntity != null)
            {
                productEntity.Name = product.Name;
                productEntity.Price = product.Price;

                _trainingUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find product");
        }

        public void DeleteProduct(int id)
        {
            _trainingUnitOfWork.Products.Remove(id);
            _trainingUnitOfWork.Save();
        }


        private bool IsNameAlreadyUsed(string name) =>
            _trainingUnitOfWork.Products.GetCount(x => x.Name == name) > 0;

        private bool IsNameAlreadyUsed(string name, int id) =>
            _trainingUnitOfWork.Products.GetCount(x => x.Name == name && x.Id != id) > 0;
    }
}
