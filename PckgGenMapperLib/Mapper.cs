using System.IO;
using System.Collections.Generic;
using System.Linq;
using PckgGenMapperLib.DTMappings;
using PckgGenMapperLib.EmbeddedResources;

using static System.Console;

namespace PckgGenMapperLib
{
    public class Mapper:IMapper
    {
        private DTMDeserializer xds = new DTMDeserializer();
        private EmbeddedResourceReader er = new EmbeddedResourceReader();
        public List<DataTypeMappingType> dtm;

        public Mapper(string sr = "SSIS10ToMSSQL.XML")
        {
            using (Stream str = er.ReadEmbeddedToStream(sr))
            {
                dtm = xds.DTMDeserialize(str);
            }
        }

        public string Map(DTMInput z)
        {
            string mapRslt;
            DataTypeMappingType dt = dtm.Where(x => x.SourceDataType.DataTypeName == z.DataTypeName)
                .FirstOrDefault();

            object o = dt.DestinationDataType.Item;
            switch (o)
            {
                case SimpleTypeSpecification ts:
                    mapRslt = GenerateSimple(z, ts);
                    break;
                case NumericTypeSpecification ts:
                    mapRslt = GenerateNumeric(z, ts);
                    break;
                case CharacterStringTypeSpecification ts:
                    mapRslt = GenerateCharacterString(z, ts);
                    break;
                case DataTypeWithLengthSpecification ts:
                    mapRslt = GenerateDataTypeWithLength(z, ts);
                    break;
                default:
                    mapRslt = "";
                    break;
            }

            return mapRslt;
        }

        private string GenerateSimple(DTMInput z, SimpleTypeSpecification o)
        {
            string sql = "[" + z.Name + "] " + o.DataTypeName.ToUpper();
            return sql;
        }

        private string GenerateNumeric(DTMInput z, NumericTypeSpecification o)
        {
            uint? p = null;
            uint? s = null;

            switch (o.Item)
            {
                case uint pr:
                    p = pr;
                    break;
                case NumericTypeSpecificationSkipPrecision pr:
                    p = null;
                    break;
                case NumericTypeSpecificationUseSourcePrecision pr:
                    p = (uint)z.Precision;
                    break;
            }
            switch (o.Item1)
            {
                case uint sc:
                    s = sc; 
                    break;
                case NumericTypeSpecificationSkipScale sc:
                    s = null;
                    break;
                case NumericTypeSpecificationUseSourceScale sc:
                    s = (uint)z.Scale;
                    break;
            }

            string param = null;
            string sql = null;

            if (p == null && s == null)
            { param = ""; }
            else if (p == null && s != null)
            { param = "(" + s.ToString() + ")"; }
            else if (p != null && s == null)
            { param = "(" + p.ToString() + ")"; }
            else
            { param = "(" + p.ToString() + "," + s.ToString() + ")"; }

            sql = "[" + z.Name + "] " + o.DataTypeName.ToUpper() + param;
            return sql;
        }

        private string GenerateCharacterString(DTMInput z, CharacterStringTypeSpecification o)
        {
            int? len = null;
            switch (o.Item)
            {
                case int l:
                    len = l;
                    break;
                case CharacterStringTypeSpecificationSkipLength l:
                    len = null;
                    break;
                case CharacterStringTypeSpecificationUseSourceLength l:
                    len = z.Length;
                    break;
            }

            string param = null;
            string sql = null;

            param = len == -1 ? "MAX" : len.ToString();

            sql = "[" + z.Name + "] " + o.DataTypeName.ToUpper() + "(" + param + ")";
            return sql;
        }

        private string GenerateDataTypeWithLength(DTMInput z, DataTypeWithLengthSpecification o)
        {
            string sql = "[" + z.Name + "] " + o.DataTypeName.ToUpper() + "(" + o.Length + ")";
            return sql;
        }
    }
}
