using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.DAL.Interfaces
{
    public interface IEnquiryDetailRepo : IDisposable
    {
        EnquiryDetailCustomModel GetById(int Id);

        object GetEnquiryDetailListing(EnquiryDetailCustomModel objEnquiryDetailModel);

        Response SaveEnquiryDetails(EnquiryDetailCustomModel objModel);

        object SetActiveEnquiryDetail(EnquiryDetailCustomModel objEnquiryDetailModel);

        object DeleteEnquiryDetail(EnquiryDetailCustomModel objEnquiryDetailModel);

        bool FindById(int Id);

        object GetEnquiryId(int AdmissionId);

        object BindSessionClassEnquiry(int? SessionId, int? ClassId, int? SectionId, string StudentName);

        object BindEnquiryReport(DateTime? FromDate, DateTime? ToDate, int? SessionId, int? ClassId, int? SectionId, string EnquiryStatus, string StudentName);

        object BindAdmissionReport(DateTime? FromDate, DateTime? ToDate, int? SessionId, int? ClassId, int? SectionId, string EnquiryStatus, string StudentName);

    }
}
