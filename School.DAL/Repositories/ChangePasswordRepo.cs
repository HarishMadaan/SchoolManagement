using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.Shared.CommonFunc;
using School.DAL.Interfaces;

namespace School.DAL.Repositories
{
    public class ChangePasswordRepo : IChangePasswordRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public Response UpdatePassword(ChangePasswordCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;                        
                        var rs = dbcontext.tblUserLogins.FirstOrDefault(x => x.IsDeleted == false && x.UserName == objModel.UserName);
                        if (rs != null)
                        {
                            var objUpdate = dbcontext.tblUserLogins.FirstOrDefault(m => m.IsDeleted == false && m.Password == objModel.Password);
                            if (objUpdate != null)
                            {
                                objUpdate.Password = objModel.NewPassword;
                                
                                objUpdate.ModifiedBy = objModel.ModifiedBy;
                                objUpdate.ModifiedDate = DateTime.Now;
                                dbcontext.SaveChanges();
                                response.responseData = new { SectionId = objUpdate.Id};
                                response.success = true;
                                response.message = "1";
                            }
                            else
                            {
                                response.success = false;
                                response.message = "2";
                            }
                        }
                        else
                        {
                            response.success = false;
                            response.message = "3";
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
