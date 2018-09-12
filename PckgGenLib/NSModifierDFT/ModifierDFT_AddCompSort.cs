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
        public IDTSComponentMetaData100 AddComp_Sort(   string ComponentName,
                                                        IDTSOutput100 outCols,
                                                        Dictionary<string, bool> sortColumns
                                                        )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.Sort";

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = ComponentName;
            Comp.Description = "Dodany Sort";

            //  Connect

            IDTSPath100 pth = dmp.PathCollection.New();
            pth.AttachPathAndPropagateNotifications(outCols, Comp.InputCollection[0]);

            //  Parametrize

            IDTSInput100 lkpInput = Comp.InputCollection[0];
            IDTSInputColumnCollection100 lkpInputCols = lkpInput.InputColumnCollection;

            IDTSVirtualInput100 lkpVirtInput = lkpInput.GetVirtualInput();
            IDTSVirtualInputColumnCollection100 lkpVirtInputCols = lkpVirtInput.VirtualInputColumnCollection;

            //  Parametrize #2 - Join Columns

            int i = 1;

            foreach (IDTSVirtualInputColumn100 vColumn in lkpVirtInputCols)
            {
                IDTSInputColumn100 inputColumn = Inst.SetUsageType(lkpInput.ID, lkpVirtInput, vColumn.LineageID, DTSUsageType.UT_READONLY);

                foreach (var colKVP in sortColumns)
                {
                    if (vColumn.Name == colKVP.Key)
                    {
                        Inst.SetInputColumnProperty(lkpInput.ID, inputColumn.ID, "NewComparisonFlags", 0);
                        Inst.SetInputColumnProperty(lkpInput.ID, inputColumn.ID, "NewSortKeyPosition", colKVP.Value ? i: -i);
                        i++;
                    }
                }
            }

            //  Return

            return Comp;
        }
    }
}