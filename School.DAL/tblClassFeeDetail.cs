//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace School.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblClassFeeDetail
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
        public Nullable<System.DateTime> FeeDate { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual tblAdmission tblAdmission { get; set; }
        public virtual tblClass tblClass { get; set; }
    }
}
