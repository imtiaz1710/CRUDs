using AttendanceSystem.Models;
using Autofac;
using System.Linq;
using AttendanceSystem.StudentAttendance.Services;

namespace AttendanceSystem.Areas.Admin.Models.StudentModel
{
    public class StudentListModel
    {
        private IStudentService _studentService;

        public StudentListModel()
        {
            _studentService = Startup.AutofacContainer.Resolve<IStudentService>();
        }

        public StudentListModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        internal object GetStudents(DataTablesAjaxRequestModel DataTableReqest)
        {
            var data = _studentService.GetStudents(
                    DataTableReqest.PageIndex,
                    DataTableReqest.PageSize,
                    DataTableReqest.GetSortText(new string[] {"Id", "Name", "StudentRollNumber" }),
                    DataTableReqest.SearchText);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Id.ToString(),
                            record.Name,
                            record.StudentRollNumber.ToString(),
                            record.Id.ToString()
                        }).ToList()
            };
        }

        internal void Delete(int id)
        {
            _studentService.DeleteStudent(id);
        }
    }   
}
