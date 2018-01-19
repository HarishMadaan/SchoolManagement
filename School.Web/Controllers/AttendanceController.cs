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
    public class AttendanceController : Controller
    {
        IAttendanceBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;
        IEnquiryDetailBusiness objBDC2;

        // GET: Attendance
        public ActionResult Index()
        {
            ClassMasterModel objModel = new ClassMasterModel();
            SectionMasterModel objSectionModel = new SectionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            SessionMasterModel objSessionModel = new SessionMasterModel();
            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            var ClassType = objBDCCommon.GetClassMaster(Convert.ToInt32(Session[CommonStrings.DefaultSession]));
            objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objModel.ClassList;

            var SectionType = objBDCCommon.GetSectionMaster(0);
            objSectionModel.SectionList = new SelectList(SectionType, "SectionId", "Title");
            ViewBag.SectionInfo = objSectionModel.SectionList;

            return View();
        }

        [HttpPost]
        public ActionResult Index(AttendanceCustomModel collection)
        {
            return View();
        }

        // GET: Attendance/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attendance/Create
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

        // GET: Attendance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Attendance/Edit/5
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

        // GET: Attendance/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Attendance/Delete/5
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

        public ActionResult SearchAttendanceAllocationList(int? SessionId, int? ClassId, int? SectionId, string StudentName, DateTime AttendanceDate)
        {
            AttendanceCustomModel objModel = new AttendanceCustomModel();
            objModel.SectionId = SectionId;
            objModel.AttendanceDate = AttendanceDate;

            objBDC = new AttendanceBusiness();
            ViewBag.AllocationDetail = objBDC.GetAttendanceListing(objModel).responseData;

            ViewBag.AllocationAction = objBDC.GetAttendanceListing(objModel).message;

            return PartialView("SearchAttendanceAllocationList");
        }

        public ActionResult SubmitAttendanceDetails(string[] Parameters, string[] ParametersValue, int SectionId, DateTime AttendanceDate, string AttendanceAction)
        {
            string ResultMessage = "";
            try
            {
                int TotalStudent = 0;
                objBDC = new AttendanceBusiness();
                AttendanceCustomModel objModel = new AttendanceCustomModel();

                if (AttendanceAction == "Insert")
                {
                    foreach (var nw in Parameters.Zip(ParametersValue, Tuple.Create))
                    {
                        //Console.WriteLine(nw.Item1 + nw.Item2);    
                        objModel.AdmissionId = Convert.ToInt32(nw.Item1);
                        objModel.Attendance = Convert.ToString(nw.Item2);
                        objModel.SectionId = SectionId;
                        objModel.AttendanceDate = AttendanceDate;

                        objBDC.SaveAttendanceDetails(objModel);
                    }
                }
                else if (AttendanceAction == "Update")
                {
                    foreach (var nw in Parameters.Zip(ParametersValue, Tuple.Create))
                    {
                        //Console.WriteLine(nw.Item1 + nw.Item2);    
                        objModel.AdmissionId = Convert.ToInt32(nw.Item1);
                        objModel.Attendance = Convert.ToString(nw.Item2);
                        objModel.SectionId = SectionId;
                        objModel.AttendanceDate = AttendanceDate;

                        objBDC.SaveAttendanceDetails(objModel);
                    }
                }
                
                ResultMessage = "Student Allocated in batch Successfully.";

            }
            catch (Exception ex)
            {

            }

            return View(ResultMessage);
        }

    }
}
