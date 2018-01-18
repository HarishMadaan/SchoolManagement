using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface IClassMasterBusiness : IDisposable
    {
        ClassMasterCustomModel GetById(int Id);

        object GetClassMasterListing(ClassMasterCustomModel objClassRegistrationModel);

        Response SaveClassMasterDetails(ClassMasterCustomModel objModel);

        object SetActiveClassRegistrationDetail(ClassMasterCustomModel objClassRegistrationModel);

        object DeleteClassRegistrationDetail(ClassMasterCustomModel objClassRegistrationModel);

        bool FindById(int Id);

    }
}
