using AttendanceSystem.Data;
using AttendanceSystem.StudentAttendance.Contexts;
using AttendanceSystem.StudentAttendance.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.StudentAttendance.UnitOfWorks
{
    class AttendanceUnitOfWork : UnitOfWork, IAttendanceUnitOfWork
    {
        public IStudentRepository Students { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }

        public AttendanceUnitOfWork(IAttendanceContext context, 
            IStudentRepository students, 
            IAttendanceRepository attendances) : base((DbContext)context)
        {
            Students = students;
            Attendances = attendances;
        }
    }
}
