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
    public class SectionAllocationBusiness : ISectionAllocationBusiness
    {
        ISectionAllocationRepo objDAL;

        public object GetSectionAllocationListing(SectionAllocationCustomModel objModel)
        {
            using (objDAL = new SectionAllocationRepo())
            {
                return objDAL.GetSectionAllocationListing(objModel);
            }
        }

        public Response SaveSectionAllocationDetails(SectionAllocationCustomModel objModel)
        {
            using (objDAL = new SectionAllocationRepo())
            {
                return objDAL.SaveSectionAllocationDetails(objModel);
            }
        }
        
        public int GetTotalStudentInSection(int? SessionId, int? ClassId, int? SectionId)
        {
            using (objDAL = new SectionAllocationRepo())
            {
                return objDAL.GetTotalStudentInSection(SessionId, ClassId, SectionId);
            }
        }

        public int InsertStudentSectionAllocation(int SessionId, int ClassId, int EnquiryId, int AdmissionId, int SectionId, bool isactive, string status)
        {
            using (objDAL = new SectionAllocationRepo())
            {
                return objDAL.InsertStudentSectionAllocation(SessionId, ClassId, EnquiryId, AdmissionId, SectionId, isactive, status);
            }
        }
        
        public int CancelAllocatedStudentSection(int Id)
        {
            using (objDAL = new SectionAllocationRepo())
            {
                return objDAL.CancelAllocatedStudentSection(Id);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
