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
    public class ProfileRepo : IProfileRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public ProfileCustomModel GetById(int Id)
        {
            ProfileCustomModel objListModel = new ProfileCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblUserLogins.Where(x => x.IsDeleted == false && x.Id == Id)
                        .Select(x => new ProfileCustomModel
                        {
                            Id = x.Id,
                            UserTypeId = x.UserTypeId,
                            EmployeeId = x.EmployeeId,
                            FName = x.FName,
                            LName = x.LName,
                            EmailId = x.EmailId,
                            Mobile = x.Mobile,
                            UserName = x.UserName,
                            Password = x.Password,
                                    
                            IsActive = x.IsActive,
                            IsDeleted = x.IsDeleted,
                            CreatedBy = x.CreatedBy,
                            CreatedDate = x.CreatedDate,
                            ModifiedBy = x.ModifiedBy,
                            ModifiedDate = x.ModifiedDate
                        }).SingleOrDefault();

                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        objListModel = null;
                    }
                }
            }
            return objListModel;
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
