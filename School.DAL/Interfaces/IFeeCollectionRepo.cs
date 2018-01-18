using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.DAL.Interfaces
{
    public interface IFeeCollectionRepo : IDisposable
    {
        FeeCollectionCustomModel GetById(int Id);

        object GetFeeCollectionListing(FeeCollectionCustomModel objModel);

        Response SaveFeeCollectionMasterDetails(FeeCollectionCustomModel objModel);

        object SetActiveFeeCollectionDetail(FeeCollectionCustomModel objModel);

        object DeleteFeeCollectionDetail(FeeCollectionCustomModel objModel);

        bool FindById(int Id);

        object GetStudentFeeDetail(int AdmissionId, int? ClassId, int? SectionId);

        object GetFeeCollectionReceiptDetail(int ReceiptId, int? AdmissionId);
    }
}
