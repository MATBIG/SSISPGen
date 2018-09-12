﻿using PckgGenLib.NSProjectGenerator;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PckgGenApp.Contoso
{
    partial class ContosoLoader
    {

        List<ProjectParamConfigRow> GetConfigListParameters()
        {
            List<ProjectParamConfigRow> lp = new List<ProjectParamConfigRow>
            {
                new ProjectParamConfigRow("ETLMode", TypeCode.Int32 , "1: Full, 3: Incremental", 3)
            };

            return lp;
        }
        List<ConManConfigRow> GetConfigListConMans()
        {
            List<ConManConfigRow> lp = new List<ConManConfigRow>
            {
                new ConManConfigRow(@"CM_OLEDB_contoso","OLEDB", @"Data Source=.;Initial Catalog=ContosoRetailDW;Provider=SQLNCLI11.1; Integrated Security=SSPI;Auto Translate=False;"),
                new ConManConfigRow(@"CM_OLEDB_sandbox","OLEDB", @"Data Source=.;Initial Catalog=sandbox;Provider=SQLNCLI11.1; Integrated Security=SSPI;Auto Translate=False;")
            };

            return lp;
        }
        List<PackageConfigRow> GetConfigListPackages()
        {
            List<PackageConfigRow> lp = new List<PackageConfigRow>();

            string ConnStr = "Data Source=.;Initial Catalog=sandbox;Integrated Security=SSPI;";
            string queryString =
                @"
                    SELECT TOP 1
                        [Package]
                    ,   [SrcType]
                    ,   [SrcCode]
                    ,   [DesTabCreateFlag]
                    ,   [DesTab]
                    ,   [MasterPackage]
                    ,   [SeqOrder]
                    FROM [dbo].[PckgGenConfig]
                ";

            SqlConnection cnn = new SqlConnection(ConnStr);
            cnn.Open();
            SqlCommand scm = new SqlCommand(queryString, cnn);
            SqlDataReader reader = scm.ExecuteReader();

            while (reader.Read())
            {
                lp.Add(new PackageConfigRow(
                    (string)reader[0],
                    (string)reader[1],
                    (string)reader[2],
                    (int)reader[3],
                    (string)reader[4],
                    (string)reader[5],
                    (int)reader[6]
                    ));
            }
            reader.Close();
            cnn.Close();

            return lp;
        }
    }
}
