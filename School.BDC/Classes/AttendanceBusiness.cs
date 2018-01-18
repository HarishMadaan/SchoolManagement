using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.BDC.Interfaces;
using School.DAL.Interfaces;
using School.DAL.Repositories;

namespace School.BDC.Classes
{
    public class AttendanceBusiness : IAttendanceBusiness
    {
        IAttendanceRepo objDAL;

        public Response GetAttendanceListing(AttendanceCustomModel objModel)
        {
            using (objDAL = new AttendanceRepo())
            {
                return objDAL.GetAttendanceListing(objModel);
            }
        }

        public Response SaveAttendanceDetails(AttendanceCustomModel objModel)
        {
            using (objDAL = new AttendanceRepo())
            {
                return objDAL.SaveAttendanceDetails(objModel);
            }
        }


        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
