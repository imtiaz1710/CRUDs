using AttendanceSystem.StudentAttendance.BusinessObjects;
using AttendanceSystem.StudentAttendance.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Models.AttendanceModel
{
    public class CreateAttendanceModel
    {
        [Required, Range(1,1000)]
        public int StudentId { get; set; }
        [Required, Range(typeof(DateTime), "1/1/2000", "1/1/2030")]
        public DateTime Date { get; set; }

        private readonly IAttendanceService _attendanceService;

        public CreateAttendanceModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }

        public CreateAttendanceModel(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        internal void CreateAttendance()
        {
            var attendance = new Attendance
            {
                StudentId = StudentId,
                Date = Date.Date
            };

            _attendanceService.CreateAttendance(attendance);
        }
    }
}
