using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface IAdmissionFeeMasterBusiness : IDisposable
    {
        AdmissionFeeMasterCustomModel GetById(int Id);

        object GetAdmissionFeeMasterListing(AdmissionFeeMasterCustomModel objModel);

        Response SaveAdmissionFeeMasterDetails(AdmissionFeeMasterCustomModel objModel);

        object SetActiveAdmissionFeeRegistrationDetail(AdmissionFeeMasterCustomModel objModel);

        object DeleteAdmissionFeeRegistrationDetail(AdmissionFeeMasterCustomModel objModel);

        bool FindById(int Id);
    }
}
