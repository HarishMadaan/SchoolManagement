using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;
using School.Shared.CommonFunc;
using School.DAL.Interfaces;

namespace School.DAL.Repositories
{
    public class CommonMasterDataRepo : ICommonMasterDataRepo
    {
        SchoolManagementEntities dbcontext;
        Response response;

        public List<ClassMasterModel> GetClassMaster()
        {
            List<ClassMasterModel> ClassListModel = new List<ClassMasterModel>();
            IQueryable<ClassMasterModel> ClassListDetail = null;
            using (dbcontext = new SchoolManagementEntities())
            {
                ClassListDetail = dbcontext.tblClasses.Where(x => x.IsActive == true
                    && x.IsDeleted == false).OrderBy(y => y.Title)
                    .Select(x => new ClassMasterModel
                    {
                        ClassId = x.ClassId,
                        Title = x.Title,
                    });

                ClassListModel = ClassListDetail.ToList();

                return ClassListModel;
            }
        }

        public List<SectionMasterModel> GetSectionMaster()
        {
            List<SectionMasterModel> SectionListModel = new List<SectionMasterModel>();
            IQueryable<SectionMasterModel> SectionListDetail = null;
            using (dbcontext = new SchoolManagementEntities())
            {
                SectionListDetail = dbcontext.tblSections.Where(x => x.IsActive == true
                    && x.IsDeleted == false).OrderBy(y => y.Title)
                    .Select(x => new SectionMasterModel
                    {
                        SectionId = x.SectionId,
                        Title = x.Title,
                    });

                SectionListModel = SectionListDetail.ToList();

                return SectionListModel;
            }
        }

        public List<SessionMasterModel> GetSessionMaster()
        {
            List<SessionMasterModel> SessionListModel = new List<SessionMasterModel>();
            IQueryable<SessionMasterModel> SessionListDetail = null;
            using (dbcontext = new SchoolManagementEntities())
            {
                SessionListDetail = dbcontext.tblSessions.Where(x => x.IsActive == true
                    && x.IsDeleted == false).OrderBy(y => y.Session)
                    .Select(x => new SessionMasterModel
                    {
                        SessionId = x.SessionId,
                        Title = x.Session,
                    });

                SessionListModel = SessionListDetail.ToList();

                return SessionListModel;
            }
        }

        public List<ClassMasterModel> BindSessionClass(int SessionId)
        {
            using (dbcontext = new SchoolManagementEntities())
            {
                var rs = dbcontext.tblClasses.Where(x => x.IsActive == true
                    && x.IsDeleted == false && x.SessionId == SessionId).OrderBy(y => y.Title)
                    .Select(x => new ClassMasterModel
                    {
                        ClassId = x.ClassId,
                        Title = x.Title,
                    }).ToList();
                
                return rs;
            }
        }

        public List<SectionMasterModel> BindClassSection(int ClassId)
        {
            using (dbcontext = new SchoolManagementEntities())
            {
                var rs = dbcontext.tblSections.Where(x => x.IsActive == true
                    && x.IsDeleted == false && x.ClassId == ClassId).OrderBy(y => y.Title)
                    .Select(x => new SectionMasterModel
                    {
                        SectionId = x.SectionId,
                        Title = x.Title,
                    }).ToList();

                return rs;
            }
        }

        public List<EmployeeMasterModel> GetEmployeeMaster()
        {
            using (dbcontext = new SchoolManagementEntities())
            {
                var rs = dbcontext.tblEmployees.Where(x => x.IsActive == true
                    && x.IsDeleted == false).OrderBy(y => y.FName)
                    .Select(x => new EmployeeMasterModel
                    {
                        EmployeeId = x.EmployeeId,
                        EmployeeName= x.FName + " " + x.LName,
                    }).ToList();

                return rs;
            }
        }

        public void Dispose()
        {
            dbcontext.Dispose();
            if (response != null)
            {
                response.Dispose();
            }

            GC.SuppressFinalize(this);
        }

    }
}
