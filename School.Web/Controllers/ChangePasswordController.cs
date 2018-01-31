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

namespace School.Web.Controllers
{
    [SessionExpire]
    public class ChangePasswordController : Controller
    {
        IChangePasswordBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ChangePasswordCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();

                    // TODO: Add update logic here
                    objBDC = new ChangePasswordBusiness();
                    _Result = objBDC.UpdatePassword(objModel);

                    if (_Result.success == true && _Result.message == "1")
                        TempData["Message"] = "Success^" + "Password changed successfully!";
                    else if (_Result.success == false && _Result.message == "2")
                        TempData["Message"] = "Error^" + "Password does not matched";
                    else if (_Result.success == false && _Result.message == "3")
                        TempData["Message"] = "Error^" + "UserName does not exists";

                    return View(); // RedirectToAction("Index");
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

        // GET: ChangePassword/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChangePassword/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChangePassword/Create
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

        // GET: ChangePassword/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChangePassword/Edit/5
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

        // GET: ChangePassword/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChangePassword/Delete/5
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
