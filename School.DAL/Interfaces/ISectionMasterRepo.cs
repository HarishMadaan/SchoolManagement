using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.DAL.Interfaces
{
    public interface ISectionMasterRepo : IDisposable
    {
        SectionMasterCustomModel GetById(int Id);

        object GetSectionMasterListing(SectionMasterCustomModel objSectionRegistrationModel);

        Response SaveSectionMasterDetails(SectionMasterCustomModel objModel);

        object SetActiveSectionRegistrationDetail(SectionMasterCustomModel objSectionRegistrationModel);

        object DeleteSectionRegistrationDetail(SectionMasterCustomModel objSectionRegistrationModel);

        bool FindById(int Id);
        

    }
}
