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
    public class SchoolController : Controller
    {
        ISchoolMasterBusiness objBDC;
        // GET: School
        public ActionResult Index()
        {
            SchoolMasterCustomModel objModel = new SchoolMasterCustomModel();
            objBDC = new SchoolMasterBusiness();
            objBDC.GetSchoolMasterListing(objModel);

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SchoolMasterCustomModel objModel)
        {
            objBDC = new SchoolMasterBusiness();
            objBDC.SaveSchoolMasterDetails(objModel);

            return View();
        }

    }
}