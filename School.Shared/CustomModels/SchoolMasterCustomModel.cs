using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Shared.CustomModels
{
    public class SchoolMasterCustomModel
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string TagLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string ShortDescription { get; set; }
        public Nullable<System.DateTime> DDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string SearchText { get; set; }
        public PagingViewModel pageModel { get; set; }

    }
}
