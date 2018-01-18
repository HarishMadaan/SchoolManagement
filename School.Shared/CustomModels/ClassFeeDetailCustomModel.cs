using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class ClassFeeDetailCustomModel
    {
        public int ClassFeeDetailId { get; set; }
        public Nullable<int> AdmissionId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<decimal> TotalFees { get; set; }
        public Nullable<decimal> FeeToBePaid { get; set; }
        public Nullable<decimal> RegistrationFees { get; set; }
        public string FeeIncharge { get; set; }
        public string DiscountType { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> BalanceAmountDue { get; set; }
        public string PaymentMode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FeeDate { get; set; }
        public string Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
