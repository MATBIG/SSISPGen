using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using PckgGenLib.NSModifierCF;
using PckgGenLib.NSModifierDFT;
using PckgGenLib.NSProjectGenerator;
using System;
using System.Collections.Generic;

namespace DCDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2m02();
        }

        static void Demo2m01()
        {
            //  Configs
            //  --------------------------------------------------------------------

            List<ConManConfigRow> coms = new List<ConManConfigRow>
            {
                new ConManConfigRow(@"CM_OLEDB_contoso","OLEDB", @"Data Source=ITK\DEV17;Initial Catalog=ContosoRetailDW;Provider=SQLNCLI11.1; Integrated Security=SSPI;Auto Translate=False;"),
                new ConManConfigRow(@"CM_OLEDB_sandbox","OLEDB", @"Data Source=ITK\DEV17;Initial Catalog=SSISPGenDemo;Provider=SQLNCLI11.1; Integrated Security=SSPI;Auto Translate=False;")
            };

            List<Tuple<string, string>> packs = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("DimAccount","[dbo].[DimAccount]"),
                new Tuple<string, string>("DimCustomer","[dbo].[DimCustomer]")
            };

            //  --------------------------------------------------------------------

            IProjectGenerator pg = new ProjectGenerator();
            pg.LoadProjectTemplate();

            //  --------------------------------------------------------------------

            pg.AddConManagers(coms);

            foreach (Tuple<string, string> p in packs)
            {
                Package pt = new Package
                {
                    Name = p.Item1
                };

                pg.Prj().PackageItems.Add(pt, p.Item1 + ".dtsx");

                Console.WriteLine($"{DateTime.Now}:\t{pt.Name}");
            }

            //  --------------------------------------------------------------------

            pg.SaveAsNewProject(@"C:\Users\tomek\Desktop\DCDemoSSIS\IspacFiles\Demo2a.ispac");
        }

        static void Demo2m02()
        {
            //  Configs
            //  --------------------------------------------------------------------

            List<ConManConfigRow> coms = new List<ConManConfigRow>
            {
                new ConManConfigRow(@"CM_OLEDB_contoso","OLEDB", @"Data Source=ITK\DEV17;Initial Catalog=ContosoRetailDW;Provider=SQLNCLI11.1; Integrated Security=SSPI;Auto Translate=False;"),
                new ConManConfigRow(@"CM_OLEDB_sandbox","OLEDB", @"Data Source=ITK\DEV17;Initial Catalog=SSISPGenDemo;Provider=SQLNCLI11.1; Integrated Security=SSPI;Auto Translate=False;")
            };

            List<Tuple<string, string>> packs = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("DimAccount","[dbo].[DimAccount]"),
                new Tuple<string, string>("DimCustomer","[dbo].[DimCustomer]")
            };

            //  --------------------------------------------------------------------

            IProjectGenerator pg = new ProjectGenerator();
            pg.LoadProjectTemplate();

            //  --------------------------------------------------------------------

            pg.AddConManagers(coms);

            foreach (Tuple<string, string> p in packs)
            {
                Package pt = new Package
                {
                    Name = p.Item1
                };

                //  CF
                //  --------------------------------------------------------------------

                ModifierCF pm = new ModifierCF(pt);
                
                Sequence seq1 = pm.Add_Sequence("Seq1"); 
                Sequence seq2 = pm.Add_Sequence("Seq2A");
                Sequence seq3 = pm.Add_Sequence("Seq2B");

                pm.Add_PrecConstr(seq1, seq2);
                pm.Add_PrecConstr(seq1, seq3, null, DTSPrecedenceEvalOp.Constraint, DTSExecResult.Failure);

                Executable exs1 = pm.AddTask_ExecSQL(   "Nazwa ESQLTsk",
                                                        "CM_OLEDB_contoso",
                                                        $"SELECT COUNT(*) AS [cnt] FROM {p.Item2}",
                                                        seq1,
                                                        SqlStatementSourceType.DirectInput,
                                                        ResultSetType.ResultSetType_None
                                                        );

                Executable exd1 = pm.AddTask_DataFlow("Load", seq2);

                //  DF
                //  --------------------------------------------------------------------

                ModifierDFT dm1 = new ModifierDFT(pg.Prj(), pt, exd1);

                IDTSComponentMetaData100 com0101 = dm1.AddComp_OleDBSource( "OLEDB Src", 
                                                                            "CM_OLEDB_contoso",
                                                                            p.Item2, 
                                                                            0
                                                                            );

                Variable v1 = pm.Add_Variable("ReadRowsCount", 0);
                IDTSComponentMetaData100 com0102 = dm1.AddComp_RowCount(    "RRC", 
                                                                            "ReadRowsCount", 
                                                                            com0101.OutputCollection[0]
                                                                            );

                IDTSComponentMetaData100 com0103 = dm1.AddComp_DerivedCol(  "DRVCol", 
                                                                            com0102.OutputCollection[0]
                                                                            );

                IDTSOutputColumn100 dc010301 = dm1.ModifyComp_DerivedCol_AddCol(    com0103, 
                                                                                    "NewCol1", 
                                                                                    "123", 
                                                                                    Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_I4
                                                                                    );

                //  --------------------------------------------------------------------

                pg.Prj().PackageItems.Add(pt, p.Item1 + ".dtsx");

                Console.WriteLine($"{DateTime.Now}:\t{pt.Name}");
            }

            //  --------------------------------------------------------------------

            pg.SaveAsNewProject(@"C:\Users\tomek\Desktop\DCDemoSSIS\IspacFiles\Demo2b.ispac");
        }
    }
}
