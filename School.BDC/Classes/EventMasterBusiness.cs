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
    public class EventMasterBusiness : IEventMasterBusiness
    {
        IEventMasterRepo objDAL;

        public object GetEventMasterListing(EventMasterCustomModel objEventRegistrationModel)
        {
            using (objDAL = new EventMasterRepo())
            {
                return objDAL.GetEventMasterListing(objEventRegistrationModel);
            }
        }

        public Response SaveEventMasterDetails(EventMasterCustomModel objModel)
        {
            using (objDAL = new EventMasterRepo())
            {
                return objDAL.SaveEventMasterDetails(objModel);
            }
        }

        public object SetActiveEventRegistrationDetail(EventMasterCustomModel objEventRegistrationModel)
        {
            using (objDAL = new EventMasterRepo())
            {
                return objDAL.SetActiveEventRegistrationDetail(objEventRegistrationModel);
            }
        }

        public object DeleteEventRegistrationDetail(EventMasterCustomModel objEventRegistrationModel)
        {
            using (objDAL = new EventMasterRepo())
            {
                return objDAL.DeleteEventRegistrationDetail(objEventRegistrationModel);
            }
        }
        
        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
