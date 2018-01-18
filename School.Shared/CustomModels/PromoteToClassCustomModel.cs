using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Shared.CustomModels
{
    public class PromoteToClassCustomModel
    {
        public int PromoteToClassId { get; set; }
        public Nullable<int> SessionId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public Nullable<int> AdmissionId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<int> LastClassId { get; set; }
        public string RollNo { get; set; }
        public string Status { get; set; }
        public string RecordStatus { get; set; }
        public string Reason { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string SearchText { get; set; }
        public PagingViewModel pageModel { get; set; }
    }
}
