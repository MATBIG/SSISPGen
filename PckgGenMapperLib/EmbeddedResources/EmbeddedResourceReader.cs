using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenMapperLib.EmbeddedResources
{
    class EmbeddedResourceReader
    {
        public Stream ReadEmbeddedToStream(string file)
        {
            return GetType().Assembly.GetManifestResourceStream("PckgGenMapperLib.EmbeddedResources." + file);
        }
    }
}
