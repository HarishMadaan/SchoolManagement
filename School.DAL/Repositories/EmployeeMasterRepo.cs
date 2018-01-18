using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.Shared.CommonFunc;
using School.DAL.Interfaces;

namespace School.DAL.Repositories
{
    public class EmployeeMasterRepo : IEmployeeMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public EmployeeMasterCustomModel GetById(int Id)
        {
            EmployeeMasterCustomModel objListModel = new EmployeeMasterCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblEmployees.Where(x => x.IsDeleted == false && x.EmployeeId == Id)
                                .Select(x => new EmployeeMasterCustomModel
                                {
                                    EmployeeId = x.EmployeeId,
                                    FName = x.FName,
                                    LName = x.LName,
                                    EmailId = x.EmailId,
                                    MobileNo = x.MobileNo,
                                    City = x.City,
                                    State = x.State,
                                    Country = x.Country,
                                    Address = x.Address,
                                    Address2 = x.Address2,
                                    BloodGroup = x.BloodGroup,
                                    DateOfBirth = x.DateOfBirth,
                                    DateOfJoining = x.DateOfJoining,                        
                                    Designation = x.Designation,
                                    Experience = x.Experience,
                                    Qualification = x.Qualification,
                                    SchoolId = x.SchoolId,
                                    DDate = x.DDate,
                                    Image = x.Image,
                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).SingleOrDefault();

                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        objListModel = null;
                    }
                }
            }
            return objListModel;
        }

        public object GetEmployeeMasterListing(EmployeeMasterCustomModel objEmployeeRegistrationModel)
        {
            IList<EmployeeMasterCustomModel> EmployeeListModel = new List<EmployeeMasterCustomModel>();
            IQueryable<EmployeeMasterCustomModel> EmployeeListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;
            
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        EmployeeListDetail = dbcontext.tblEmployees.Where(x => x.IsDeleted == false)
                                .Select(x => new EmployeeMasterCustomModel
                                {
                                    EmployeeId = x.EmployeeId,
                                    FName = x.FName + " " + x.LName,
                                    LName = x.LName,
                                    EmailId = x.EmailId,
                                    MobileNo = x.MobileNo,
                                    City = x.City,
                                    State = x.State,
                                    Country = x.Country,
                                    Address = x.Address,
                                    Address2 = x.Address2,                                    
                                    BloodGroup = x.BloodGroup,
                                    DateOfBirth = x.DateOfBirth,
                                    DateOfJoining = x.DateOfJoining,
                                    Designation = x.Designation,
                                    Experience = x.Experience,
                                    Qualification = x.Qualification,       
                                    SchoolId = x.SchoolId,
                                    DDate = x.DDate,
                                    Image = x.Image,
                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.EmployeeId);

                        if (objEmployeeRegistrationModel.pageModel != null)
                        {
                            if (objEmployeeRegistrationModel.pageModel.SerachTerm != null && objEmployeeRegistrationModel.pageModel.SerachTerm != "")
                            {
                                EmployeeListDetail = EmployeeListDetail.Where(x =>
                                (x.FName.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.LName.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.EmailId.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.MobileNo.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.BloodGroup.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.Designation.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.Experience.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.Qualification.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                
                                );
                            }

                            if (!String.IsNullOrEmpty(objEmployeeRegistrationModel.pageModel.SortBy))
                            {
                                //EmployeeListDetail= objEmployeeRegistrationModel.pageModel.SortDir.ToLower() == "desc" ? EmployeeListDetail.OrderByDescending(objEmployeeRegistrationModel.pageModel.SortBy)
                                //    : EmployeeListDetail.OrderBy(objEmployeeRegistrationModel.pageModel.SortBy);
                            }

                            TotalRec = EmployeeListDetail.Count();
                            if (objEmployeeRegistrationModel.pageModel.PageSize > 0)
                            {
                                EmployeeListDetail = EmployeeListDetail.Skip(objEmployeeRegistrationModel.pageModel.Skip);
                                EmployeeListDetail = EmployeeListDetail.Take(objEmployeeRegistrationModel.pageModel.PageSize);
                            }

                            EmployeeListModel = EmployeeListDetail.ToList() as IList<EmployeeMasterCustomModel>;

                        }
                        else
                        {
                            TotalRec = EmployeeListDetail.Count();
                            EmployeeListModel = EmployeeListDetail.ToList() as IList<EmployeeMasterCustomModel>;
                        }

                        //CurrentPageSize = objEmployeeRegistrationModel.pageModel.PageSize;

                        //if (EmployeeListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    EmployeeListModel[0].pageModel = new PagingViewModel();

                        //    EmployeeListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        EmployeeListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        EmployeeListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return EmployeeListModel;

                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        public Response SaveEmployeeMasterDetails(EmployeeMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.EmployeeId == 0)
                        {
                            var rs = dbcontext.tblEmployees.FirstOrDefault(x => x.IsDeleted == false && (x.EmailId == objModel.EmailId || x.MobileNo == objModel.MobileNo));
                            if (rs == null)
                            {
                                tblEmployee objAddNew = new tblEmployee
                                {
                                    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                    FName = objModel.FName,
                                    LName = objModel.LName,
                                    MobileNo = objModel.MobileNo,
                                    EmailId = objModel.EmailId,
                                    City = objModel.City,
                                    State = objModel.State,
                                    Country = objModel.Country,
                                    Address = objModel.Address,
                                    Address2 = objModel.Address2,
                                    Qualification = objModel.Qualification,
                                    Experience = objModel.Experience,
                                    Designation = objModel.Designation,
                                    DateOfJoining = objModel.DateOfJoining,
                                    DateOfBirth = objModel.DateOfBirth,
                                    DDate = objModel.DDate,
                                    BloodGroup = objModel.BloodGroup,
                                    Image = objModel.Image,

                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblEmployees.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { EmployeeId = objAddNew.EmployeeId, FName = objAddNew.FName };
                                response.message = "Record Added Successfully!";
                            }
                            else
                            {
                                response.success = false;
                                response.message = "Record Already Exists!";
                            }
                        }
                        else
                        {
                            var rs = dbcontext.tblEmployees.FirstOrDefault(x => x.IsDeleted == false && (x.EmailId == objModel.EmailId || x.MobileNo == objModel.MobileNo) && x.EmployeeId != objModel.EmployeeId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblEmployees.FirstOrDefault(m => m.EmployeeId == objModel.EmployeeId);
                                if (objUpdate != null)
                                {
                                    objUpdate.FName = objModel.FName;
                                    objUpdate.LName = objModel.LName;
                                    objUpdate.MobileNo = objModel.MobileNo;
                                    objUpdate.EmailId = objModel.EmailId;
                                    objUpdate.City = objModel.City;
                                    objUpdate.State = objModel.State;
                                    objUpdate.Country = objModel.Country;
                                    objUpdate.Address = objModel.Address;
                                    objUpdate.Address2 = objModel.Address2;
                                    objUpdate.Qualification = objModel.Qualification;
                                    objUpdate.Experience = objModel.Experience;
                                    objUpdate.Designation = objModel.Designation;
                                    objUpdate.DateOfJoining = objModel.DateOfJoining;
                                    objUpdate.DateOfBirth = objModel.DateOfBirth;
                                    objUpdate.DDate=DateTime.Now;
                                    objUpdate.BloodGroup = objModel.BloodGroup;
                                    objUpdate.Image = objModel.Image;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { EmployeeId = objUpdate.EmployeeId, FName = objUpdate.FName };
                                    response.message = "Record Updated Successfully!";
                                }
                            }
                            else
                            {
                                response.success = false;
                                response.message = "Record Already Exists!";
                            }
                        }
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        return response;
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to Activate particular farmer detail
        /// </summary>
        /// <param name="AssetId">Unique id of Farmer</param>
        /// <returns>Response</returns>
        public object SetActiveEmployeeRegistrationDetail(EmployeeMasterCustomModel objEmployeeRegistrationModel)
        {
            object objEmployeeResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblEmployees.FirstOrDefault(x => x.EmployeeId == objEmployeeRegistrationModel.EmployeeId);

                        if (rs != null)
                        {
                            rs.IsActive = rs.IsActive == true ? false : true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objEmployeeRegistrationModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objEmployeeResult = true;
                        }
                        else
                        {
                            objEmployeeResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objEmployeeResult = null;
                        throw ex;
                    }
                }
                return objEmployeeResult;
            }
        }

        /// <summary>
        /// This method is used to delete particular farmer detail
        /// </summary>
        /// <param name="AssetId">Unique id of asset</param>
        /// <returns>Response</returns>
        public object DeleteEmployeeRegistrationDetail(EmployeeMasterCustomModel objEmployeeRegistrationModel)
        {
            object objEmployeeResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblEmployees.FirstOrDefault(x => x.EmployeeId == objEmployeeRegistrationModel.EmployeeId);

                        if (rs != null)
                        {
                            rs.IsDeleted = true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objEmployeeRegistrationModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objEmployeeResult = true;
                        }
                        else
                        {
                            objEmployeeResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objEmployeeResult = null;
                        throw ex;
                    }
                }
                return objEmployeeResult;
            }
        }

        public bool FindById(int Id)
        {
            bool ActiveResult = false;
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        ActiveResult = dbcontext.tblEmployees.FirstOrDefault(x => x.EmployeeId == Id).IsActive ?? false;
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        throw ex;
                    }
                }
                return ActiveResult;
            }
        }

        public void Dispose()
        {
            dbcontext.Dispose();
            if (response != null)
            {
                response.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
