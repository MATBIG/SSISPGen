using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;


namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF
    {
        public Sequence Add_Sequence(string SequenceName, Sequence sqp = null)
        {
            Executable ex = null;

            if (sqp == null) { ex = p.Executables.Add("STOCK:Sequence"); }
            else { ex = sqp.Executables.Add("STOCK:Sequence"); }

            Sequence sq = ex as Sequence;
            sq.Name = SequenceName;

            return sq;
        }
    }
}