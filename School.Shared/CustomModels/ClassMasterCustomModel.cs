using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace School.Shared.CustomModels
{
    public class ClassMasterCustomModel
    {

        public int ClassId { get; set; }
        public Nullable<int> SchoolId { get; set; }

        [Required(ErrorMessage = "Please enter session")]
        public Nullable<int> SessionId { get; set; }        

        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter short description")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Please enter date")]

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DDate { get; set; }

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
        public string SchoolName { get; set; }
        public string SessionName { get; set; }

    }
}
