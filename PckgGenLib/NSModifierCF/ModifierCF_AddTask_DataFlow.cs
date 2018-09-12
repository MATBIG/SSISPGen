using Microsoft.SqlServer.Dts.Runtime;

namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF
    {
        public Executable AddTask_DataFlow(string TaskName, Sequence sqp = null)
        {
            Executable ex = null;

            if (sqp == null) { ex = p.Executables.Add("STOCK:PipelineTask"); }
            else { ex = sqp.Executables.Add("STOCK:PipelineTask"); }

            TaskHost taskHost = ex as TaskHost;
            taskHost.Name = TaskName;

            return ex;
        }
    }
}