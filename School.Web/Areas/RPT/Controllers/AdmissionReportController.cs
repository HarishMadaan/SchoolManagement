﻿using System;
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

namespace School.Web.Areas.RPT.Controllers
{
    public class AdmissionReportController : Controller
    {
        IEnquiryDetailBusiness objEnquiry;
        ISectionAllocationBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;
        IEnquiryDetailBusiness objBDC2;

        //
        // GET: /RPT/AdmissionReport/
        public ActionResult Index()
        {
            ClassMasterModel objModel = new ClassMasterModel();
            SectionMasterModel objSectionModel = new SectionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();
                        
            var SectionType = objBDCCommon.GetSectionMaster(0);
            objSectionModel.SectionList = new SelectList(SectionType, "SectionId", "Title");
            ViewBag.SectionInfo = objSectionModel.SectionList;

            var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
            objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objModel.ClassList;

            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            ViewBag.SessionValue = Session[CommonStrings.DefaultSession].ToString();

            return View();
        }

        public ActionResult FillCity(int SessionId)
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

        public ActionResult SearchAdmissionList(DateTime? FromDate, DateTime? ToDate, int? SessionId, int? ClassId, int? SectionId, string EnquiryStatus, string StudentName)
        {
            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            objEnquiry = new EnquiryDetailBusiness();
            var rs = objEnquiry.BindAdmissionReport(FromDate, ToDate, SessionId, ClassId, SectionId, EnquiryStatus, StudentName);

            ViewBag.TotalStudents = rs.ToString().Count();
            return PartialView(rs);
        }

        //
        // GET: /RPT/AdmissionReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RPT/AdmissionReport/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RPT/AdmissionReport/Create
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

        //
        // GET: /RPT/AdmissionReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RPT/AdmissionReport/Edit/5
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

        //
        // GET: /RPT/AdmissionReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RPT/AdmissionReport/Delete/5
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
