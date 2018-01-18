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
    public class BusChargesMasterController : Controller
    {
        IBusChargesMasterBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        //
        // GET: /BusChargesMaster/
        public ActionResult Index()
        {
            SessionMasterModel objSessionModel = new SessionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            BusChargesMasterCustomModel objModel = new BusChargesMasterCustomModel();
            objBDC = new BusChargesMasterBusiness();
            var rs = objBDC.GetBusChargesMasterListing(objModel);

            return View(rs);
        }

        //
        // GET: /BusChargesMaster/Details/5
        public ActionResult Details(int id)
        {
            if (id != 0)
            {
                objBDC = new BusChargesMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /BusChargesMaster/Create
        public ActionResult Create(int id = 0)
        {
            BusChargesMasterCustomModel objModel = new BusChargesMasterCustomModel();
            objBDCCommon = new CommonMasterDataBusiness();

            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            if (id > 0)
            {
                objBDC = new BusChargesMasterBusiness();
                objModel = objBDC.GetById(id);
            }

            return View(objModel);
        }

        //
        // POST: /BusChargesMaster/Create
        [HttpPost]
        public ActionResult Create(BusChargesMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();
                    // TODO: Add insert logic here
                    objBDC = new BusChargesMasterBusiness();
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    _Result = objBDC.SaveBusChargesMasterDetails(objModel);

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

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BusChargesMaster/Edit/5
        public ActionResult Edit(int id)
        {
            objBDCCommon = new CommonMasterDataBusiness();

            SessionMasterModel objModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objModel.SessionList;

            objBDC = new BusChargesMasterBusiness();
            var rs = objBDC.GetById(id);

            return View(rs);
        }

        //
        // POST: /BusChargesMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusChargesMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    objBDC = new BusChargesMasterBusiness();
                    objModel.BusChargesMasterId = id;
                    objBDC.SaveBusChargesMasterDetails(objModel);

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
        // GET: /BusChargesMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BusChargesMaster/Delete/5
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
            objBDC = new BusChargesMasterBusiness();
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

            BusChargesMasterCustomModel objModel = new BusChargesMasterCustomModel();
            objModel.BusChargesMasterId = Id;
            objBDC.SetActiveBusChargesRegistrationDetail(objModel);
            return Json(new { result = _Result });
        }

        public ActionResult DeleteStatus(string id)
        {
            objBDC = new BusChargesMasterBusiness();
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

            BusChargesMasterCustomModel objModel = new BusChargesMasterCustomModel();
            objModel.BusChargesMasterId = Id;
            objBDC.DeleteBusChargesRegistrationDetail(objModel);

            return RedirectToAction("Index");
        }

        public ActionResult SearchSessionWiseResult(int? SessionId)
        {
            BusChargesMasterCustomModel objModel = new BusChargesMasterCustomModel();
            objBDC = new BusChargesMasterBusiness();
            objModel.SessionId = SessionId;
            var rs = objBDC.GetBusChargesMasterListing(objModel);

            @ViewBag.TotalStudents = rs.ToString().Count();
            return PartialView(rs);
        }

    }
}
