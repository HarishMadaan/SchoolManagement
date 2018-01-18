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
    public class SchoolMasterRepo : ISchoolMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public object GetSchoolMasterListing(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            IList<SchoolMasterCustomModel> SchoolListModel = new List<SchoolMasterCustomModel>();
            IQueryable<SchoolMasterCustomModel> SchoolListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;


            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        
                        SchoolListDetail = dbcontext.tblSchools.Where(x => x.IsDeleted == false)
                                .Select(x => new SchoolMasterCustomModel
                                {
                                    SchoolId = x.SchoolId,
                                    Name = x.Name,
                                    MobileNo = x.MobileNo,
                                    Address = x.Address,
                                    Address2 = x.Address2,
                                    City = x.City,
                                    State = x.State,

                                    Country = x.Country,
                                    DDate = x.DDate,

                                    //DistrictId = x.VillageMaster != null ? x.VillageMaster.CityMaster.DistrictId : 0,
                                    //DistrictName = x.VillageMaster != null ? x.VillageMaster.CityMaster.DistrictMaster.DistrictName : "",

                                    //StateId = x.VillageMaster != null ? x.VillageMaster.CityMaster.DistrictMaster.StateId : 0,
                                    //StateName = x.VillageMaster != null ? x.VillageMaster.CityMaster.DistrictMaster.StateMaster.StateName : "",

                                    ShortDescription = x.ShortDescription, 
                                    EmailId = x.EmailId,
                                    TagLine = x.TagLine, 
                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.SchoolId);

                        if (objSchoolRegistrationModel.pageModel != null)
                        {
                            if (objSchoolRegistrationModel.pageModel.SerachTerm != null && objSchoolRegistrationModel.pageModel.SerachTerm != "")
                            {
                                SchoolListDetail = SchoolListDetail.Where(x =>
                                (x.Name.ToLower().Trim().Contains(objSchoolRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objSchoolRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.EmailId.ToLower().Trim().Contains(objSchoolRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objSchoolRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.MobileNo.ToLower().Trim().Contains(objSchoolRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objSchoolRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.Address.ToLower().Trim().Contains(objSchoolRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objSchoolRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                );
                            }

                            if (!String.IsNullOrEmpty(objSchoolRegistrationModel.pageModel.SortBy))
                            {
                                //SchoolListDetail= objSchoolRegistrationModel.pageModel.SortDir.ToLower() == "desc" ? SchoolListDetail.OrderByDescending(objSchoolRegistrationModel.pageModel.SortBy)
                                //    : SchoolListDetail.OrderBy(objSchoolRegistrationModel.pageModel.SortBy);
                            }

                            TotalRec = SchoolListDetail.Count();
                            if (objSchoolRegistrationModel.pageModel.PageSize > 0)
                            {
                                SchoolListDetail = SchoolListDetail.Skip(objSchoolRegistrationModel.pageModel.Skip);
                                SchoolListDetail= SchoolListDetail.Take(objSchoolRegistrationModel.pageModel.PageSize);
                            }

                            SchoolListModel = SchoolListDetail.ToList() as IList<SchoolMasterCustomModel>;

                        }
                        else
                        {
                            TotalRec = SchoolListDetail.Count();
                            SchoolListModel = SchoolListDetail.ToList() as IList<SchoolMasterCustomModel>;
                        }

                        //CurrentPageSize = objSchoolRegistrationModel.pageModel.PageSize;

                        //if (SchoolListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    SchoolListModel[0].pageModel = new PagingViewModel();

                        //    SchoolListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        SchoolListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        SchoolListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return SchoolListModel;

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


        public Response SaveSchoolMasterDetails(SchoolMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.SchoolId == 0)
                        {
                            var rs = dbcontext.tblSchools.FirstOrDefault(x => x.IsDeleted == false && x.Name == objModel.Name);
                            if (rs == null)
                            {
                                tblSchool objAddNew = new tblSchool
                                {
                                    Name = objModel.Name,
                                    MobileNo = objModel.MobileNo,
                                    ShortDescription = objModel.ShortDescription,
                                    EmailId = objModel.EmailId,
                                    Address = objModel.Address,
                                    Address2 = objModel.Address2,
                                    City = objModel.City,
                                    Country = objModel.Country,
                                    Image = objModel.Image,
                                    Logo = objModel.Logo,
                                    State = objModel.State,
                                    TagLine = objModel.TagLine,

                                    DDate = DateTime.Now,

                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblSchools.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { SchoolId = objAddNew.SchoolId, Name = objAddNew.Name };
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
                            var rs = dbcontext.tblSchools.FirstOrDefault(x => x.IsDeleted == false && x.Name == objModel.Name && x.SchoolId != objModel.SchoolId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblSchools.FirstOrDefault(m => m.SchoolId == objModel.SchoolId);
                                if (objUpdate != null)
                                {
                                    objUpdate.Name = objModel.Name;
                                    objUpdate.ShortDescription = objModel.ShortDescription;
                                    objUpdate.MobileNo = objModel.MobileNo;
                                    objUpdate.EmailId = objModel.EmailId;
                                    objUpdate.Address = objModel.Address;
                                    objUpdate.Address2 = objModel.Address2;
                                    objUpdate.City = objModel.City;
                                    objUpdate.Country = objModel.Country;
                                    objUpdate.Image = objModel.Image;
                                    objUpdate.Logo = objModel.Logo;
                                    objUpdate.State = objModel.State;
                                    objUpdate.TagLine = objModel.TagLine;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { SchoolId = objUpdate.SchoolId, Name = objUpdate.Name };
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
        public object SetActiveSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            object objSchoolResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblSchools.FirstOrDefault(x => x.SchoolId == objSchoolRegistrationModel.SchoolId);

                        if (rs != null)
                        {
                            rs.IsActive = rs.IsActive == true ? false : true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objSchoolRegistrationModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objSchoolResult = true;
                        }
                        else
                        {
                            objSchoolResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objSchoolResult = null;
                        throw ex;
                    }
                }
                return objSchoolResult;
            }
        }

        /// <summary>
        /// This method is used to delete particular farmer detail
        /// </summary>
        /// <param name="AssetId">Unique id of asset</param>
        /// <returns>Response</returns>
        public object DeleteSchoolRegistrationDetail(SchoolMasterCustomModel objSchoolRegistrationModel)
        {
            object objSchoolResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblSchools.FirstOrDefault(x => x.SchoolId == objSchoolRegistrationModel.SchoolId);

                        if (rs != null)
                        {
                            rs.IsDeleted = true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objSchoolRegistrationModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objSchoolResult = true;
                        }
                        else
                        {
                            objSchoolResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objSchoolResult = null;
                        throw ex;
                    }
                }
                return objSchoolResult;
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
