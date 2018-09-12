using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenLib.NSProjectGenerator
{
    public delegate void ModifyPackageFun(Project pj, Package pi, PackageConfigRow pstr);
}
