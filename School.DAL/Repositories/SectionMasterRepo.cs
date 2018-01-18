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
    public class SectionMasterRepo : ISectionMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;
            
        public SectionMasterCustomModel GetById(int Id)
        {
            SectionMasterCustomModel SectionListModel = new SectionMasterCustomModel();
            
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        SectionListModel = dbcontext.tblSections.Where(x => x.IsDeleted == false && x.SectionId == Id)
                                .Select(x => new SectionMasterCustomModel
                                {
                                    SectionId = x.SectionId,
                                    SchoolId = x.SchoolId,
                                    SchoolName = x.tblSchool != null ? x.tblSchool.Name : "",
                                    ClassId = x.ClassId,
                                    ClassName = x.tblClass != null ? x.tblClass.Title : "",
                                    Title = x.Title,
                                    Image = x.Image,
                                    DDate = x.DDate,
                                    ShortDescription = x.ShortDescription,
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
                        SectionListModel = null;
                    }
                }
            }
            return SectionListModel;
        }

        public object GetSectionMasterListing(SectionMasterCustomModel objSectionRegistrationModel)
        {
            IList<SectionMasterCustomModel> SectionListModel = new List<SectionMasterCustomModel>();
            IQueryable<SectionMasterCustomModel> SectionListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        SectionListDetail = dbcontext.tblSections.Where(x => x.IsDeleted == false
                        && (objSectionRegistrationModel.SessionId == null || objSectionRegistrationModel.SessionId == 0 || x.tblClass.SessionId == objSectionRegistrationModel.SessionId)
                        && (objSectionRegistrationModel.ClassId == null || objSectionRegistrationModel.ClassId == 0 || x.ClassId == objSectionRegistrationModel.ClassId)
                        )
                                .Select(x => new SectionMasterCustomModel
                                {
                                    SectionId = x.SectionId,
                                    SchoolId = x.SchoolId,
                                    SchoolName = x.tblSchool != null ? x.tblSchool.Name : "",
                                    ClassId = x.ClassId,
                                    ClassName = x.tblClass != null ? x.tblClass.Title : "",
                                    Title = x.Title,
                                    Image = x.Image,
                                    DDate = x.DDate,
                                    ShortDescription = x.ShortDescription,
                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.SectionId);

                        if (objSectionRegistrationModel.pageModel != null)
                        {
                            if (objSectionRegistrationModel.pageModel.SerachTerm != null && objSectionRegistrationModel.pageModel.SerachTerm != "")
                            {
                                SectionListDetail = SectionListDetail.Where(x =>
                                (x.Title.ToLower().Trim().Contains(objSectionRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objSectionRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.ShortDescription.ToLower().Trim().Contains(objSectionRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objSectionRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                );
                            }

                            if (!String.IsNullOrEmpty(objSectionRegistrationModel.pageModel.SortBy))
                            {
                                //SectionListDetail= objSectionRegistrationModel.pageModel.SortDir.ToLower() == "desc" ? SectionListDetail.OrderByDescending(objSectionRegistrationModel.pageModel.SortBy)
                                //    : SectionListDetail.OrderBy(objSectionRegistrationModel.pageModel.SortBy);
                            }

                            TotalRec = SectionListDetail.Count();
                            if (objSectionRegistrationModel.pageModel.PageSize > 0)
                            {
                                SectionListDetail = SectionListDetail.Skip(objSectionRegistrationModel.pageModel.Skip);
                                SectionListDetail = SectionListDetail.Take(objSectionRegistrationModel.pageModel.PageSize);
                            }

                            SectionListModel = SectionListDetail.ToList() as IList<SectionMasterCustomModel>;

                        }
                        else
                        {
                            TotalRec = SectionListDetail.Count();
                            SectionListModel = SectionListDetail.ToList() as IList<SectionMasterCustomModel>;
                        }

                        //CurrentPageSize = objSectionRegistrationModel.pageModel.PageSize;

                        //if (SectionListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    SectionListModel[0].pageModel = new PagingViewModel();

                        //    SectionListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        SectionListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        SectionListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return SectionListModel;

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

        public Response SaveSectionMasterDetails(SectionMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.SectionId == 0)
                        {
                            var rs = dbcontext.tblSections.FirstOrDefault(x => x.IsDeleted == false && x.Title == objModel.Title && x.ClassId == objModel.ClassId); 
                            if (rs == null)
                            {
                                tblSection objAddNew = new tblSection
                                {
                                    ClassId = objModel.ClassId,
                                    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                    Title = objModel.Title,
                                    ShortDescription = objModel.ShortDescription,
                                    DDate = objModel.DDate,
                                    
                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblSections.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { SectionId = objAddNew.SectionId, Title = objAddNew.Title };
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
                            var rs = dbcontext.tblSections.FirstOrDefault(x => x.IsDeleted == false && x.ClassId == objModel.ClassId && x.Title == objModel.Title && x.SectionId != objModel.SectionId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblSections.FirstOrDefault(m => m.SectionId == objModel.SectionId);
                                if (objUpdate != null)
                                {
                                    objUpdate.ClassId = objModel.ClassId;
                                    objUpdate.Title = objModel.Title;
                                    objUpdate.ShortDescription = objModel.ShortDescription;
                                    objUpdate.DDate = objModel.DDate;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { SectionId = objUpdate.SectionId, Title = objUpdate.Title };
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
        public object SetActiveSectionRegistrationDetail(SectionMasterCustomModel objSectionRegistrationModel)
        {
            object objSectionResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblSections.FirstOrDefault(x => x.SectionId == objSectionRegistrationModel.SectionId);

                        if (rs != null)
                        {
                            rs.IsActive = rs.IsActive == true ? false : true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objSectionRegistrationModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objSectionResult = true;
                        }
                        else
                        {
                            objSectionResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objSectionResult = null;
                        throw ex;
                    }
                }
                return objSectionResult;
            }
        }

        /// <summary>
        /// This method is used to delete particular farmer detail
        /// </summary>
        /// <param name="AssetId">Unique id of asset</param>
        /// <returns>Response</returns>
        public object DeleteSectionRegistrationDetail(SectionMasterCustomModel objSectionRegistrationModel)
        {
            object objSectionResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblSections.FirstOrDefault(x => x.SectionId == objSectionRegistrationModel.SectionId);

                        if (rs != null)
                        {
                            rs.IsDeleted = true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objSectionRegistrationModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objSectionResult = true;
                        }
                        else
                        {
                            objSectionResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objSectionResult = null;
                        throw ex;
                    }
                }
                return objSectionResult;
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
                        ActiveResult = dbcontext.tblSections.FirstOrDefault(x => x.SectionId == Id).IsActive ?? false;
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
