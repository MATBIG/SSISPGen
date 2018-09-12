using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenLib.EmbeddedResources
{
    public class EmbeddedResourceReader
    {
        public Stream ReadEmbeddedToStream(string embFileName)
        {
            return GetType().Assembly.GetManifestResourceStream("PckgGenLib.EmbeddedResources." + embFileName);
        }
        public void SaveEmbeddedToFile(string filePath)
        {
            Stream str = GetType().Assembly.GetManifestResourceStream(@"PckgGenLib.EmbeddedResources.ProjectTemplate.ispac");

            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            for (int i = 0; i < str.Length; i++)
                fileStream.WriteByte((byte)str.ReadByte());
            fileStream.Close();
        }
    }
}
