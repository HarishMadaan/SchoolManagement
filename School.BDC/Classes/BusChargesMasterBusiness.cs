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
    public class BusChargesMasterBusiness : IBusChargesMasterBusiness
    {
        IBusChargesMasterRepo objDAL;

        public BusChargesMasterCustomModel GetById(int Id)
        {
            using (objDAL = new BusChargesMasterRepo())
            {
                return objDAL.GetById(Id);
            }
        }

        public object GetBusChargesMasterListing(BusChargesMasterCustomModel objModel)
        {
            using (objDAL = new BusChargesMasterRepo())
            {
                return objDAL.GetBusChargesMasterListing(objModel);
            }
        }

        public Response SaveBusChargesMasterDetails(BusChargesMasterCustomModel objModel)
        {
            using (objDAL = new BusChargesMasterRepo())
            { 
                return objDAL.SaveBusChargesMasterDetails(objModel);
            }
        }

        public object SetActiveBusChargesRegistrationDetail(BusChargesMasterCustomModel objModel)
        {
            using (objDAL = new BusChargesMasterRepo())
            {
                return objDAL.SetActiveBusChargesRegistrationDetail(objModel);
            }
        }

        public object DeleteBusChargesRegistrationDetail(BusChargesMasterCustomModel objModel)
        {
            using (objDAL = new BusChargesMasterRepo())
            {
                return objDAL.DeleteBusChargesRegistrationDetail(objModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new BusChargesMasterRepo())
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
