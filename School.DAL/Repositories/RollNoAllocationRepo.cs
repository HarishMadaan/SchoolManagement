using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.DAL.Interfaces;

namespace School.DAL.Repositories
{
    public class RollNoAllocationRepo : IRollNoAllocationRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public Response UpdateStudentRollNoAllocation(PromoteToClassCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;                        
                        var rs = dbcontext.tblPromoteToClasses.FirstOrDefault(x => x.IsDeleted == false && x.IsActive == true && x.PromoteToClassId == objModel.PromoteToClassId);
                        if (rs != null)
                        {
                            rs.RollNo = objModel.RollNo;

                            rs.ModifiedBy = objModel.ModifiedBy;
                            rs.ModifiedDate = DateTime.Now;
                            
                            dbcontext.SaveChanges();
                            response.responseData = new { PromoteToClassId = objModel.PromoteToClassId };
                            response.message = "Record Added Successfully!";
                        }
                        else
                        {
                            response.message = "Record Not Exists!";
                        }
                        
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        public void Dispose()
        {
            dbcontext.Dispose();
            if (response != null)
            {
                response.Dispose();
            }

            GC.SuppressFinalize(this);
        }

    }
}
