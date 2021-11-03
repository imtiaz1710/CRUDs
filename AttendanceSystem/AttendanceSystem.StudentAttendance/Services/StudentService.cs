using AttendanceSystem.StudentAttendance.BusinessObjects;
using AttendanceSystem.StudentAttendance.Contexts;
using AttendanceSystem.StudentAttendance.Exceptions;
using AttendanceSystem.StudentAttendance.Profiles;
using AttendanceSystem.StudentAttendance.UnitOfWorks;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.StudentAttendance.Services
{
    public class StudentService : IStudentService
    {
        public readonly IAttendanceUnitOfWork _attendanceUnitOfWork;
        public readonly IMapper _mapper;

        public StudentService(IAttendanceUnitOfWork attendanceUnitOfWork,
            IMapper mapper)
        {
            _attendanceUnitOfWork = attendanceUnitOfWork;
            _mapper = mapper;
        }

        public Student GetStudent(int id)
        {
            var student = _attendanceUnitOfWork.Students.GetById(id);
            if (student == null) return null;

            return _mapper.Map<Student>(student);
        }

        public IList<Student> GetAllStudents()
        {
            var studentEntities = _attendanceUnitOfWork.Students.GetAll();
            var students = new List<Student>();

            foreach (var student in studentEntities)
            {
                students.Add(_mapper.Map<Student>(student));
            }

            return students;
        }

        public (IList<Student> records, int total, int totalDisplay) GetStudents(
            int pageIndex, int pageSize, string sortText, string searchText)
        {
            var studentData = _attendanceUnitOfWork.Students.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : s => s.Name.Contains(searchText), sortText,
                string.Empty, pageIndex, pageSize);

            var records = (from student in studentData.data
                           select _mapper.Map<Student>(student)).ToList();

            return (records, studentData.total, studentData.totalDisplay);
        }

        public void CreateStudent(Student student)
        {
            if (student == null)
                throw new NullReferenceException("Object must not be null");

            _attendanceUnitOfWork.Students.Add(_mapper.Map<Entities.Student>(student));
            _attendanceUnitOfWork.Save();
        }

        public void UpdateStudent(Student student)
        {
            if (student == null)
                throw new NullReferenceException("Student is not exist");

            var studentEntity = _attendanceUnitOfWork.Students.GetById(student.Id);

            if (studentEntity != null)
            {
                _mapper.Map(student, studentEntity);
                _attendanceUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Could not find student");
        }

        public void DeleteStudent(int id)
        {

            var studentEntity = _attendanceUnitOfWork.Students.GetById(id);

            if (studentEntity != null)
            {
                _attendanceUnitOfWork.Students.Remove(studentEntity);
                _attendanceUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Could not find student");
        }
    }
}
