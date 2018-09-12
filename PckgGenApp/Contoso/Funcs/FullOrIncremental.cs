using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using PckgGenLib.Enums;
using PckgGenLib.NSModifierCF;
using PckgGenLib.NSModifierDFT;
using PckgGenLib.NSProjectGenerator;

namespace PckgGenApp.Contoso
{
    partial class ContosoLoader
    {
        void FullOrIncremental(Project pj, Package pi, PackageConfigRow pcr)
        {
            //  obróbka CF
            //  --------------------------------------------------------------------

            ModifierCF pm = new ModifierCF(pi);

            Variable v1 = pm.Add_Variable("AuditKey", 0);
            Variable v2 = pm.Add_Variable("maxUdate", "1990-01-01 00:00:00.000");
            Variable v3 = pm.Add_Variable("SQLCmd", "", true, $"\"SELECT * FROM {pcr.SrcCode} WHERE UDATE > '\" + @[User::maxUdate]" + "+\"'\"");

            Variable rc1 = pm.Add_Variable("FailedRowsCount", 0);
            Variable rc2 = pm.Add_Variable("InsertRowsCount", 0);
            Variable rc3 = pm.Add_Variable("ReadRowsCount", 0);
            Variable rc4 = pm.Add_Variable("UpdateRowsCount", 0);

            //  
            //  --------------------------------------------------------------------

            Executable exs1 = pm.AddTask_ExecSQL(
                "usp_ETL_Audit_Start",
                "CM_OLEDB_sandbox",
                $"EXEC [etl].[usp_ETL_Audit_Start] '{pcr.DesTable}',?,?",
                null,
                SqlStatementSourceType.DirectInput,
                ResultSetType.ResultSetType_SingleRow
                );

            pm.Modify_ExecSQL_AddParameterBinding(exs1, "System::PackageName", OleDBDataTypes.VARCHAR);
            pm.Modify_ExecSQL_AddParameterBinding(exs1, "System::PackageID", OleDBDataTypes.GUID);

            pm.Modify_ExecSQL_AddResultSetBinding(exs1, "AuditPK", "User::AuditKey");

            //  
            //  --------------------------------------------------------------------

            Executable exs2 = pm.AddTask_ExecSQL(
                "TRUNC TAB",
                "CM_OLEDB_sandbox",
                $"TRUNCATE TABLE {pcr.DesTable};"
                );

            //  
            //  --------------------------------------------------------------------

            Executable exs3 = pm.AddTask_ExecSQL(
                "Get MaxDate",
                "CM_OLEDB_sandbox",
                $"SELECT ISNULL(MAX(CONVERT(VARCHAR,UDATE,121)),'1990-01-01 00:00:00.000') AS maxUDATE FROM {pcr.DesTable}",
                null,
                SqlStatementSourceType.DirectInput,
                ResultSetType.ResultSetType_SingleRow
                );

            pm.Modify_ExecSQL_AddResultSetBinding(exs3, "maxUDATE", "User::maxUdate");

            ////  ModifyDFT exd1: FULL
            ////  --------------------------------------------------------------------

            Executable exd1 = pm.AddTask_DataFlow("Full");
            ModifierDFT dm1 = new ModifierDFT(pj, pi, exd1);

            IDTSComponentMetaData100 com0101 = dm1.AddComp_OleDBSource("OLEDB Src", "CM_OLEDB_contoso", pcr.SrcCode, 0);

            IDTSComponentMetaData100 com0102 = dm1.AddComp_RowCount("RRC", "ReadRowsCount", com0101.OutputCollection[0]);

            IDTSComponentMetaData100 com0103 = dm1.AddComp_DerivedCol("AddAuditKey", com0102.OutputCollection[0]);
            IDTSOutputColumn100 dc010301 = dm1.ModifyComp_DerivedCol_AddCol(com0103, "AuditKey", "@[User::AuditKey]", Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_I4);

            IDTSComponentMetaData100 com0104 = dm1.AddComp_RowCount("IRC", "InsertRowsCount", com0103.OutputCollection[0]);

            IDTSComponentMetaData100 com0105 = dm1.AddComp_OleDBDestination("OLEDB Dest", pcr.DesTable, "CM_OLEDB_sandbox", com0104.OutputCollection[0], CreateTableFlag.DropAndCreate);

            //  ModifyDFT exd2: INCREMENTAL
            //  --------------------------------------------------------------------

            Executable exd2 = pm.AddTask_DataFlow("Incremental");
            ModifierDFT dm2 = new ModifierDFT(pj, pi, exd2);

            IDTSComponentMetaData100 com0201 = dm2.AddComp_OleDBSource("OLEDB Src", "CM_OLEDB_contoso", "User::SQLCmd", 3);

            IDTSComponentMetaData100 com0202 = dm2.AddComp_RowCount("RRC", "ReadRowsCount", com0201.OutputCollection[0]);

            IDTSComponentMetaData100 com0203 = dm2.AddComp_DerivedCol("AddAuditKey", com0202.OutputCollection[0]);
            IDTSOutputColumn100 dc020301 = dm2.ModifyComp_DerivedCol_AddCol(com0203, "AuditKey", "@[User::AuditKey]", Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_I4);

            IDTSComponentMetaData100 com0204 = dm2.AddComp_RowCount("IRC", "InsertRowsCount", com0203.OutputCollection[0]);

            IDTSComponentMetaData100 com0205 = dm2.AddComp_OleDBDestination("OLEDB Dest", pcr.DesTable, "CM_OLEDB_sandbox", com0204.OutputCollection[0], CreateTableFlag.NoAction);

            //  
            //  --------------------------------------------------------------------

            Executable exs4 = pm.AddTask_ExecSQL(
                "usp_ETL_Audit_Stop",
                "CM_OLEDB_sandbox",
                "[etl].[usp_ETL_Audit_Stop] ?,?,?,?,?"
                );

            pm.Modify_ExecSQL_AddParameterBinding(exs4, "User::AuditKey", OleDBDataTypes.LONG);
            pm.Modify_ExecSQL_AddParameterBinding(exs4, "User::ReadRowsCount", OleDBDataTypes.LONG);
            pm.Modify_ExecSQL_AddParameterBinding(exs4, "User::FailedRowsCount", OleDBDataTypes.LONG);
            pm.Modify_ExecSQL_AddParameterBinding(exs4, "User::InsertRowsCount", OleDBDataTypes.LONG);
            pm.Modify_ExecSQL_AddParameterBinding(exs4, "User::UpdateRowsCount", OleDBDataTypes.LONG);

            //  ModifyCF - Precedence Constraints
            //  --------------------------------------------------------------------

            pm.Add_PrecConstr(exs1, exs2, null, DTSPrecedenceEvalOp.ExpressionAndConstraint, DTSExecResult.Success, "@[$Project::ETLMode] == 1");
            pm.Add_PrecConstr(exs1, exs3, null, DTSPrecedenceEvalOp.ExpressionAndConstraint, DTSExecResult.Success, "@[$Project::ETLMode] == 3");
            pm.Add_PrecConstr(exs2, exd1, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success);
            pm.Add_PrecConstr(exs3, exd2, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success);
            pm.Add_PrecConstr(exd1, exs4, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success, null, false);
            pm.Add_PrecConstr(exd2, exs4, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success, null, false);

        }
    }
}
