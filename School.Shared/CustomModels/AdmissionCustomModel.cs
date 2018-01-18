using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class AdmissionCustomModel
    {
        public int AdmissionId { get; set; }
        public Nullable<int> EnquiryId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AdmissionDate { get; set; }
        public Nullable<decimal> TotalFees { get; set; }
        public Nullable<decimal> FeeToBePaid { get; set; }
        public Nullable<decimal> RegistrationFees { get; set; }
        public string FeeIncharge { get; set; }
        public string DiscountType { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> BalanceAmountDue { get; set; }
        public string NoOfInstallments { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> NextPaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string EnquiryStatus { get; set; }
        public string InSectionStatus { get; set; }
        public string Reason { get; set; }
        public string Photo { get; set; }
        public string Resume { get; set; }

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
