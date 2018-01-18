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
    public class EnquiryController : Controller
    {
        IEnquiryDetailBusiness objBDC;
        ICommonMasterDataBusiness objBDCCommon;

        //
        // GET: /Enquiry/
        public ActionResult Index()
        {
            SessionMasterModel objSessionModel = new SessionMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            objBDC = new EnquiryDetailBusiness();
            var rs = objBDC.GetEnquiryDetailListing(objModel);

            return View(rs);
        }

        //
        // GET: /Enquiry/Details/5
        public ActionResult Details(int id)
        {
            if (id != 0)
            {
                objBDC = new EnquiryDetailBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Enquiry/Create
        public ActionResult Create(int id = 0)
        {
            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            ClassMasterModel objClassModel = new ClassMasterModel();
            SessionMasterModel objSessionModel = new SessionMasterModel();
            EmployeeMasterModel objEmployeeModel = new EmployeeMasterModel();
            objBDCCommon = new CommonMasterDataBusiness();

            var ClassType = objBDCCommon.GetClassMaster();
            objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
            ViewBag.ClassInfo = objClassModel.ClassList;

            var SessionType = objBDCCommon.GetSessionMaster();
            objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
            ViewBag.SessionInfo = objSessionModel.SessionList;

            var EmployeeType = objBDCCommon.GetEmployeeMaster();
            objEmployeeModel.EmployeeList = new SelectList(EmployeeType, "EmployeeId", "EmployeeName");
            ViewBag.EmployeeInfo = objEmployeeModel.EmployeeList;

            if (id != 0)
            {                
                objBDC = new EnquiryDetailBusiness();
                objModel = objBDC.GetById(id);                
            }

            return View(objModel);
        }

        //
        // POST: /Enquiry/Create
        [HttpPost]
        public ActionResult Create(EnquiryDetailCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();

                    if (Request.Files[MessageDisplay.int0].ContentLength > MessageDisplay.int0)
                    {
                        var path = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + MessageDisplay.EnquiryFilePath);
                        objModel.Image = Thumbnails.UploadImage(Request.Files[MessageDisplay.int0].InputStream, path, Path.GetExtension(Request.Files[0].FileName), "e_", MessageDisplay.imageheightSize, MessageDisplay.imageweidthSize, false);
                    }

                    // TODO: Add insert logic here
                    objBDC = new EnquiryDetailBusiness();
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    _Result = objBDC.SaveEnquiryDetails(objModel);

                    if (_Result.success == true)
                        TempData["Message"] = "Success^" + _Result.message;
                    else if (_Result.success == false)
                        TempData["Message"] = "Error^" + _Result.message;

                    return RedirectToAction("Index");
                }
                else
                {
                    ClassMasterModel objModelClass = new ClassMasterModel();
                    SessionMasterModel objSessionModel = new SessionMasterModel();
                    EmployeeMasterModel objEmployeeModel = new EmployeeMasterModel();
                    objBDCCommon = new CommonMasterDataBusiness();

                    var ClassType = objBDCCommon.GetClassMaster();
                    objModelClass.ClassList = new SelectList(ClassType, "ClassId", "Title");
                    ViewBag.ClassInfo = objModelClass.ClassList;

                    var SessionType = objBDCCommon.GetSessionMaster();
                    objSessionModel.SessionList = new SelectList(SessionType, "SessionId", "Title");
                    ViewBag.SessionInfo = objSessionModel.SessionList;


                    var EmployeeType = objBDCCommon.GetEmployeeMaster();
                    objEmployeeModel.EmployeeList = new SelectList(EmployeeType, "EmployeeId", "EmployeeName");
                    ViewBag.EmployeeInfo = objEmployeeModel.EmployeeList;

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Enquiry/Edit/5
        public ActionResult Edit(int id)
        {
            if (id != 0)
            {
                ClassMasterModel objModel = new ClassMasterModel();
                EmployeeMasterModel objEmployeeModel = new EmployeeMasterModel();
                objBDCCommon = new CommonMasterDataBusiness();

                var ClassType = objBDCCommon.GetClassMaster();
                objModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
                ViewBag.ClassInfo = objModel.ClassList;

                var EmployeeType = objBDCCommon.GetEmployeeMaster();
                objEmployeeModel.EmployeeList = new SelectList(EmployeeType, "EmployeeId", "EmployeeName");
                ViewBag.EmployeeInfo = objEmployeeModel.EmployeeList;

                objBDC = new EnquiryDetailBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /Enquiry/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EnquiryDetailCustomModel objModel)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    Response response = new Response();
                    // TODO: Add update logic here
                    objBDC = new EnquiryDetailBusiness();
                    objModel.EnquiryId = id;
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    response = objBDC.SaveEnquiryDetails(objModel);

                    if (response.success == false)
                    {
                        return new JavaScriptResult() { Script = "alert('Record already exists');" };
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ClassMasterModel objClassModel = new ClassMasterModel();
                    EmployeeMasterModel objEmployeeModel = new EmployeeMasterModel();
                    objBDCCommon = new CommonMasterDataBusiness();

                    var ClassType = objBDCCommon.GetClassMaster();
                    objClassModel.ClassList = new SelectList(ClassType, "ClassId", "Title");
                    ViewBag.ClassInfo = objClassModel.ClassList;

                    var EmployeeType = objBDCCommon.GetEmployeeMaster();
                    objEmployeeModel.EmployeeList = new SelectList(EmployeeType, "EmployeeId", "EmployeeName");
                    ViewBag.EmployeeInfo = objEmployeeModel.EmployeeList;

                    objBDC = new EnquiryDetailBusiness();
                    var rs = objBDC.GetById(id);

                    return View(rs);
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Enquiry/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Enquiry/Delete/5
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
            objBDC = new EnquiryDetailBusiness();
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

            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            objModel.EnquiryId = Id;
            objBDC.DeleteEnquiryDetail(objModel);

            return RedirectToAction("Index");
        }

        public ActionResult SearchSessionWiseResult(int? SessionId, int? ClassId, string StudentName)
        {
            EnquiryDetailCustomModel objModel = new EnquiryDetailCustomModel();
            PagingViewModel objModelPage = new PagingViewModel();

            objBDC = new EnquiryDetailBusiness();
            objModel.SessionId = SessionId;
            objModel.ClassId = ClassId;
            if (StudentName != "")
            {
                objModelPage.SerachTerm = StudentName;
                objModel.pageModel = objModelPage;
            }

            var rs = objBDC.GetEnquiryDetailListing(objModel);

            @ViewBag.TotalStudents = rs.ToString().Count();
            return PartialView(rs);
        }

    }
}
