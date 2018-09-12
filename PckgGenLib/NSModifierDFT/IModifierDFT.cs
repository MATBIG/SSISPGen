using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using PckgGenLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenLib.NSModifierDFT
{
    public interface IModifierDFT
    {

        #region AttunitySource
        IDTSComponentMetaData100 AddComp_AttunitySource(string componentName,
                                                        string conManName,
                                                        string source
                                                        );

        #endregion AttunitySource

        #region CondSplit
        IDTSComponentMetaData100 AddComp_CondSplit(string ComponentName,
                                                    IDTSOutput100 outCols
                                                    );

        IDTSOutput100 ModifyComp_CondSplit_AddOut(IDTSComponentMetaData100 Comp,
                                                            string OutName,
                                                            int OutEvalOrder,
                                                            string OutExpression,
                                                            string OutDesc = "SampleDescription"
                                                            );
        #endregion CondSplit

        #region DerivedColumn
        IDTSComponentMetaData100 AddComp_DerivedCol(string ComponentName, IDTSOutput100 outCols);

        IDTSOutputColumn100 ModifyComp_DerivedCol_AddCol(IDTSComponentMetaData100 Comp,
                                                            string DCName,
                                                            string DCExpression,
                                                            DataType DTDataType,
                                                            int DCDTLength = 0,
                                                            int DCDTPrecision = 0,
                                                            int DCDTScale = 0,
                                                            int DCDTCodePage = 0
                                                            );

        #endregion DerivedColumn

        #region Lookup
        IDTSComponentMetaData100 AddComp_Lookup(string componentName,
                                                string conManName,
                                                string sqlComm,
                                                IDTSOutput100 outCols,
                                                List<string> joinColumns,
                                                Dictionary<string, string> newColumns,
                                                DTSRowDisposition rowDisposition = DTSRowDisposition.RD_IgnoreFailure,
                                                int redirectRowsToNoMatchOutput = 0
                                                );
        #endregion Lookup

        #region MergeJoin

        IDTSComponentMetaData100 AddComp_MergeJoin(string ComponentName,
                                                    IDTSOutput100 outColsL,
                                                    IDTSOutput100 outColsR,
                                                    int joinType = 2,
                                                    int keyColNum = -1
                                                    );

        #endregion MergeJoin

        #region Multicast
        IDTSComponentMetaData100 AddComp_Multicast( string ComponentName,
                                                    IDTSOutput100 outCols
                                                    );
        #endregion Multicast

        #region OleDBCommand
        IDTSComponentMetaData100 AddComp_OleDBCommand(  string componentName,
                                                        string conManName,
                                                        IDTSOutput100 outCols,
                                                        string sqlCmd,
                                                        Dictionary<string, string> paramMapping
                                                        );
        #endregion OleDBCommand

        #region OleDBDestination
        IDTSComponentMetaData100 AddComp_OleDBDestination
            (
                string componentName,
                string destObjectName,
                string conManName,
                IDTSOutput100 outCols,
                CreateTableFlag createTableFlag = CreateTableFlag.Create
            );
        #endregion OleDbDestination

        #region OleDBSource
        IDTSComponentMetaData100 AddComp_OleDBSource
            (
                string componentName,
                string conManName,
                string source,
                int accessMode = 0
            );

        #endregion OleDBSource

        #region RowCount
        IDTSComponentMetaData100 AddComp_RowCount
            (
                string ComponentName,
                string VarName,
                IDTSOutput100 outCols
            );
        #endregion RowCount

        #region Sort
        IDTSComponentMetaData100 AddComp_Sort(  string ComponentName,
                                                IDTSOutput100 outCols,
                                                Dictionary<string, bool> sortColumns
                                                );
        #endregion Sort

        #region UnionAll
        IDTSComponentMetaData100 AddComp_UnionAll(string ComponentName);
        void ModifyComp_UnionAll_AddInput(IDTSComponentMetaData100 Comp,
                                                   IDTSOutput100 outCols
                                                   );
        #endregion UnionAll

        #region SQLMethods

        string CreateTempTableSQL(string objName, string conManName);

        #endregion SQLMethods

    }
}
