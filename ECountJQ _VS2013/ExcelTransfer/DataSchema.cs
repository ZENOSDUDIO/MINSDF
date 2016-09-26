using System;
using System.Xml;
using System.Xml.Serialization;

namespace ECount.Infrustructure.Data.Integration
{
    [XmlRoot("dataschema", IsNullable = false)]
    public class DataSchema
    {
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr;

        [XmlArray("fields")]
        [XmlArrayItem("field", Type = typeof(DataSchemaField), IsNullable = false)]
        public DataSchemaField[] Fields;

        [XmlArray("rules")]
        [XmlArrayItem("rule", Type = typeof(DataSchemaRule), IsNullable = true)]
        public DataSchemaRule[] Rules;

        [XmlAttribute("badrecord")]
        public string TransferMode;

        private bool _sorted;

        public DataSchemaField this[string name]
        {
            get { return GetField(name); }
        }

        public DataSchemaField GetField(string fieldName)
        {
            DataSchemaField field;
            // find by name
            field = Array.Find(Fields, delegate(DataSchemaField item)
            {
                if (string.IsNullOrEmpty(item.Name)) return false;
                else
                    return item.Name.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase);
            });
            // find by source
            if (field == null)
                field = Array.Find(Fields, delegate(DataSchemaField item)
                {
                    if (string.IsNullOrEmpty(item.Source)) return false;
                    else
                        return item.Source.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase);
                });

            // find by destination
            if (field == null)
                field = Array.Find(Fields, delegate(DataSchemaField item)
                {
                    if (string.IsNullOrEmpty(item.Destination)) return false;
                    else
                        return item.Destination.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase);
                });
            return field;
        }

        public string GetAnyAttributeValue(string name)
        {
            if (string.IsNullOrEmpty(name) || AnyAttr == null)
                return null;
            return DataSchemaField.GetAnyAttributeValue(AnyAttr, name);
        }

        public bool SetAnyAttributeValue(string name, string value)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return false;
            AnyAttr = DataSchemaField.SetAnyAttributeValue(AnyAttr, name, value);
            return true;
        }

        public void IndexRules()
        {
            if (!_sorted && Rules != null)
            {
                Array.Sort(Rules, delegate(DataSchemaRule x, DataSchemaRule y) { return x.Index.CompareTo(y.Index); });
                _sorted = true;
            }
        }
    }
}