using InventorySystem.Data;
using InventorySystem.Inventory.Contexts;
using InventorySystem.Inventory.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Inventory.Repositories
{
    public class StockRepository : Repository<Stock, int>, IStockRepository
    {
        public StockRepository(IInventoryDbContext context) : base((DbContext)context)
        {
        }
    }
}
