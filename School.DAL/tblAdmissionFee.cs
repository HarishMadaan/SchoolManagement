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
    
    public partial class tblAdmissionFee
    {
        public int AdmissionFeeId { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public Nullable<int> SessionId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<decimal> AdmissionFee { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> NetAdmissionFee { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblClass tblClass { get; set; }
        public virtual tblSession tblSession { get; set; }
    }
}
