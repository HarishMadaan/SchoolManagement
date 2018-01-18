using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.DAL.Interfaces
{
    public interface IFeeMasterRepo : IDisposable
    {
        FeeMasterCustomModel GetById(int Id);

        object GetFeeMasterListing(FeeMasterCustomModel objModel);

        Response SaveFeeMasterDetails(FeeMasterCustomModel objModel);

        object SetActiveFeeRegistrationDetail(FeeMasterCustomModel objModel);

        object DeleteFeeRegistrationDetail(FeeMasterCustomModel objModel);

        bool FindById(int Id);
    }
}
