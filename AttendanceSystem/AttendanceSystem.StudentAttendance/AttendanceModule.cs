using AttendanceSystem.StudentAttendance.Contexts;
using AttendanceSystem.StudentAttendance.Repositories;
using AttendanceSystem.StudentAttendance.Services;
using AttendanceSystem.StudentAttendance.UnitOfWorks;
using Autofac;

namespace AttendanceSystem.StudentAttendance
{
    public class AttendanceModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public AttendanceModule(string connectionStringName, string migrationAssemblyName)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AttendanceContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssemblyName", _migrationAssemblyName)
               .InstancePerLifetimeScope();

            builder.RegisterType<AttendanceContext>().As<IAttendanceContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<AttendanceRepository>().As<IAttendanceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentRepository>().As<IStudentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AttendanceUnitOfWork>().As<IAttendanceUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AttendanceService>().As<IAttendanceService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentService>().As<IStudentService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
