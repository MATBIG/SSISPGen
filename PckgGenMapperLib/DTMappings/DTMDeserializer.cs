using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PckgGenMapperLib.DTMappings
{
    class DTMDeserializer
    {
        public List<DataTypeMappingType> DTMDeserialize(Stream str)
        {
            DataTypeMappings dtm = new DataTypeMappings();
            XmlSerializer xs = new XmlSerializer(dtm.GetType());
            XmlReader reader = XmlReader.Create(str);

            dtm = (DataTypeMappings)xs.Deserialize(reader);
            List<DataTypeMappingType> dtl = dtm.DataTypeMapping.ToList();

            return dtl;
        }
    }
}
