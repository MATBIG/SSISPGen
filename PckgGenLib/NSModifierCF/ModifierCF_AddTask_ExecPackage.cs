using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;

namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF
    {
        public Executable AddTask_ExecPackage(string TaskName,
                                            string PackageName,
                                            Sequence sqp = null
            )
        {
            Executable ex = null;

            if (sqp == null) { ex = p.Executables.Add("Microsoft.ExecutePackageTask"); }
            else { ex = sqp.Executables.Add("Microsoft.ExecutePackageTask"); }

            TaskHost SQLth = (TaskHost)ex;

            DtsProperty dt_name = SQLth.Properties["Name"];
            DtsProperty dt_pnam = SQLth.Properties["PackageName"];
            DtsProperty dt_uref = SQLth.Properties["UseProjectReference"];

            dt_name.SetValue(SQLth, TaskName);
            dt_pnam.SetValue(SQLth, PackageName);
            dt_uref.SetValue(SQLth, true);

            return ex;
        }
    }
}