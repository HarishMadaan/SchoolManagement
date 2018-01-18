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
    public class RollNoAllocationBusiness : IRollNoAllocationBusiness
    {
        IRollNoAllocationRepo objDAL;

        public Response UpdateStudentRollNoAllocation(PromoteToClassCustomModel objModel)
        {
            using (objDAL = new RollNoAllocationRepo())
            {
                return objDAL.UpdateStudentRollNoAllocation(objModel);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
