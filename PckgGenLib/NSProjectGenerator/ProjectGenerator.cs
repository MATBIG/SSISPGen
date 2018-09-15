using System;
using System.IO;
using System.Data;
using System.Linq;
using Microsoft.SqlServer.Dts.Runtime;
using System.Data.OleDb;
using System.Collections.Generic;
using PckgGenLib.EmbeddedResources;
using PckgGenLib.NSModifierCF;

namespace PckgGenLib.NSProjectGenerator
{
    public class ProjectGenerator: IProjectGenerator
    {
        private EmbeddedResourceReader er;
        private string tempIspacPath;
        private Project prj;

        public ProjectGenerator()
        {
            Directory.CreateDirectory("Ispac");
            tempIspacPath = @"Ispac\SSISProjectTemplate.ispac";
            er = new EmbeddedResourceReader();
            er.SaveEmbeddedToFile(tempIspacPath);
        }

        public void LoadProjectTemplate()
        {
            prj = Project.OpenProject(tempIspacPath);
        }

        public void SaveAsNewProject(string pjPath)
        {
            if (prj == null) { return; }
            prj.SaveTo(pjPath);
        }

        public void AddProjectParams(List<ProjectParamConfigRow> prms)
        {
            if (prj == null) { return; }
            foreach (ProjectParamConfigRow ppc in prms)
            {
                Parameter pr = prj.Parameters.Add(ppc.Name, ppc.Type);
                pr.Description = ppc.Description;
                pr.Value = ppc.Value;
            }
        }
        public void AddConManagers(List<ConManConfigRow> cons)
        {
            if (prj == null) { return; }
            foreach (ConManConfigRow cmc in cons)
            {
                ConnectionManagerItem cm = prj.ConnectionManagerItems.Add(cmc.CMType, cmc.Name + ".conmgr");
                cm.ConnectionManager.Name = cmc.Name;
                cm.ConnectionManager.ConnectionString = cmc.ConnectionString;
            }
        }

        public void AddPackages(List<PackageConfigRow> packs, ModifyPackageFun fun)
        {
            foreach (PackageConfigRow pkcon in packs)
            {
                Package pt = new Package();
                fun(prj, pt, pkcon);

                pt.Name = pkcon.Name;
                prj.PackageItems.Add(pt, pkcon.Name + ".dtsx");

                Console.WriteLine($"{DateTime.Now}:\t{pt.Name}");
            }
        }

        public void AddMasterPackages(List<PackageConfigRow> packs)
        {
            //  not completed

            IEnumerable<string> mp = packs.Select(x => x.MasterPackage).Distinct();

            foreach (string mpn in mp)
            {
                Package pt = new Package();
                IModifierCF pm = new ModifierCF(pt);

                IEnumerable<int> sqn = packs.Where(x => x.MasterPackage == mpn)
                    .Select(x => x.SeqOrder).Distinct()
                    .OrderBy(x => x);
                Sequence seq1 = null, seq2 = null;

                foreach (int sqno in sqn)
                {
                    seq1 = seq2;
                    seq2 = pm.Add_Sequence(string.Format("SEQ_{0:D3}", sqno));

                    IEnumerable<string> exp = packs.Where(x => x.MasterPackage == mpn && x.SeqOrder == sqno)
                        .Select(x => x.Name);

                    foreach (string expn in exp)
                    {
                        pm.AddTask_ExecPackage("ExecPkg_" + expn, expn + ".dtsx", seq2);
                    }
                    if (seq1 != null)
                    {
                        pm.Add_PrecConstr(seq1, seq2);
                    }
                }

                prj.PackageItems.Add(pt, mpn + ".dtsx");
            }
        }
    }
}