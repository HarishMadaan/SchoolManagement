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
    public class EventMasterRepo : IEventMasterRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public object GetEventMasterListing(EventMasterCustomModel objEventRegistrationModel)
        {
            IList<EventMasterCustomModel> EventListModel = new List<EventMasterCustomModel>();
            //IQueryable<EventMasterCustomModel> EventListDetail = null;
            //int TotalRec = 0;
            //int CurrentPageSize = 0;

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        //EventListDetail = dbcontext.tblEvents
                        //        .Select(x => new EventMasterCustomModel
                        //        {
                        //            EventId = x.EventId,
                        //            Title = x.Title,
                        //            Image = x.Image,
                        //            StartDate = x.StartDate,
                        //            EndDate = x.EndDate,
                        //            ShortDescription = x.ShortDescription,                                       
                                    
                        //            SchoolId = x.SchoolId,
                        //            DDate = x.DDate,
                        //            IsActive = x.IsActive,
                        //            //IsDeleted = x.IsDeleted,
                        //            //CreatedBy = x.CreatedBy,
                        //            //CreatedDate = x.CreatedDate,
                        //            //ModifiedBy = x.ModifiedBy,
                        //            //ModifiedDate = x.ModifiedDate
                        //        }).OrderByDescending(x => x.EmployeeId);

                        //if (objEmployeeRegistrationModel.pageModel != null)
                        //{
                        //    if (objEmployeeRegistrationModel.pageModel.SerachTerm != null && objEmployeeRegistrationModel.pageModel.SerachTerm != "")
                        //    {
                        //        EventListDetail = EventListDetail.Where(x =>
                        //        (x.FName.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                        //        || (x.LName.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                        //        || (x.EmailId.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                        //        || (x.Phone.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                        //        || (x.BloodGroup.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                        //        || (x.Designation.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                        //        || (x.Experience.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)
                        //        || (x.Qualification.ToLower().Trim().Contains(objEmployeeRegistrationModel.pageModel.SerachTerm.ToLower().Trim()) || objEmployeeRegistrationModel.pageModel.SerachTerm.Trim() == String.Empty)

                        //        );
                        //    }

                        //    if (!String.IsNullOrEmpty(objEmployeeRegistrationModel.pageModel.SortBy))
                        //    {
                        //        //EventListDetail= objEmployeeRegistrationModel.pageModel.SortDir.ToLower() == "desc" ? EventListDetail.OrderByDescending(objEmployeeRegistrationModel.pageModel.SortBy)
                        //        //    : EventListDetail.OrderBy(objEmployeeRegistrationModel.pageModel.SortBy);
                        //    }

                        //    TotalRec = EventListDetail.Count();
                        //    if (objEmployeeRegistrationModel.pageModel.PageSize > 0)
                        //    {
                        //        EventListDetail = EventListDetail.Skip(objEmployeeRegistrationModel.pageModel.Skip);
                        //        EventListDetail = EventListDetail.Take(objEmployeeRegistrationModel.pageModel.PageSize);
                        //    }

                        //    EventListModel = EventListDetail.ToList() as IList<EventMasterCustomModel>;

                        //}
                        //else
                        //{
                        //    TotalRec = EventListDetail.Count();
                        //    EventListModel = EventListDetail.ToList() as IList<EventMasterCustomModel>;
                        //}

                        //CurrentPageSize = objEmployeeRegistrationModel.pageModel.PageSize;

                        //if (EventListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    EventListModel[0].pageModel = new PagingViewModel();

                        //    EventListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        EventListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        EventListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return EventListModel;

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

        public Response SaveEventMasterDetails(EventMasterCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.EventId == 0)
                        {
                            var rs = dbcontext.tblEvents.FirstOrDefault(x => x.IsDeleted == false && x.Title == objModel.Title);
                            if (rs == null)
                            {
                                tblEvent objAddNew = new tblEvent
                                {
                                    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                    Title = objModel.Title,
                                    ShortDescription = objModel.ShortDescription,
                                    DDate = DateTime.Now,
                                    StartDate = objModel.StartDate,
                                    EndDate = objModel.EndDate,
                                    Image = objModel.Image,

                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblEvents.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { EventId = objAddNew.EventId, Title = objAddNew.Title };
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
                            var rs = dbcontext.tblEvents.FirstOrDefault(x => x.IsDeleted == false && x.Title == objModel.Title && x.EventId != objModel.EventId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblEvents.FirstOrDefault(m => m.EventId == objModel.EventId);
                                if (objUpdate != null)
                                {
                                    objUpdate.Title = objModel.Title;
                                    objUpdate.ShortDescription = objModel.ShortDescription;
                                    objUpdate.DDate = DateTime.Now;
                                    objUpdate.StartDate = objModel.StartDate;
                                    objUpdate.EndDate = objModel.EndDate;
                                    objUpdate.Image = objModel.Image;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { EventId = objUpdate.EventId, Title = objUpdate.Title };
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
        public object SetActiveEventRegistrationDetail(EventMasterCustomModel objEventRegistrationModel)
        {
            object objEmployeeResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblEvents.FirstOrDefault(x => x.EventId == objEventRegistrationModel.EventId);

                        if (rs != null)
                        {
                            rs.IsActive = rs.IsActive == true ? false : true;
                            //rs.ModifiedDate = DateTime.Now;
                            //rs.ModifiedBy = objEventRegistrationModel.ModifiedBy;

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
        public object DeleteEventRegistrationDetail(EventMasterCustomModel objEventRegistrationModel)
        {
            object objEmployeeResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblEvents.FirstOrDefault(x => x.EventId == objEventRegistrationModel.EventId);

                        if (rs != null)
                        {
                            rs.IsDeleted = true;
                            //rs.ModifiedDate = DateTime.Now;
                            //rs.ModifiedBy = objEmployeeRegistrationModel.ModifiedBy;

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
