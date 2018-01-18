using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class FeeMasterCustomModel
    {
        public int FeeMasterId { get; set; }
        public Nullable<int> SchoolId { get; set; }

        [Required(ErrorMessage = "Please enter session")]
        public Nullable<int> SessionId { get; set; }

        [Required(ErrorMessage = "Please enter class")]
        public Nullable<int> ClassId { get; set; }

        [Required(ErrorMessage = "Please enter total fee")]
        public Nullable<decimal> TotalFee { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> NetFee { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string SearchText { get; set; }
        public PagingViewModel pageModel { get; set; }
        public string ClassName { get; set; }
        public string SessionName { get; set; }
    }
}
