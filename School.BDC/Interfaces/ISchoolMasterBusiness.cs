using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface ISchoolMasterBusiness : IDisposable
    {
        object GetSchoolMasterListing(SchoolMasterCustomModel objSchoolRegistrationModel);

        Response SaveSchoolMasterDetails(SchoolMasterCustomModel objModel);

        object SetActiveSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel);

        object DeleteSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel);

    }
}
