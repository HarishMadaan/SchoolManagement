using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface ISectionAllocationBusiness : IDisposable
    {
        object GetSectionAllocationListing(SectionAllocationCustomModel objModel);

        Response SaveSectionAllocationDetails(SectionAllocationCustomModel objModel);

        int GetTotalStudentInSection(int? SessionId, int? ClassId, int? SectionId);

        int InsertStudentSectionAllocation(int SessionId, int ClassId, int EnquiryId, int AdmissionId, int SectionId, bool isactive, string status);

        int CancelAllocatedStudentSection(int Id);

    }
}
