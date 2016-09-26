using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace SGM.ECountJQ.UPG.BLL.DBBase
{
    internal class FieldItem
    {
        private MapColumnAttribute _Column;
        private DataObjectFieldAttribute _DataObjectField;
        private static IDictionary<Type, IList<FieldItem>> _DataObjectFields = new Dictionary<Type, IList<FieldItem>>();
        private static IDictionary<Type, IList<FieldItem>> _Fields = new Dictionary<Type, IList<FieldItem>>();
        private PropertyInfo _Property;
        private static IDictionary<Type, MapTableAttribute> _Tables = new Dictionary<Type, MapTableAttribute>();

        public static string GetConnName(Type type)
        {
            MapTableAttribute table = GetTable(type);
            if ((table != null) && !string.IsNullOrEmpty(table.ConnName))
            {
                return table.ConnName;
            }
            return null;
        }

        public static IList<FieldItem> GetDataObjectFields(Type type)
        {
            if (_DataObjectFields.ContainsKey(type))
            {
                return _DataObjectFields[type];
            }
            lock (_DataObjectFields)
            {
                if (_DataObjectFields.ContainsKey(type))
                {
                    return _DataObjectFields[type];
                }
                List<FieldItem> fields = GetFields(type) as List<FieldItem>;
                fields = fields.FindAll(item => item.DataObjectField != null);
                _DataObjectFields.Add(type, fields);
                return fields;
            }
        }

        public static IList<FieldItem> GetFields(Type type)
        {
            if (_Fields.ContainsKey(type))
            {
                return _Fields[type];
            }
            lock (_Fields)
            {
                if (_Fields.ContainsKey(type))
                {
                    return _Fields[type];
                }
                PropertyInfo[] properties = type.GetProperties();
                List<FieldItem> list = new List<FieldItem>();
                foreach (PropertyInfo info in properties)
                {
                    FieldItem item = new FieldItem
                    {
                        Property = info,
                        DataObjectField = Attribute.GetCustomAttribute(info, typeof(DataObjectFieldAttribute)) as DataObjectFieldAttribute,
                        Column = MapColumnAttribute.GetCustomAttribute(info)
                    };
                    list.Add(item);
                }
                _Fields.Add(type, list);
                return list;
            }
        }

        public static FieldItem GetIdentityField(Type type)
        {
            List<FieldItem> fields = GetFields(type) as List<FieldItem>;
            if (fields != null && fields.Count != 0)
            {
                foreach (FieldItem item in fields)
                {
                    if (item.DataObjectField != null && item.DataObjectField.IsIdentity)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public static MapTableAttribute GetTable(Type type)
        {
            if (_Tables.ContainsKey(type))
            {
                return _Tables[type];
            }
            lock (_Tables)
            {
                if (_Tables.ContainsKey(type))
                {
                    return _Tables[type];
                }
                MapTableAttribute customAttribute = MapTableAttribute.GetCustomAttribute(type);
                _Tables.Add(type, customAttribute);
                return customAttribute;
            }
        }

        public static string GetTableName(Type type)
        {
            MapTableAttribute table = GetTable(type);
            if ((table != null) && !string.IsNullOrEmpty(table.Name))
            {
                return table.Name;
            }
            return type.Name;
        }

        public MapColumnAttribute Column
        {
            get
            {
                return this._Column;
            }
            set
            {
                this._Column = value;
            }
        }

        public string ColumnName
        {
            get
            {
                if ((this.Column != null) && !string.IsNullOrEmpty(this.Column.Name))
                {
                    return this.Column.Name;
                }
                return this.Property.Name;
            }
        }

        public DataObjectFieldAttribute DataObjectField
        {
            get
            {
                return this._DataObjectField;
            }
            set
            {
                this._DataObjectField = value;
            }
        }

        public string Name
        {
            get
            {
                if (this.Property != null)
                {
                    return this.Property.Name;
                }
                return null;
            }
        }

        public PropertyInfo Property
        {
            get
            {
                return this._Property;
            }
            set
            {
                this._Property = value;
            }
        }
    }
}
