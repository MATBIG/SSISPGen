using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using PckgGenLib.Enums;
using PckgGenMapperLib;
using PckgGenMapperLib.DTMappings;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        public string CreateTempTableSQL(string objName, string conManName)
        {
            ConnectionManager cm = prj.ConnectionManagerItems[conManName + ".conmgr"].ConnectionManager;

            string tmpName;
            string sqlcmd;

            if (objName.EndsWith("]"))
            {
                tmpName = objName.Remove(objName.Length - 1) + "_Temp]";
            }
            else
            {
                tmpName = objName + "_Temp";
            }

            sqlcmd = $"IF OBJECT_ID('{tmpName}') IS NOT NULL DROP TABLE {tmpName}; SELECT TOP 0 * INTO {tmpName} FROM {objName}";
            ExecSQL(sqlcmd, cm);

            return tmpName;
        }
        private string GetSQL(string tn, List<DTMInput> ld, CreateTableFlag createTableFlag, IMapper m)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DTMInput d in ld)
            {
                sb.Append("\n,\t");
                sb.Append(m.Map(d));
            }
            sb.Remove(0, 2);
            sb.Append("\n)\n;");

            switch (createTableFlag)
            {
                case CreateTableFlag.Create:
                    sb.Insert(0, $"CREATE TABLE {tn}\n(\n");
                    break;
                case CreateTableFlag.CreateIfNotExists:
                    sb.Insert(0, $"IF OBJECT_ID('{tn}') IS NOT NULL RETURN;\nCREATE TABLE {tn}\n(\n");
                    break;
                case CreateTableFlag.DropAndCreate:
                    sb.Insert(0, $"IF OBJECT_ID('{tn}') IS NOT NULL DROP TABLE {tn};\nCREATE TABLE {tn}\n(\n");
                    break;
            }

            return sb.ToString();
        }
        private void ExecSQL(string sqlcmd, ConnectionManager cm)
        {
            var lines = Regex.Split(cm.ConnectionString, ";")
                .Select(z => z.Trim())
                .Where(x => !x.StartsWith("Provider") && !x.StartsWith("Auto Translate"))
                .Where(x => x != "")
                .Select(x => $"{x};");

            string ConnStr = string.Join("", lines);

            SqlConnection cnn = new SqlConnection(ConnStr);
            cnn.Open();
            SqlCommand scm = new SqlCommand(sqlcmd, cnn);
            scm.ExecuteNonQuery();
            cnn.Close();
        }

    }
}