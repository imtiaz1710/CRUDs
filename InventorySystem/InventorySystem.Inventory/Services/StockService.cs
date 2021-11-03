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
    public class StockService : IStockService
    {
        private readonly IInventoryUnitOfWork _trainingUnitOfWork;

        public StockService(IInventoryUnitOfWork trainingUnitOfWork)
        {
            _trainingUnitOfWork = trainingUnitOfWork;
        }

        public IList<Stock> GetAllStocks()
        {
            var stockEntities = _trainingUnitOfWork.Stocks.GetAll();
            var stocks = new List<Stock>();

            foreach (var entity in stockEntities)
            {
                var stock = new Stock()
                {
                    Id = entity.Id,
                    ProductId = entity.ProductId,
                    Quantity = entity.Quantity,
                };

                stocks.Add(stock);
            }

            return stocks;
        }

        public void CreateStock(Stock stock)
        {
            if (stock == null)
                throw new InvalidParameterException("Stock was not provided");

            if (IsProductIdAlreadyUsed(stock.Id))
                throw new DuplicateNameException("Product Id already exists");

            _trainingUnitOfWork.Stocks.Add(
                new Entities.Stock
                {
                    ProductId = stock.ProductId,
                    Quantity = stock.Quantity
                }
            );

            _trainingUnitOfWork.Save();
        }

        public (IList<Stock> records, int total, int totalDisplay) GetStocks(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var stockData = _trainingUnitOfWork.Stocks.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.ProductId.ToString().Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from stock in stockData.data
                              select new Stock
                              {
                                  Id = stock.Id,
                                  ProductId = stock.ProductId,
                                  Quantity = stock.Quantity
                              }).ToList();

            return (resultData, stockData.total, stockData.totalDisplay);
        }

        public Stock GetStock(int id)
        {
            var stock = _trainingUnitOfWork.Stocks.GetById(id);

            if (stock == null) return null;

            return new Stock
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity
            };
        }

        public void UpdateStock(Stock stock)
        {
            if (stock == null)
                throw new InvalidOperationException("Stock is missing");

            if (IsProductIdAlreadyUsed(stock.ProductId, stock.Id))
                throw new DuplicateNameException("Product Id already used in other stock.");

            var stockEntity = _trainingUnitOfWork.Stocks.GetById(stock.Id);

            if (stockEntity != null)
            {
                stockEntity.ProductId = stock.ProductId;
                stockEntity.Quantity = stock.Quantity;

                _trainingUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find stock");
        }

        public void DeleteStock(int id)
        {
            _trainingUnitOfWork.Stocks.Remove(id);
            _trainingUnitOfWork.Save();
        }


        private bool IsProductIdAlreadyUsed(int productId) =>
            _trainingUnitOfWork.Stocks.GetCount(x => x.ProductId == productId) > 0;


        private bool IsProductIdAlreadyUsed(int productId, int id) =>
            _trainingUnitOfWork.Stocks.GetCount(x => x.ProductId == productId && x.Id != id) > 0;
    }
}
