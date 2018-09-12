using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgGenMapperLib.DTMappings
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd", IsNullable = false)]
    public partial class DataTypeMappings
    {

        private DataTypeMappingType[] dataTypeMappingField;

        private string sourceTypeField;

        private string minSourceVersionField;

        private string maxSourceVersionField;

        private string destinationTypeField;

        private string minDestinationVersionField;

        private string maxDestinationVersionField;

        public DataTypeMappings()
        {
            this.minSourceVersionField = "*";
            this.maxSourceVersionField = "*";
            this.minDestinationVersionField = "*";
            this.maxDestinationVersionField = "*";
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataTypeMapping")]
        public DataTypeMappingType[] DataTypeMapping
        {
            get
            {
                return this.dataTypeMappingField;
            }
            set
            {
                this.dataTypeMappingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SourceType
        {
            get
            {
                return this.sourceTypeField;
            }
            set
            {
                this.sourceTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("*")]
        public string MinSourceVersion
        {
            get
            {
                return this.minSourceVersionField;
            }
            set
            {
                this.minSourceVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("*")]
        public string MaxSourceVersion
        {
            get
            {
                return this.maxSourceVersionField;
            }
            set
            {
                this.maxSourceVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DestinationType
        {
            get
            {
                return this.destinationTypeField;
            }
            set
            {
                this.destinationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("*")]
        public string MinDestinationVersion
        {
            get
            {
                return this.minDestinationVersionField;
            }
            set
            {
                this.minDestinationVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("*")]
        public string MaxDestinationVersion
        {
            get
            {
                return this.maxDestinationVersionField;
            }
            set
            {
                this.maxDestinationVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class DataTypeMappingType
    {

        private SimpleTypeSpecification sourceDataTypeField;

        private DestinationDataTypeSpecification destinationDataTypeField;

        /// <remarks/>
        public SimpleTypeSpecification SourceDataType
        {
            get
            {
                return this.sourceDataTypeField;
            }
            set
            {
                this.sourceDataTypeField = value;
            }
        }

        /// <remarks/>
        public DestinationDataTypeSpecification DestinationDataType
        {
            get
            {
                return this.destinationDataTypeField;
            }
            set
            {
                this.destinationDataTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class SimpleTypeSpecification
    {

        private string dataTypeNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
        public string DataTypeName
        {
            get
            {
                return this.dataTypeNameField;
            }
            set
            {
                this.dataTypeNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class NumericTypeSpecification
    {

        private string dataTypeNameField;

        private object itemField;

        private object item1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
        public string DataTypeName
        {
            get
            {
                return this.dataTypeNameField;
            }
            set
            {
                this.dataTypeNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Precision", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("SkipPrecision", typeof(NumericTypeSpecificationSkipPrecision))]
        [System.Xml.Serialization.XmlElementAttribute("UseSourcePrecision", typeof(NumericTypeSpecificationUseSourcePrecision))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Scale", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("SkipScale", typeof(NumericTypeSpecificationSkipScale))]
        [System.Xml.Serialization.XmlElementAttribute("UseSourceScale", typeof(NumericTypeSpecificationUseSourceScale))]
        public object Item1
        {
            get
            {
                return this.item1Field;
            }
            set
            {
                this.item1Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class NumericTypeSpecificationSkipPrecision
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class NumericTypeSpecificationUseSourcePrecision
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class NumericTypeSpecificationSkipScale
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class NumericTypeSpecificationUseSourceScale
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class DataTypeWithLengthSpecification
    {

        private string dataTypeNameField;

        private int lengthField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
        public string DataTypeName
        {
            get
            {
                return this.dataTypeNameField;
            }
            set
            {
                this.dataTypeNameField = value;
            }
        }

        /// <remarks/>
        public int Length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class CharacterStringTypeSpecification
    {

        private string dataTypeNameField;

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
        public string DataTypeName
        {
            get
            {
                return this.dataTypeNameField;
            }
            set
            {
                this.dataTypeNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Length", typeof(int))]
        [System.Xml.Serialization.XmlElementAttribute("SkipLength", typeof(CharacterStringTypeSpecificationSkipLength))]
        [System.Xml.Serialization.XmlElementAttribute("UseSourceLength", typeof(CharacterStringTypeSpecificationUseSourceLength))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class CharacterStringTypeSpecificationSkipLength
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class CharacterStringTypeSpecificationUseSourceLength
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.microsoft.com/SqlServer/Dts/DataTypeMapping.xsd")]
    public partial class DestinationDataTypeSpecification
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BitStringType", typeof(DataTypeWithLengthSpecification))]
        [System.Xml.Serialization.XmlElementAttribute("CharacterStringType", typeof(CharacterStringTypeSpecification))]
        [System.Xml.Serialization.XmlElementAttribute("NumericType", typeof(NumericTypeSpecification))]
        [System.Xml.Serialization.XmlElementAttribute("SimpleType", typeof(SimpleTypeSpecification))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
}
