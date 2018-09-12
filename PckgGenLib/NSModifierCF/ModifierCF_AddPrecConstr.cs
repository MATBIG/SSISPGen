using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;


namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF
    {
        public PrecedenceConstraint Add_PrecConstr(
                                                    Executable ex1,
                                                    Executable ex2,
                                                    Sequence sqn = null,
                                                    DTSPrecedenceEvalOp EvalOp = DTSPrecedenceEvalOp.Constraint,
                                                    DTSExecResult Result = DTSExecResult.Success,
                                                    string Expression = "",
                                                    bool LogicalAnd = true
                                                    )
        {
            PrecedenceConstraint cstr = null;

            if (sqn == null) { cstr = p.PrecedenceConstraints.Add(ex1, ex2); }
            else { cstr = sqn.PrecedenceConstraints.Add(ex1, ex2); }

            cstr.EvalOp = EvalOp;
            cstr.Value = Result;
            cstr.Expression = Expression;
            cstr.LogicalAnd = LogicalAnd;

            return cstr;
        }
    }
}


//  Expression  1
//  Constraint  2
//  ExpressionAndConstraint 3
//  ExpressionOrConstraint  4

//  Success	    0
//  Failure	    1
//  Completion	2
//  Canceled	3	