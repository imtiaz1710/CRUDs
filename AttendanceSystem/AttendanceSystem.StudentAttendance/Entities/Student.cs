using AttendanceSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.StudentAttendance.Entities
{
    public class Student : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentRollNumber { get; set; }
        public IList<Attendance> Attendances { get; set; }
    }
}
