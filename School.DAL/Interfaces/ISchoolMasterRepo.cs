using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.DAL.Interfaces
{
    public interface ISchoolMasterRepo : IDisposable
    {  
        object GetSchoolMasterListing(SchoolMasterCustomModel objSchoolRegistrationModel);

        Response SaveSchoolMasterDetails(SchoolMasterCustomModel objModel);

        object SetActiveSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel);

        object DeleteSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel);

    }
}
