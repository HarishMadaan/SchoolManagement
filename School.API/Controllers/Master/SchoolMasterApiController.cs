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
    public class SchoolMasterApiController : ApiController
    {
        #region Global Variable
        Response _response = new Response();
        ISchoolMasterBusiness objBDC;
        #endregion
        
        public SchoolMasterApiController(ISchoolMasterBusiness _objBDC)
        {
            this.objBDC = _objBDC;
        }

        /// <summary>
        /// This method is used to fetch Farmer Registration
        /// </summary>
        /// <returns>List of Farmer Registration</returns>
        [HttpPost]
        [Route("api/SchoolMasterApi/GetSchoolMasterListing")]
        public Response GetSchoolMasterListing(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            _response = new Response();
            try
            {
                _response.responseData = objBDC.GetSchoolMasterListing(objSchoolRegistrationModel);
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
        [Route("api/SchoolMasterApi/SetActiveSchoolRegistrationDetail")]
        public Response SetActiveSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            _response = new Response();
            try
            {
                _response.responseData = objBDC.SetActiveSchoolRegistrationDetail(objSchoolRegistrationModel);
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
        [Route("api/SchoolMasterApi/DeleteSchoolRegistrationDetail")]
        public Response DeleteSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            _response = new Response();
            try
            {
                _response.responseData = objBDC.DeleteSchoolRegistrationDetail(objSchoolRegistrationModel);
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