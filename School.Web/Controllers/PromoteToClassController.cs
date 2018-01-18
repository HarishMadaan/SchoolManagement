using School.Web.App_Start;
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
    [SessionExpire]
    public class PromoteToClassController : Controller
    {
        IPromoteToClassBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;
        IEnquiryDetailBusiness objBDC2;

        // GET: PromoteToClass
        public ActionResult Index()
        {
            ClassMasterModel objModel = new ClassMasterModel();
            SectionMasterModel objSectionModel = new SectionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var ClassType = objBDCCommon.GetClassMaster();
            objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objModel.ClassList;

            var SectionType = objBDCCommon.GetSectionMaster();
            objSectionModel.SectionList = new SelectList(SectionType, "SectionId", "Title");
            ViewBag.SectionInfo = objSectionModel.SectionList;

            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            return View();
        }

        // GET: PromoteToClass/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PromoteToClass/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PromoteToClass/Create
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

        // GET: PromoteToClass/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PromoteToClass/Edit/5
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

        // GET: PromoteToClass/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PromoteToClass/Delete/5
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

        public ActionResult SearchPromoteList(int? SessionId, int? ClassId, int? SectionId, string StudentName)
        {
            objBDC = new PromoteToClassBusiness();
            ViewBag.AllocationDetail = objBDC.BindSessionClassEnquiry(SessionId, ClassId, SectionId, StudentName);

            return PartialView("SearchPromoteList");
        }

        public ActionResult UpdateAllocationList(string[] Parameters, int SessionId, int ClassId, int SectionId)
        {
            string ResultMessage = "";
            try
            {
                int TotalStudent = 0;
                objBDC = new PromoteToClassBusiness();
                PromoteToClassCustomModel objModel = new PromoteToClassCustomModel();
                //TotalStudent = objBDC.GetTotalStudentInSection(SessionId, ClassId, SectionId);

                //if (TotalStudent > 30)
                //{
                //    ResultMessage = "Only 70 students are allowed in one batch.";
                //}

                if (Parameters != null)
                {
                    int AfterStudentCount = TotalStudent + Parameters.Length;
                    if (AfterStudentCount > 30)
                    {
                        ResultMessage = "Only 30 students are allowed in one section. You have selected " + Parameters.Length + " students. There are already " + TotalStudent + " students in this section.";
                    }
                    else
                    {
                        foreach (string Id in Parameters)
                        {
                            objModel.SessionId = SessionId;
                            objModel.ClassId = ClassId;
                            objModel.SectionId = SectionId;
                            objModel.AdmissionId = Convert.ToInt32(Id);
                            objModel.IsActive = true;
                            objModel.Status = "Attending";

                            objBDC.SavePromoteToClassDetails(objModel);

                        }
                        ResultMessage = "Student Promoted to new class Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View(ResultMessage);
        }

    }
}
