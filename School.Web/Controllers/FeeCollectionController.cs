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
    public class FeeCollectionController : Controller
    {
        IFeeCollectionBusiness objBDC;
        IEnquiryDetailBusiness objBDCENQ;
        ICommonMasterDataBusiness objBDCCommon;
        IEnquiryDetailBusiness objBDC2;

        // GET: FeeCollection
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

            return View();
        }

        // GET: FeeCollection/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeeCollection/Create
        public ActionResult Create(int id = 0, int Aid = 0)
        {
            FeeCollectionCustomModel objFeeModel = new FeeCollectionCustomModel();
            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            EmployeeMasterModel objEmployeeModel = new EmployeeMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();
            objBDCENQ = new EnquiryDetailBusiness();

            var EmployeeType = objBDCCommon.GetEmployeeMaster();
            objEmployeeModel.EmployeeList = new SelectList(EmployeeType, "EmployeeId", "EmployeeName");
            ViewBag.EmployeeInfo = objEmployeeModel.EmployeeList;

            StudentCommonDetail objRes = new StudentCommonDetail();

            var rs = objBDCENQ.GetEnquiryId(Aid);
            int Eid = ((School.Shared.CustomModels.StudentCommonDetail)rs).EnquiryId;
            int? ClassId = ((School.Shared.CustomModels.StudentCommonDetail)rs).ClassId;
            int? SectionId = ((School.Shared.CustomModels.StudentCommonDetail)rs).SectionId;

            if (Eid != 0)
            {
                ViewBag.StudentAdmissionDate = objBDCENQ.GetById(Eid);

                objBDC = new FeeCollectionBusiness();
                ViewBag.FeeCollection = objBDC.GetStudentFeeDetail(Aid, ClassId, SectionId);
            }

            if (id != 0)
            {
                objBDC = new FeeCollectionBusiness();
                objFeeModel = objBDC.GetById(id);
            }

            return View(objFeeModel);
        }

        // POST: FeeCollection/Create
        [HttpPost]
        public ActionResult Create(FeeCollectionCustomModel objModel, int Aid = 0)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    objBDC = new FeeCollectionBusiness();
                    objBDCENQ = new EnquiryDetailBusiness();
                    StudentCommonDetail objResult = new StudentCommonDetail();

                    var rs = objBDCENQ.GetEnquiryId(Aid);
                    int Eid = ((School.Shared.CustomModels.StudentCommonDetail)rs).EnquiryId;
                    int? ClassId = ((School.Shared.CustomModels.StudentCommonDetail)rs).ClassId;
                    int? SectionId = ((School.Shared.CustomModels.StudentCommonDetail)rs).SectionId;

                    if (Eid > 0 && Aid > 0)
                    {
                        objModel.EnquiryId = Eid;
                        objModel.AdmissionId = Aid;
                        objModel.ClassId = ClassId;
                        objModel.SectionId = SectionId;
                        objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                        objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                        objBDC.SaveFeeCollectionMasterDetails(objModel);
                        
                        return RedirectToAction("Create", new { id = 0, AId = Aid });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    objBDCENQ = new EnquiryDetailBusiness();
                    EmployeeMasterModel objEmployeeModel = new EmployeeMasterModel();
                    objBDCCommon = new CommonMasterDataBusiness();

                    var EmployeeType = objBDCCommon.GetEmployeeMaster();
                    objEmployeeModel.EmployeeList = new SelectList(EmployeeType, "EmployeeId", "EmployeeName");
                    ViewBag.EmployeeInfo = objEmployeeModel.EmployeeList;

                    var rs = objBDCENQ.GetEnquiryId(Aid);
                    int Eid = ((School.Shared.CustomModels.StudentCommonDetail)rs).EnquiryId;
                    int? ClassId = ((School.Shared.CustomModels.StudentCommonDetail)rs).ClassId;
                    int? SectionId = ((School.Shared.CustomModels.StudentCommonDetail)rs).SectionId;

                    if (Eid != 0)
                    {
                        ViewBag.StudentAdmissionDate = objBDCENQ.GetById(Eid);

                        objBDC = new FeeCollectionBusiness();
                        ViewBag.FeeCollection = objBDC.GetStudentFeeDetail(Aid, ClassId, SectionId);
                    }

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult FeeCollectionRecepiet(int id = 0)
        {
            if (id != 0)
            {
                int? AdmissionId = null;

                objBDC = new FeeCollectionBusiness();
                ViewBag.FeeCollectionDetail = objBDC.GetFeeCollectionReceiptDetail(id, AdmissionId);
            }

            return View();
        }

        // GET: FeeCollection/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeeCollection/Edit/5
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

        // GET: FeeCollection/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeeCollection/Delete/5
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

        public ActionResult SearchStudentList(int? SessionId, int? ClassId, int? SectionId, string StudentName)
        {
            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            objBDC2 = new EnquiryDetailBusiness();
            var rs = objBDC2.BindSessionClassEnquiry(SessionId, ClassId, SectionId, StudentName);

            return PartialView(rs);
        }

        public ActionResult DeleteStatus(string id)
        {
            objBDC = new FeeCollectionBusiness();
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

            FeeCollectionCustomModel objModel = new FeeCollectionCustomModel();
            objModel.FeeCollectionId = Id;            
            objBDC.DeleteFeeCollectionDetail(objModel);

            return RedirectToAction("Index");
        }

    }
}
