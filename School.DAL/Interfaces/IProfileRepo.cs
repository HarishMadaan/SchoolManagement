using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.DAL.Interfaces
{
    public interface IProfileRepo : IDisposable
    {
        ProfileCustomModel GetById(int Id);
    }
}
