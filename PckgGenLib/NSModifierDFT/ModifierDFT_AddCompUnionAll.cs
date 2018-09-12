using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.Collections.Generic;

namespace PckgGenLib.NSModifierDFT
{

    public partial class ModifierDFT
    {
        public IDTSComponentMetaData100 AddComp_UnionAll(   string ComponentName                             )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.UnionAll";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = ComponentName;
            Comp.Description = "DodanyUA";

            //  Return

            return Comp;
        }

        public void ModifyComp_UnionAll_AddInput(  IDTSComponentMetaData100 Comp,
                                                   IDTSOutput100 outCols
                                                   )
        {
            int i = Comp.InputCollection.Count;

            IDTSPath100 pt = dmp.PathCollection.New();
            pt.AttachPathAndPropagateNotifications(outCols, Comp.InputCollection[i-1]);
        }
    }
}