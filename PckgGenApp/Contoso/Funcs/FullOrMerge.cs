using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using PckgGenLib.Enums;
using PckgGenLib.NSModifierCF;
using PckgGenLib.NSModifierDFT;
using PckgGenLib.NSProjectGenerator;
using System.Collections.Generic;

namespace PckgGenApp.Contoso
{
    partial class ContosoLoader
    {
        void FullOrMerge(Project pj, Package pi, PackageConfigRow pcr)
        {

            ModifierCF pm = new ModifierCF(pi);

            //  Variables
            //  --------------------------------------------------------------------

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

            //  ModifyDFT exd1: FULL
            //  --------------------------------------------------------------------

            Executable exd1 = pm.AddTask_DataFlow("Full");

            ModifierDFT dm1 = new ModifierDFT(pj, pi, exd1);

            IDTSComponentMetaData100 com0101 = dm1.AddComp_OleDBSource("OLEDB Src", "CM_OLEDB_contoso", pcr.SrcCode, 0);

            IDTSComponentMetaData100 com0102 = dm1.AddComp_RowCount("RRC", "ReadRowsCount", com0101.OutputCollection[0]);

            IDTSComponentMetaData100 com0103 = dm1.AddComp_DerivedCol("AddAuditKey", com0102.OutputCollection[0]);
            IDTSOutputColumn100 dc010301 = dm1.ModifyComp_DerivedCol_AddCol(com0103, "AuditKey", "@[User::AuditKey]", Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_I4);

            IDTSComponentMetaData100 com0104 = dm1.AddComp_RowCount("IRC", "InsertRowsCount", com0103.OutputCollection[0]);

            IDTSComponentMetaData100 com0105 = dm1.AddComp_OleDBDestination("OLEDB Dest", pcr.DesTable, "CM_OLEDB_sandbox", com0104.OutputCollection[0], CreateTableFlag.DropAndCreate);
            string tmpName = dm1.CreateTempTableSQL(pcr.DesTable, "CM_OLEDB_sandbox");

            //  ModifyDFT exd2: MERGE DELETE
            //  --------------------------------------------------------------------

            Executable exd2 = pm.AddTask_DataFlow("Partial 01 - DELETE");
            ModifierDFT dm2 = new ModifierDFT(pj, pi, exd2);

            IDTSComponentMetaData100 com0201 = dm2.AddComp_OleDBSource("StagingTable", "CM_OLEDB_sandbox", pcr.DesTable, 0);

            string sqlCmd0202 = $"SELECT OID FROM {pcr.SrcCode}";
            List<string> joinColumns = new List<string> { "OID" };
            Dictionary<string, string> newColumns = new Dictionary<string, string> { { "OID", "oldOID" } };

            IDTSComponentMetaData100 com0202 = dm2.AddComp_Lookup(
                "LookupToSource",
                "CM_OLEDB_contoso",
                sqlCmd0202,
                com0201.OutputCollection[0],
                joinColumns,
                newColumns,
                DTSRowDisposition.RD_NotUsed,
                1
                );

            string sqlCmd0203 = $"DELETE FROM {pcr.DesTable} WHERE [OID] = ?";
            Dictionary<string, string> paramMapping0203 = new Dictionary<string, string>
            {
                { "Param_0", "OID" }
            };

            IDTSComponentMetaData100 com0203 = dm2.AddComp_OleDBCommand("OLE OLE Command!",
                                                                            "CM_OLEDB_sandbox",
                                                                            com0202.OutputCollection["Lookup No Match Output"],
                                                                            sqlCmd0203,
                                                                            paramMapping0203
                                                                            );

            //  ModifyDFT exd2: MERGE Match->TempTbl, NoMatch->Dest
            //  --------------------------------------------------------------------

            Executable exd3 = pm.AddTask_DataFlow("Partial 02 - INSERT");
            ModifierDFT dm3 = new ModifierDFT(pj, pi, exd3);

            IDTSComponentMetaData100 com0300 = dm3.AddComp_OleDBSource("OLEDB Src", "CM_OLEDB_contoso", "User::SQLCmd", 3);

            IDTSComponentMetaData100 com0301 = dm3.AddComp_DerivedCol("AddAuditKey", com0300.OutputCollection[0]);
            IDTSOutputColumn100 dc0301 = dm3.ModifyComp_DerivedCol_AddCol(com0301, "AuditKey", "@[User::AuditKey]", Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_I4);

            IDTSComponentMetaData100 com0302 = dm3.AddComp_RowCount("RRC", "ReadRowsCount", com0301.OutputCollection[0]);

            string sqlCmd0303 = $"SELECT OID FROM {pcr.DesTable}";
            List<string> joinColumns0303 = new List<string> { "OID" };
            Dictionary<string, string> newColumns0303 = new Dictionary<string, string>();

            IDTSComponentMetaData100 com0303 = dm3.AddComp_Lookup("LookupToSource",
                                                                    "CM_OLEDB_sandbox",
                                                                    sqlCmd0303,
                                                                    com0302.OutputCollection[0],
                                                                    joinColumns0303,
                                                                    newColumns0303,
                                                                    DTSRowDisposition.RD_NotUsed,
                                                                    1
                                                                    );

            IDTSComponentMetaData100 com0304I = dm3.AddComp_RowCount("IRC", "InsertRowsCount", com0303.OutputCollection["Lookup No Match Output"]);
            IDTSComponentMetaData100 com0304U = dm3.AddComp_RowCount("URC", "UpdateRowsCount", com0303.OutputCollection["Lookup Match Output"]);

            IDTSComponentMetaData100 com0305I = dm3.AddComp_OleDBDestination("OLEDB Dest - INSERT", pcr.DesTable, "CM_OLEDB_sandbox", com0304I.OutputCollection[0], CreateTableFlag.NoAction);
            IDTSComponentMetaData100 com0305U = dm3.AddComp_OleDBDestination("OLEDB Dest - UPDATE", tmpName, "CM_OLEDB_sandbox", com0304U.OutputCollection[0], CreateTableFlag.NoAction);

            //  
            //  --------------------------------------------------------------------

            Executable exs4 = pm.AddTask_ExecSQL(
                "MERGE TempAndDest 01 - DELETE",
                "CM_OLEDB_sandbox",
                $"DELETE f \nFROM {pcr.DesTable} AS f \nINNER JOIN {tmpName} AS t ON f.[OID] = t.[OID]"
                );

            //  
            //  --------------------------------------------------------------------

            Executable exs5 = pm.AddTask_ExecSQL(
                "MERGE TempAndDest 02 - INSERT",
                "CM_OLEDB_sandbox",
                $"INSERT INTO {pcr.DesTable} \nSELECT * \nFROM {tmpName} AS t"
                );

            //  
            //  --------------------------------------------------------------------

            Executable exs6 = pm.AddTask_ExecSQL(
                "usp_ETL_Audit_Stop",
                "CM_OLEDB_sandbox",
                "[etl].[usp_ETL_Audit_Stop] ?,?,?,?,?"
                );

            pm.Modify_ExecSQL_AddParameterBinding(exs6, "User::AuditKey", OleDBDataTypes.LONG);
            pm.Modify_ExecSQL_AddParameterBinding(exs6, "User::ReadRowsCount", OleDBDataTypes.LONG);
            pm.Modify_ExecSQL_AddParameterBinding(exs6, "User::FailedRowsCount", OleDBDataTypes.LONG);
            pm.Modify_ExecSQL_AddParameterBinding(exs6, "User::InsertRowsCount", OleDBDataTypes.LONG);
            pm.Modify_ExecSQL_AddParameterBinding(exs6, "User::UpdateRowsCount", OleDBDataTypes.LONG);

            //  ModifyCF - Precedence Constraints
            //  --------------------------------------------------------------------

            pm.Add_PrecConstr(exs1, exs2, null, DTSPrecedenceEvalOp.ExpressionAndConstraint, DTSExecResult.Success, "@[$Project::ETLMode] == 1");
            pm.Add_PrecConstr(exs1, exs3, null, DTSPrecedenceEvalOp.ExpressionAndConstraint, DTSExecResult.Success, "@[$Project::ETLMode] == 3");
            pm.Add_PrecConstr(exs2, exd1, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success);
            pm.Add_PrecConstr(exs3, exd2, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success);

            pm.Add_PrecConstr(exd2, exd3, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success);
            pm.Add_PrecConstr(exd3, exs4, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success);
            pm.Add_PrecConstr(exs4, exs5, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success);

            pm.Add_PrecConstr(exd1, exs6, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success, null, false);
            pm.Add_PrecConstr(exs5, exs6, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Success, null, false);

        }
    }
}
