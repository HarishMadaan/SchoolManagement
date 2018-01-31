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
    public class SectionController : Controller
    {
        ISectionMasterBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        //
        // GET: /Section/
        public ActionResult Index()
        {
            SessionMasterModel objSessionModel = new SessionMasterModel();
            ClassMasterModel objClassModel = new ClassMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
            objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objClassModel.ClassList;

            SectionMasterCustomModel objModel = new SectionMasterCustomModel();
            objBDC = new SectionMasterBusiness();
            objModel.SessionId = Convert.ToInt32(Session[CommonStrings.DefaultSession]);
            var rs = objBDC.GetSectionMasterListing(objModel);

            ViewBag.SessionValue = Session[CommonStrings.DefaultSession].ToString();

            return View(rs);
        }

        //
        // GET: /Section/Details/5
        public ActionResult Details(int id)
        {
            if (id != 0)
            {
                objBDC = new SectionMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Section/Create
        public ActionResult Create(int id = 0)
        {
            SectionMasterCustomModel objModel = new SectionMasterCustomModel();
            ClassMasterModel objClassModel = new ClassMasterModel();
            SessionMasterModel objSessionModel = new SessionMasterModel();

            objBDCCommon = new CommonMasterDataBusiness();

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;
            
            if (id > 0)
            {
                objBDC = new SectionMasterBusiness();
                objModel = objBDC.GetById(id);

                var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(objModel.SessionId));
                objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
                ViewBag.ClassInfo = objClassModel.ClassList;

                ViewBag.ClassValue = Convert.ToString(objModel.ClassId);
                ViewBag.SessionValue = Convert.ToString(objModel.SessionId);
            }
            else
            {
                var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
                objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
                ViewBag.ClassInfo = objClassModel.ClassList;

                ViewBag.SessionValue = Session[CommonStrings.DefaultSession].ToString();
            }

            return View(objModel);
        }

        //
        // POST: /Section/Create
        [HttpPost]
        public ActionResult Create(SectionMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();
                    // TODO: Add insert logic here
                    objBDC = new SectionMasterBusiness();
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    _Result = objBDC.SaveSectionMasterDetails(objModel);

                    if (_Result.success == true)
                        TempData["Message"] = "Success^" + _Result.message;
                    else if (_Result.success == false)
                        TempData["Message"] = "Error^" + _Result.message;

                    return RedirectToAction("Index");
                }
                else
                {
                    ClassMasterModel objClassModel = new ClassMasterModel();
                    SessionMasterModel objSessionModel = new SessionMasterModel();
                    objBDCCommon = new CommonMasterDataBusiness();
                    
                    var SessionType = objBDCCommon.GetSessionMaster();
                    objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
                    ViewBag.SessionInfo = objSessionModel.SessionList;

                    var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
                    objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
                    ViewBag.ClassInfo = objClassModel.ClassList;

                    ViewBag.SessionValue = Session[CommonStrings.DefaultSession].ToString();

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Section/Edit/5
        public ActionResult Edit(int id)
        {
            objBDC = new SectionMasterBusiness();
            var rs = objBDC.GetById(id);


            return View(rs);
        }

        //
        // POST: /Section/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SectionMasterCustomModel objModel)
        {
            try
            {
                // TODO: Add update logic here
                objBDC = new SectionMasterBusiness();
                objModel.SectionId = id;
                objBDC.SaveSectionMasterDetails(objModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Section/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Section/Delete/5
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


        public JsonResult UpdateStatus(string id)
        {
            objBDC = new SectionMasterBusiness();
            int Id = Convert.ToInt32(id);
            bool _Result = objBDC.FindById(Id);
            if (_Result == true)
            {
                _Result = false;
            }
            else
            {
                _Result = true;
            }

            SectionMasterCustomModel objModel = new SectionMasterCustomModel();
            objModel.SectionId = Id;
            objBDC.SetActiveSectionRegistrationDetail(objModel);
            return Json(new { result = _Result });             
        }

        public JsonResult DeleteStatus(string id)
        {
            objBDC = new SectionMasterBusiness();
            int Id = Convert.ToInt32(id);
            bool _Result = objBDC.FindById(Id);
            if (_Result == true)
            {
                _Result = false;
            }
            else
            {
                _Result = true;
            }

            SectionMasterCustomModel objModel = new SectionMasterCustomModel();
            objModel.SectionId = Id;
            objBDC.DeleteSectionRegistrationDetail(objModel);

            return Json(new { result = _Result });
        }

        public ActionResult SearchSessionWiseResult(int? SessionId, int? ClassId)
        {
            SectionMasterCustomModel objModel = new SectionMasterCustomModel();
            objBDC = new SectionMasterBusiness();
            objModel.SessionId = SessionId;
            objModel.ClassId = ClassId;
            var rs = objBDC.GetSectionMasterListing(objModel);

            @ViewBag.TotalStudents = rs.ToString().Count();
            return PartialView(rs);
        }

    }
}
