using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenLib.NSProjectGenerator
{
    public struct ProjectParamConfigRow
    {
        public ProjectParamConfigRow(
            string Name,
            TypeCode Type,
            string Description,
            object Value
            )
        {
            this.Name = Name;
            this.Type = Type;
            this.Description = Description;
            this.Value = Value;
        }
        public string Name;
        public TypeCode Type;
        public string Description;
        public object Value;
    }
}
