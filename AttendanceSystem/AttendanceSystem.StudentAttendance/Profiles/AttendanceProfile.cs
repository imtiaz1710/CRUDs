using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = AttendanceSystem.StudentAttendance.BusinessObjects;
using EO = AttendanceSystem.StudentAttendance.Entities;

namespace AttendanceSystem.StudentAttendance.Profiles
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<EO.Attendance, BO.Attendance>().ReverseMap();
            CreateMap<EO.Student, BO.Student>().ReverseMap();
        }
    }
}
