using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.BDC.Interfaces;
using School.DAL.Interfaces;
using School.DAL.Repositories;

namespace School.BDC.Classes
{
    public class EmployeeMasterBusiness : IEmployeeMasterBusiness
    {
        IEmployeeMasterRepo objDAL;

        public EmployeeMasterCustomModel GetById(int Id)
        {
            using (objDAL = new EmployeeMasterRepo())
            {
                return objDAL.GetById(Id);
            }
        }

        public object GetEmployeeMasterListing(EmployeeMasterCustomModel objEmployeeRegistrationModel)
        {
            using (objDAL = new EmployeeMasterRepo())
            {
                return objDAL.GetEmployeeMasterListing(objEmployeeRegistrationModel);
            }
        }

        public Response SaveEmployeeMasterDetails(EmployeeMasterCustomModel objModel)
        {
            using (objDAL = new EmployeeMasterRepo())
            {
                return objDAL.SaveEmployeeMasterDetails(objModel);
            }
        }

        public object SetActiveEmployeeRegistrationDetail(EmployeeMasterCustomModel objEmployeeRegistrationModel)
        {
            using (objDAL = new EmployeeMasterRepo())
            {
                return objDAL.SetActiveEmployeeRegistrationDetail(objEmployeeRegistrationModel);
            }
        }

        public object DeleteEmployeeRegistrationDetail(EmployeeMasterCustomModel objEmployeeRegistrationModel)
        {
            using (objDAL = new EmployeeMasterRepo())
            {
                return objDAL.DeleteEmployeeRegistrationDetail(objEmployeeRegistrationModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new EmployeeMasterRepo())
            {
                return objDAL.FindById(Id);
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
