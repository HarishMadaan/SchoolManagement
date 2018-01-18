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
    public class AdmissionFeeMasterController : Controller
    {
        IAdmissionFeeMasterBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        //
        // GET: /AdmissionFeeMaster/
        public ActionResult Index()
        {
            SessionMasterModel objSessionModel = new SessionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            AdmissionFeeMasterCustomModel objModel = new AdmissionFeeMasterCustomModel();
            objBDC = new AdmissionFeeMasterBusiness();
            var rs = objBDC.GetAdmissionFeeMasterListing(objModel);

            return View(rs);
        }

        //
        // GET: /AdmissionFeeMaster/Details/5
        public ActionResult Details(int id)
        {
            if (id != 0)
            {
                objBDC = new AdmissionFeeMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /AdmissionFeeMaster/Create
        public ActionResult Create(int id = 0)
        {
            AdmissionFeeMasterCustomModel objModel = new AdmissionFeeMasterCustomModel();
            ClassMasterModel objClassModel = new ClassMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var ClassType = objBDCCommon.GetClassMaster();
            objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objClassModel.ClassList;

            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            if (id > 0)
            {
                objBDC = new AdmissionFeeMasterBusiness();
                objModel = objBDC.GetById(id);
            }

            return View(objModel);
        }

        //
        // POST: /AdmissionFeeMaster/Create
        [HttpPost]
        public ActionResult Create(AdmissionFeeMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();
                    // TODO: Add insert logic here
                    objBDC = new AdmissionFeeMasterBusiness();
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    _Result = objBDC.SaveAdmissionFeeMasterDetails(objModel);

                    if (_Result.success == true)
                        TempData["Message"] = "Success^" + _Result.message;
                    else if (_Result.success == false)
                        TempData["Message"] = "Error^" + _Result.message;

                    return RedirectToAction("Index");
                }
                else
                {
                    ClassMasterModel objClassModel = new ClassMasterModel();
                    objBDCCommon = new CommonMasterDataBusiness();

                    var ClassType = objBDCCommon.GetClassMaster();
                    objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
                    ViewBag.ClassInfo = objClassModel.ClassList;

                    SessionMasterModel objSessionModel = new SessionMasterModel();
                    var SessionType = objBDCCommon.GetSessionMaster();
                    objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
                    ViewBag.SessionInfo = objSessionModel.SessionList;
                    
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /AdmissionFeeMaster/Edit/5
        public ActionResult Edit(int id)
        {
            ClassMasterModel objModel = new ClassMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var ClassType = objBDCCommon.GetClassMaster();
            objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objModel.ClassList;

            SessionMasterModel objModel2 = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objModel2.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objModel2.SessionList;

            objBDC = new AdmissionFeeMasterBusiness();
            var rs = objBDC.GetById(id);

            return View(rs);
        }

        //
        // POST: /AdmissionFeeMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AdmissionFeeMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    objBDC = new AdmissionFeeMasterBusiness();
                    objModel.AdmissionFeeId = id;
                    objBDC.SaveAdmissionFeeMasterDetails(objModel);

                    return RedirectToAction("Index");
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
        // GET: /AdmissionFeeMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AdmissionFeeMaster/Delete/5
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

        public ActionResult DeleteStatus(string id)
        {
            objBDC = new AdmissionFeeMasterBusiness();
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

            AdmissionFeeMasterCustomModel objModel = new AdmissionFeeMasterCustomModel();
            objModel.AdmissionFeeId = Id;
            objBDC.DeleteAdmissionFeeRegistrationDetail(objModel);

            return RedirectToAction("Index");
        }

        public ActionResult SearchSessionWiseResult(int? SessionId)
        {
            AdmissionFeeMasterCustomModel objModel = new AdmissionFeeMasterCustomModel();
            objBDC = new AdmissionFeeMasterBusiness();
            objModel.SessionId = SessionId;
            var rs = objBDC.GetAdmissionFeeMasterListing(objModel);

            @ViewBag.TotalStudents = rs.ToString().Count();
            return PartialView(rs);
        }

    }
}
