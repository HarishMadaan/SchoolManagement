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
    public class SchoolMasterBusiness : ISchoolMasterBusiness
    {
        ISchoolMasterRepo objDAL;

        /// <summary>
        /// This method is used to fetch All Kisan Market
        /// </summary>
        /// <returns></returns>
        /// 
        public object GetSchoolMasterListing(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            using (objDAL = new SchoolMasterRepo())
            {
                return objDAL.GetSchoolMasterListing(objSchoolRegistrationModel);
            }
        }

        public Response SaveSchoolMasterDetails(SchoolMasterCustomModel objModel)
        {
            using (objDAL = new SchoolMasterRepo())
            {
                return objDAL.SaveSchoolMasterDetails(objModel);
            }
        }


        public object SetActiveSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            using (objDAL = new SchoolMasterRepo())
            {
                return objDAL.SetActiveSchoolRegistrationDetail(objSchoolRegistrationModel);
            }
        }

        public object DeleteSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            using (objDAL = new SchoolMasterRepo())
            {
                return objDAL.DeleteSchoolRegistrationDetail(objSchoolRegistrationModel);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
