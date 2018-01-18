using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class AdmissionFeeMasterCustomModel
    {
        public int AdmissionFeeId { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Please enter session")]
        public Nullable<int> SessionId { get; set; }
        public string SessionName { get; set; }

        [Required(ErrorMessage = "Please enter class")]
        public Nullable<int> ClassId { get; set; }
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Please enter admission fee")]
        public Nullable<decimal> AdmissionFee { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> NetAdmissionFee { get; set; }
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
