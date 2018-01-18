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
    public class UserLoginRepo : IUserLoginRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        #region 'Authenticates User'
        /// <summary>
        /// Authenticate the Login User
        /// </summary>
        /// <param name="_userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserLoginCustomModel Authenticate(string UserName, string Password)
        {
            UserLoginCustomModel objModel = new UserLoginCustomModel();
            string IsSuccess = "false";
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        {
                            if (UserName.Contains("@"))
                            {
                                var UserDetails = dbcontext.tblUserLogins.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
                                if (UserDetails != null)
                                {
                                    objModel.Id = UserDetails.Id;
                                    objModel.FName = UserDetails.FName;
                                    objModel.LName = UserDetails.LName;
                                    objModel.EmailId = UserDetails.EmailId;
                                    objModel.UserName = UserDetails.UserName;
                                    objModel.Password = UserDetails.Password;

                                    IsSuccess = "true";
                                }
                                else
                                {
                                    IsSuccess = "false";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        objModel = null;
                    }
                }
            }                    
            return objModel;
        }
        #endregion

        public int? GetSessionID()
        {
            int? intSessionId = null;
            using (dbcontext = new SchoolManagementEntities())
            {
                try
                {
                    intSessionId = (from obj in dbcontext.tblSessions.Where(x => x.IsActive == true && x.IsDefault == true)
                                    select obj.SessionId).Take(1).SingleOrDefault();
                }
                catch(Exception ex)
                {

                }
                return intSessionId;
            }                       
        }

        #region 'Maintain User Session'
        /// <summary>
        /// Maintains user session After login
        /// </summary>
        /// <param name="_userName"></param>
        /// <param name="password"></param>
        /// <param name="objCustomLogin"></param>
        /// <param name="UICulture"></param>
        /// <returns></returns>
        //public object MaintainUsersession(string _userName, string password)
        //{

        //    tblUserLogin objTask = new tblUserLogin();
        //    objTask = _databaseContext.tblUserLogins.Where(id => id.Email == _userName && id.PasswordHash == password).FirstOrDefault();
        //    // Fill details in UserCustomModel for using in session
        //    objCustomLogin.UserId = Convert.ToInt32(objTask.ID);
        //    objCustomLogin.UserName = objTask.Username;
        //    //to br chngd objCustomLogin.UserPassword = CheckLogin.UserPassword;
        //    objCustomLogin.RoleId = objTask.UserType;

        //    return objTask;
        //}
        #endregion

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
