using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class ChangePasswordCustomModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter New Password")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 25 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please enter Confirm Password")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 25 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [CompareAttribute("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
