using AttendanceSystem.StudentAttendance.BusinessObjects;
using AttendanceSystem.StudentAttendance.Exceptions;
using AttendanceSystem.StudentAttendance.UnitOfWorks;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.StudentAttendance.Services
{
    public class AttendanceService : IAttendanceService
    {
        public readonly IAttendanceUnitOfWork _attendanceUnitOfWork;
        public readonly IMapper _mapper;

        public AttendanceService(IAttendanceUnitOfWork attendanceUnitOfWork,
            IMapper mapper)
        {
            _attendanceUnitOfWork = attendanceUnitOfWork;
            _mapper = mapper;
        }

        public Attendance GetAttendance(int id)
        {
            var attendance = _attendanceUnitOfWork.Attendances.GetById(id);
            if (attendance == null) return null;

            return _mapper.Map<Attendance>(attendance);
        }

        public IList<Attendance> GetAllAttendances()
        {
            var attendanceEntities = _attendanceUnitOfWork.Attendances.GetAll();
            var attendances = new List<Attendance>();

            foreach (var attendance in attendanceEntities)
            {
                attendances.Add(_mapper.Map<Attendance>(attendance));
            }

            return attendances;
        }

        public (IList<Attendance> records, int total, int totalDisplay) GetAttendances(
            int pageIndex, int pageSize, string sortText, string searchText)
        {
            var attendanceData = _attendanceUnitOfWork.Attendances.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : a => a.Date.ToString().Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var records = (from attendance in attendanceData.data
                           select _mapper.Map<Attendance>(attendance)).ToList();

            return (records, attendanceData.total, attendanceData.totalDisplay);
        }

        public void CreateAttendance(Attendance attendance)
        {
            if (attendance == null)
                throw new NullReferenceException("Object must not be null");

            if (IsSameRecordAlreadyExist(attendance.StudentId, attendance.Date, attendance.Id))
                throw new DuplicateValueException("You should not enter same attendance twice");

            _attendanceUnitOfWork.Attendances.Add(_mapper.Map<StudentAttendance.Entities.Attendance>(attendance));
            _attendanceUnitOfWork.Save();
        }

        public void UpdateAttendance(Attendance attendance)
        {
            if (attendance == null)
                throw new NullReferenceException("Object must not be null");

            if (IsSameRecordAlreadyExist(attendance.StudentId, attendance.Date, attendance.Id))
                throw new DuplicateValueException("You should not enter same attendance twice");

            var attendanceEntity = _attendanceUnitOfWork.Attendances.GetById(attendance.Id);

            if (attendanceEntity != null)
            {
                _mapper.Map(attendance, attendanceEntity);
                _attendanceUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Could not find attendance");
        }

        public void DeleteAttendance(int id)
        {

            var attendanceEntity = _attendanceUnitOfWork.Attendances.GetById(id);

            if (attendanceEntity != null)
            {
                _attendanceUnitOfWork.Attendances.Remove(attendanceEntity);
                _attendanceUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Could not find attendance");
        }

        private bool IsSameRecordAlreadyExist(int studentId, DateTime Date) =>
            _attendanceUnitOfWork.Attendances.GetCount(a =>
            ((a.StudentId == studentId) && (a.Date.Date == Date.Date))) > 0;

        private bool IsSameRecordAlreadyExist(int studentId, DateTime Date, int id) =>
            _attendanceUnitOfWork.Attendances.GetCount(a =>
            (a.StudentId == studentId) && (a.Date.Date == Date.Date) && (a.Id != id)) > 0;

    }
}
