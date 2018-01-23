using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.DAL.Interfaces;

namespace School.DAL.Repositories
{
    public class PromoteToClassRepo : IPromoteToClassRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public object BindSessionClassEnquiry(int? SessionId, int? ClassId, int? SectionId, string StudentName)
        {
            List<EnquiryDetailCustomModel> objListModel = new List<EnquiryDetailCustomModel>();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        //  -------------- Main -----------------
                        objListModel = (from P in dbcontext.tblPromoteToClasses
                            join A in dbcontext.tblAdmissions on P.AdmissionId equals A.AdmissionId
                            join E in dbcontext.tblEnquiryDetails on A.EnquiryId equals E.EnquiryId
                            where E.IsDeleted == false && E.Status == "Registered"
                            && P.IsActive == true
                            && (SessionId == null || SessionId == 0 || P.SessionId == SessionId)
                            && (ClassId == null || ClassId == 0 || P.ClassId == ClassId)
                            && (SectionId == null || SectionId == 0 || P.SectionId == SectionId)
                            select new EnquiryDetailCustomModel
                            {
                                SessionId = P.SessionId,
                                SessionName = P.SessionId != null ? P.tblSession.Session : "",
                                ClassId = P.ClassId,
                                ClassName = P.ClassId != null ? P.tblClass.Title : "",
                                SectionId = P.SectionId,
                                SectionName = P.SectionId != null ? P.tblSection.Title : "",
                                //AdmissionId = P.AdmissionId,
                                AdmissionModel = dbcontext.tblAdmissions.Where(a => a.EnquiryId == E.EnquiryId).Select(s => new AdmissionCustomModel
                                {
                                    AdmissionId = s.AdmissionId,
                                    AdmissionDate = s.AdmissionDate,
                                    TotalFees = s.TotalFees,
                                    RegistrationFees = s.RegistrationFees,
                                    DiscountType = s.DiscountType,
                                    DiscountAmount = s.DiscountAmount,
                                    FeeIncharge = s.FeeIncharge,
                                    BalanceAmountDue = s.BalanceAmountDue,
                                    PaymentMode = s.PaymentMode
                                }).FirstOrDefault(),
                                                                
                                FName = E.FName,
                                LName = E.LName,
                                MobileNo = E.MobileNo,
                                EmailId = E.EmailId,
                            }).ToList();
                        // -------------- Main End -----------------

                        return objListModel;
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

        public Response SavePromoteToClassDetails(PromoteToClassCustomModel objModel)
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
                            var rs = dbcontext.tblPromoteToClasses.FirstOrDefault(x => x.IsDeleted == false && x.SessionId == objModel.SessionId && x.ClassId == objModel.ClassId && x.SectionId == objModel.SectionId && x.AdmissionId == objModel.AdmissionId);
                            if (rs == null)
                            {
                                var AdmId = dbcontext.tblPromoteToClasses.Where(x => x.IsDeleted == false && x.IsActive == true && x.AdmissionId == objModel.AdmissionId);

                                if (AdmId != null)
                                {
                                    foreach (var item in AdmId)
                                    {
                                        item.IsActive = false;
                                    }
                                    dbcontext.SaveChanges();
                                }
                                
                                tblPromoteToClass objAddNew = new tblPromoteToClass
                                {
                                    SessionId = objModel.SessionId,                                    
                                    ClassId = objModel.ClassId,
                                    SectionId = objModel.SectionId,
                                    LastClassId = objModel.LastClassId,
                                    AdmissionId = objModel.AdmissionId,
                                    Status = "Attending",
                                    RecordStatus = "Promote To New Class",

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
                            //else
                            //{
                            //    rs.SectionId = objModel.SectionId;
                            //    rs.Status = objModel.Status;
                            //    rs.IsActive = true;

                            //    rs.ModifiedBy = objModel.ModifiedBy;
                            //    rs.ModifiedDate = DateTime.Now;
                            //    dbcontext.SaveChanges();
                            //    response.responseData = new { PromoteToClassId = rs.PromoteToClassId, Status = rs.Status };
                            //    response.message = "Record Updated Successfully!";
                            //}
                        }
                        //else
                        //{
                        //    var rs = dbcontext.tblPromoteToClasses.FirstOrDefault(x => x.IsDeleted == false && x.SessionId == objModel.SessionId && x.ClassId == objModel.ClassId && x.AdmissionId == objModel.AdmissionId && x.PromoteToClassId != objModel.PromoteToClassId);
                        //    if (rs == null)
                        //    {
                        //        var objUpdate = dbcontext.tblPromoteToClasses.FirstOrDefault(m => m.PromoteToClassId == objModel.PromoteToClassId);
                        //        if (objUpdate != null)
                        //        {
                        //            objUpdate.SessionId = objModel.SessionId;
                        //            objUpdate.EnquiryId = objModel.EnquiryId;
                        //            objUpdate.AdmissionId = objModel.AdmissionId;
                        //            objUpdate.ClassId = objModel.ClassId;
                        //            objUpdate.SectionId = objModel.SectionId;

                        //            objUpdate.ModifiedBy = objModel.ModifiedBy;
                        //            objUpdate.ModifiedDate = DateTime.Now;
                        //            dbcontext.SaveChanges();
                        //            response.responseData = new { PromoteToClassId = objUpdate.PromoteToClassId, Status = objUpdate.Status };
                        //            response.message = "Record Updated Successfully!";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        response.success = false;
                        //        response.message = "Record Already Exists!";
                        //    }
                        //}
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
