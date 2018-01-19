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
    public class AllocationController : Controller
    {
        IEmployeeMasterBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        // GET: Allocation
        public ActionResult Index()
        {
            ClassMasterModel objModel = new ClassMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
            objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objModel.ClassList;

            SessionMasterModel objModel2 = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objModel2.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objModel2.SessionList;

            return View();
        }

        // GET: Allocation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Allocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Allocation/Create
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

        // GET: Allocation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Allocation/Edit/5
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

        // GET: Allocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Allocation/Delete/5
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
