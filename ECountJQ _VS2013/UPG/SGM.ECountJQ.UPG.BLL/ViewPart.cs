using System;
using System.ComponentModel;
using SGM.ECountJQ.UPG.BLL.DBBase;
using System.Collections;
using System.Collections.Generic;

namespace SGM.ECountJQ.UPG.BLL
{
    [DataObject]
    [MapTable("View_Part", ConnName = Const.ConnName)]
    public class ViewPart : Entity<ViewPart>
    {
        [DataObjectField(false)]
        [MapColumn("PartID")]
        public int PartID { get; set; }

        [DataObjectField(false)]
        [MapColumn("PartCode")]
        public string PartCode { get; set; }

        [DataObjectField(false)]
        [MapColumn("PlantID")]
        public int PlantID { get; set; }

        [DataObjectField(false)]
        [MapColumn("PlantCode")]
        public int PlantCode { get; set; }

        [DataObjectField(false)]
        [MapColumn("DUNS")]
        public string DUNS { get; set; }

        public static List<ViewPart> FindAll(string startCode, string endCode)
        {
            return FindAllBySql(string.Format(@"SELECT * FROM View_Part WHERE [PartCode] BETWEEN '{0}' AND '{1}' ORDER BY [PartCode] ASC", startCode, endCode));
        }
    }
}
