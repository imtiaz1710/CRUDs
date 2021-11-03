using AttendanceSystem.StudentAttendance.BusinessObjects;
using AttendanceSystem.StudentAttendance.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Models.StudentModel
{
    public class CreateStudentModel
    {
        [Required, MaxLength(200, ErrorMessage ="Name should be less than 200 characters")]
        public string Name { get; set; }
        [Required, Range(1, 1000)]
        public int StudentRollNumber { get; set; }

        private readonly IStudentService _studentService;

        public CreateStudentModel()
        {
            _studentService = Startup.AutofacContainer.Resolve<IStudentService>();
        }

        public CreateStudentModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        internal void CreateStudent()
        {
            var student = new Student
            {
                Name = Name,
                StudentRollNumber = StudentRollNumber
            };

            _studentService.CreateStudent(student);
        }
    }
}
