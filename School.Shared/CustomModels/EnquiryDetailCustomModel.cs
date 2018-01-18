using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class EnquiryDetailCustomModel
    {
        public int EnquiryId { get; set; }
        public Nullable<int> SchoolId { get; set; }

        [Required(ErrorMessage = "Please enter class")]
        public Nullable<int> ClassId { get; set; }
                
        public Nullable<int> SectionId { get; set; }

        [Required(ErrorMessage = "Please enter session")]
        public Nullable<int> SessionId { get; set; }

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

        [Required(ErrorMessage = "Please enter father name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Min 2 and max 100 characters are allowed.")]
        public string FatherName { get; set; }
                
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Min 2 and max 100 characters are allowed.")]
        public string MotherName { get; set; }
        public string Address { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }

        [StringLength(12, MinimumLength = 12, ErrorMessage = "Min and max 12 characters are allowed.")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{12,12}$", ErrorMessage = "Please enter valid aadhar no.")]
        public string AadharNumber { get; set; }
        public string Landline { get; set; }
        public string Qualification { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EnquiryDate { get; set; }

        [Required(ErrorMessage = "Please enter how to know us")]
        public string HowToKnowUs { get; set; }
        public string ReferredBy { get; set; }
        public string SchoolName { get; set; }
        public string CounsellorName { get; set; }
        public string FollowUpComment { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FollowUpDate { get; set; }
        public string Comments { get; set; }
        public string PinCode { get; set; }
        public string Others { get; set; }

        [Required(ErrorMessage = "Please select status")]
        public string Status { get; set; }

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

        public string ClassName { get; set; }
        public string SessionName { get; set; }
        public string SectionName { get; set; }
        
        public AdmissionCustomModel AdmissionModel { get; set; }

        public ClassFeeDetailCustomModel ClassFeeDetailModel { get; set; }

    }

    public class StudentCommonDetail
    {
        public int EnquiryId { get; set; }
        public Nullable<int> SchoolId { get; set; }
        
        public Nullable<int> ClassId { get; set; }

        public Nullable<int> SectionId { get; set; }

        public Nullable<int> SessionId { get; set; }
    }

}
