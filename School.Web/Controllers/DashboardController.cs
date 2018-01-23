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
    public class DashboardController : Controller
    {
        IDashboardMasterBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session[CommonStrings.DefaultSession] != null)
            {
                ViewBag.DefaultSessionId = Session[CommonStrings.DefaultSession];
            }

            SessionMasterModel objSessionModel = new SessionMasterModel();
            ClassMasterModel objClassModel = new ClassMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
            objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objClassModel.ClassList;

            int? SessionId = null;
            int? ClassId = null;

            objBDC = new DashboardMasterBusiness();
            ViewBag.EnquiryCount = objBDC.GetDashboardEnquiryCount(SessionId, ClassId);

            ViewBag.SessionValue = Session[CommonStrings.DefaultSession].ToString();

            return View();
        }

        public ActionResult SearchSessionWiseResult(int? SessionId, int? ClassId)
        {
            ClassMasterCustomModel objModel = new ClassMasterCustomModel();
            objBDC = new DashboardMasterBusiness();
            objModel.SessionId = SessionId;
            ViewBag.EnquiryCount = objBDC.GetDashboardEnquiryCount(SessionId, ClassId);
            
            //@ViewBag.TotalStudents = rs.ToString().Count();
            return PartialView();
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
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

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
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

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
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
