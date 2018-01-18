using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class BusChargesMasterCustomModel
    {
        public int BusChargesMasterId { get; set; }
        public Nullable<int> SchoolId { get; set; }

        [Required(ErrorMessage = "Please enter session")]
        public Nullable<int> SessionId { get; set; }
        
        [Required(ErrorMessage = "Please enter start point")]
        public Nullable<decimal> StartPoint { get; set; }

        [Required(ErrorMessage = "Please enter end point")]
        public Nullable<decimal> EndPoint { get; set; }

        [Required(ErrorMessage = "Please enter amount")]
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string SearchText { get; set; }
        public PagingViewModel pageModel { get; set; }
        public string SchoolName { get; set; }
        public string SessionName { get; set; }
    }
}
