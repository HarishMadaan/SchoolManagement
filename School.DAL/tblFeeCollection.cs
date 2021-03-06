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
    
    public partial class tblFeeCollection
    {
        public int FeeCollectionId { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public Nullable<int> AdmissionId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public string FeeMode { get; set; }
        public Nullable<System.DateTime> FeeDate { get; set; }
        public string FeeInCharge { get; set; }
        public string FeeType { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual tblAdmission tblAdmission { get; set; }
        public virtual tblClass tblClass { get; set; }
        public virtual tblEnquiryDetail tblEnquiryDetail { get; set; }
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblSection tblSection { get; set; }
    }
}
