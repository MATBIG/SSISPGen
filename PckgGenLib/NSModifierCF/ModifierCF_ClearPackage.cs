using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;


namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF
    {
        public void ClearPackage()
        {
            for (int i = p.Executables.Count - 1; i >= 0; i--)
            {
                p.Executables.Remove(p.Executables[i]);
            }

            for (int i = p.Variables.Count - 1; i >= 0; i--)
            {
                if (p.Variables[i].Namespace != "System")
                { p.Variables.Remove(p.Variables[i]); }
            }
        }
    }
}