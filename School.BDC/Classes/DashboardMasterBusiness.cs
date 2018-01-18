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
    public class DashboardMasterBusiness : IDashboardMasterBusiness
    {
        IDashboardMasterRepo objDAL;

        public object GetDashboardEnquiryCount(int? SessionId, int? ClassId)
        {
            using (objDAL = new DashboardMasterRepo())
            {
                return objDAL.GetDashboardEnquiryCount(SessionId, ClassId);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
