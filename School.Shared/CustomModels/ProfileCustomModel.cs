using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Shared.CustomModels
{
    public class ProfileCustomModel
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

        public int SessionId { get; set; }
    }
}
