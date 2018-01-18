using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface IDashboardMasterBusiness : IDisposable
    {
        object GetDashboardEnquiryCount(int? SessionId, int? ClassId);

    }
}
