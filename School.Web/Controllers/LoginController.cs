using School.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Shared.CustomModels;
using School.Web;
using School.BDC.Interfaces;
using School.BDC.Classes;
using System.Web.Security;

namespace School.Web.Controllers
{
    public class LoginController : Controller
    {
        IUserLoginBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;
        UserLoginCustomModel _objLoginCustom = new UserLoginCustomModel();

        public ActionResult Index()
        {
            return View();            
        }

        // GET: Login
        public ActionResult Login()
        {
            if (Session[CommonStrings.UserSession] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(UserLoginCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result2 = new Response();
                    UserLoginCustomModel _Result = new UserLoginCustomModel();
                    // TODO: Add insert logic here
                    objBDC = new UserLoginBusiness();

                    string UserName = "";
                    string Password = "";
                    UserName = objModel.UserName;
                    Password = objModel.Password;

                    _Result = objBDC.Authenticate(UserName, Password);
                                        
                    if (_Result.Id > 0)
                    {
                        _objLoginCustom.Id = _Result.Id;
                        _objLoginCustom.FName = _Result.FName;
                        _objLoginCustom.LName = _Result.LName;
                        _objLoginCustom.EmailId = _Result.EmailId;
                        _objLoginCustom.UserName = _Result.UserName;
                        
                        Session[CommonStrings.UserSession] = _objLoginCustom;

                        int? _DefaultSessionId = objBDC.GetSessionID();
                        Session[CommonStrings.DefaultSession] = _DefaultSessionId;

                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        TempData["Message"] = "Error^" + "Either username or password is not valid!";
                        
                        return View();
                    }
                    //TempData["Success"] = "Added Successfully!";

                    //if (_Result.success == true)
                    //    TempData["Message"] = "Success^" + _Result.message;
                    //else if (_Result.success == false)
                    //    TempData["Message"] = "Error^" + _Result.message;

                    //return RedirectToAction("Login", "Login");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        #region 'Logout'
        [SessionExpire]
        public ActionResult Logout()
        {

            //Self Profiling Done Activity Logging
            //Int32 userID = (int)((LoginCustomModel)Session[CommonStrings.UserSession]).UserId;
            //UserLastActivityEntity objActivity = CommonDBMethods.GetUserLastActivityDetails(userID);
            //objActivity.UserID = userID;
            //CommonMethods.SetUserLastActivityDetails("Login" + '-' + "Logout", ref objActivity);
            //CommonDBMethods.UpdateUserLastActivityDetails(objActivity);


            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            return RedirectToAction("Login", "Login");
        }
        #endregion

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
