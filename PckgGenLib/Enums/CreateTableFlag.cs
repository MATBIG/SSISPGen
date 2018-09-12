using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenLib.Enums
{
    public enum CreateTableFlag
    {
        Create = 0,
        DropAndCreate = 1,
        CreateIfNotExists = 2,
        NoAction = 3
    }
}
