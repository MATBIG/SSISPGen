using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        public IDTSComponentMetaData100 AddComp_DerivedCol(string ComponentName,
                                                            IDTSOutput100 outCols
                                                            )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.DerivedColumn";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = ComponentName;
            Comp.Description = "Derajw kolumn";

            //  Parametrize

            Comp.OutputCollection[0].TruncationRowDisposition = DTSRowDisposition.RD_NotUsed;
            Comp.OutputCollection[0].ErrorRowDisposition = DTSRowDisposition.RD_NotUsed;

            //  Connect

            IDTSPath100 pth = dmp.PathCollection.New();
            pth.AttachPathAndPropagateNotifications(outCols, Comp.InputCollection[0]);

            //  Return

            return Comp;
        }

        public IDTSOutputColumn100 ModifyComp_DerivedCol_AddCol(IDTSComponentMetaData100 Comp,
                                                                    string DCName,
                                                                    string DCExpression,
                                                                    DataType DTDataType,
                                                                    int DCDTLength = 0,
                                                                    int DCDTPrecision = 0,
                                                                    int DCDTScale = 0,
                                                                    int DCDTCodePage = 0
                                                                    )
        {
            IDTSOutputColumn100 Col = Comp.OutputCollection[0].OutputColumnCollection.New();

            Col.Name = DCName;
            Col.SetDataTypeProperties(DTDataType, DCDTLength, DCDTPrecision, DCDTScale, DCDTCodePage);
            Col.ExternalMetadataColumnID = 0;
            Col.ErrorRowDisposition = DTSRowDisposition.RD_FailComponent;
            Col.TruncationRowDisposition = DTSRowDisposition.RD_FailComponent;

            IDTSCustomProperty100 propExp = Col.CustomPropertyCollection.New();
            propExp.Name = "Expression";
            propExp.Value = DCExpression;

            IDTSCustomProperty100 propFrExp = Col.CustomPropertyCollection.New();
            propFrExp.Name = "FriendlyExpression";
            propFrExp.Value = DCExpression;

            return Col;
        }
    }
}