using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Inventory.BusinessObjects
{
    public class Stock
    {
        public  int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
