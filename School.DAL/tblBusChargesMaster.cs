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
    
    public partial class tblBusChargesMaster
    {
        public int BusChargesMasterId { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public Nullable<int> SessionId { get; set; }
        public Nullable<decimal> StartPoint { get; set; }
        public Nullable<decimal> EndPoint { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblSession tblSession { get; set; }
    }
}
