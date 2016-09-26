using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SGM.ECount.DataModel
{
    public partial class PartGroup
    {
        [DataMember]
        [XmlArray("Parts")]
        public List<Part> Parts
        {
            get
            {
                return this.GroupParts.Select(gp => gp.Part).ToList();
            }
        }
    }
}
