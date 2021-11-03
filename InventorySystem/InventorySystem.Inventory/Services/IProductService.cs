using InventorySystem.Inventory.BusinessObjects;
using System.Collections.Generic;

namespace InventorySystem.Inventory.Services
{
    public interface IProductService
    {
        void CreateProduct(Product product);
        void DeleteProduct(int id);
        IList<Product> GetAllProducts();
        Product GetProduct(int id);
        (IList<Product> records, int total, int totalDisplay) GetProducts(
            int pageIndex, int pageSize, string searchText, string sortText);
        void UpdateProduct(Product product);
    }
}