using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.DAL.Interfaces;

namespace School.DAL.Repositories
{
    public class SectionAllocationRepo : ISectionAllocationRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public object GetSectionAllocationListing(SectionAllocationCustomModel objModel)
        {
            IList<SectionAllocationCustomModel> SectionAllocationListModel = new List<SectionAllocationCustomModel>();
            IQueryable<SectionAllocationCustomModel> SectionAllocationListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        SectionAllocationListDetail = dbcontext.tblPromoteToClasses.Where(x => x.IsDeleted == false)
                                .Select(x => new SectionAllocationCustomModel
                                {
                                    PromoteToClassId = x.PromoteToClassId,
                                    SessionId = x.SessionId,
                                    EnquiryId = x.EnquiryId,
                                    AdmissionId = x.AdmissionId,
                                    ClassId = x.ClassId,
                                    SectionId = x.SectionId,
                                    Status = x.Status,
                                    
                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.PromoteToClassId);

                        if (objModel.pageModel != null)
                        {
                            //if (objModel.pageModel.SerachTerm != null && objModel.pageModel.SerachTerm != "")
                            //{
                            //    SectionAllocationListDetail = SectionAllocationListDetail.Where(x =>
                            //    (x.Title.ToLower().Trim().Contains(objModel.pageModel.SerachTerm.ToLower().Trim()) || objModel.pageModel.SerachTerm.Trim() == String.Empty)
                            //    || (x.ShortDescription.ToLower().Trim().Contains(objModel.pageModel.SerachTerm.ToLower().Trim()) || objModel.pageModel.SerachTerm.Trim() == String.Empty)
                            //    );
                            //}

                            if (!String.IsNullOrEmpty(objModel.pageModel.SortBy))
                            {
                                //SectionAllocationListDetail= objModel.pageModel.SortDir.ToLower() == "desc" ? SectionAllocationListDetail.OrderByDescending(objModel.pageModel.SortBy)
                                //    : SectionAllocationListDetail.OrderBy(objModel.pageModel.SortBy);
                            }

                            TotalRec = SectionAllocationListDetail.Count();
                            if (objModel.pageModel.PageSize > 0)
                            {
                                SectionAllocationListDetail = SectionAllocationListDetail.Skip(objModel.pageModel.Skip);
                                SectionAllocationListDetail = SectionAllocationListDetail.Take(objModel.pageModel.PageSize);
                            }

                            SectionAllocationListModel = SectionAllocationListDetail.ToList() as IList<SectionAllocationCustomModel>;

                        }
                        else
                        {
                            TotalRec = SectionAllocationListDetail.Count();
                            SectionAllocationListModel = SectionAllocationListDetail.ToList() as IList<SectionAllocationCustomModel>;
                        }

                        //CurrentPageSize = objModel.pageModel.PageSize;

                        //if (SectionAllocationListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    SectionAllocationListModel[0].pageModel = new PagingViewModel();

                        //    SectionAllocationListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        SectionAllocationListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        SectionAllocationListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return SectionAllocationListModel;

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

        public Response SaveSectionAllocationDetails(SectionAllocationCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.PromoteToClassId == 0)
                        {
                            var rs = dbcontext.tblPromoteToClasses.FirstOrDefault(x => x.IsDeleted == false && x.SessionId == objModel.SessionId && x.ClassId == objModel.ClassId && x.AdmissionId == objModel.AdmissionId);
                            if (rs == null)
                            {
                                tblPromoteToClass objAddNew = new tblPromoteToClass
                                {
                                    SessionId = objModel.SessionId,                                    
                                    //EnquiryId = objModel.EnquiryId,
                                    AdmissionId = objModel.AdmissionId,
                                    ClassId = objModel.ClassId,
                                    SectionId = objModel.SectionId,
                                    Status = "Attending",

                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now
                                };

                                dbcontext.tblPromoteToClasses.Add(objAddNew);
                                dbcontext.SaveChanges();
                                response.responseData = new { PromoteToClassId = objAddNew.PromoteToClassId, Status = objAddNew.Status };
                                response.message = "Record Added Successfully!";
                            }
                            else
                            {   
                                rs.SectionId = objModel.SectionId;
                                rs.Status = objModel.Status;
                                rs.IsActive = true;

                                rs.ModifiedBy = objModel.ModifiedBy;
                                rs.ModifiedDate = DateTime.Now;
                                dbcontext.SaveChanges();
                                response.responseData = new { PromoteToClassId = rs.PromoteToClassId, Status = rs.Status };
                                response.message = "Record Updated Successfully!";                                
                            }
                        }
                        else
                        {
                            var rs = dbcontext.tblPromoteToClasses.FirstOrDefault(x => x.IsDeleted == false && x.SessionId == objModel.SessionId && x.ClassId == objModel.ClassId && x.AdmissionId == objModel.AdmissionId && x.PromoteToClassId != objModel.PromoteToClassId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblPromoteToClasses.FirstOrDefault(m => m.PromoteToClassId == objModel.PromoteToClassId);
                                if (objUpdate != null)
                                {
                                    objUpdate.SessionId = objModel.SessionId;
                                    objUpdate.EnquiryId = objModel.EnquiryId;
                                    objUpdate.AdmissionId = objModel.AdmissionId;
                                    objUpdate.ClassId = objModel.ClassId;
                                    objUpdate.SectionId = objModel.SectionId;
                                    
                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    response.responseData = new { PromoteToClassId = objUpdate.PromoteToClassId, Status = objUpdate.Status };
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
        
        public int GetTotalStudentInSection(int? SessionId, int? ClassId, int? SectionId)
        {
            int TotalStudent = 0;
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        TotalStudent = dbcontext.tblPromoteToClasses.Where(x => x.IsDeleted == false
                            && (SessionId == null || SessionId == 0 || x.SessionId == SessionId)
                            && (ClassId == null || ClassId == 0 || x.ClassId == ClassId)
                            && (SectionId == null || SectionId == 0 || x.SectionId == SectionId)
                        ).Count();
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        TotalStudent = 0;
                    }
                }
            }
            return TotalStudent;
        }
        
        public int InsertStudentSectionAllocation(int SessionId, int ClassId, int EnquiryId, int AdmissionId, int SectionId, bool isactive, string status)
        {
            int TotalStudent = 0;
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        tblPromoteToClass obj = new tblPromoteToClass();
                        if (obj != null)
                        {
                            obj.AdmissionId = AdmissionId;
                            obj.SectionId = SectionId;
                            obj.IsActive = isactive;
                            obj.Status = status;
                            dbcontext.tblPromoteToClasses.Add(obj);
                            dbcontext.SaveChanges();

                            tblEnquiryDetail obj1 = dbcontext.tblEnquiryDetails.Where(x => x.EnquiryId == EnquiryId).FirstOrDefault();
                            if (obj1 != null)
                            {
                                obj1.SectionId = SectionId;
                                dbcontext.SaveChanges();
                            }

                            tblAdmission obj2 = dbcontext.tblAdmissions.Where(x => x.AdmissionId == AdmissionId).FirstOrDefault();
                            if (obj2 != null)
                            {
                                obj2.InSectionStatus = status;
                                dbcontext.SaveChanges();
                            }
                        }

                        TotalStudent = 1;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        TotalStudent = 0;
                    }
                }
            }
            return TotalStudent;
        }

        public int CancelAllocatedStudentSection(int Id)
        {
            int Result = 0;
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        var rs = dbcontext.tblPromoteToClasses.FirstOrDefault(x => x.PromoteToClassId == Id);
                        if (rs != null)
                        {
                            rs.SectionId = null;
                            rs.RollNo = null;
                            rs.Status = null;
                        }
                        dbcontext.SaveChanges();
                        Result = 1;
                    }
                    catch (Exception ex)
                    {
                        response.success = false;
                        response.message = ex.Message;
                        Result = 0;
                    }
                }
            }
            return Result;
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
