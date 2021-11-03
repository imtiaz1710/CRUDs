using AutoMapper;
using ECommerceSystem.ProductManager.BusinessObjects;
using ECommerceSystem.ProductManager.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace ECommerceSystem.ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductUnitOfWork _productUnitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IProductUnitOfWork productUnitOfWork, IMapper mapper)
        {
            _productUnitOfWork = productUnitOfWork;
            _mapper = mapper;
        }

        public IList<Product> GetAllProducts()
        {
            var productEntities = _productUnitOfWork.Products.GetAll();
            var products = new List<Product>();

            foreach (var entity in productEntities)
            {
                products.Add(_mapper.Map<Product>(entity));
            }

            return products;
        }

        public void CreateProduct(Product product)
        {
            if (product == null)
                throw new NullReferenceException("Product is Null");

            if (IsNameAlreadyUsed(product.Name))
                throw new DuplicateNameException("Product title already exists");

            _productUnitOfWork.Products.Add(
                _mapper.Map<Entities.Product>(product)
            );

            _productUnitOfWork.Save();
        }

        public (IList<Product> records, int total, int totalDisplay) GetProducts(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var productData = _productUnitOfWork.Products.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from product in productData.data
                              select _mapper.Map<Product>(product)).ToList();

            return (resultData, productData.total, productData.totalDisplay);
        }

        public Product GetProduct(int id)
        {
            var product = _productUnitOfWork.Products.GetById(id);

            if (product == null) return null;

            return _mapper.Map<Product>(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new NullReferenceException("Product is Null");

            if (IsNameAlreadyUsed(product.Name, product.Id))
                throw new DuplicateNameException("Product title already used in other product.");

            var productEntity = _productUnitOfWork.Products.GetById(product.Id);

            if (productEntity != null)
            {
                _mapper.Map(product, productEntity);
                _productUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find product");
        }

        public void DeleteProduct(int id)
        {
            _productUnitOfWork.Products.Remove(id);
            _productUnitOfWork.Save();
        }

        private bool IsNameAlreadyUsed(string name) =>
            _productUnitOfWork.Products.GetCount(x => x.Name == name) > 0;

        private bool IsNameAlreadyUsed(string name, int id) =>
            _productUnitOfWork.Products.GetCount(x => x.Name == name && x.Id != id) > 0;
    }
}
