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
    public class EnquiryDetailBusiness : IEnquiryDetailBusiness
    {
        IEnquiryDetailRepo objDAL;

        public EnquiryDetailCustomModel GetById(int Id)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.GetById(Id);
            }
        }

        public object GetEnquiryDetailListing(EnquiryDetailCustomModel objEnquiryDetailModel)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.GetEnquiryDetailListing(objEnquiryDetailModel);
            }
        }

        public Response SaveEnquiryDetails(EnquiryDetailCustomModel objModel)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.SaveEnquiryDetails(objModel);
            }
        }

        public object SetActiveEnquiryDetail(EnquiryDetailCustomModel objEnquiryDetailModel)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.SetActiveEnquiryDetail(objEnquiryDetailModel);
            }
        }

        public object DeleteEnquiryDetail(EnquiryDetailCustomModel objEnquiryDetailModel)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.DeleteEnquiryDetail(objEnquiryDetailModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.FindById(Id);
            }
        }

        public object GetEnquiryId(int AdmissionId)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.GetEnquiryId(AdmissionId);
            }
        }

        public object BindSessionClassEnquiry(int? SessionId, int? ClassId, int? SectionId, string StudentName)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.BindSessionClassEnquiry(SessionId, ClassId, SectionId, StudentName);
            }
        }

        public object BindEnquiryReport(DateTime? FromDate, DateTime? ToDate, int? SessionId, int? ClassId, int? SectionId, string EnquiryStatus, string StudentName)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.BindEnquiryReport(FromDate, ToDate, SessionId, ClassId, SectionId, EnquiryStatus, StudentName);
            }
        }

        public object BindAdmissionReport(DateTime? FromDate, DateTime? ToDate, int? SessionId, int? ClassId, int? SectionId, string EnquiryStatus, string StudentName)
        {
            using (objDAL = new EnquiryDetailRepo())
            {
                return objDAL.BindAdmissionReport(FromDate, ToDate, SessionId, ClassId, SectionId, EnquiryStatus, StudentName);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
