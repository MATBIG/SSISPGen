using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        //  https://blogs.msdn.microsoft.com/mattm/2008/12/30/api-sample-conditional-split/

        public IDTSComponentMetaData100 AddComp_CondSplit(string ComponentName,
                                                            IDTSOutput100 outCols
                                                            )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.ConditionalSplit";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = ComponentName;
            Comp.Description = "Dodany ConditionalSplit";

            //  Connect

            IDTSPath100 pth = dmp.PathCollection.New();
            pth.AttachPathAndPropagateNotifications(outCols, Comp.InputCollection[0]);

            //  Return

            return Comp;
        }
        public IDTSOutput100 ModifyComp_CondSplit_AddOut(   IDTSComponentMetaData100 Comp,
                                                            string OutName,
                                                            int OutEvalOrder,
                                                            string OutExpression,
                                                            string OutDesc = "SampleDescription"
                                                            )
        {
            CManagedComponentWrapper Inst = Comp.Instantiate();

            // We need to set a column's usage type before we can use it in an expression.
            // The code here will make all of the input columns available, but we could also
            // restrict it to just the columns that we need in the conditional split expression(s).

            IDTSInput100 splitInput = Comp.InputCollection[0];
            IDTSVirtualInput100 splitVInput = splitInput.GetVirtualInput();

            IDTSInputColumnCollection100 splitInputCols = splitInput.InputColumnCollection;
            IDTSVirtualInputColumnCollection100 splitVInputCols = splitVInput.VirtualInputColumnCollection;

            foreach (IDTSVirtualInputColumn100 vCol in splitVInputCols)
            {
                Inst.SetUsageType(splitInput.ID, splitVInput, vCol.LineageID, DTSUsageType.UT_READONLY);
            }

            //  Parametrize

            IDTSOutput100 splitOutput = Inst.InsertOutput(DTSInsertPlacement.IP_BEFORE, Comp.OutputCollection[0].ID);
            splitOutput.Name = OutName;
            splitOutput.Description = OutDesc;
            splitOutput.IsErrorOut = false;

            // Note: You will get an exception if you try to set these properties on the Default Output.
            Inst.SetOutputProperty(splitOutput.ID, "EvaluationOrder", OutEvalOrder);
            Inst.SetOutputProperty(splitOutput.ID, "FriendlyExpression", OutExpression);

            Inst.AcquireConnections(null);
            Inst.ReinitializeMetaData();
            Inst.ReleaseConnections();

            return splitOutput;
        }

    }
}