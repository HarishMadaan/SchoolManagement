using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface IChangePasswordBusiness : IDisposable
    {
        Response UpdatePassword(ChangePasswordCustomModel objModel);

    }
}
