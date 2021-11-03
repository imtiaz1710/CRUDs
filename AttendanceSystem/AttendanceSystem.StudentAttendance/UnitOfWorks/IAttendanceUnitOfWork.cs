using AttendanceSystem.Data;
using AttendanceSystem.StudentAttendance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.StudentAttendance.UnitOfWorks
{
    public interface IAttendanceUnitOfWork : IUnitOfWork
    {
        public IStudentRepository Students { get;}
        public IAttendanceRepository Attendances { get;}
    }
}
