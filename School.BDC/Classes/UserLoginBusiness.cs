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
    public class UserLoginBusiness : IUserLoginBusiness
    {
        IUserLoginRepo objDAL;

        public UserLoginCustomModel Authenticate(string UserName, string Password)
        {
            using (objDAL = new UserLoginRepo())
            {
                return objDAL.Authenticate(UserName, Password);
            }
        }

        public int? GetSessionID()
        {
            using (objDAL = new UserLoginRepo())
            {
                return objDAL.GetSessionID();
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
