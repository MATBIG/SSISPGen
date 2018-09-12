using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;


namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF
    {
        public Variable Add_Variable(   string vName,
                                        object vValue,
                                        bool evalAsExp = false,
                                        string expCode = ""
                                        )
        {
            Variable nv = p.Variables.Add(vName, false, "User", vValue);

            if (evalAsExp)
            {
                nv.EvaluateAsExpression = true;
                nv.Expression = expCode;
            }

            return nv;
        }
    }
}