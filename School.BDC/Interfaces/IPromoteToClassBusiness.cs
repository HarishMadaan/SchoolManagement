using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface IPromoteToClassBusiness : IDisposable
    {
        object BindSessionClassEnquiry(int? SessionId, int? ClassId, int? SectionId, string StudentName);

        Response SavePromoteToClassDetails(PromoteToClassCustomModel objModel);
    }
}
