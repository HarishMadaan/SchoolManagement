using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.DAL.Interfaces
{
    public interface IEmployeeMasterRepo : IDisposable
    {
        EmployeeMasterCustomModel GetById(int Id);
        object GetEmployeeMasterListing(EmployeeMasterCustomModel objEmployeeRegistrationModel);

        Response SaveEmployeeMasterDetails(EmployeeMasterCustomModel objModel);

        object SetActiveEmployeeRegistrationDetail(EmployeeMasterCustomModel objEmployeeRegistrationModel);

        object DeleteEmployeeRegistrationDetail(EmployeeMasterCustomModel objEmployeeRegistrationModel);

        bool FindById(int Id);

    }
}
