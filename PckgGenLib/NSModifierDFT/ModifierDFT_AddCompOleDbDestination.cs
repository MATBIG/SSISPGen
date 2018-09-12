using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using PckgGenLib.Enums;
using PckgGenMapperLib;
using PckgGenMapperLib.DTMappings;
using System.Collections.Generic;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        public IDTSComponentMetaData100 AddComp_OleDBDestination(   string componentName,
                                                                    string destObjectName,
                                                                    string conManName,
                                                                    IDTSOutput100 outCols,
                                                                    CreateTableFlag createTableFlag = CreateTableFlag.Create
                                                                    )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.OLEDBDestination";
            Comp.ValidateExternalMetadata = true;

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = componentName;
            Comp.Description = "Destinejszyn!";

            //  ConnManager

            ConnectionManager cm = prj.ConnectionManagerItems[conManName + ".conmgr"].ConnectionManager;
            Comp.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(cm);
            Comp.RuntimeConnectionCollection[0].ConnectionManagerID = cm.ID;

            //  Connect Tasks

            IDTSPath100 pth = dmp.PathCollection.New();
            pth.AttachPathAndPropagateNotifications(outCols, Comp.InputCollection[0]);

            if(createTableFlag != CreateTableFlag.NoAction)
            {
                List<DTMInput> inputColList = new List<DTMInput>();
                IDTSVirtualInput100 v = Comp.InputCollection[0].GetVirtualInput();
                foreach (IDTSVirtualInputColumn100 z in v.VirtualInputColumnCollection)
                {
                    inputColList.Add(new DTMInput
                                (
                                  z.Name
                                , z.DataType.ToString()
                                , z.CodePage
                                , z.Length
                                , z.Precision
                                , z.Scale
                                )
                            );
                }

                string sqlcmd;

                sqlcmd = GetSQL(destObjectName, inputColList, createTableFlag, new Mapper());
                ExecSQL(sqlcmd, cm);
            }

            //  Set Destination

            Inst.SetComponentProperty("AccessMode", 3);
            Inst.SetComponentProperty("OpenRowset", destObjectName);

            //  Get Metadata

            Inst.AcquireConnections(null);
            Inst.ReinitializeMetaData();
            Inst.ReleaseConnections();

            //  Match Output->Input

            IDTSInput100 input = Comp.InputCollection[0];
            IDTSVirtualInput100 vInput = input.GetVirtualInput();

            foreach (IDTSVirtualInputColumn100 vColumn in vInput.VirtualInputColumnCollection)
            {
                Inst.SetUsageType(input.ID, vInput, vColumn.LineageID, DTSUsageType.UT_READONLY);
            }
            foreach (IDTSInputColumn100 inColumn in Comp.InputCollection[0].InputColumnCollection)
            {
                foreach(IDTSExternalMetadataColumn100 exColumn in Comp.InputCollection[0].ExternalMetadataColumnCollection)
                {
                    if(exColumn.Name == inColumn.Name)
                    {
                        Inst.MapInputColumn(Comp.InputCollection[0].ID, inColumn.ID, exColumn.ID);
                    }
                }
            }

            //  Return
            Inst.AcquireConnections(null);
            Inst.ReinitializeMetaData();
            Inst.ReleaseConnections();

            return Comp;
        }
    }
}