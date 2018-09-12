using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using PckgGenLib.Enums;

namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF
    {
        public Executable AddTask_ExecSQL(string taskName,
                                        string connManName,
                                        string statement,
                                        Sequence sqp = null,
                                        SqlStatementSourceType sourceType = SqlStatementSourceType.DirectInput,
                                        ResultSetType resultSetType = ResultSetType.ResultSetType_None)
        {
            //  DirectInput     1
            //  FileConnection  2
            //  Variable        3

            Executable ex = null;

            if (sqp == null) { ex = p.Executables.Add("STOCK:SQLTask"); }
            else { ex = sqp.Executables.Add("STOCK:SQLTask"); }

            TaskHost SQLth = (TaskHost)ex;
            ExecuteSQLTask est = SQLth.InnerObject as ExecuteSQLTask;

            DtsProperty dt_name = SQLth.Properties["Name"];
            DtsProperty dt_conn = SQLth.Properties["Connection"];
            DtsProperty dt_sttp = SQLth.Properties["SqlStatementSourceType"];
            DtsProperty dt_stat = SQLth.Properties["SqlStatementSource"];

            dt_name.SetValue(SQLth, taskName);
            dt_conn.SetValue(SQLth, connManName);
            dt_sttp.SetValue(SQLth, sourceType);

            est.ResultSetType = resultSetType;

            switch (sourceType)
            {
                case SqlStatementSourceType.DirectInput:
                    dt_stat.SetValue(SQLth, statement);
                    break;
                case SqlStatementSourceType.Variable:
                    dt_stat.SetValue(SQLth, statement);
                    break;
            }

            return ex;
        }

        public IDTSResultBinding Modify_ExecSQL_AddResultSetBinding(Executable ex,
                                                        string ResultName,
                                                        string VariableName
            )
        {
            TaskHost SQLth = (TaskHost)ex;
            ExecuteSQLTask task = SQLth.InnerObject as ExecuteSQLTask;

            int i = 0;
            foreach (IDTSResultBinding z in task.ResultSetBindings)
            { i++; }

            // Add result set binding, map the id column to variable
            task.ResultSetBindings.Add();
            IDTSResultBinding rb = task.ResultSetBindings.GetBinding(i);
            rb.ResultName = ResultName;
            rb.DtsVariableName = VariableName;

            return rb;
        }

        public IDTSParameterBinding Modify_ExecSQL_AddParameterBinding(  
                        Executable ex,
                        string BingingVariableName,
                        OleDBDataTypes BingingDataType
            )
        {
            TaskHost SQLth = (TaskHost)ex;
            ExecuteSQLTask task = SQLth.InnerObject as ExecuteSQLTask;

            int i = 0;
            foreach (IDTSParameterBinding z in task.ParameterBindings)
            { i++; }

            task.ParameterBindings.Add();
            IDTSParameterBinding pb = task.ParameterBindings.GetBinding(i);
            pb.DtsVariableName = BingingVariableName;
            pb.ParameterDirection = ParameterDirections.Input;
            pb.DataType = (int)BingingDataType;
            pb.ParameterName = i;
            pb.ParameterSize = -1;

            return pb;
        }
    }
}