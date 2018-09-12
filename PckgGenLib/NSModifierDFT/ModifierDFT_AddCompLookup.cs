using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using System.Collections.Generic;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        //  todo:
        //  CacheMode

        //  https://blogs.msdn.microsoft.com/mattm/2009/01/02/api-sample-lookup-transform/

        public IDTSComponentMetaData100 AddComp_Lookup( string componentName,
                                                        string conManName,
                                                        string sqlComm,
                                                        IDTSOutput100 outCols,
                                                        List<string> joinColumns,
                                                        Dictionary<string, string> newColumns,
                                                        DTSRowDisposition rowDisposition = DTSRowDisposition.RD_IgnoreFailure,
                                                        int redirectRowsToNoMatchOutput = 0
                                                        )
        {

            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.Lookup";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = componentName;
            Comp.Description = "Dodany Lookup";

            //  Connect

            IDTSPath100 pth = dmp.PathCollection.New();
            pth.AttachPathAndPropagateNotifications(outCols, Comp.InputCollection[0]);

            //  GetConnectionManager

            ConnectionManager cm = prj.ConnectionManagerItems[conManName + ".conmgr"].ConnectionManager;

            Comp.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(cm);
            Comp.RuntimeConnectionCollection[0].ConnectionManagerID = cm.ID;

            //  Parametrize #1

            IDTSOutput100 lmo = Comp.OutputCollection["Lookup Match Output"];
            lmo.ErrorRowDisposition = rowDisposition;

            //  Cache Type:
            //  0   =   Full
            //  1   =   Partial
            //  2   =   None
            Inst.SetComponentProperty("CacheType", 0);

            //  0   =   FailComponent, IgnoreFailure, RedirectRowsToErrorOutput  
            //  1   =   RedirectRowsToNoMatchOutput
            Inst.SetComponentProperty("NoMatchBehavior", redirectRowsToNoMatchOutput); 
            Inst.SetComponentProperty("SqlCommand", sqlComm);

            //  MetaData

            Inst.AcquireConnections(null);
            Inst.ReinitializeMetaData();
            Inst.ReleaseConnections();

            //  Parametrize #2 - Join Columns

            IDTSInput100 lkpInput = Comp.InputCollection[0];
            IDTSInputColumnCollection100 lkpInputCols = lkpInput.InputColumnCollection;
            IDTSVirtualInput100 lkpVirtInput = lkpInput.GetVirtualInput();
            IDTSVirtualInputColumnCollection100 lkpVirtInputCols = lkpVirtInput.VirtualInputColumnCollection;

            foreach (string columnName in joinColumns)
            {
                IDTSVirtualInputColumn100 vColumn = lkpVirtInputCols[columnName];
                IDTSInputColumn100 inputColumn = Inst.SetUsageType(lkpInput.ID, lkpVirtInput, vColumn.LineageID, DTSUsageType.UT_READONLY);
                Inst.SetInputColumnProperty(lkpInput.ID, inputColumn.ID, "JoinToReferenceColumn", columnName);
            }

            //  Parametrize #3 - New Lookup Columns

            IDTSOutput100 lookupMatchOutput = Comp.OutputCollection["Lookup Match Output"];
            foreach (string srcCol in newColumns.Keys)
            {
                string newColumnName = newColumns[srcCol];
                string description = string.Format("Copy of {0}", srcCol);

                IDTSOutputColumn100 outputColumn = Inst.InsertOutputColumnAt(lookupMatchOutput.ID, 0, newColumnName, description);
                Inst.SetOutputColumnProperty(lookupMatchOutput.ID, outputColumn.ID, "CopyFromReferenceColumn", srcCol);
            }

            //  Return

            return Comp;
        }
    }
}

//  Replace Cols CODE
//  Overwrite the existing FirstName column value with the one returned by the Lookup.
//  To do this, we need to flag the column as READWRITE, and set the CopyFromReferenceColumn property on the input
//  var overwriteColumns = new string[] { "FirstName" };
//  foreach (string columnName in overwriteColumns)
//  {
//      IDTSVirtualInputColumn100 virtualColumn = lookupVirtualInputColumns[columnName];
//      IDTSInputColumn100 inputColumn = lookupWrapper.SetUsageType(lookupInput.ID, lookupVirtualInput, virtualColumn.LineageID, DTSUsageType.UT_READWRITE);
//      lookupWrapper.SetInputColumnProperty(lookupInput.ID, inputColumn.ID, "CopyFromReferenceColumn", columnName);
//  }