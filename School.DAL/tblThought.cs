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
    
    public partial class tblThought
    {
        public int ThoughtId { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public Nullable<System.DateTime> DDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual tblSchool tblSchool { get; set; }
    }
}