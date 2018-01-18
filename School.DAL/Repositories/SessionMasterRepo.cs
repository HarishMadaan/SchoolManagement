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
    public class SessionMasterRepo : ISessionMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public SessionMasterCustomModel GetById(int Id)
        {
            SessionMasterCustomModel objListModel = new SessionMasterCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblSessions.Where(x => x.IsDeleted == false && x.SessionId == Id)
                                .Select(x => new SessionMasterCustomModel
                                {
                                    SessionId = x.SessionId,
                                    Session = x.Session,
                                    Description = x.Description,
                                    DDate = x.DDate,
                                    IsDefault = x.IsDefault,

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

        public object GetSessionMasterListing(SessionMasterCustomModel objModel)
        {
            IList<SessionMasterCustomModel> SessionListModel = new List<SessionMasterCustomModel>();
            IQueryable<SessionMasterCustomModel> SessionListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        SessionListDetail = dbcontext.tblSessions.Where(x => x.IsDeleted == false)
                                .Select(x => new SessionMasterCustomModel
                                {
                                    SessionId = x.SessionId,
                                    Session = x.Session,
                                    Description = x.Description,
                                    DDate = x.DDate,
                                    IsDefault = x.IsDefault,                                    

                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.SessionId);

                        if (objModel.pageModel != null)
                        {
                            if (objModel.pageModel.SerachTerm != null && objModel.pageModel.SerachTerm != "")
                            {
                                SessionListDetail = SessionListDetail.Where(x =>
                                (x.Session.ToLower().Trim().Contains(objModel.pageModel.SerachTerm.ToLower().Trim()) || objModel.pageModel.SerachTerm.Trim() == String.Empty)
                                );
                            }

                            if (!String.IsNullOrEmpty(objModel.pageModel.SortBy))
                            {
                                //ClassListDetail= objClassRegistrationModel.pageModel.SortDir.ToLower() == "desc" ? ClassListDetail.OrderByDescending(objClassRegistrationModel.pageModel.SortBy)
                                //    : ClassListDetail.OrderBy(objClassRegistrationModel.pageModel.SortBy);
                            }

                            TotalRec = SessionListDetail.Count();
                            if (objModel.pageModel.PageSize > 0)
                            {
                                SessionListDetail = SessionListDetail.Skip(objModel.pageModel.Skip);
                                SessionListDetail = SessionListDetail.Take(objModel.pageModel.PageSize);
                            }

                            SessionListModel = SessionListDetail.ToList() as IList<SessionMasterCustomModel>;

                        }
                        else
                        {
                            TotalRec = SessionListDetail.Count();
                            SessionListModel = SessionListDetail.ToList() as IList<SessionMasterCustomModel>;
                        }

                        CurrentPageSize = objModel.pageModel.PageSize;

                        if (SessionListModel.Count() > 0 && CurrentPageSize > 0)
                        {
                            SessionListModel[0].pageModel = new PagingViewModel();

                            SessionListModel[0].pageModel.TotalRecords = TotalRec;
                            if ((TotalRec % CurrentPageSize) == 0)
                            {
                                SessionListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                            }
                            else
                            {
                                SessionListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                            }
                        }

                        return SessionListModel;

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

        public Response SaveSessionMasterDetails(SessionMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.SessionId == 0)
                        {
                            var rs = dbcontext.tblSessions.FirstOrDefault(x => x.IsDeleted == false && x.Session == objModel.Session);
                            if (rs == null)
                            {
                                tblSession objAddNew = new tblSession
                                {
                                    Session = objModel.Session,
                                    Description = objModel.Description,
                                    DDate = DateTime.Now,

                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblSessions.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { SessionId = objAddNew.SessionId, Session = objAddNew.Session };
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
                            var rs = dbcontext.tblSessions.FirstOrDefault(x => x.IsDeleted == false && x.Session == objModel.Session && x.SessionId != objModel.SessionId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblSessions.FirstOrDefault(m => m.SessionId == objModel.SessionId);
                                if (objUpdate != null)
                                {
                                    objUpdate.Session = objModel.Session;
                                    objUpdate.Description = objModel.Description;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { SessionId = objUpdate.SessionId, Session = objUpdate.Session };
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

        public object SetDefaultSessionRegistrationDetail(SessionMasterCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var CheckActive = dbcontext.tblSessions.FirstOrDefault(x => x.IsDefault == true && x.SessionId != objModel.SessionId);
                        if (CheckActive == null)
                        {
                            var rs = dbcontext.tblSessions.FirstOrDefault(x => x.SessionId == objModel.SessionId);
                            if (rs != null)
                            {
                                rs.IsDefault = rs.IsDefault == true ? false : true;
                                rs.ModifiedDate = DateTime.Now;
                                rs.ModifiedBy = objModel.ModifiedBy;

                                dbcontext.SaveChanges();
                                objClassResult = true;
                            }
                            else
                            {
                                objClassResult = false;
                            }
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
        /// This method is used to Activate particular farmer detail
        /// </summary>
        /// <param name="AssetId">Unique id of Farmer</param>
        /// <returns>Response</returns>
        public object SetActiveSessionRegistrationDetail(SessionMasterCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblSessions.FirstOrDefault(x => x.SessionId == objModel.SessionId);

                        if (rs != null)
                        {
                            rs.IsActive = rs.IsActive == true ? false : true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objModel.ModifiedBy;

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

        public object DeleteSessionRegistrationDetail(SessionMasterCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblSessions.FirstOrDefault(x => x.SessionId == objModel.SessionId);

                        if (rs != null)
                        {
                            rs.IsDeleted = true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objModel.ModifiedBy;

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
                        ActiveResult = dbcontext.tblSessions.FirstOrDefault(x => x.SessionId == Id).IsActive ?? false;
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
