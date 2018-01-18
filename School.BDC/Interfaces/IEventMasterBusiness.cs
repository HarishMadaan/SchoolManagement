using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface IEventMasterBusiness : IDisposable
    {
        object GetEventMasterListing(EventMasterCustomModel objEventRegistrationModel);

        Response SaveEventMasterDetails(EventMasterCustomModel objModel);

        object SetActiveEventRegistrationDetail(EventMasterCustomModel objEventRegistrationModel);

        object DeleteEventRegistrationDetail(EventMasterCustomModel objEventRegistrationModel);
    }
}
