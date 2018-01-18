using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class UserLoginCustomModel
    {
        public int Id { get; set; }
        public Nullable<int> UserTypeId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
