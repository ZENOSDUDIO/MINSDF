using System.Xml;
using System.Xml.Serialization;

namespace SGM.ECountJQ.UPG.BLL
{
    [XmlRoot("StocktakeItem")]
    public class StocktakeItemSimple
    {
        [XmlElement]
        public long ItemID { get; set; }
        [XmlElement]
        public decimal? Line { get; set; }
        [XmlElement]
        public decimal? Machining { get; set; }
        [XmlElement]
        public decimal? Store { get; set; }
        [XmlElement]
        public string StartCSN { get; set; }
        [XmlElement]
        public string EndCSN { get; set; }
        [XmlElement]
        public decimal? Block { get; set; }
        [XmlElement]
        public decimal? Available { get; set; }
        [XmlElement]
        public decimal? QI { get; set; }
        [XmlElement]
        public int? BlockAdjust { get; set; }
        [XmlElement]
        public int? AvailableAdjust { get; set; }
        [XmlElement]
        public int? QIAdjust { get; set; }
        [XmlElement]
        public string FillinTime { get; set; }
        [XmlElement]
        public int? FillinBy { get; set; }
    }
}
