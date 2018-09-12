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

        public IDTSComponentMetaData100 AddComp_OleDBCommand(   string componentName,
                                                                string conManName,
                                                                IDTSOutput100 outCols,
                                                                string sqlCmd,
                                                                Dictionary<string, string> paramMapping
                                                                )
        {
            //
            //  --------------------------------------------------------------------------------------------------------

            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.OLEDBCommand";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = componentName;
            Comp.Description = "bloczek OLEDBSource";

            //  Connect

            IDTSPath100 pth = dmp.PathCollection.New();
            pth.AttachPathAndPropagateNotifications(outCols, Comp.InputCollection[0]);

            //  GetConnectionManager

            ConnectionManager cm = prj.ConnectionManagerItems[conManName + ".conmgr"].ConnectionManager;

            //  Parametrize #1

            Comp.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(cm);
            Comp.RuntimeConnectionCollection[0].ConnectionManagerID = cm.ID;


            Inst.SetComponentProperty("SqlCommand", sqlCmd);

            Inst.AcquireConnections(null);  //  Establishes a connection to a connection manager
            Inst.ReinitializeMetaData();    //  Called to allow the component to repair problems with the IDTSComponentMetaData100 object that were identified by the component during the Validate() method.
            Inst.ReleaseConnections();      //  Frees the connections established by the component during AcquireConnections(Object).

            //  Parametrize #2

            IDTSInput100 input = Comp.InputCollection[0];
            IDTSVirtualInput100 vInput = input.GetVirtualInput();

            IDTSVirtualInputColumnCollection100 sourceColumns = vInput.VirtualInputColumnCollection;

            foreach (KeyValuePair<string, string> kvp in paramMapping)
            {
                IDTSVirtualInputColumn100 vColumn = vInput.VirtualInputColumnCollection[kvp.Value];
                Inst.SetUsageType(input.ID, vInput, vColumn.LineageID, DTSUsageType.UT_READONLY);

                IDTSExternalMetadataColumn100 exColumn = Comp.InputCollection[0].ExternalMetadataColumnCollection[kvp.Key];
                IDTSInputColumn100 inColumn = Comp.InputCollection[0].InputColumnCollection[kvp.Value];
                Inst.MapInputColumn(Comp.InputCollection[0].ID, inColumn.ID, exColumn.ID);
            }

            return Comp;
        }
    }
}