using System;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        public IDTSComponentMetaData100 AddComp_OleDBSource(    string componentName,
                                                                string conManName,
                                                                string source,
                                                                int accessMode = 0
                                                                )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.OLEDBSource";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = componentName;
            Comp.Description = "bloczek OLEDBSource";

            //  GetConnectionManager

            ConnectionManager cm = prj.ConnectionManagerItems[conManName + ".conmgr"].ConnectionManager;

            //  Parametrize

            Comp.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(cm);
            Comp.RuntimeConnectionCollection[0].ConnectionManagerID = cm.ID;

            //  AccessMode: 0, OpenRowset: [dbo].[DimAccount]
            //  AccessMode: 1, OpenRowsetVariable: User::VarTName
            //  AccessMode: 2, SqlCommand: SELECT* FROM DimAccount
            //  AccessMode: 3, SqlCommandVariable: User::VarSQLcmd

            Inst.SetComponentProperty("AccessMode", accessMode);
            switch (accessMode)
            {
                case 0:
                    Inst.SetComponentProperty("OpenRowset", source);
                    break;
                case 1:
                    Inst.SetComponentProperty("OpenRowsetVariable", source);
                    break;
                case 2:
                    Inst.SetComponentProperty("SqlCommand", source);
                    break;
                case 3:
                    Inst.SetComponentProperty("SqlCommandVariable", source);
                    break;
            }

            Inst.AcquireConnections(null);  //  Establishes a connection to a connection manager
            Inst.ReinitializeMetaData();    //  Called to allow the component to repair problems with the IDTSComponentMetaData100 object that were identified by the component during the Validate() method.
            Inst.ReleaseConnections();      //  Frees the connections established by the component during AcquireConnections(Object).

            //  Return

            return Comp;
        }
    }
}