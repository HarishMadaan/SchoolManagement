using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Shared.CustomModels
{
    public class PagingViewModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortDir { get; set; }
        public int Skip { get; set; }
        public bool Reverse { get; set; }
        public string SerachTerm { get; set; }
        public int SessionId { get; set; }
        public long ProfileId { get; set; }
        public long SchoolId { get; set; }
        public string EncryptedSchoolId { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
