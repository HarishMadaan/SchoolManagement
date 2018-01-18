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
    public class SessionMasterBusiness : ISessionMasterBusiness
    {
        ISessionMasterRepo objDAL;

        public SessionMasterCustomModel GetById(int Id)
        {
            using (objDAL = new SessionMasterRepo())
            {
                return objDAL.GetById(Id);
            }
        }

        public object GetSessionMasterListing(SessionMasterCustomModel objModel)
        {
            using (objDAL = new SessionMasterRepo())
            {
                return objDAL.GetSessionMasterListing(objModel);
            }
        }

        public Response SaveSessionMasterDetails(SessionMasterCustomModel objModel)
        {
            using (objDAL = new SessionMasterRepo())
            {
                return objDAL.SaveSessionMasterDetails(objModel);
            }
        }

        public object SetDefaultSessionRegistrationDetail(SessionMasterCustomModel objModel)
        {
            using (objDAL = new SessionMasterRepo())
            {
                return objDAL.SetDefaultSessionRegistrationDetail(objModel);
            }
        }

        public object SetActiveSessionRegistrationDetail(SessionMasterCustomModel objModel)
        {
            using (objDAL = new SessionMasterRepo())
            {
                return objDAL.SetActiveSessionRegistrationDetail(objModel);
            }
        }

        public object DeleteSessionRegistrationDetail(SessionMasterCustomModel objModel)
        {
            using (objDAL = new SessionMasterRepo())
            {
                return objDAL.DeleteSessionRegistrationDetail(objModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new SessionMasterRepo())
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
