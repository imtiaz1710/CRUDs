using InventorySystem.Data;
using InventorySystem.Inventory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Inventory.Repositories
{
    public interface IStockRepository : IRepository<Stock, int>
    {
    }
}
