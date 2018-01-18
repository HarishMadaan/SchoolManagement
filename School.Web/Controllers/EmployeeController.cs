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
    public class EmployeeController : Controller
    {
        IEmployeeMasterBusiness  objBDC;

        //
        // GET: /Employee/
        public ActionResult Index()
        {
            EmployeeMasterCustomModel objModel = new EmployeeMasterCustomModel();
            objBDC = new EmployeeMasterBusiness();
            var rs = objBDC.GetEmployeeMasterListing(objModel);

            return View(rs);
        }

        //
        // GET: /Employee/Details/5
        public ActionResult Details(int id)
        {
            if (id != 0)
            {
                objBDC = new EmployeeMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Employee/Create
        public ActionResult Create(int id = 0)
        {
            EmployeeMasterCustomModel objModel = new EmployeeMasterCustomModel();

            if (id != 0)
            {
                objBDC = new EmployeeMasterBusiness();
                objModel = objBDC.GetById(id);
            }

            return View(objModel);
        }

        //
        // POST: /Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();

                    if (Request.Files[MessageDisplay.int0].ContentLength > MessageDisplay.int0)
                    {
                        var path = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + MessageDisplay.EmployeeFilePath);
                        objModel.Image = Thumbnails.UploadImage(Request.Files[MessageDisplay.int0].InputStream, path, Path.GetExtension(Request.Files[0].FileName), "e_", MessageDisplay.imageheightSize, MessageDisplay.imageweidthSize, false);
                    }

                    // TODO: Add insert logic here
                    objBDC = new EmployeeMasterBusiness();
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    _Result = objBDC.SaveEmployeeMasterDetails(objModel);

                    if (_Result.success == true)
                        TempData["Message"] = "Success^" + _Result.message;
                    else if (_Result.success == false)
                        TempData["Message"] = "Error^" + _Result.message;

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
        // GET: /Employee/Edit/5
        public ActionResult Edit(int id)
        {
            if (id != 0)
            {
                objBDC = new EmployeeMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files[MessageDisplay.int0].ContentLength > MessageDisplay.int0)
                    {
                        var path = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + MessageDisplay.EmployeeFilePath);
                        objModel.Image = Thumbnails.UploadImage(Request.Files[MessageDisplay.int0].InputStream, path, Path.GetExtension(Request.Files[0].FileName), "e_", MessageDisplay.imageheightSize, MessageDisplay.imageweidthSize, false);
                    }

                    Response response = new Response();
                    // TODO: Add update logic here
                    objBDC = new EmployeeMasterBusiness();
                    objModel.EmployeeId = id;
                    response = objBDC.SaveEmployeeMasterDetails(objModel);

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
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Employee/Delete/5
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
            objBDC = new EmployeeMasterBusiness();
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

            EmployeeMasterCustomModel objModel = new EmployeeMasterCustomModel();
            objModel.EmployeeId = Id;
            objBDC.DeleteEmployeeRegistrationDetail(objModel);

            return RedirectToAction("Index");
        }

    }
}
