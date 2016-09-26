using System;
using System.Reflection;

namespace SGM.ECountJQ.UPG.BLL.DBBase
{
    public class MapTableAttribute : Attribute
    {
        private string _ConnName;
        private string _Description;
        private string _Name;

        public MapTableAttribute(string name)
        {
            this.Name = name;
        }

        public static MapTableAttribute GetCustomAttribute(MemberInfo element)
        {
            return (Attribute.GetCustomAttribute(element, typeof(MapTableAttribute)) as MapTableAttribute);
        }

        public string ConnName
        {
            get
            {
                return this._ConnName;
            }
            set
            {
                this._ConnName = value;
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
    }
}
