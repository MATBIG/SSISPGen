using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenMapperLib.DTMappings
{
    public class DTMInput
    {
        public DTMInput(string Name,
                        string DataTypeName,
                        int CodePage = 0,
                        int Length = 0,
                        int Precision = 0,
                        int Scale = 0
                        )
        {
            this.Name = Name;
            this.DataTypeName = DataTypeName;
            this.CodePage = CodePage;
            this.Length = Length;
            this.Precision = Precision;
            this.Scale = Scale;
        }
        public string Name;
        public string DataTypeName;
        public int CodePage;
        public int Length;
        public int Precision;
        public int Scale;

        public override string ToString()
        {
            return string.Format("{0} - dt: {1} cp: {2}, len: {3}, pre: {4}, sca: {5}",
                Name,
                DataTypeName, 
                CodePage, 
                Length, 
                Precision, 
                Scale
                );
        }
    }
}
