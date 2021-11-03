using InventorySystem.Inventory.BusinessObjects;
using System.Collections.Generic;

namespace InventorySystem.Inventory.Services
{
    public interface IStockService
    {
        void CreateStock(Stock stock);
        void DeleteStock(int id);
        IList<Stock> GetAllStocks();
        Stock GetStock(int id);
        (IList<Stock> records, int total, int totalDisplay) GetStocks(
            int pageIndex, int pageSize, string searchText, string sortText);
        void UpdateStock(Stock stock);
    }
}