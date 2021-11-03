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
    public class EditAttendanceModel
    {
        public int? Id { get; set; }
        [Required, Range(1, 1000)]
        public int? StudentId { get; set; }
        [Required, Range(typeof(DateTime), "1/1/2000", "1/1/2030")]
        public DateTime? Date { get; set; }

        private readonly IAttendanceService _attendanceService;

        public EditAttendanceModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }

        public EditAttendanceModel(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public void LoadModelData(int id)
        {
            var attendance = _attendanceService.GetAttendance(id);
            Id = attendance?.Id;
            StudentId = attendance?.StudentId;
            Date = attendance?.Date;
        }

        public void Update()
        {
            var attendance = new Attendance
            {
                Id = Id.HasValue ? Id.Value : 0,
                StudentId = StudentId.HasValue ? StudentId.Value : 0,
                Date = Date.HasValue ? Date.Value : DateTime.MinValue
            };

            _attendanceService.UpdateAttendance(attendance);
        }
    }
}
