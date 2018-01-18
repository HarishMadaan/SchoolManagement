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
    public class RollNoAllocationController : Controller
    {
        IRollNoAllocationBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;
        IEnquiryDetailBusiness objBDC2;

        // GET: RollNoAllocation
        public ActionResult Index()
        {
            ClassMasterModel objModel = new ClassMasterModel();
            SectionMasterModel objSectionModel = new SectionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            var ClassType = objBDCCommon.GetClassMaster();
            objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objModel.ClassList;

            var SectionType = objBDCCommon.GetSectionMaster();
            objSectionModel.SectionList = new SelectList(SectionType, "SectionId", "Title");
            ViewBag.SectionInfo = objSectionModel.SectionList;
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(SectionAllocationCustomModel collection)
        {
            //var fru = collection.SelectedFruits;

            return View();
        }

        // GET: RollNoAllocation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RollNoAllocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RollNoAllocation/Create
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

        // GET: RollNoAllocation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RollNoAllocation/Edit/5
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

        // GET: RollNoAllocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RollNoAllocation/Delete/5
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

        public ActionResult SearchRollNoAllocationList(int? SessionId, int? ClassId, int? SectionId, string StudentName)
        {
            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            objBDC2 = new EnquiryDetailBusiness();
            ViewBag.AllocationDetail = objBDC2.BindSessionClassEnquiry(SessionId, ClassId, SectionId, StudentName);

            return PartialView("SearchRollNoAllocationList");
        }

        public ActionResult UpdateAllocationList(string[] Parameters, string[] ParametersValue)
        {
            string ResultMessage = "";
            try
            {
                int TotalStudent = 0;
                objBDC = new RollNoAllocationBusiness();
                PromoteToClassCustomModel objModel = new PromoteToClassCustomModel();

                foreach (var nw in Parameters.Zip(ParametersValue, Tuple.Create))
                {
                    //Console.WriteLine(nw.Item1 + nw.Item2);    
                    objModel.PromoteToClassId = Convert.ToInt32(nw.Item1);
                    objModel.RollNo = Convert.ToString(nw.Item2);

                    objBDC.UpdateStudentRollNoAllocation(objModel);
                }

                //foreach (string Id in Parameters)
                //{
                //    objModel.SessionId = SessionId;
                //    objModel.ClassId = ClassId;
                //    objModel.SectionId = SectionId;
                //    objModel.AdmissionId = Convert.ToInt32(Id);
                //    objModel.IsActive = true;
                //    objModel.Status = "Attending";

                //    objBDC.SaveSectionAllocationDetails(objModel);

                //}
                ResultMessage = "Student Allocated in batch Successfully.";
                 
            }
            catch (Exception ex)
            {

            }

            return View(ResultMessage);
        }

    }
}
