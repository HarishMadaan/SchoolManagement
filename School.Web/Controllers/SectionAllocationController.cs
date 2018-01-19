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
    public class SectionAllocationController : Controller
    {
        ISectionAllocationBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;
        IEnquiryDetailBusiness objBDC2;

        //
        // GET: /SectionAllocation/
        public ActionResult Index()
        {
            ClassMasterModel objModel = new ClassMasterModel();
            SectionMasterModel objSectionModel = new SectionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
            objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objModel.ClassList;

            var SectionType = objBDCCommon.GetSectionMaster(0);
            objSectionModel.SectionList = new SelectList(SectionType, "SectionId", "Title");
            ViewBag.SectionInfo = objSectionModel.SectionList;

            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            ViewBag.SessionValueToSet = Session[CommonStrings.DefaultSession].ToString();

            return View();
        }

        [HttpPost]
        public ActionResult Index(SectionAllocationCustomModel collection)
        {
            //var fru = collection.SelectedFruits;

            return View();
        }

        //
        // GET: /SectionAllocation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SectionAllocation/Create
        public ActionResult Create()
        {
            ClassMasterModel objModel = new ClassMasterModel();
            SectionMasterModel objSectionModel = new SectionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
            objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objModel.ClassList;

            var SectionType = objBDCCommon.GetSectionMaster(0);
            objSectionModel.SectionList = new SelectList(SectionType, "SectionId", "Title");
            ViewBag.SectionInfo = objSectionModel.SectionList;

            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            return View();
        }

        //
        // POST: /SectionAllocation/Create
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
        // GET: /SectionAllocation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SectionAllocation/Edit/5
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
        // GET: /SectionAllocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SectionAllocation/Delete/5
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

        public ActionResult FillCity(int SessionId)
        {
            objBDCCommon = new CommonMasterDataBusiness();
            var classes = objBDCCommon.BindSessionClass(SessionId);
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchAllocationList(int? SessionId, int? ClassId, int? SectionId, string StudentName)
        {
            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            objBDC2 = new EnquiryDetailBusiness();
            ViewBag.AllocationDetail = objBDC2.BindSessionClassEnquiry(SessionId, ClassId, SectionId, StudentName);

            return PartialView("SearchAllocationList");
        }

        public ActionResult UpdateAllocationList(string[] Parameters, int SessionId, int ClassId, int SectionId)
        {
            string ResultMessage = "";
            try
            {                
                int TotalStudent = 0;
                objBDC = new SectionAllocationBusiness();
                SectionAllocationCustomModel objModel = new SectionAllocationCustomModel();
                TotalStudent = objBDC.GetTotalStudentInSection(SessionId, ClassId, SectionId);

                if (TotalStudent > 30)
                {
                    ResultMessage = "Only 70 students are allowed in one batch.";
                }

                if (Parameters.Length > 0)
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

                            objBDC.SaveSectionAllocationDetails(objModel);
                            
                        }
                        ResultMessage = "Student Allocated in batch Successfully.";
                    }
                }
            }
            catch(Exception ex)
            {

            }
            
            return View(ResultMessage);
        }
        public ActionResult CancelAllocatedStudent(int Id)
        {
            if (Id != 0)
            {
                objBDC = new SectionAllocationBusiness();
                int _Result = objBDC.CancelAllocatedStudentSection(Id);
                if (_Result == 1)
                {
                    return Json(new { IsSuccess = true });
                }
                else
                {
                    return Json(new { IsSuccess = false });
                }
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }

    }
}
