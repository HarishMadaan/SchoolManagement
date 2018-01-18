using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.Shared.CommonFunc;
using School.DAL.Interfaces;

namespace School.DAL.Interfaces
{
    public interface IUserLoginRepo : IDisposable
    {
        UserLoginCustomModel Authenticate(string UserName, string Password);
        int? GetSessionID();
    }
}
