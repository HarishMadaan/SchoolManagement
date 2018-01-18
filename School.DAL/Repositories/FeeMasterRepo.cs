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
    public class FeeMasterRepo : IFeeMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public FeeMasterCustomModel GetById(int Id)
        {
            FeeMasterCustomModel objListModel = new FeeMasterCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblFeeMasters.Where(x => x.IsDeleted == false && x.FeeMasterId == Id)
                                .Select(x => new FeeMasterCustomModel
                                {
                                    FeeMasterId = x.FeeMasterId,
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    ClassId = x.ClassId,
                                    ClassName = x.ClassId != null ? x.tblClass.Title : "",
                                    SchoolId = x.SchoolId,
                                    TotalFee = x.TotalFee,
                                    Discount = x.Discount,
                                    NetFee = x.NetFee,

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

        public object GetFeeMasterListing(FeeMasterCustomModel objModel)
        {
            IList<FeeMasterCustomModel> FeeListModel = new List<FeeMasterCustomModel>();
            IQueryable<FeeMasterCustomModel> FeeListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        FeeListDetail = dbcontext.tblFeeMasters.Where(x => x.IsDeleted == false
                        && (objModel.SessionId == null || objModel.SessionId == 0 || x.tblClass.SessionId == objModel.SessionId)
                        && (objModel.ClassId == null || objModel.ClassId == 0 || x.ClassId == objModel.ClassId)
                        )
                                .Select(x => new FeeMasterCustomModel
                                {
                                    FeeMasterId = x.FeeMasterId,
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    ClassId = x.ClassId,
                                    ClassName = x.ClassId != null ? x.tblClass.Title : "",
                                    SchoolId = x.SchoolId,
                                    TotalFee = x.TotalFee,
                                    Discount = x.Discount,
                                    NetFee = x.NetFee,

                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.FeeMasterId);

                        if (objModel.pageModel != null)
                        {
                            if (objModel.pageModel.SerachTerm != null && objModel.pageModel.SerachTerm != "")
                            {
                                //FeeListDetail = FeeListDetail.Where(x =>
                                //(x.TotalFee.ToLower().Trim().Contains(objModel.pageModel.SerachTerm.ToLower().Trim()) || objModel.pageModel.SerachTerm.Trim() == String.Empty)
                                //|| (x.ShortDescription.ToLower().Trim().Contains(objClassRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objClassRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                                //);
                            }

                            if (!String.IsNullOrEmpty(objModel.pageModel.SortBy))
                            {
                                //ClassListDetail= objClassRegistrationModel.pageModel.SortDir.ToLower() == "desc" ? ClassListDetail.OrderByDescending(objClassRegistrationModel.pageModel.SortBy)
                                //    : ClassListDetail.OrderBy(objClassRegistrationModel.pageModel.SortBy);
                            }

                            TotalRec = FeeListDetail.Count();
                            if (objModel.pageModel.PageSize > 0)
                            {
                                FeeListDetail = FeeListDetail.Skip(objModel.pageModel.Skip);
                                FeeListDetail = FeeListDetail.Take(objModel.pageModel.PageSize);
                            }

                            FeeListModel = FeeListDetail.ToList() as IList<FeeMasterCustomModel>;

                        }
                        else
                        {
                            TotalRec = FeeListDetail.Count();
                            FeeListModel = FeeListDetail.ToList() as IList<FeeMasterCustomModel>;
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

                        return FeeListModel;

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

        public Response SaveFeeMasterDetails(FeeMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.FeeMasterId == 0)
                        {
                            var rs = dbcontext.tblFeeMasters.FirstOrDefault(x => x.IsDeleted == false && x.ClassId == objModel.ClassId);
                            if (rs == null)
                            {
                                tblFeeMaster objAddNew = new tblFeeMaster
                                {
                                    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                    SessionId = objModel.SessionId,
                                    ClassId = objModel.ClassId,
                                    TotalFee = objModel.TotalFee,
                                    Discount = objModel.Discount,
                                    NetFee = objModel.NetFee,

                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblFeeMasters.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { FeeMasterId = objAddNew.FeeMasterId, TotalFee = objAddNew.TotalFee };
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
                            var rs = dbcontext.tblFeeMasters.FirstOrDefault(x => x.IsDeleted == false && x.ClassId == objModel.ClassId && x.FeeMasterId != objModel.FeeMasterId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblFeeMasters.FirstOrDefault(m => m.FeeMasterId == objModel.FeeMasterId);
                                if (objUpdate != null)
                                {
                                    objUpdate.SessionId = objModel.SessionId;
                                    objUpdate.ClassId = objModel.ClassId;
                                    objUpdate.TotalFee = objModel.TotalFee;
                                    objUpdate.Discount = objModel.Discount;
                                    objUpdate.NetFee = objModel.NetFee;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { FeeMasterId = objUpdate.FeeMasterId, TotalFee = objUpdate.TotalFee };
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

        public object SetActiveFeeRegistrationDetail(FeeMasterCustomModel objModel)
        {
            object objFeeResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblFeeMasters.FirstOrDefault(x => x.FeeMasterId == objModel.FeeMasterId);

                        if (rs != null)
                        {
                            rs.IsActive = rs.IsActive == true ? false : true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objFeeResult = true;
                        }
                        else
                        {
                            objFeeResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objFeeResult = null;
                        throw ex;
                    }
                }
                return objFeeResult;
            }
        }

        public object DeleteFeeRegistrationDetail(FeeMasterCustomModel objModel)
        {
            object objFeeResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblFeeMasters.FirstOrDefault(x => x.FeeMasterId == objModel.FeeMasterId);

                        if (rs != null)
                        {
                            rs.IsDeleted = true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objModel.ModifiedBy;

                            dbcontext.SaveChanges();
                            objFeeResult = true;
                        }
                        else
                        {
                            objFeeResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        objFeeResult = null;
                        throw ex;
                    }
                }
                return objFeeResult;
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
                        ActiveResult = dbcontext.tblFeeMasters.FirstOrDefault(x => x.FeeMasterId == Id).IsActive ?? false;
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
