using PckgGenAppMain;
using PckgGenLib.NSProjectGenerator;
using System.Collections.Generic;
using System.Linq;

namespace PckgGenApp.Contoso
{
    partial class ContosoLoader : ILoader
    {
        public void Start(string fPath)
        {

            //  Configs
            //  --------------------------------------------------------------------

            List<ConManConfigRow> coms = GetConfigListConMans();
            List<ProjectParamConfigRow> pars = GetConfigListParameters();
            List<PackageConfigRow> packs = GetConfigListPackages();

            //  --------------------------------------------------------------------

            IProjectGenerator pg = new ProjectGenerator();
            pg.LoadProjectTemplate();

            //  --------------------------------------------------------------------

            pg.AddProjectParams(pars);
            pg.AddConManagers(coms);
            pg.AddPackages(packs.Where(p => p.Name.StartsWith("Dim")).ToList(), FullOrMerge);
            pg.AddPackages(packs.Where(p => p.Name.StartsWith("Fact")).ToList(), FullOrIncremental);

            pg.AddMasterPackages(packs);

            //  --------------------------------------------------------------------

            pg.SaveAsNewProject(fPath);
        }
    }
}
