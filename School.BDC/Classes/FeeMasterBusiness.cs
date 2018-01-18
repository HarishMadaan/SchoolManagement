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
    public class FeeMasterBusiness : IFeeMasterBusiness
    {
        IFeeMasterRepo objDAL;

        public FeeMasterCustomModel GetById(int Id)
        {
            using (objDAL = new FeeMasterRepo())
            {
                return objDAL.GetById(Id);
            }
        }

        public object GetFeeMasterListing(FeeMasterCustomModel objModel)
        {
            using (objDAL = new FeeMasterRepo())
            {
                return objDAL.GetFeeMasterListing(objModel);
            }
        }

        public Response SaveFeeMasterDetails(FeeMasterCustomModel objModel)
        {
            using (objDAL = new FeeMasterRepo())
            {
                return objDAL.SaveFeeMasterDetails(objModel);
            }
        }

        public object SetActiveFeeRegistrationDetail(FeeMasterCustomModel objModel)
        {
            using (objDAL = new FeeMasterRepo())
            {
                return objDAL.SetActiveFeeRegistrationDetail(objModel);
            }
        }

        public object DeleteFeeRegistrationDetail(FeeMasterCustomModel objModel)
        {
            using (objDAL = new FeeMasterRepo())
            {
                return objDAL.DeleteFeeRegistrationDetail(objModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new FeeMasterRepo())
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
