using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class SessionMasterCustomModel
    {
        public int SessionId { get; set; }

        [Required(ErrorMessage = "Please enter session")]
        public string Session { get; set; }

        public string Description { get; set; }
        public Nullable<System.DateTime> DDate { get; set; }
        public Nullable<bool> IsDefault { get; set; }
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
