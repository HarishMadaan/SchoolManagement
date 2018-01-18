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
    public class EventController : Controller
    {
        IEventMasterBusiness objBDC;
 
        //
        // GET: /Event/
        public ActionResult Index()
        {
            EventMasterCustomModel objModel = new EventMasterCustomModel();
            objBDC = new EventMasterBusiness();
            var rs = objBDC.GetEventMasterListing(objModel);

            return View(rs);
        }

        //
        // GET: /Event/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Event/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Event/Create
        [HttpPost]
        public ActionResult Create(EventMasterCustomModel objModel)
        {
            try
            {
                // TODO: Add insert logic here
                objBDC = new EventMasterBusiness();
                objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                objBDC.SaveEventMasterDetails(objModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EventMasterCustomModel objModel)
        {
            try
            {
                // TODO: Add update logic here
                objBDC = new EventMasterBusiness();
                objModel.EventId = id;
                objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                objBDC.SaveEventMasterDetails(objModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Event/Delete/5
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
