using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.BDC.Interfaces;
using School.DAL.Interfaces;
using School.DAL.Repositories;

namespace School.BDC.Classes
{
    public class FeeCollectionBusiness : IFeeCollectionBusiness
    {
        IFeeCollectionRepo objDAL;

        public FeeCollectionCustomModel GetById(int Id)
        {
            using (objDAL = new FeeCollectionRepo())
            {
                return objDAL.GetById(Id);
            }
        }

        public object GetFeeCollectionListing(FeeCollectionCustomModel objModel)
        {
            using (objDAL = new FeeCollectionRepo())
            {
                return objDAL.GetFeeCollectionListing(objModel);
            }
        }

        public Response SaveFeeCollectionMasterDetails(FeeCollectionCustomModel objModel)
        {
            using (objDAL = new FeeCollectionRepo())
            {
                return objDAL.SaveFeeCollectionMasterDetails(objModel);
            }
        }

        public object SetActiveFeeCollectionDetail(FeeCollectionCustomModel objModel)
        {
            using (objDAL = new FeeCollectionRepo())
            {
                return objDAL.SetActiveFeeCollectionDetail(objModel);
            }
        }

        public object DeleteFeeCollectionDetail(FeeCollectionCustomModel objModel)
        {
            using (objDAL = new FeeCollectionRepo())
            {
                return objDAL.DeleteFeeCollectionDetail(objModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new FeeCollectionRepo())
            {
                return objDAL.FindById(Id);
            }
        }

        public object GetStudentFeeDetail(int AdmissionId, int? ClassId, int? SectionId)
        {
            using (objDAL = new FeeCollectionRepo())
            {
                return objDAL.GetStudentFeeDetail(AdmissionId, ClassId, SectionId);
            }
        }

        public object GetFeeCollectionReceiptDetail(int ReceiptId, int? AdmissionId)
        {
            using (objDAL = new FeeCollectionRepo())
            {
                return objDAL.GetFeeCollectionReceiptDetail(ReceiptId, AdmissionId);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
