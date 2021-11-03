using AttendanceSystem.Models;
using Autofac;
using System.Linq;
using AttendanceSystem.StudentAttendance.Services;

namespace AttendanceSystem.Areas.Admin.Models.AttendanceModel
{
    public class AttendanceListModel
    {
        private IAttendanceService _attendanceService;

        public AttendanceListModel()
        {
            _attendanceService = Startup.AutofacContainer.Resolve<IAttendanceService>();
        }

        public AttendanceListModel(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        internal object GetAttendances(DataTablesAjaxRequestModel DataTableReqest)
        {
            var data = _attendanceService.GetAttendances(
                    DataTableReqest.PageIndex,
                    DataTableReqest.PageSize,
                    DataTableReqest.GetSortText(new string[] {"StudentId", "Date" }),
                    DataTableReqest.SearchText);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.StudentId.ToString(),
                            record.Date.Date.ToString(),
                            record.Id.ToString()
                        }).ToList()
            };
        }

        internal void Delete(int id)
        {
            _attendanceService.DeleteAttendance(id);
        }

    }
}
