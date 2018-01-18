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
    public class ClassMasterRepo : IClassMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;
        
        public ClassMasterCustomModel GetById(int Id)
        {
            ClassMasterCustomModel objListModel = new ClassMasterCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblClasses.Where(x => x.IsDeleted == false && x.ClassId == Id)
                                .Select(x => new ClassMasterCustomModel
                                {
                                    ClassId = x.ClassId,
                                    SchoolId = x.SchoolId,
                                    SchoolName = x.SchoolId != null ? x.tblSchool.Name : "",
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    Title = x.Title,
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
                        objListModel = null;
                    }
                }
            }
            return objListModel;
        }

        public object GetClassMasterListing(ClassMasterCustomModel objClassRegistrationModel)
        {
            IList<ClassMasterCustomModel> ClassListModel = new List<ClassMasterCustomModel>();
            IQueryable<ClassMasterCustomModel> ClassListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;
            
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        ClassListDetail = dbcontext.tblClasses.Where(x => x.IsDeleted == false
                        && (objClassRegistrationModel.SessionId == null || objClassRegistrationModel.SessionId == 0 || x.SessionId == objClassRegistrationModel.SessionId)
                        )
                                .Select(x => new ClassMasterCustomModel
                                {
                                    ClassId = x.ClassId,
                                    SchoolId = x.SchoolId,
                                    SchoolName = x.tblSchool != null ? x.tblSchool.Name : "",
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    Title = x.Title,
                                    DDate = x.DDate,
                                    ShortDescription = x.ShortDescription,
                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.ClassId);

                        if (objClassRegistrationModel.pageModel != null)
                        {
                            if (objClassRegistrationModel.pageModel.SerachTerm != null && objClassRegistrationModel.pageModel.SerachTerm != "")
                            {
                                ClassListDetail = ClassListDetail.Where(x =>
                                (x.Title.ToLower().Trim().Contains(objClassRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objClassRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.ShortDescription.ToLower().Trim().Contains(objClassRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objClassRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)                                
                                );
                            }

                            if (!String.IsNullOrEmpty(objClassRegistrationModel.pageModel.SortBy))
                            {
                                //ClassListDetail= objClassRegistrationModel.pageModel.SortDir.ToLower() == "desc" ? ClassListDetail.OrderByDescending(objClassRegistrationModel.pageModel.SortBy)
                                //    : ClassListDetail.OrderBy(objClassRegistrationModel.pageModel.SortBy);
                            }

                            TotalRec = ClassListDetail.Count();
                            if (objClassRegistrationModel.pageModel.PageSize > 0)
                            {
                                ClassListDetail = ClassListDetail.Skip(objClassRegistrationModel.pageModel.Skip);
                                ClassListDetail = ClassListDetail.Take(objClassRegistrationModel.pageModel.PageSize);
                            }

                            ClassListModel = ClassListDetail.ToList() as IList<ClassMasterCustomModel>;

                        }
                        else
                        {
                            TotalRec = ClassListDetail.Count();
                            ClassListModel = ClassListDetail.ToList() as IList<ClassMasterCustomModel>;
                        }

                        //CurrentPageSize = objClassRegistrationModel.pageModel.PageSize;

                        //if (ClassListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    ClassListModel[0].pageModel = new PagingViewModel();

                        //    ClassListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        ClassListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        ClassListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return ClassListModel;

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

        public Response SaveClassMasterDetails(ClassMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.ClassId == 0)
                        {
                            var rs = dbcontext.tblClasses.FirstOrDefault(x => x.IsDeleted == false && x.SessionId == objModel.SessionId && x.Title == objModel.Title);
                            if (rs == null)
                            {
                                tblClass objAddNew = new tblClass
                                {
                                    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                    SessionId = objModel.SessionId,
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

                                dbcontext.tblClasses.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { ClassId = objAddNew.ClassId, Title = objAddNew.Title };
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
                            var rs = dbcontext.tblClasses.FirstOrDefault(x => x.IsDeleted == false && x.SessionId == objModel.SessionId && x.Title == objModel.Title && x.ClassId != objModel.ClassId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblClasses.FirstOrDefault(m => m.ClassId == objModel.ClassId);
                                if (objUpdate != null)
                                {
                                    objUpdate.SessionId = objModel.SessionId;
                                    objUpdate.Title = objModel.Title;
                                    objUpdate.ShortDescription = objModel.ShortDescription;
                                    objUpdate.DDate = objModel.DDate;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { ClassId = objUpdate.ClassId, Title = objUpdate.Title };
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
        public object SetActiveClassRegistrationDetail(ClassMasterCustomModel objClassRegistrationModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblClasses.FirstOrDefault(x => x.ClassId == objClassRegistrationModel.ClassId);

                        if (rs != null)
                        {
                            rs.IsActive = rs.IsActive == true ? false : true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objClassRegistrationModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objClassResult = true;
                        }
                        else
                        {
                            objClassResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objClassResult = null;
                        throw ex;
                    }
                }
                return objClassResult;
            }
        }

        /// <summary>
        /// This method is used to delete particular farmer detail
        /// </summary>
        /// <param name="AssetId">Unique id of asset</param>
        /// <returns>Response</returns>
        public object DeleteClassRegistrationDetail(ClassMasterCustomModel objClassRegistrationModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblClasses.FirstOrDefault(x => x.ClassId == objClassRegistrationModel.ClassId);

                        if (rs != null)
                        {
                            rs.IsDeleted = true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objClassRegistrationModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objClassResult = true;
                        }
                        else
                        {
                            objClassResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objClassResult = null;
                        throw ex;
                    }
                }
                return objClassResult;
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
                        ActiveResult = dbcontext.tblClasses.FirstOrDefault(x => x.ClassId == Id).IsActive ?? false;
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
