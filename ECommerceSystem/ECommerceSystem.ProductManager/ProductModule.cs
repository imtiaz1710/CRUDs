using Autofac;
using ECommerceSystem.ProductManager.Contexts;
using ECommerceSystem.ProductManager.Repositories;
using ECommerceSystem.ProductManager.Services;
using ECommerceSystem.ProductManager.UnitOfWorks;

namespace ECommerceSystem.ProductManager
{
    public class ProductModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ProductModule(string connectionStringName, string migrationAssemblyName)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ProductContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssemblyName", _migrationAssemblyName)
               .InstancePerLifetimeScope();

            builder.RegisterType<ProductContext>().As<IProductContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductUnitOfWork>().As<IProductUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
