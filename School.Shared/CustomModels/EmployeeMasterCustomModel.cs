using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class EmployeeMasterCustomModel
    {
        public int EmployeeId { get; set; }
        public Nullable<int> SchoolId { get; set; }

        [Required(ErrorMessage = "Please enter fname")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Min 2 and max 100 characters are allowed.")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Please enter lname")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Min 2 and max 100 characters are allowed.")]
        public string LName { get; set; }
        
        [Required(ErrorMessage = "Please enter mobileno")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,10}$", ErrorMessage = "Please enter valid mobile no.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Min and max 10 characters are allowed.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please enter emailid")]
        [StringLength(250, ErrorMessage = "Max 250 characters are allowed.")]
        [RegularExpression(@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Invalid email address.")]
        public string EmailId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Designation { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfJoining { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string BloodGroup { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DDate { get; set; }

        public string Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string SearchText { get; set; }
        public PagingViewModel pageModel { get; set; }


    }
}
