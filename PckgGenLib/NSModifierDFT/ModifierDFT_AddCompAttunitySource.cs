using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        public IDTSComponentMetaData100 AddComp_AttunitySource(string componentName,
                                                                    string conManName,
                                                                    string source
                                                                    )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Attunity.SSISOraSrc";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = componentName;
            Comp.Description = "Attunity Source Component";

            Inst.SetComponentProperty("AccessMode", 0);
            Inst.SetComponentProperty("TableName", source);

            //  GetConnectionManager

            ConnectionManager cm = prj.ConnectionManagerItems[conManName + ".conmgr"].ConnectionManager;

            ////  Parametrize

            Comp.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(cm);
            Comp.RuntimeConnectionCollection[0].ConnectionManagerID = cm.ID;

            Inst.AcquireConnections(null);  //  Establishes a connection to a connection manager
            Inst.ReinitializeMetaData();    //  Called to allow the component to repair problems with the IDTSComponentMetaData100 object that were identified by the component during the Validate() method.
            Inst.ReleaseConnections();      //  Frees the connections established by the component during AcquireConnections(Object).

            //  Return

            return Comp;
        }
    }
}