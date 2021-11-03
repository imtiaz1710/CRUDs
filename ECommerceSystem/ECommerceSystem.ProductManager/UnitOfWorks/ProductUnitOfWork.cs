using ECommerceSystem.Data;
using ECommerceSystem.ProductManager.Contexts;
using ECommerceSystem.ProductManager.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.ProductManager.UnitOfWorks
{
    public class ProductUnitOfWork : UnitOfWork, IProductUnitOfWork
    {
        public IProductRepository Products { get; private set; }

        public ProductUnitOfWork(IProductContext context, IProductRepository products) : 
            base((DbContext)context)
        {
            Products = products;
        }
    }
}
