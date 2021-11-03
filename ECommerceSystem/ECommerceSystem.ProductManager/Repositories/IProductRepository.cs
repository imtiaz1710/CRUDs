using ECommerceSystem.Data;
using ECommerceSystem.ProductManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.ProductManager.Repositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
    }
}
