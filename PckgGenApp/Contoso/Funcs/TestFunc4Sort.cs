using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using PckgGenLib.Enums;
using PckgGenLib.NSModifierCF;
using PckgGenLib.NSModifierDFT;
using PckgGenLib.NSProjectGenerator;
using System.Collections.Generic;

namespace PckgGenApp.Contoso
{
    partial class ContoloLoader
    {
        void TestFunc4Sort(Project pj, Package pi, PackageConfigRow pcr)
        {
            //  obróbka CF
            //  --------------------------------------------------------------------

            ModifierCF pm = new ModifierCF(pi);
            Executable ex2 = pm.Add_DataFlowTask("Full Reload");

            //  obróbka DF
            //  --------------------------------------------------------------------

            ModifierDFT dm = new ModifierDFT(pj, pi, ex2);

            IDTSComponentMetaData100 com1 = dm.AddComp_OleDBSource("BloczekSource", "Contoso", pcr.SrcCode, 0);

            //  Sort
            // --------------------------------------------------------

            //  com1.OutputCollection[0]
        }
    }
}
