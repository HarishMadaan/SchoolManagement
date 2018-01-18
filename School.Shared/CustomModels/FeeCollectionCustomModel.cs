using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class FeeCollectionCustomModel
    {
        public int FeeCollectionId { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public Nullable<int> AdmissionId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }

        [Required(ErrorMessage = "Please enter amount")]
        public Nullable<decimal> AmountPaid { get; set; }

        [Required(ErrorMessage = "Please enter fee mode")]
        public string FeeMode { get; set; }

        [Required(ErrorMessage = "Please enter fee date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FeeDate { get; set; }

        [Required(ErrorMessage = "Please enter fee incharge")]
        public string FeeInCharge { get; set; }

        [Required(ErrorMessage = "Please enter fee type")]
        public string FeeType { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

    }
}
