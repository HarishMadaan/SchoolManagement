using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.Shared.CommonFunc;
using School.DAL.Interfaces;

namespace School.DAL.Repositories
{
    public class DashboardMasterRepo : IDashboardMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public object GetDashboardEnquiryCount(int? SessionId, int? ClassId)
        {
            List<USP_GetSchoolEnquiryAdmissionCount_Result> objListDashboard = new List<USP_GetSchoolEnquiryAdmissionCount_Result>();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        objListDashboard = dbcontext.USP_GetSchoolEnquiryAdmissionCount(SessionId, ClassId).ToList();
                        return objListDashboard;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        public void Dispose()
        {
            dbcontext.Dispose();
            if (response != null)
            {
                response.Dispose();
            }

            GC.SuppressFinalize(this);
        }

    }
}
