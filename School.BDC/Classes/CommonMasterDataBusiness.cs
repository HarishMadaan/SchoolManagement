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
    public class CommonMasterDataBusiness : ICommonMasterDataBusiness 
    {
        ICommonMasterDataRepo objDAL;

        public List<ClassMasterModel> GetClassMaster(int? SessionId)
        {
            using (objDAL = new CommonMasterDataRepo())
            {
                return objDAL.GetClassMaster(SessionId);
            }
        }

        public List<SectionMasterModel> GetSectionMaster(int? ClassId)
        {
            using (objDAL = new CommonMasterDataRepo())
            {
                return objDAL.GetSectionMaster(ClassId);
            }
        }

        public List<SessionMasterModel> GetSessionMaster()
        {
            using (objDAL = new CommonMasterDataRepo())
            {
                return objDAL.GetSessionMaster();
            }
        }

        public List<ClassMasterModel> BindSessionClass(int SessionId)
        {
            using (objDAL = new CommonMasterDataRepo())
            {
                return objDAL.BindSessionClass(SessionId);
            }
        }

        public List<SectionMasterModel> BindClassSection(int ClassId)
        {
            using (objDAL = new CommonMasterDataRepo())
            {
                return objDAL.BindClassSection(ClassId);
            }
        }

        public List<EmployeeMasterModel> GetEmployeeMaster()
        {
            using (objDAL = new CommonMasterDataRepo())
            {
                return objDAL.GetEmployeeMaster();
            }
        }

        public void Dispose()
        {
            objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
