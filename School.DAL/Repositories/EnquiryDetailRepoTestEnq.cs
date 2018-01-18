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
    public class EnquiryDetailRepoTest : IEnquiryDetailRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public EnquiryDetailCustomModel GetById(int Id)
        {
            EnquiryDetailCustomModel objListModel = new EnquiryDetailCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblEnquiryDetails.Where(x => x.IsDeleted == false && x.EnquiryId == Id)
                                .Select(x => new EnquiryDetailCustomModel
                                {
                                    EnquiryId = x.EnquiryId,
                                    ClassId = x.ClassId,
                                    SessionId = x.SessionId,
                                    FName = x.FName,
                                    LName = x.LName,
                                    MobileNo = x.MobileNo,
                                    EmailId = x.EmailId,
                                    FatherName = x.FatherName,
                                    MotherName = x.MotherName,
                                    Village = x.Village,
                                    City = x.City,
                                    PinCode = x.PinCode,
                                    Address = x.Address,
                                    State = x.State,
                                    Gender = x.Gender,
                                    BloodGroup = x.BloodGroup,
                                    AadharNumber = x.AadharNumber,
                                    Landline = x.Landline,
                                    Qualification = x.Qualification,
                                    DateOfBirth = x.DateOfBirth,
                                    EnquiryDate = x.EnquiryDate,
                                    CounsellorName = x.CounsellorName,
                                    Comments = x.Comments,
                                    Status = x.Status,
                                    HowToKnowUs = x.HowToKnowUs,
                                    ReferredBy = x.ReferredBy,


                                    AdmissionModel = x.tblAdmissions.Where(a => a.EnquiryId == x.EnquiryId).Select(s => new AdmissionCustomModel
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

        public object GetEnquiryDetailListing(EnquiryDetailCustomModel objEnquiryDetailModel)
        {
            IList<EnquiryDetailCustomModel> EnquiryListModel = new List<EnquiryDetailCustomModel>();
            IQueryable<EnquiryDetailCustomModel> EnquiryListDetail = null;
            int TotalRec = 0;
            int CurrentPageSize = 0;

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        EnquiryListDetail = dbcontext.tblEnquiryDetails.Where(x => x.IsDeleted == false
                        && (objEnquiryDetailModel.SessionId == null || objEnquiryDetailModel.SessionId == 0 || x.SessionId == objEnquiryDetailModel.SessionId)
                        && (objEnquiryDetailModel.ClassId == null || objEnquiryDetailModel.ClassId == 0 || x.ClassId == objEnquiryDetailModel.ClassId)
                        )
                                .Select(x => new EnquiryDetailCustomModel
                                {
                                    EnquiryId = x.EnquiryId,
                                    ClassId = x.ClassId,
                                    SectionId = x.SectionId,
                                    FName = x.FName,
                                    LName = x.LName,
                                    MobileNo = x.MobileNo,
                                    EmailId = x.EmailId,
                                    FatherName = x.FatherName,
                                    MotherName = x.MotherName,
                                    Village = x.Village,
                                    City = x.City,
                                    PinCode = x.PinCode,
                                    Address = x.Address,
                                    State = x.State,
                                    Gender = x.Gender,
                                    BloodGroup = x.BloodGroup,
                                    AadharNumber = x.AadharNumber,
                                    Landline = x.Landline,
                                    Qualification = x.Qualification,
                                    DateOfBirth = x.DateOfBirth,
                                    EnquiryDate = x.EnquiryDate,
                                    CounsellorName = x.CounsellorName,
                                    Comments = x.Comments,
                                    Status = x.Status,
                                    HowToKnowUs = x.HowToKnowUs,
                                    ReferredBy = x.ReferredBy,

                                    IsActive = x.IsActive,
                                    IsDeleted = x.IsDeleted,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    ModifiedBy = x.ModifiedBy,
                                    ModifiedDate = x.ModifiedDate
                                }).OrderByDescending(x => x.EnquiryId);

                        if (objEnquiryDetailModel.pageModel != null)
                        {
                            if (objEnquiryDetailModel.pageModel.SerachTerm != null && objEnquiryDetailModel.pageModel.SerachTerm != "")
                            {
                                EnquiryListDetail = EnquiryListDetail.Where(x =>
                                (x.FName.ToLower().Trim().Contains(objEnquiryDetailModel.pageModel.SerachTerm.ToLower().Trim()) || objEnquiryDetailModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.LName.ToLower().Trim().Contains(objEnquiryDetailModel.pageModel.SerachTerm.ToLower().Trim()) || objEnquiryDetailModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.EmailId.ToLower().Trim().Contains(objEnquiryDetailModel.pageModel.SerachTerm.ToLower().Trim()) || objEnquiryDetailModel.pageModel.SerachTerm.Trim() == String.Empty)
                                || (x.MobileNo.ToLower().Trim().Contains(objEnquiryDetailModel.pageModel.SerachTerm.ToLower().Trim()) || objEnquiryDetailModel.pageModel.SerachTerm.Trim() == String.Empty)
                                );
                            }

                            if (!String.IsNullOrEmpty(objEnquiryDetailModel.pageModel.SortBy))
                            {
                                //EnquiryListDetail= objEnquiryDetailModel.pageModel.SortDir.ToLower() == "desc" ? EnquiryListDetail.OrderByDescending(objEnquiryDetailModel.pageModel.SortBy)
                                //    : EnquiryListDetail.OrderBy(objEnquiryDetailModel.pageModel.SortBy);
                            }

                            TotalRec = EnquiryListDetail.Count();
                            if (objEnquiryDetailModel.pageModel.PageSize > 0)
                            {
                                EnquiryListDetail = EnquiryListDetail.Skip(objEnquiryDetailModel.pageModel.Skip);
                                EnquiryListDetail = EnquiryListDetail.Take(objEnquiryDetailModel.pageModel.PageSize);
                            }

                            EnquiryListModel = EnquiryListDetail.ToList() as IList<EnquiryDetailCustomModel>;

                        }
                        else
                        {
                            TotalRec = EnquiryListDetail.Count();
                            EnquiryListModel = EnquiryListDetail.ToList() as IList<EnquiryDetailCustomModel>;
                        }

                        //CurrentPageSize = objEnquiryDetailModel.pageModel.PageSize;

                        //if (EnquiryListModel.Count() > 0 && CurrentPageSize > 0)
                        //{
                        //    EnquiryListModel[0].pageModel = new PagingViewModel();

                        //    EnquiryListModel[0].pageModel.TotalRecords = TotalRec;
                        //    if ((TotalRec % CurrentPageSize) == 0)
                        //    {
                        //        EnquiryListModel[0].pageModel.TotalPages = TotalRec / CurrentPageSize;
                        //    }
                        //    else
                        //    {
                        //        EnquiryListModel[0].pageModel.TotalPages = (TotalRec / CurrentPageSize) + 1;
                        //    }
                        //}

                        return EnquiryListModel;

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

        public Response SaveEnquiryDetails(EnquiryDetailCustomModel objModel)
        {
            int NewEnquiryId = 0;
            int NewAdmissionId = 0;
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.EnquiryId == 0)
                        {
                            var rs = dbcontext.tblEnquiryDetails.FirstOrDefault(x => x.IsDeleted == false && x.EmailId == objModel.EmailId && x.MobileNo == objModel.MobileNo);
                            if (rs == null)
                            {
                                tblEnquiryDetail objAddNew = new tblEnquiryDetail
                                {
                                    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                    ClassId = objModel.ClassId,
                                    SessionId = objModel.SessionId,
                                    FName = objModel.FName,
                                    LName = objModel.LName,
                                    MobileNo = objModel.MobileNo,
                                    EmailId = objModel.EmailId,
                                    FatherName = objModel.FatherName,
                                    MotherName = objModel.MotherName,
                                    Village = objModel.Village,
                                    City = objModel.City,
                                    State = objModel.State,
                                    PinCode = objModel.PinCode,
                                    Address = objModel.Address,
                                    Country = "India",
                                    Gender = objModel.Gender,
                                    BloodGroup = objModel.BloodGroup,
                                    AadharNumber = objModel.AadharNumber,
                                    Landline = objModel.Landline,
                                    Qualification = objModel.Qualification,
                                    DateOfBirth = objModel.DateOfBirth,
                                    EnquiryDate = objModel.EnquiryDate,
                                    CounsellorName = objModel.CounsellorName,
                                    Comments = objModel.Comments,
                                    Status = objModel.Status,
                                    HowToKnowUs = objModel.HowToKnowUs,
                                    ReferredBy = objModel.ReferredBy,

                                    IsActive = true,
                                    IsDeleted = false,
                                    CreatedBy = objModel.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = objModel.ModifiedBy,
                                    ModifiedDate = DateTime.Now,

                                };

                                dbcontext.tblEnquiryDetails.Add(objAddNew);
                                dbcontext.SaveChanges();

                                if (objModel.Status == "Registered")
                                {
                                    tblAdmission objAddAdmission = new tblAdmission
                                    {
                                        EnquiryId = objAddNew.EnquiryId,
                                        AdmissionDate = objModel.AdmissionModel.AdmissionDate,
                                        TotalFees = objModel.AdmissionModel.TotalFees,
                                        RegistrationFees = objModel.AdmissionModel.RegistrationFees,
                                        FeeToBePaid = (Convert.ToDecimal(objModel.AdmissionModel.TotalFees) - Convert.ToDecimal(objModel.AdmissionModel.DiscountAmount)),
                                        DiscountType = objModel.AdmissionModel.DiscountType,
                                        DiscountAmount = objModel.AdmissionModel.DiscountAmount,
                                        BalanceAmountDue = (Convert.ToInt32(objModel.AdmissionModel.TotalFees) - (Convert.ToDecimal(objModel.AdmissionModel.DiscountAmount) + Convert.ToDecimal(objModel.AdmissionModel.RegistrationFees))),

                                        FeeIncharge = objModel.AdmissionModel.FeeIncharge,
                                        PaymentMode = objModel.AdmissionModel.PaymentMode,
                                        NoOfInstallments = objModel.AdmissionModel.NoOfInstallments,
                                        NextPaymentDate = objModel.AdmissionModel.NextPaymentDate,
                                        EnquiryStatus = objModel.Status,
                                        InSectionStatus = objModel.Status,

                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedBy = objModel.AdmissionModel.CreatedBy,
                                        CreatedDate = DateTime.Now,
                                        ModifiedBy = objModel.AdmissionModel.ModifiedBy,
                                        ModifiedDate = DateTime.Now,

                                    };

                                    dbcontext.tblAdmissions.Add(objAddAdmission);
                                    dbcontext.SaveChanges();

                                    tblFeeCollection objAddAdmissionFee = new tblFeeCollection
                                    {
                                        SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                        AdmissionId = objAddAdmission.AdmissionId,
                                        AmountPaid = objModel.AdmissionModel.RegistrationFees,
                                        FeeDate = DateTime.Now,
                                        FeeInCharge = objModel.AdmissionModel.FeeIncharge,
                                        FeeMode = objModel.AdmissionModel.PaymentMode,
                                        FeeType = "Registration Fees",

                                        CreatedDate = DateTime.Now,
                                        CreatedBy = objModel.CreatedBy,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedBy = objModel.ModifiedBy,
                                        IsActive = true,
                                        IsDeleted = false,
                                    };

                                    dbcontext.tblFeeCollections.Add(objAddAdmissionFee);
                                    dbcontext.SaveChanges();
                                }

                                response.responseData = new { EnquiryId = objAddNew.EnquiryId, FName = objAddNew.FName };
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
                            var rs = dbcontext.tblEnquiryDetails.FirstOrDefault(x => x.IsDeleted == false && x.EmailId == objModel.EmailId && x.EnquiryId != objModel.EnquiryId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblEnquiryDetails.FirstOrDefault(m => m.EnquiryId == objModel.EnquiryId);
                                if (objUpdate != null)
                                {
                                    objUpdate.ClassId = objModel.ClassId;
                                    objUpdate.SessionId = objModel.SessionId;
                                    objUpdate.FName = objModel.FName;
                                    objUpdate.LName = objModel.LName;
                                    objUpdate.MobileNo = objModel.MobileNo;
                                    objUpdate.EmailId = objModel.EmailId;
                                    objUpdate.FatherName = objModel.FatherName;
                                    objUpdate.MotherName = objModel.MotherName;
                                    objUpdate.Village = objModel.Village;
                                    objUpdate.City = objModel.City;
                                    objUpdate.PinCode = objModel.PinCode;
                                    objUpdate.Address = objModel.Address;
                                    objUpdate.State = objModel.State;
                                    objUpdate.Gender = objModel.Gender;
                                    objUpdate.BloodGroup = objModel.BloodGroup;
                                    objUpdate.AadharNumber = objModel.AadharNumber;
                                    objUpdate.Landline = objModel.Landline;
                                    objUpdate.Qualification = objModel.Qualification;
                                    objUpdate.DateOfBirth = objModel.DateOfBirth;
                                    objUpdate.EnquiryDate = objModel.EnquiryDate;
                                    objUpdate.CounsellorName = objModel.CounsellorName;
                                    objUpdate.Comments = objModel.Comments;
                                    objUpdate.Status = objModel.Status;
                                    objUpdate.HowToKnowUs = objModel.HowToKnowUs;
                                    objUpdate.ReferredBy = objModel.ReferredBy;

                                    objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    objUpdate.ModifiedDate = DateTime.Now;
                                    dbcontext.SaveChanges();
                                    // return objExpense.ExpenseApprovalId
                                    NewEnquiryId = objModel.EnquiryId;
                                    response.responseData = new { EnquiryId = objModel.EnquiryId, FName = objModel.FName };
                                    response.message = "Record Updated Successfully!";
                                }

                                if (objModel.Status == "Registered")
                                {
                                    decimal? FeePaidAmount = dbcontext.tblFeeCollections.Where(x => x.AdmissionId == objModel.EnquiryId).Sum(x => x.AmountPaid);
                                    var objUpdateAdm = dbcontext.tblAdmissions.FirstOrDefault(m => m.EnquiryId == objModel.EnquiryId);
                                    if (objUpdateAdm != null)
                                    {
                                        objUpdateAdm.AdmissionDate = objModel.AdmissionModel.AdmissionDate;
                                        objUpdateAdm.TotalFees = objModel.AdmissionModel.TotalFees;
                                        objUpdateAdm.RegistrationFees = objModel.AdmissionModel.RegistrationFees;
                                        objUpdateAdm.FeeToBePaid = (Convert.ToDecimal(objModel.AdmissionModel.TotalFees) - Convert.ToDecimal(objModel.AdmissionModel.DiscountAmount));
                                        objUpdateAdm.DiscountType = objModel.AdmissionModel.DiscountType;
                                        objUpdateAdm.DiscountAmount = objModel.AdmissionModel.DiscountAmount;

                                        //objUpdateAdm.BalanceAmountDue = (Convert.ToInt32(objModel.AdmissionModel.TotalFees) - (Convert.ToDecimal(objModel.AdmissionModel.DiscountAmount) + Convert.ToDecimal(objModel.AdmissionModel.RegistrationFees)));

                                        objUpdateAdm.BalanceAmountDue = (Convert.ToInt32(objModel.AdmissionModel.TotalFees) - (Convert.ToDecimal(objModel.AdmissionModel.DiscountAmount) + Convert.ToDecimal(dbcontext.tblFeeCollections.Where(x => x.AdmissionId == objUpdateAdm.AdmissionId).Sum(x => x.AmountPaid))));

                                        objUpdateAdm.FeeIncharge = objModel.AdmissionModel.FeeIncharge;
                                        objUpdateAdm.PaymentMode = objModel.AdmissionModel.PaymentMode;
                                        objUpdateAdm.NoOfInstallments = objModel.AdmissionModel.NoOfInstallments;
                                        objUpdateAdm.NextPaymentDate = objModel.AdmissionModel.NextPaymentDate;
                                        objUpdateAdm.EnquiryStatus = objModel.Status;
                                        objUpdateAdm.InSectionStatus = objModel.Status;

                                        objUpdateAdm.ModifiedBy = objModel.AdmissionModel.ModifiedBy;
                                        objUpdateAdm.ModifiedDate = DateTime.Now;

                                        dbcontext.SaveChanges();
                                        NewAdmissionId = objUpdateAdm.AdmissionId;

                                        //response.responseData = new { EnquiryId = objUpdate.EnquiryId, FName = objUpdate.FName };
                                        //response.message = "Record Updated Successfully!";

                                    }
                                    else
                                    {
                                        tblAdmission objAddAdmission = new tblAdmission
                                        {
                                            EnquiryId = NewEnquiryId,
                                            AdmissionDate = objModel.AdmissionModel.AdmissionDate,
                                            TotalFees = objModel.AdmissionModel.TotalFees,
                                            RegistrationFees = objModel.AdmissionModel.RegistrationFees,
                                            FeeToBePaid = (Convert.ToDecimal(objModel.AdmissionModel.TotalFees) - Convert.ToDecimal(objModel.AdmissionModel.DiscountAmount)),
                                            DiscountType = objModel.AdmissionModel.DiscountType,
                                            DiscountAmount = objModel.AdmissionModel.DiscountAmount,
                                            BalanceAmountDue = (Convert.ToInt32(objModel.AdmissionModel.TotalFees) - (Convert.ToDecimal(objModel.AdmissionModel.DiscountAmount) + Convert.ToDecimal(objModel.AdmissionModel.RegistrationFees))),

                                            FeeIncharge = objModel.AdmissionModel.FeeIncharge,
                                            PaymentMode = objModel.AdmissionModel.PaymentMode,
                                            NoOfInstallments = objModel.AdmissionModel.NoOfInstallments,
                                            NextPaymentDate = objModel.AdmissionModel.NextPaymentDate,
                                            EnquiryStatus = objModel.Status,
                                            InSectionStatus = objModel.Status,

                                            IsActive = true,
                                            IsDeleted = false,
                                            CreatedBy = objModel.AdmissionModel.CreatedBy,
                                            CreatedDate = DateTime.Now,
                                            ModifiedBy = objModel.AdmissionModel.ModifiedBy,
                                            ModifiedDate = DateTime.Now,

                                        };

                                        dbcontext.tblAdmissions.Add(objAddAdmission);
                                        dbcontext.SaveChanges();
                                        NewAdmissionId = objAddAdmission.AdmissionId;
                                    }

                                    var objUpdateAdmFee = dbcontext.tblFeeCollections.FirstOrDefault(m => m.AdmissionId == NewAdmissionId);
                                    if (objUpdateAdmFee != null)
                                    {
                                        objUpdateAdmFee.AdmissionId = NewAdmissionId;
                                        objUpdateAdmFee.AmountPaid = objModel.AdmissionModel.RegistrationFees;
                                        //objUpdateAdmFee.paymentmode
                                        objUpdateAdmFee.FeeDate = DateTime.Now;
                                        objUpdateAdmFee.FeeInCharge = objModel.AdmissionModel.FeeIncharge;
                                        objUpdateAdmFee.FeeMode = objModel.AdmissionModel.PaymentMode;
                                        objUpdateAdmFee.FeeType = "Registration Fees";

                                        objUpdateAdmFee.CreatedDate = DateTime.Now;
                                        objUpdateAdmFee.CreatedBy = objModel.CreatedBy;
                                        objUpdateAdmFee.ModifiedDate = DateTime.Now;
                                        objUpdateAdmFee.ModifiedBy = objModel.ModifiedBy;
                                        objUpdateAdmFee.IsActive = true;
                                        objUpdateAdmFee.IsDeleted = false;

                                        dbcontext.SaveChanges();
                                    }
                                    else
                                    {
                                        tblFeeCollection objAddAdmissionFee = new tblFeeCollection
                                        {
                                            SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                            AdmissionId = NewAdmissionId,
                                            AmountPaid = objModel.AdmissionModel.RegistrationFees,
                                            FeeDate = DateTime.Now,
                                            FeeInCharge = objModel.AdmissionModel.FeeIncharge,
                                            FeeMode = objModel.AdmissionModel.PaymentMode,
                                            FeeType = "Registration Fees",

                                            CreatedDate = DateTime.Now,
                                            CreatedBy = objModel.CreatedBy,
                                            ModifiedDate = DateTime.Now,
                                            ModifiedBy = objModel.ModifiedBy,
                                            IsActive = true,
                                            IsDeleted = false,
                                        };

                                        dbcontext.tblFeeCollections.Add(objAddAdmissionFee);
                                        dbcontext.SaveChanges();
                                    }
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

        public object SetActiveEnquiryDetail(EnquiryDetailCustomModel objEnquiryDetailModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblEnquiryDetails.FirstOrDefault(x => x.EnquiryId == objEnquiryDetailModel.EnquiryId);

                        if (rs != null)
                        {
                            rs.IsActive = rs.IsActive == true ? false : true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objEnquiryDetailModel.ModifiedBy;

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

        public object DeleteEnquiryDetail(EnquiryDetailCustomModel objEnquiryDetailModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblEnquiryDetails.FirstOrDefault(x => x.EnquiryId == objEnquiryDetailModel.EnquiryId);

                        if (rs != null)
                        {
                            rs.IsDeleted = true;
                            rs.ModifiedDate = DateTime.Now;
                            rs.ModifiedBy = objEnquiryDetailModel.ModifiedBy;

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
                        ActiveResult = dbcontext.tblEnquiryDetails.FirstOrDefault(x => x.EnquiryId == Id).IsActive ?? false;
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

        public object GetEnquiryId(int AdmissionId)
        {
            StudentCommonDetail objResult = new StudentCommonDetail();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        objResult = dbcontext.GetEnquiryIdFromAdmissionId(AdmissionId)
                            .Select(x => new StudentCommonDetail
                            {
                                EnquiryId = x.EnquiryId,
                                SessionId = x.SessionId,
                                ClassId = x.ClassId,
                                SectionId = x.SectionId
                            }).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        throw ex;
                    }
                }
                return objResult;
            }
        }

        public object BindSessionClassEnquiry(int? SessionId, int? ClassId)
        {
            List<EnquiryDetailCustomModel> objListModel = new List<EnquiryDetailCustomModel>();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblEnquiryDetails.Where(x => x.IsActive == true
                        && x.IsDeleted == false && x.Status == "Registered"
                        && (SessionId == null || SessionId == 0 || x.SessionId == SessionId)
                        && (ClassId == null || ClassId == 0 || x.ClassId == ClassId))
                                .Select(x => new EnquiryDetailCustomModel
                                {
                                    EnquiryId = x.EnquiryId,
                                    SessionId = x.SessionId,
                                    SessionName = x.SessionId != null ? x.tblSession.Session : "",
                                    ClassId = x.ClassId,
                                    ClassName = x.ClassId != null ? x.tblClass.Title : "",
                                    SectionId = x.SectionId,
                                    FName = x.FName,
                                    LName = x.LName,
                                    MobileNo = x.MobileNo,
                                    EmailId = x.EmailId,
                                    FatherName = x.FatherName,
                                    MotherName = x.MotherName,
                                    Village = x.Village,
                                    Address = x.Address,
                                    Gender = x.Gender,
                                    EnquiryDate = x.EnquiryDate,
                                    Status = x.Status,

                                    //AdmissionModel = x.tblAdmissions.FirstOrDefault(a => a.EnquiryId == x.EnquiryId) != null ? x.tblAdmissions.Where(a => a.EnquiryId == x.EnquiryId).FirstOrDefault() : null,

                                    AdmissionModel = x.tblAdmissions.Where(a => a.EnquiryId == x.EnquiryId).Select(s => new AdmissionCustomModel
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


                                }).ToList();

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


        // ****************************** Start Reporting Section Code **********************************

        public object BindEnquiryReport(DateTime? FromDate, DateTime? ToDate, int? SessionId, int? ClassId, int? SectionId, string EnquiryStatus, string StudentName)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;

                        var rs = dbcontext.USP_BindEnquiryReport(FromDate, ToDate, SessionId, ClassId, SectionId, EnquiryStatus, StudentName)
                                .Select(x => new EnquiryDetailCustomModel
                                {
                                    EnquiryId = x.EnquiryId,
                                    SessionId = x.SessionId,
                                    SessionName = x.Session,
                                    ClassId = x.ClassId,
                                    ClassName = x.Title,
                                    SectionId = x.SectionId,
                                    FName = x.StudentName,
                                    MobileNo = x.MobileNo,
                                    EmailId = x.EmailId,
                                    FatherName = x.FatherName,
                                    MotherName = x.MotherName,
                                    Village = x.Village,
                                    Address = x.Address,
                                    AadharNumber = x.AadharNumber,
                                    EnquiryDate = x.EnquiryDate,
                                    Status = x.Status,

                                }).ToList();

                        return rs;
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

        public object BindAdmissionReport(DateTime? FromDate, DateTime? ToDate, int? SessionId, int? ClassId, int? SectionId, string EnquiryStatus, string StudentName)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        var rs = dbcontext.USP_BindAdmissionReport(FromDate, ToDate, SessionId, ClassId, SectionId, EnquiryStatus, StudentName)
                                .Select(x => new EnquiryDetailCustomModel
                                {
                                    EnquiryId = x.EnquiryId,
                                    SessionId = x.SessionId,
                                    SessionName = x.Session,
                                    ClassId = x.ClassId,
                                    ClassName = x.Title,
                                    SectionId = x.SectionId,
                                    FName = x.StudentName,
                                    MobileNo = x.MobileNo,
                                    EmailId = x.EmailId,
                                    FatherName = x.FatherName,
                                    MotherName = x.MotherName,
                                    Village = x.Village,
                                    Address = x.Address,
                                    AadharNumber = x.AadharNumber,
                                    EnquiryDate = x.EnquiryDate,
                                    Status = x.Status,

                                }).ToList();

                        return rs;
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

        // ****************************** End Reporting Section Code **********************************

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
