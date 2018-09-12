using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace PckgGenLib.NSModifierDFT
{
    public partial class ModifierDFT
    {
        public IDTSComponentMetaData100 AddComp_MergeJoin(  string ComponentName,
                                                            IDTSOutput100 outColsL,
                                                            IDTSOutput100 outColsR,
                                                            int joinType = 2,
                                                            int keyColNum = -1
                                                            )
        {
            //  Create

            IDTSComponentMetaData100 Comp = dmp.ComponentMetaDataCollection.New();
            Comp.ComponentClassID = "Microsoft.MergeJoin";
            Comp.ValidateExternalMetadata = true;

            //  Instantiate

            CManagedComponentWrapper Inst = Comp.Instantiate();
            Inst.ProvideComponentProperties();

            Comp.Name = ComponentName;
            Comp.Description = "MJ!";

            //  Parametrize #1

            //  0:  FULL
            //  1:  LEFT
            //  2:  INNER

            Comp.CustomPropertyCollection["JoinType"].Value = joinType;
            Comp.CustomPropertyCollection["TreatNullsAsEqual"].Value = true;

            IDTSOutput100 outt = Comp.OutputCollection[0];

            //  Create Paths

            IDTSPath100 pthLeft = dmp.PathCollection.New();
            pthLeft.AttachPathAndPropagateNotifications(outColsL, Comp.InputCollection[0]);

            IDTSPath100 pthRight = dmp.PathCollection.New();
            pthRight.AttachPathAndPropagateNotifications(outColsR, Comp.InputCollection[1]);

            //  sprawdzić czy portrzebne i zobaczyć co robi

            Comp.InputCollection[0].ExternalMetadataColumnCollection.IsUsed = false;
            Comp.InputCollection[0].HasSideEffects = false;
            Comp.InputCollection[1].ExternalMetadataColumnCollection.IsUsed = false;
            Comp.InputCollection[1].HasSideEffects = false;

            //  Add Columns
            //  -------------------------------------------------------------------- 

            int maxSortKeyVal = 0;

            IDTSInput100 mInputL = Comp.InputCollection[0];
            IDTSVirtualInput100 vMInputL = mInputL.GetVirtualInput();

            foreach (IDTSVirtualInputColumn100 vCol in vMInputL.VirtualInputColumnCollection)
            {
                Inst.SetUsageType(mInputL.ID, vMInputL, vCol.LineageID, DTSUsageType.UT_READONLY);
                if (vCol.SortKeyPosition > maxSortKeyVal)
                {
                    maxSortKeyVal = vCol.SortKeyPosition;
                }

            }

            IDTSInput100 mInputR = Comp.InputCollection[1];
            IDTSVirtualInput100 vMInputR = mInputR.GetVirtualInput();

            foreach (IDTSVirtualInputColumn100 vCol in vMInputR.VirtualInputColumnCollection)
            {
                Inst.SetUsageType(mInputR.ID, vMInputR, vCol.LineageID, DTSUsageType.UT_READONLY);
            }

            Comp.CustomPropertyCollection["NumKeyColumns"].Value = keyColNum == -1 ? maxSortKeyVal : keyColNum;

            //  Column Names
            //  to możnaby podrasować:
            //  kolumny JOINa tylko raz z nazwą z pierwszego
            //  kolumny pozostałe suffiksować tylko jak są dwie takie same po lewej i prawej
            //  --------------------------------------------------------------------

            foreach (IDTSOutputColumn100 oc in outt.OutputColumnCollection)
            {
                string suffix = "";

                foreach (IDTSInputColumn100 icl in mInputL.InputColumnCollection)
                {
                    if (oc.CustomPropertyCollection["InputColumnID"].Value == icl.ID)
                    {
                        suffix = "L_";
                    }
                }

                foreach (IDTSInputColumn100 icr in mInputR.InputColumnCollection)
                {
                    if (oc.CustomPropertyCollection["InputColumnID"].Value == icr.ID)
                    {
                        suffix = "R_";
                    }
                }

                oc.Name = suffix + oc.Name;
            }

            Inst.AcquireConnections(null);
            Inst.ReinitializeMetaData();
            Inst.ReleaseConnections();

            //  Return

            return Comp;
        }
    }
}