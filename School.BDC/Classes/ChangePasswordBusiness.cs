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
    public class ChangePasswordBusiness : IChangePasswordBusiness
    {
        IChangePasswordRepo objDAL;

        public Response UpdatePassword(ChangePasswordCustomModel objModel)
        {
            using (objDAL = new ChangePasswordRepo())
            {
                return objDAL.UpdatePassword(objModel);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
