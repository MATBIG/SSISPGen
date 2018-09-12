using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        public IDTSComponentMetaData100 AddComp_Multicast(  string ComponentName,
                                                            IDTSOutput100 outCols
                                                            )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.Multicast";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = ComponentName;
            Comp.Description = "Dodany Sort";

            //  Connect

            IDTSPath100 pth = dmp.PathCollection.New();
            pth.AttachPathAndPropagateNotifications(outCols, Comp.InputCollection[0]);

            //  Return

            return Comp;
        }
    }
}