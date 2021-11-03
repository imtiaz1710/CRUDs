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
    public class EditStockModel
    {
        [Required, Range(1, 10000000)]
        public int? Id { get; set; }
        [Required, Range(1, 10000000)]
        public int? ProductId { get; set; }
        [Required, Range(1, 100000)]
        public int? Quantity { get; set; }

        private readonly IStockService _stockService;

        public EditStockModel()
        {
            _stockService = Startup.AutofacContainer.Resolve<IStockService>();
        }

        public void LoadModelData(int id)
        {
            var stock = _stockService.GetStock(id);
            Id = stock?.Id;
            ProductId = stock?.ProductId;
            Quantity = stock?.Quantity;
        }

        internal void Update()
        {
            var stock = new Stock
            {
                Id = Id.HasValue ? Id.Value : 0,
                ProductId = ProductId.HasValue ? ProductId.Value : 0,
                Quantity = Quantity.HasValue ? Quantity.Value : 0
            };

            _stockService.UpdateStock(stock);
        }
    }
}
