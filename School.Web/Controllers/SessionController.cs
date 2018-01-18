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
    public class SessionController : Controller
    {
        ISessionMasterBusiness objBDC;

        //
        // GET: /Session/
        public ActionResult Index()
        {
            SessionMasterCustomModel objModel = new SessionMasterCustomModel();
            objBDC = new SessionMasterBusiness();
            PagingViewModel objPagingModel = new PagingViewModel();
            objPagingModel.PageSize = 10;
            objPagingModel.CurrentPage = 1;
            objPagingModel.Reverse = false;
            objPagingModel.Skip = 0;
            objPagingModel.SortDir = "Desc";

            //var pagingModel = {
            //    Page: parseInt($scope.currentPage),
            //    PageSize: parseInt($scope.itemsPerPage),
            //    SerachTerm: String($scope.SearchText),

            //    Reverse: $scope.reverse,
            //    SortBy: String($scope.sortingOrder),
            //    Skip: parseInt($scope.Skip),
            //    SortDir: String($scope.SortDir)
            //}

            objModel.pageModel = objPagingModel;
            //objModel.pageModel.CurrentPage = Convert.ToInt32("1");
            var rs = objBDC.GetSessionMasterListing(objModel);

            return View(rs);
        }

        //
        // GET: /Session/Details/5
        public ActionResult Details(int id)
        {
            if (id != 0)
            {
                objBDC = new SessionMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Session/Create
        public ActionResult Create(int id = 0)
        {
            SessionMasterCustomModel objModel = new SessionMasterCustomModel();
            if (id != 0)
            {
                objBDC = new SessionMasterBusiness();
                objModel = objBDC.GetById(id);
            }

            return View(objModel);
        }

        //
        // POST: /Session/Create
        [HttpPost]
        public ActionResult Create(SessionMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response _Result = new Response();
                    // TODO: Add insert logic here
                    objBDC = new SessionMasterBusiness();
                    objModel.CreatedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    objModel.ModifiedBy = ((School.Shared.CustomModels.UserLoginCustomModel)(Session[CommonStrings.UserSession])).Id;
                    _Result = objBDC.SaveSessionMasterDetails(objModel);

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
        // GET: /Session/Edit/5
        public ActionResult Edit(int id)
        {
            if (id != 0)
            {
                objBDC = new SessionMasterBusiness();
                var rs = objBDC.GetById(id);

                return View(rs);
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /Session/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SessionMasterCustomModel objModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response response = new Response();
                    // TODO: Add update logic here
                    objBDC = new SessionMasterBusiness();
                    objModel.SessionId = id;
                    response = objBDC.SaveSessionMasterDetails(objModel);

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
        // GET: /Session/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Session/Delete/5
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

        public JsonResult UpdateDefaultStatus(string id)
        {
            object _Result = new object();
            objBDC = new SessionMasterBusiness();
            int Id = Convert.ToInt32(id);
            //bool _Result = objBDC.FindById(Id);
            //if (_Result == true)
            //{
            //    _Result = false;
            //}
            //else
            //{
            //    _Result = true;
            //}

            SessionMasterCustomModel objModel = new SessionMasterCustomModel();
            objModel.SessionId = Id;
            _Result = objBDC.SetDefaultSessionRegistrationDetail(objModel);

            return Json(new { result = _Result });
        }

        public JsonResult UpdateStatus(string id)
        {
            objBDC = new SessionMasterBusiness();
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

            SessionMasterCustomModel objModel = new SessionMasterCustomModel();
            objModel.SessionId = Id;
            objBDC.SetActiveSessionRegistrationDetail(objModel);

            return Json(new { result = _Result });
        }

        public JsonResult DeleteStatus(string id)
        {
            objBDC = new SessionMasterBusiness();
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

            SessionMasterCustomModel objModel = new SessionMasterCustomModel();
            objModel.SessionId = Id;
            objBDC.DeleteSessionRegistrationDetail(objModel);

            return Json(new { result = _Result });
        }

    }
}
