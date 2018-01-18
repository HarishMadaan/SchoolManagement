using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using School.Shared.CustomModels;
using School.BDC.Interfaces;
using School.BDC.Classes;

namespace School.API.Controllers.Master
{
    public class ClassMasterApiController : ApiController
    {
        #region Global Variable
        Response _response = new Response();
        IClassMasterBusiness objBDC;
        #endregion

        public ClassMasterApiController(IClassMasterBusiness _objBDC)
        {
            this.objBDC = _objBDC;
        }

        /// <summary>
        /// This method is used to fetch Farmer Registration
        /// </summary>
        /// <returns>List of Farmer Registration</returns>
        [HttpPost]
        [Route("api/ClassMasterApi/GetClassMasterListing")]
        public Response GetClassMasterListing(ClassMasterCustomModel objClassRegistrationModel)
        {
            _response = new Response();
            try
            {
                _response.responseData = objBDC.GetClassMasterListing(objClassRegistrationModel);
                _response.message = "Records loaded successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                objBDC.Dispose();
            }
            return _response;
        }

        /// <summary>
        /// This method is used to delete Particular Asset
        /// </summary>
        /// <param name="id">Unique id of asset</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ClassMasterApi/SetActiveClassRegistrationDetail")]
        public Response SetActiveClassRegistrationDetail(ClassMasterCustomModel objClassRegistrationModel)
        {
            _response = new Response();
            try
            {
                _response.responseData = objBDC.SetActiveClassRegistrationDetail(objClassRegistrationModel);
                _response.message = "Status updated Successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                objBDC.Dispose();
            }
            return _response;
        }

        /// <summary>
        /// This method is used to delete Particular Asset
        /// </summary>
        /// <param name="id">Unique id of asset</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ClassMasterApi/DeleteClassRegistrationDetail")]
        public Response DeleteClassRegistrationDetail(ClassMasterCustomModel objClassRegistrationModel)
        {
            _response = new Response();
            try
            {
                _response.responseData = objBDC.DeleteClassRegistrationDetail(objClassRegistrationModel);
                _response.message = "Record Deleted Successfully !!";
                _response.success = true;
            }
            catch (Exception ex)
            {
                _response.success = false;
                _response.message = ex.Message.ToString();
            }
            finally
            {
                objBDC.Dispose();
            }
            return _response;
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}