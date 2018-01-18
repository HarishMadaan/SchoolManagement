﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Shared.CustomModels;

namespace School.BDC.Interfaces
{
    public interface ICommonMasterDataBusiness : IDisposable
    {
        List<ClassMasterModel> GetClassMaster();

        List<SectionMasterModel> GetSectionMaster();

        List<SessionMasterModel> GetSessionMaster();

        List<ClassMasterModel> BindSessionClass(int SessionId);

        List<SectionMasterModel> BindClassSection(int ClassId);

        List<EmployeeMasterModel> GetEmployeeMaster();

    }
}