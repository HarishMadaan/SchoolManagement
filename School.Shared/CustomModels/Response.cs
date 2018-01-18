using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Shared.CustomModels
{
    public class Response : IDisposable
    {
        public bool success { get; set; }
        public string message { get; set; }

        public object responseData { get; set; }

        public int? LicenseId { get; set; }

        public long? UserId { get; set; }

        public int? LoginUserId { get; set; }

        public int? UserType { get; set; }

        public int? BranchId { get; set; }

        public string UserName { get; set; }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
