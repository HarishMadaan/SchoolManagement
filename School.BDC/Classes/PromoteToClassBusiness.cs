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
    public class PromoteToClassBusiness : IPromoteToClassBusiness
    {
        IPromoteToClassRepo objDAL;

        public object BindSessionClassEnquiry(int? SessionId, int? ClassId, int? SectionId, string StudentName)
        {
            using (objDAL = new PromoteToClassRepo())
            {
                return objDAL.BindSessionClassEnquiry(SessionId, ClassId, SectionId, StudentName);
            }
        }

        public Response SavePromoteToClassDetails(PromoteToClassCustomModel objModel)
        {
            using (objDAL = new PromoteToClassRepo())
            {
                return objDAL.SavePromoteToClassDetails(objModel);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
