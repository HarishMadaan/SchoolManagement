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
    public class ClassController : Controller
    {
        IClassMasterBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        //
        // GET: /Class/
        public ActionResult Index()
        {
            ViewBag.SessionValue = Session[CommonStrings.DefaultSession].ToString();

            SessionMasterModel objSessionModel = new SessionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            ClassMasterCustomModel objModel = new ClassMasterCustomModel();
            objBDC = new ClassMasterBusiness();
            objModel.SessionId = Convert.ToInt32(Session[CommonStrings.DefaultSession]);
            var rs = objBDC.GetClassMasterListing(objModel);
            
            return View(rs);
        }

        //
        // GET: /Class/Details/5
        public ActionResult Details(int id)
        {
            if (id != 0)
            {
                objBDC = new ClassMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Class/Create
        public ActionResult Create(int id = 0)
        {
            ClassMasterCustomModel objModel = new ClassMasterCustomModel();

            objBDCCommon = new CommonMasterDataBusiness();
                        
            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            if (id != 0)
            {
                objBDC = new ClassMasterBusiness();
                objModel = objBDC.GetById(id);

                ViewBag.SessionValue = Convert.ToString(objModel.SessionId);
            }
            else
            {
                ViewBag.SessionValue = Session[CommonStrings.DefaultSession].ToString();
            }

            return View(objModel);
        }

        //
        // POST: /Class/Create
        [HttpPost]
        public ActionResult Create(ClassMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();
                    // TODO: Add insert logic here
                    objBDC = new ClassMasterBusiness();
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    _Result = objBDC.SaveClassMasterDetails(objModel);

                    //TempData["Success"] = "Added Successfully!";

                    if (_Result.success == true)
                        TempData["Message"] = "Success^" + _Result.message;
                    else if (_Result.success == false)
                        TempData["Message"] = "Error^" + _Result.message;

                    return RedirectToAction("Index");
                }
                else
                {
                    objBDCCommon = new CommonMasterDataBusiness();

                    SessionMasterModel objSessionModel = new SessionMasterModel();
                    var SessionType = objBDCCommon.GetSessionMaster();
                    objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
                    ViewBag.SessionInfo = objSessionModel.SessionList;

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
        // GET: /Class/Edit/5
        public ActionResult Edit(int id)
        {
            objBDCCommon = new CommonMasterDataBusiness();

            SessionMasterModel objModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objModel.SessionList;
            
            objBDC = new ClassMasterBusiness();
            var rs = objBDC.GetById(id);

            ViewBag.SessionValue = Session[CommonStrings.DefaultSession].ToString();

            return View(rs);
        }

        //
        // POST: /Class/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClassMasterCustomModel objModel)
        {
            try
            {
                // TODO: Add update logic here
                objBDC = new ClassMasterBusiness();
                objModel.ClassId = id;
                objBDC.SaveClassMasterDetails(objModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Class/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Class/Delete/5
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
            objBDC = new ClassMasterBusiness();
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

            ClassMasterCustomModel objModel = new ClassMasterCustomModel();
            objModel.ClassId = Id;
            objBDC.SetActiveClassRegistrationDetail(objModel);
            
            return Json(new { result = _Result });
        }

        public JsonResult DeleteStatus(string id)
        {
            objBDC = new ClassMasterBusiness();
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

            ClassMasterCustomModel objModel = new ClassMasterCustomModel();
            objModel.ClassId = Id;
            objBDC.DeleteClassRegistrationDetail(objModel);

            return Json(new { result = _Result });
        }

        public ActionResult SearchSessionWiseResult(int? SessionId)
        {
            ClassMasterCustomModel objModel = new ClassMasterCustomModel();
            objBDC = new ClassMasterBusiness();
            objModel.SessionId = SessionId;
            var rs = objBDC.GetClassMasterListing(objModel);

            @ViewBag.TotalStudents = rs.ToString().Count();
            return PartialView(rs);
        }

    }
}
