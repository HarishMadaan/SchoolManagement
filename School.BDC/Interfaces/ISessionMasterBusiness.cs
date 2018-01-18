using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface ISessionMasterBusiness : IDisposable
    {
        SessionMasterCustomModel GetById(int Id);

        object GetSessionMasterListing(SessionMasterCustomModel objModel);

        Response SaveSessionMasterDetails(SessionMasterCustomModel objModel);

        object SetDefaultSessionRegistrationDetail(SessionMasterCustomModel objModel);

        object SetActiveSessionRegistrationDetail(SessionMasterCustomModel objModel);

        object DeleteSessionRegistrationDetail(SessionMasterCustomModel objModel);

        bool FindById(int Id);
    }
}
