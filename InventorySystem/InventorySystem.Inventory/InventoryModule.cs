using Autofac;
using InventorySystem.Inventory.Contexts;
using InventorySystem.Inventory.Repositories;
using InventorySystem.Inventory.Services;
using InventorySystem.Inventory.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Inventory
{
    public class InventoryModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public InventoryModule(string connectionStringName, string migrationAssemblyName)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InventoryDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<InventoryDbContext>().As<IInventoryDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockRepository>().As<IStockRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InventoryUnitOfWork>().As<IInventoryUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockService>().As<IStockService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
