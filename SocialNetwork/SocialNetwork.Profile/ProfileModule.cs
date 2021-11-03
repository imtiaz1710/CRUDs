using Autofac;
using SocialNetwork.ProfileManager.Contexts;
using SocialNetwork.ProfileManager.Repositories;
using SocialNetwork.ProfileManager.Services;
using SocialNetwork.ProfileManager.UnitOfWorks;

namespace SocialNetwork.ProfileManager
{
    public class ProfileModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ProfileModule(string connectionStringName, string migrationAssemblyName)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ProfileContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssemblyName", _migrationAssemblyName)
               .InstancePerLifetimeScope();

            builder.RegisterType<ProfileContext>().As<IProfileContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<MemberRepository>().As<IMemberRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProfileUnitOfWork>().As<IProfileUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PhotoService>().As<IPhotoService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MemberService>().As<IMemberService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
