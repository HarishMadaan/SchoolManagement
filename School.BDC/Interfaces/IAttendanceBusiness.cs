using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.BDC.Interfaces;
using School.DAL.Interfaces;
using School.DAL.Repositories;

namespace School.BDC.Interfaces
{
    public interface IAttendanceBusiness : IDisposable
    {
        Response GetAttendanceListing(AttendanceCustomModel objModel);

        Response SaveAttendanceDetails(AttendanceCustomModel objModel);

    }
}
