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
    public class SectionMasterBusiness : ISectionMasterBusiness
    {
        ISectionMasterRepo objDAL;

        public SectionMasterCustomModel GetById(int Id)
        {
            using (objDAL = new SectionMasterRepo())
            {
                return objDAL.GetById(Id);
            }
        }


        public object GetSectionMasterListing(SectionMasterCustomModel objSectionRegistrationModel)
        {
            using (objDAL = new SectionMasterRepo())
            {
                return objDAL.GetSectionMasterListing(objSectionRegistrationModel);
            }
        }

        public Response SaveSectionMasterDetails(SectionMasterCustomModel objModel)
        {
            using (objDAL = new SectionMasterRepo())
            {
                return objDAL.SaveSectionMasterDetails(objModel);
            }
        }

        public object SetActiveSectionRegistrationDetail(SectionMasterCustomModel objSectionRegistrationModel)
        {
            using (objDAL = new SectionMasterRepo())
            {
                return objDAL.SetActiveSectionRegistrationDetail(objSectionRegistrationModel);
            }
        }

        public object DeleteSectionRegistrationDetail(SectionMasterCustomModel objSectionRegistrationModel)
        {
            using (objDAL = new SectionMasterRepo())
            {
                return objDAL.DeleteSectionRegistrationDetail(objSectionRegistrationModel);
            }
        }

        public bool FindById(int Id)
        {
            using (objDAL = new SectionMasterRepo())
            {
                return objDAL.FindById(Id);
            }
        }

        public void Dispose()
        {
            //objDAL.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
