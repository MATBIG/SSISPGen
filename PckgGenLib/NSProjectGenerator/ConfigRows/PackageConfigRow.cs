using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenLib.NSProjectGenerator
{
    public struct PackageConfigRow
    {
        public PackageConfigRow(
            string Name,
            string SrcType,
            string SrcCode,
            int DesTabCreateFlag,
            string DesTable,
            string MasterPackage,
            int SeqOrder
            )
        {
            this.Name = Name;
            this.SrcType = SrcType;
            this.SrcCode = SrcCode;
            this.DesTabCreateFlag = DesTabCreateFlag;
            this.DesTable = DesTable;
            this.MasterPackage = MasterPackage;
            this.SeqOrder = SeqOrder;
        }

        public string Name;
        public string SrcType;
        public string SrcCode;
        public int DesTabCreateFlag;
        public string DesTable;
        public string MasterPackage;
        public int SeqOrder;
    }
}
