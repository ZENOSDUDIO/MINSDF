using System;
using System.Reflection;

namespace SGM.ECountJQ.UPG.BLL.DBBase
{
    public class MapColumnAttribute : Attribute
    {
        private string _DefaultValue;
        private string _Description;
        private string _Name;
        private int _Order;

        public MapColumnAttribute(string name)
        {
            this.Name = name;
        }

        public static MapColumnAttribute GetCustomAttribute(MemberInfo element)
        {
            return (Attribute.GetCustomAttribute(element, typeof(MapColumnAttribute)) as MapColumnAttribute);
        }

        public string DefaultValue
        {
            get
            {
                return this._DefaultValue;
            }
            set
            {
                this._DefaultValue = value;
            }
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public int Order
        {
            get
            {
                return this._Order;
            }
            set
            {
                this._Order = value;
            }
        }
    }
}
