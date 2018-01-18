using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Shared.CustomModels
{
    public class AttendanceCustomModel
    {
        public int AttendanceId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public int AdmissionId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public string Attendance { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> SessionId { get; set; }
        public string SearchText { get; set; }
        public PagingViewModel pageModel { get; set; }
    }
}
