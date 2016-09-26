using System;
using System.ComponentModel;
using SGM.ECountJQ.UPG.BLL.DBBase;

namespace SGM.ECountJQ.UPG.BLL
{
    [Serializable]
    [MapTable("StoreLocation", ConnName = Const.ConnName)]
    public class StoreLocation : Entity<StoreLocation>
    {
        [DataObjectField(true, true, false)]
        [MapColumn("LocationID")]
        public int LocationID { get; set; }

        [DataObjectField(false)]
        [MapColumn("LocationName")]
        public string LocationName { get; set; }

        [DataObjectField(false)]
        [MapColumn("TypeID")]
        public int TypeID { get; set; }

        [DataObjectField(false)]
        [MapColumn("AvailableIncluded")]
        public bool AvailableIncluded { get; set; }

        [DataObjectField(false)]
        [MapColumn("QIIncluded")]
        public bool QIIncluded { get; set; }

        [DataObjectField(false)]
        [MapColumn("BlockIncluded")]
        public bool BlockIncluded { get; set; }

        [DataObjectField(false)]
        [MapColumn("LogisticsSysSLOC")]
        public string LogisticsSysSLOC { get; set; }

        [DataObjectField(false)]
        [MapColumn("Available")]
        public bool Available { get; set; }

        [DataObjectField(false)]
        [MapColumn("PlantID")]
        public int PlantID { get; set; }
    }
}
