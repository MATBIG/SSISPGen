using PckgGenAppMain;
using PckgGenLib.NSProjectGenerator;
using System.Collections.Generic;
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
            pg.AddPackages(packs, FullOrMerge);

            // odpalić dwa razy dla paczek generowanych różnymi funkcjami

            pg.AddMasterPackages(packs);

            //  --------------------------------------------------------------------

            pg.SaveAsNewProject(fPath);
        }
    }
}
