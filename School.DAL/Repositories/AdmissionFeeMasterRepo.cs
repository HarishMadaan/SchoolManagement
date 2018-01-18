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
    public class AdmissionFeeMasterRepo : IAdmissionFeeMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public AdmissionFeeMasterCustomModel GetById(int Id)
        {
            AdmissionFeeMasterCustomModel objListModel = new AdmissionFeeMasterCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblAdmissionFees.Where(x => x.IsDeleted == false && x.AdmissionFeeId == Id)
                                .Select(x => new AdmissionFeeMasterCustomModel
                                {
                                    AdmissionFeeId = x.AdmissionFeeId,
                                    SchoolId = x.SchoolId,
                                    SchoolName = x.SchoolId != null ? x.tblSchool.Name : "",
                                    ClassId = x.ClassId,
                                    ClassName = x.ClassId != null ? x.tblClass.Title : "",
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    AdmissionFee = x.AdmissionFee,
                                    Discount = x.Discount,
                                    NetAdmissionFee = x.NetAdmissionFee,

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

        public object GetAdmissionFeeMasterListing(AdmissionFeeMasterCustomModel objModel)
        {
            IList<AdmissionFeeMasterCustomModel> AdmissionFeeListModel = new List<AdmissionFeeMasterCustomModel>();
            IQueryable<AdmissionFeeMasterCustomModel> AdmissionFeeListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        AdmissionFeeListDetail = dbcontext.tblAdmissionFees.Where(x => x.IsDeleted == false
                        && (objModel.SessionId == null || objModel.SessionId == 0 || x.tblClass.SessionId == objModel.SessionId)
                        && (objModel.ClassId == null || objModel.ClassId == 0 || x.ClassId == objModel.ClassId)
                        )
                                .Select(x => new AdmissionFeeMasterCustomModel
                                {
                                    AdmissionFeeId = x.AdmissionFeeId,
                                    SchoolId = x.SchoolId,
                                    SchoolName = x.SchoolId != null ? x.tblSchool.Name : "",
                                    ClassId = x.ClassId,
                                    ClassName = x.ClassId != null ? x.tblClass.Title : "",
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    AdmissionFee = x.AdmissionFee,
                                    Discount = x.Discount,
                                    NetAdmissionFee = x.NetAdmissionFee,

                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.AdmissionFeeId);

                         if (objModel.pageModel != null)
                        {
                            if (objModel.pageModel.SerachTerm != null && objModel.pageModel.SerachTerm != "")
                            {

                            }

                            if (!String.IsNullOrEmpty(objModel.pageModel.SortBy))
                            {
                                //AdmissionFeeListDetail= objModel.pageModel.SortDir.ToLower() == "desc" ? AdmissionFeeListDetail.OrderByDescending(objModel.pageModel.SortBy)
                                //    : AdmissionFeeListDetail.OrderBy(objModel.pageModel.SortBy);
                            }

                            TotalRec = AdmissionFeeListDetail.Count();
                            if (objModel.pageModel.PageSize > 0)
                            {
                                AdmissionFeeListDetail = AdmissionFeeListDetail.Skip(objModel.pageModel.Skip);
                                AdmissionFeeListDetail = AdmissionFeeListDetail.Take(objModel.pageModel.PageSize);
                            }

                            AdmissionFeeListModel = AdmissionFeeListDetail.ToList() as IList<AdmissionFeeMasterCustomModel>;

                        }
                        else
                        {
                            TotalRec = AdmissionFeeListDetail.Count();
                            AdmissionFeeListModel = AdmissionFeeListDetail.ToList() as IList<AdmissionFeeMasterCustomModel>;
                        }

                        //CurrentPageSize = objModel.pageModel.PageSize;

                        //if (AdmissionFeeListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    AdmissionFeeListModel[0].pageModel = new PagingViewModel();

                        //    AdmissionFeeListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        AdmissionFeeListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        AdmissionFeeListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return AdmissionFeeListModel;

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

        public Response SaveAdmissionFeeMasterDetails(AdmissionFeeMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.AdmissionFeeId == 0)
                        {
                            var rs = dbcontext.tblAdmissionFees.FirstOrDefault(x => x.IsDeleted == false && x.SchoolId == objModel.SchoolId && x.SessionId == objModel.SessionId && x.ClassId == objModel.ClassId);
                            if (rs == null)
                            {
                                tblAdmissionFee objAddNew = new tblAdmissionFee
                                {
                                    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                    SessionId = objModel.SessionId,
                                    ClassId = objModel.ClassId,
                                    AdmissionFee = objModel.AdmissionFee,
                                    Discount = objModel.Discount,
                                    NetAdmissionFee = objModel.NetAdmissionFee,
                                    
                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblAdmissionFees.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { AdmissionFeeId = objAddNew.AdmissionFeeId, AdmissionFee = objAddNew.AdmissionFee };
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
                            var rs = dbcontext.tblAdmissionFees.FirstOrDefault(x => x.IsDeleted == false && x.SchoolId == objModel.SchoolId && x.SessionId == objModel.SessionId && x.ClassId == objModel.ClassId && x.AdmissionFeeId != objModel.AdmissionFeeId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblAdmissionFees.FirstOrDefault(m => m.AdmissionFeeId == objModel.AdmissionFeeId);
                                if (objUpdate != null)
                                {
                                    objUpdate.ClassId = objModel.ClassId;
                                    objUpdate.SessionId = objModel.SessionId;
                                    objUpdate.AdmissionFee = objModel.AdmissionFee;
                                    objUpdate.Discount = objModel.Discount;
                                    objUpdate.NetAdmissionFee = objModel.NetAdmissionFee;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { AdmissionFeeId = objUpdate.AdmissionFeeId, AdmissionFee = objUpdate.AdmissionFee };
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

        public object SetActiveAdmissionFeeRegistrationDetail(AdmissionFeeMasterCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblAdmissionFees.FirstOrDefault(x => x.AdmissionFeeId == objModel.AdmissionFeeId);

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

        public object DeleteAdmissionFeeRegistrationDetail(AdmissionFeeMasterCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblAdmissionFees.FirstOrDefault(x => x.AdmissionFeeId == objModel.AdmissionFeeId);

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
                        ActiveResult = dbcontext.tblAdmissionFees.FirstOrDefault(x => x.AdmissionFeeId == Id).IsActive ?? false;
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
