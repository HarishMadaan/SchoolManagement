using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Shared.CustomModels
{
    public class EventMasterCustomModel
    {
        public int EventId { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
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
