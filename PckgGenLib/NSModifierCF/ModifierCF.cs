using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF: IModifierCF
    {
        public ModifierCF(Package p)
        {
            this.p = p;
        }

        private Package p;
    }
}
