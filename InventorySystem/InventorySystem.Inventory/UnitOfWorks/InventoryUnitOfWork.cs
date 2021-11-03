using InventorySystem.Data;
using InventorySystem.Inventory.Contexts;
using InventorySystem.Inventory.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Inventory.UnitOfWorks
{
    public class InventoryUnitOfWork : UnitOfWork, IInventoryUnitOfWork
    {
        public InventoryUnitOfWork(IProductRepository products, IStockRepository stock, 
            IInventoryDbContext context) : base((DbContext)context)
        {
            Products = products;
            Stocks = stock;
        }

        public IProductRepository Products { get; set; }
        public IStockRepository Stocks { get; set; }
    }
}
