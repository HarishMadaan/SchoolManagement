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
    public class AdmissionFeeMasterBusiness : IAdmissionFeeMasterBusiness
    {
        IAdmissionFeeMasterRepo objDAL;

        public AdmissionFeeMasterCustomModel GetById(int Id)
        {
            using (objDAL = new AdmissionFeeMasterRepo())
            {
                return objDAL.GetById(Id);
            }
        }

        public object GetAdmissionFeeMasterListing(AdmissionFeeMasterCustomModel objModel)
        {
            using (objDAL = new AdmissionFeeMasterRepo())
            {
                return objDAL.GetAdmissionFeeMasterListing(objModel);
            }
        }

        public Response SaveAdmissionFeeMasterDetails(AdmissionFeeMasterCustomModel objModel)
        {
            using (objDAL = new AdmissionFeeMasterRepo())
            {
                return objDAL.SaveAdmissionFeeMasterDetails(objModel);
            }
        }

        public object SetActiveAdmissionFeeRegistrationDetail(AdmissionFeeMasterCustomModel objModel)
        {
            using (objDAL = new AdmissionFeeMasterRepo())
            {
                return objDAL.SetActiveAdmissionFeeRegistrationDetail(objModel);
            }
        }

        public object DeleteAdmissionFeeRegistrationDetail(AdmissionFeeMasterCustomModel objModel)
        {
            using (objDAL = new AdmissionFeeMasterRepo())
            {
                return objDAL.DeleteAdmissionFeeRegistrationDetail(objModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new AdmissionFeeMasterRepo())
            {
                return objDAL.FindById(Id);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
