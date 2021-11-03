using AttendanceSystem.Data;
using AttendanceSystem.StudentAttendance.Contexts;
using AttendanceSystem.StudentAttendance.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.StudentAttendance.Repositories
{
    public class StudentRepository : Repository<Student, int>, IStudentRepository
    {
        public StudentRepository(IAttendanceContext context) : 
            base((DbContext)context)
        {
        }
    }
}
