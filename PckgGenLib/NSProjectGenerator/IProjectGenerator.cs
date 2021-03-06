﻿using Microsoft.SqlServer.Dts.Runtime;
using System.Collections.Generic;

namespace PckgGenLib.NSProjectGenerator
{
    public interface IProjectGenerator
    {
        Project Prj();
        void LoadProjectTemplate();
        void SaveAsNewProject(string pjPath);
        void AddProjectParams(List<ProjectParamConfigRow> prms);
        void AddConManagers(List<ConManConfigRow> cons);
        void AddPackages(List<PackageConfigRow> packs, ModifyPackageFun fun);
        void AddMasterPackages(List<PackageConfigRow> packs);
    }
}
