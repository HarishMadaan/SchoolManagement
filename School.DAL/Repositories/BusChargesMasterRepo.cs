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
    public class BusChargesMasterRepo : IBusChargesMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public BusChargesMasterCustomModel GetById(int Id)
        {
            BusChargesMasterCustomModel objListModel = new BusChargesMasterCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblBusChargesMasters.Where(x => x.IsDeleted == false && x.BusChargesMasterId == Id)
                                .Select(x => new BusChargesMasterCustomModel
                                {
                                    BusChargesMasterId = x.BusChargesMasterId,
                                    SchoolId = x.SchoolId,
                                    SchoolName = x.tblSchool != null ? x.tblSchool.Name : "",
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    StartPoint = x.StartPoint,
                                    EndPoint = x.EndPoint,
                                    Amount = x.Amount,

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

        public object GetBusChargesMasterListing(BusChargesMasterCustomModel objModel)
        {
            IList<BusChargesMasterCustomModel> BusChargesListModel = new List<BusChargesMasterCustomModel>();
            IQueryable<BusChargesMasterCustomModel> BusChargesListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        BusChargesListDetail = dbcontext.tblBusChargesMasters.Where(x => x.IsDeleted == false
                        && (objModel.SessionId == null || objModel.SessionId == 0 || x.SessionId == objModel.SessionId)
                        )
                                .Select(x => new BusChargesMasterCustomModel
                                {
                                    BusChargesMasterId = x.BusChargesMasterId,
                                    SchoolId = x.SchoolId,
                                    SchoolName = x.tblSchool != null ? x.tblSchool.Name : "",
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    StartPoint = x.StartPoint,
                                    EndPoint = x.EndPoint,
                                    Amount = x.Amount,
                                    
                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.BusChargesMasterId);

                        if (objModel.pageModel != null)
                        {
                            if (objModel.pageModel.SerachTerm != null && objModel.pageModel.SerachTerm != "")
                            {

                            }

                            if (!String.IsNullOrEmpty(objModel.pageModel.SortBy))
                            {
                                //BusChargesListDetail= objModel.pageModel.SortDir.ToLower() == "desc" ? BusChargesListDetail.OrderByDescending(objModel.pageModel.SortBy)
                                //    : BusChargesListDetail.OrderBy(objModel.pageModel.SortBy);
                            }

                            TotalRec = BusChargesListDetail.Count();
                            if (objModel.pageModel.PageSize > 0)
                            {
                                BusChargesListDetail = BusChargesListDetail.Skip(objModel.pageModel.Skip);
                                BusChargesListDetail = BusChargesListDetail.Take(objModel.pageModel.PageSize);
                            }

                            BusChargesListModel = BusChargesListDetail.ToList() as IList<BusChargesMasterCustomModel>;

                        }
                        else
                        {
                            TotalRec = BusChargesListDetail.Count();
                            BusChargesListModel = BusChargesListDetail.ToList() as IList<BusChargesMasterCustomModel>;
                        }

                        //CurrentPageSize = objModel.pageModel.PageSize;

                        //if (BusChargesListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    BusChargesListModel[0].pageModel = new PagingViewModel();

                        //    BusChargesListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        BusChargesListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        BusChargesListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return BusChargesListModel;

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

        public Response SaveBusChargesMasterDetails(BusChargesMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.BusChargesMasterId == 0)
                        {
                            var rs = dbcontext.tblBusChargesMasters.FirstOrDefault(x => x.IsDeleted == false && x.SessionId == objModel.SessionId && x.StartPoint == objModel.StartPoint && x.EndPoint == objModel.EndPoint);
                            if (rs == null)
                            {
                                tblBusChargesMaster objAddNew = new tblBusChargesMaster
                                {
                                    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                    SessionId = objModel.SessionId,
                                    StartPoint = objModel.StartPoint,
                                    EndPoint = objModel.EndPoint,
                                    Amount = objModel.Amount,
                                    
                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblBusChargesMasters.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { BusChargesMasterId = objAddNew.BusChargesMasterId, StartPoint = objAddNew.StartPoint};
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
                            var rs = dbcontext.tblBusChargesMasters.FirstOrDefault(x => x.IsDeleted == false && x.SessionId == objModel.SessionId && x.StartPoint == objModel.StartPoint && x.EndPoint == objModel.EndPoint && x.BusChargesMasterId != objModel.BusChargesMasterId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblBusChargesMasters.FirstOrDefault(m => m.BusChargesMasterId == objModel.BusChargesMasterId);
                                if (objUpdate != null)
                                {
                                    objUpdate.SessionId = objModel.SessionId;
                                    objUpdate.StartPoint = objModel.StartPoint;
                                    objUpdate.EndPoint = objModel.EndPoint;
                                    objUpdate.Amount = objModel.Amount;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { BusChargesMasterId = objUpdate.BusChargesMasterId, StartPoint = objUpdate.StartPoint };
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

        public object SetActiveBusChargesRegistrationDetail(BusChargesMasterCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblBusChargesMasters.FirstOrDefault(x => x.BusChargesMasterId == objModel.BusChargesMasterId);

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

        public object DeleteBusChargesRegistrationDetail(BusChargesMasterCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblBusChargesMasters.FirstOrDefault(x => x.BusChargesMasterId == objModel.BusChargesMasterId);

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
                        ActiveResult = dbcontext.tblBusChargesMasters.FirstOrDefault(x => x.BusChargesMasterId == Id).IsActive ?? false;
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
