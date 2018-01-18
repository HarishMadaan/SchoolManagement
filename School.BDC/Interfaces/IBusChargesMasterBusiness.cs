using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface IBusChargesMasterBusiness : IDisposable
    {
        BusChargesMasterCustomModel GetById(int Id);

        object GetBusChargesMasterListing(BusChargesMasterCustomModel objModel);

        Response SaveBusChargesMasterDetails(BusChargesMasterCustomModel objModel);

        object SetActiveBusChargesRegistrationDetail(BusChargesMasterCustomModel objModel);

        object DeleteBusChargesRegistrationDetail(BusChargesMasterCustomModel objModel);

        bool FindById(int Id);
    }
}
