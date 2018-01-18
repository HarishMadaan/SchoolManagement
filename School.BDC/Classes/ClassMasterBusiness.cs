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
    public class ClassMasterBusiness : IClassMasterBusiness
    {
        IClassMasterRepo objDAL;

        public ClassMasterCustomModel GetById(int Id)
        {
            using (objDAL = new ClassMasterRepo())
            {
                return objDAL.GetById(Id);
            }
        }

        public object GetClassMasterListing(ClassMasterCustomModel objClassRegistrationModel)
        {
            using (objDAL = new ClassMasterRepo())
            {
                return objDAL.GetClassMasterListing(objClassRegistrationModel);
            }
        }

        public Response SaveClassMasterDetails(ClassMasterCustomModel objModel)
        {
            using (objDAL = new ClassMasterRepo())
            {
                return objDAL.SaveClassMasterDetails(objModel);
            }
        }

        public object SetActiveClassRegistrationDetail(ClassMasterCustomModel objClassRegistrationModel)
        {
            using (objDAL = new ClassMasterRepo())
            {
                return objDAL.SetActiveClassRegistrationDetail(objClassRegistrationModel);
            }
        }

        public object DeleteClassRegistrationDetail(ClassMasterCustomModel objClassRegistrationModel)
        {
            using (objDAL = new ClassMasterRepo())
            {
                return objDAL.DeleteClassRegistrationDetail(objClassRegistrationModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new ClassMasterRepo())
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
