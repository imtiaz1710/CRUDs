using ECommerceSystem.Data;
using ECommerceSystem.ProductManager.Contexts;
using ECommerceSystem.ProductManager.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.ProductManager.Repositories
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(IProductContext context) : 
            base((DbContext)context)
        {
        }
    }
}
