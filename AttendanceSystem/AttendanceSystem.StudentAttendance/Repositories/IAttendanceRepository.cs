using AttendanceSystem.Data;
using AttendanceSystem.StudentAttendance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.StudentAttendance.Repositories
{
    public interface IAttendanceRepository : IRepository<Attendance, int>
    {
    }
}
