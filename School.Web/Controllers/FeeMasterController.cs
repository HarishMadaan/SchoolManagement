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
    public class FeeMasterController : Controller
    {
        IFeeMasterBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        //
        // GET: /FeeMaster/
        public ActionResult Index()
        {
            SessionMasterModel objSessionModel = new SessionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            FeeMasterCustomModel objModel = new FeeMasterCustomModel();
            objBDC = new FeeMasterBusiness();
            var rs = objBDC.GetFeeMasterListing(objModel);

            return View(rs);
        }

        //
        // GET: /FeeMaster/Details/5
        public ActionResult Details(int id)
        {
            if (id != 0)
            {
                objBDC = new FeeMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /FeeMaster/Create
        public ActionResult Create(int id = 0)
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
            
            FeeMasterCustomModel objModel = new FeeMasterCustomModel();

            if (id != 0)
            {
                objBDC = new FeeMasterBusiness();
                objModel = objBDC.GetById(id);
            }

            return View(objModel);
        }

        //
        // POST: /FeeMaster/Create
        [HttpPost]
        public ActionResult Create(FeeMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();
                    // TODO: Add insert logic here
                    objBDC = new FeeMasterBusiness();
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    _Result = objBDC.SaveFeeMasterDetails(objModel);

                    if (_Result.success == true)
                        TempData["Message"] = "Success^" + _Result.message;
                    else if (_Result.success == false)
                        TempData["Message"] = "Error^" + _Result.message;

                    return RedirectToAction("Index");
                }
                else
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

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FeeMaster/Edit/5
        public ActionResult Edit(int id)
        {
            if (id != 0)
            {
                ClassMasterModel objModel = new ClassMasterModel();
                objBDCCommon = new CommonMasterDataBusiness();

                var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
                objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
                ViewBag.ClassInfo = objModel.ClassList;

                objBDC = new FeeMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /FeeMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FeeMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response response = new Response();
                    // TODO: Add update logic here
                    objBDC = new FeeMasterBusiness();
                    objModel.FeeMasterId = id;
                    response = objBDC.SaveFeeMasterDetails(objModel);

                    if (response.success == false)
                    {
                        return new JavaScriptResult() { Script = "alert('Record already exists');" };
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
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

        //
        // GET: /FeeMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FeeMaster/Delete/5
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
            objBDC = new FeeMasterBusiness();
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

            FeeMasterCustomModel objModel = new FeeMasterCustomModel();
            objModel.FeeMasterId = Id;
            objBDC.SetActiveFeeRegistrationDetail(objModel);
            return Json(new { result = _Result });
        }
        
        public ActionResult DeleteStatus(string id)
        {
            objBDC = new FeeMasterBusiness();
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

            FeeMasterCustomModel objModel = new FeeMasterCustomModel();
            objModel.FeeMasterId = Id;
            objBDC.DeleteFeeRegistrationDetail(objModel);

            return RedirectToAction("Index");
        }

        public ActionResult SearchSessionWiseResult(int? SessionId)
        {
            FeeMasterCustomModel objModel = new FeeMasterCustomModel();
            objBDC = new FeeMasterBusiness();
            objModel.SessionId = SessionId;
            var rs = objBDC.GetFeeMasterListing(objModel);

            @ViewBag.TotalStudents = rs.ToString().Count();
            return PartialView(rs);
        }

    }
}
