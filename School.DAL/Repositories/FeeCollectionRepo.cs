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
    public class FeeCollectionRepo : IFeeCollectionRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public FeeCollectionCustomModel GetById(int Id)
        {
            FeeCollectionCustomModel objListModel = new FeeCollectionCustomModel();
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        objListModel = dbcontext.tblFeeCollections.Where(x => x.IsDeleted == false && x.FeeCollectionId == Id)
                        .Select(x => new FeeCollectionCustomModel
                        {
                            FeeCollectionId = x.FeeCollectionId,
                            SchoolId = x.SchoolId,
                            EnquiryId = x.EnquiryId,
                            AdmissionId = x.AdmissionId,
                            ClassId = x.ClassId,
                            SectionId = x.SectionId,
                            AmountPaid = x.AmountPaid,
                            FeeMode = x.FeeMode,
                            FeeDate = x.FeeDate,
                            FeeInCharge = x.FeeInCharge,
                            FeeType = x.FeeType,
                                    
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

        public object GetFeeCollectionListing(FeeCollectionCustomModel objModel)
        {
            IList<FeeCollectionCustomModel> FeeListModel = new List<FeeCollectionCustomModel>();
            IQueryable<FeeCollectionCustomModel> FeeListDetail = null;
            int TotalRec = 0;
            
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        FeeListDetail = dbcontext.tblFeeCollections.Where(x => x.IsDeleted == false)
                            .Select(x => new FeeCollectionCustomModel
                            {
                                FeeCollectionId = x.FeeCollectionId,
                                SchoolId = x.SchoolId,
                                EnquiryId = x.EnquiryId,
                                AdmissionId = x.AdmissionId,
                                AmountPaid = x.AmountPaid,
                                FeeMode = x.FeeMode,
                                FeeDate = x.FeeDate,
                                FeeInCharge = x.FeeInCharge,
                                FeeType = x.FeeType,

                                IsActive = x.IsActive,
                                IsDeleted = x.IsDeleted,
                                CreatedBy = x.CreatedBy,
                                CreatedDate = x.CreatedDate,
                                ModifiedBy = x.ModifiedBy,
                                ModifiedDate = x.ModifiedDate
                            }).OrderByDescending(x => x.FeeCollectionId);
                                                
                        TotalRec = FeeListDetail.Count();
                        FeeListModel = FeeListDetail.ToList() as IList<FeeCollectionCustomModel>;                        
                        
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

        public Response SaveFeeCollectionMasterDetails(FeeCollectionCustomModel objModel)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        response.success = true;
                        if (objModel.FeeCollectionId == 0)
                        {
                            var rs = dbcontext.tblFeeCollections.FirstOrDefault(x => x.IsDeleted == false && x.AdmissionId == objModel.AdmissionId && x.FeeDate == objModel.FeeDate && x.FeeType == objModel.FeeType);
                            if (rs == null)
                            {
                                dbcontext.insert_fees_collection(objModel.EnquiryId, objModel.AdmissionId, objModel.ClassId, objModel.SectionId, objModel.FeeDate, objModel.FeeType, objModel.AmountPaid, objModel.FeeMode, 0, objModel.FeeInCharge, objModel.CreatedBy, objModel.ModifiedBy);

                                //tblFeeCollection objAddNew = new tblFeeCollection
                                //{
                                //    SchoolId = Convert.ToInt32(CommonHelper.TaskSchoolId.SchoolId),
                                //    EnquiryId = objModel.SessionId,
                                //    Title = objModel.Title,
                                //    ShortDescription = objModel.ShortDescription,
                                //    DDate = objModel.DDate,

                                //    IsActive = true,
                                //    IsDeleted = false,
                                //    CreatedBy = objModel.CreatedBy,
                                //    CreatedDate = DateTime.Now,
                                //    ModifiedBy = objModel.ModifiedBy,
                                //    ModifiedDate = DateTime.Now
                                //};

                                //dbcontext.tblClasses.Add(objAddNew);
                                //dbcontext.SaveChanges();
                                //response.responseData = new { ClassId = objAddNew.ClassId, Title = objAddNew.Title };
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
                            var rs = dbcontext.tblFeeCollections.FirstOrDefault(x => x.IsDeleted == false && x.AdmissionId == objModel.AdmissionId && x.FeeDate == objModel.FeeDate && x.FeeType == objModel.FeeType &&  x.FeeCollectionId != objModel.FeeCollectionId);
                            if (rs == null)
                            {
                                var objUpdate = dbcontext.tblFeeCollections.FirstOrDefault(m => m.FeeCollectionId == objModel.FeeCollectionId);
                                if (objUpdate != null)
                                {
                                    dbcontext.update_fees_collection(objModel.FeeCollectionId, objModel.EnquiryId, objModel.AdmissionId, objModel.ClassId, objModel.SectionId, objModel.FeeDate, objModel.FeeType, objModel.AmountPaid, objModel.FeeMode, 0, objModel.FeeInCharge);

                                    //objUpdate.SessionId = objModel.SessionId;
                                    //objUpdate.Title = objModel.Title;
                                    //objUpdate.ShortDescription = objModel.ShortDescription;
                                    //objUpdate.DDate = objModel.DDate;

                                    //objUpdate.ModifiedBy = objModel.ModifiedBy;
                                    //objUpdate.ModifiedDate = DateTime.Now;
                                    //dbcontext.SaveChanges();
                                    //response.responseData = new { ClassId = objUpdate.ClassId, Title = objUpdate.Title };
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
        public object SetActiveFeeCollectionDetail(FeeCollectionCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblFeeCollections.FirstOrDefault(x => x.FeeCollectionId == objModel.FeeCollectionId);

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

        /// <summary>
        /// This method is used to delete particular farmer detail
        /// </summary>
        /// <param name="AssetId">Unique id of asset</param>
        /// <returns>Response</returns>
        public object DeleteFeeCollectionDetail(FeeCollectionCustomModel objModel)
        {
            object objClassResult = new object();

            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        var rs = dbcontext.tblFeeCollections.FirstOrDefault(x => x.FeeCollectionId == objModel.FeeCollectionId);

                        if (rs != null)
                        {
                            dbcontext.Delete_Fee_Collection(objModel.FeeCollectionId, objModel.ModifiedBy);
                            //rs.IsDeleted = true;
                            //rs.ModifiedDate = DateTime.Now;
                            //rs.ModifiedBy = objModel.ModifiedBy;

                            //dbcontext.SaveChanges();
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
                        ActiveResult = dbcontext.tblFeeCollections.FirstOrDefault(x => x.FeeCollectionId == Id).IsActive ?? false;
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

        public object GetStudentFeeDetail(int AdmissionId, int? ClassId, int? SectionId)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        return dbcontext.USP_GetFeeCollectionDetail(AdmissionId, ClassId, SectionId).ToList();
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        throw ex;
                    }
                }
                
            }
        }

        public object GetFeeCollectionReceiptDetail(int ReceiptId, int? AdmissionId)
        {
            using (response = new Response())
            {
                using (dbcontext = new SchoolManagementEntities())
                {
                    try
                    {
                        return dbcontext.USP_FeeCollectionReceipt(ReceiptId, AdmissionId).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        dbcontext.Dispose();
                        throw ex;
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
