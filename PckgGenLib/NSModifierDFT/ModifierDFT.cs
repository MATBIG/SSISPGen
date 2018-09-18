using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;

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