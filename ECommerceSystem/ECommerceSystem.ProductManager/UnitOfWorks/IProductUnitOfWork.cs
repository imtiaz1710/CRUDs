using ECommerceSystem.Data;
using ECommerceSystem.ProductManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.ProductManager.UnitOfWorks
{
    public interface IProductUnitOfWork : IUnitOfWork
    {
        IProductRepository Products { get; }
    }
}
