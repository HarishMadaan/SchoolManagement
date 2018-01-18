using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Shared.CustomModels;
using School.Shared.CommonFunc;
using School.Web;
using School.BDC.Interfaces;
using School.BDC.Classes;
using System.IO;

namespace School.Web.Controllers
{
    public class CommonMasterController : Controller
    {
        ICommonMasterDataBusiness objBDCCommon;

        public ActionResult FillClass(int SessionId)
        {
            objBDCCommon = new CommonMasterDataBusiness();
            var classes = objBDCCommon.BindSessionClass(SessionId);
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillSection(int ClassId)
        {
            objBDCCommon = new CommonMasterDataBusiness();
            var sections = objBDCCommon.BindClassSection(ClassId);
            return Json(sections, JsonRequestBehavior.AllowGet);
        }

        // GET: CommonMaster
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommonMaster/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommonMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommonMaster/Create
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

        // GET: CommonMaster/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommonMaster/Edit/5
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

        // GET: CommonMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommonMaster/Delete/5
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
