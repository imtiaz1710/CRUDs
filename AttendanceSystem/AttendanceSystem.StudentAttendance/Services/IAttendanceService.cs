using AttendanceSystem.StudentAttendance.BusinessObjects;
using System.Collections.Generic;

namespace AttendanceSystem.StudentAttendance.Services
{
    public interface IAttendanceService
    {
        void CreateAttendance(Attendance attendance);
        void DeleteAttendance(int id);
        IList<Attendance> GetAllAttendances();
        Attendance GetAttendance(int id);

        (IList<Attendance> records, int total, int totalDisplay) GetAttendances(
            int pageIndex, int pageSize, string sortText, string searchText);

        void UpdateAttendance(Attendance attendance);
    }
}