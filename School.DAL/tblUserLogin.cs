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
    
    public partial class tblUserLogin
    {
        public int Id { get; set; }
        public Nullable<int> UserTypeId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual tblEmployee tblEmployee { get; set; }
        public virtual tblUserTypeMaster tblUserTypeMaster { get; set; }
    }
}
