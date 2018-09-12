using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenLib.NSProjectGenerator
{
    public struct ConManConfigRow
    {
        public ConManConfigRow(
            string Name,
            string CMType,
            string ConnectionString
            )
        {
            this.Name = Name;
            this.CMType = CMType;
            this.ConnectionString = ConnectionString;
        }
        public string Name;
        public string CMType;
        public string ConnectionString;
    }
}
