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
    public class EditStudentModel
    {
        public int? Id { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 characters")]
        public string Name { get; set; }
        [Required, Range(1, 1000)]
        public int? StudentRollNumber { get; set; }

        private readonly IStudentService _studentService;

        public EditStudentModel()
        {
            _studentService = Startup.AutofacContainer.Resolve<IStudentService>();
        }

        public EditStudentModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public void LoadModelData(int id)
        {
            var student = _studentService.GetStudent(id);
            Id = student?.Id;
            Name = student?.Name;
            StudentRollNumber = student?.StudentRollNumber;
        }

        public void Update()
        {
            var student = new Student
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                StudentRollNumber = StudentRollNumber.HasValue ? StudentRollNumber.Value : 0
            };

            _studentService.UpdateStudent(student);
        }
    }
}
