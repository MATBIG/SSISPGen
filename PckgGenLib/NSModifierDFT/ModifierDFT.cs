using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT : IModifierDFT
    {
        public ModifierDFT( Project prj,
                            Package p,
                            Executable ex_dft
                            )
        {
            this.prj = prj;
            this.p = p;
            dft = ex_dft;
            dmp = ((TaskHost)ex_dft).InnerObject as MainPipe;
        }

        Project prj;
        Package p;
        Executable dft;
        MainPipe dmp;
    }
}