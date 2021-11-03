using AttendanceSystem.StudentAttendance.BusinessObjects;
using System.Collections.Generic;

namespace AttendanceSystem.StudentAttendance.Services
{
    public interface IStudentService
    {
        void CreateStudent(Student student);
        void DeleteStudent(int id);
        IList<Student> GetAllStudents();
        Student GetStudent(int id);

        (IList<Student> records, int total, int totalDisplay) GetStudents(
            int pageIndex, int pageSize, string sortText, string searchText);

        void UpdateStudent(Student student);
    }
}