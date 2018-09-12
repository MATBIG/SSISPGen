using PckgGenMapperLib.DTMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenMapperLib
{
    public interface IMapper
    {
        string Map(DTMInput z);
    }
}
