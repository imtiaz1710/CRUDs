using InventorySystem.Data;
using InventorySystem.Inventory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Inventory.UnitOfWorks
{
    public interface IInventoryUnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; }
        public IStockRepository Stocks { get; }
    }
}
