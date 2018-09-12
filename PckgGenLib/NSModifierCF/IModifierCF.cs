using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using PckgGenLib.Enums;

namespace PckgGenLib.NSModifierCF
{
    public interface IModifierCF
    {
        #region PrecConstr
        PrecedenceConstraint Add_PrecConstr
            (
                Executable ex1,
                Executable ex2,
                Sequence sqn = null,
                DTSPrecedenceEvalOp EvalOp = DTSPrecedenceEvalOp.Constraint,
                DTSExecResult Result = DTSExecResult.Success,
                string Expression = "",
                bool LogicalAnd = true
            );

        #endregion PrecConstr

        #region Sequence
        Sequence Add_Sequence
        (
            string SequenceName,
            Sequence sqp = null
        );
        #endregion Sequence

        #region DataFlow
        Executable AddTask_DataFlow
        (
            string TaskName,
            Sequence sqp = null
        );
        #endregion DataFlow

        #region ExecPackage
        Executable AddTask_ExecPackage
        (
            string TaskName,
            string PackageName,
            Sequence sqp
        );
        #endregion ExecPackage

        #region ExecSQL

        Executable AddTask_ExecSQL
            (
                string taskName,
                string connManName,
                string statement,
                Sequence sqp = null,
                SqlStatementSourceType sourceType = SqlStatementSourceType.DirectInput,
                ResultSetType resultSetType = ResultSetType.ResultSetType_None
            );
        IDTSResultBinding Modify_ExecSQL_AddResultSetBinding
            (
                Executable ex,
                string ResultName,
                string VariableName
            );
        IDTSParameterBinding Modify_ExecSQL_AddParameterBinding
            (
                Executable ex,
                string BingingVariableName,
                OleDBDataTypes BingingDataType
            );

        #endregion ExecSQL

        #region Variable
        Variable Add_Variable
        (
            string vName,
            object vValue,
            bool evalAsExp = false,
            string expCode = ""
        );

        #endregion Variable

        #region ClearPackage

        void ClearPackage();

        #endregion ClearPackage

    }
}
