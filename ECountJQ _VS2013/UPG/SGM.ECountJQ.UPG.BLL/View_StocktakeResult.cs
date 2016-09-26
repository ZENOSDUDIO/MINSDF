using System;
using System.ComponentModel;
using SGM.ECountJQ.UPG.BLL.DBBase;
using System.Collections;
using System.Collections.Generic;

namespace SGM.ECountJQ.UPG.BLL
{
    [MapTable("View_StocktakeResult", ConnName = Const.ConnName)]
    public class View_StocktakeResult : Entity<View_StocktakeResult>
    {
        [DataObjectField(false)]
        [MapColumn("PartCode")]
        public string PartCode { get; set; }

        [DataObjectField(false)]
        [MapColumn("PartPlantCode")]
        public string PartPlantCode { get; set; }

        [DataObjectField(false)]
        [MapColumn("DUNS")]
        public string DUNS { get; set; }

        [DataObjectField(false)]
        [MapColumn("SGMLocationID")]
        public int SGMLocationID { get; set; }

        [DataObjectField(false)]
        [MapColumn("RepairLocationID")]
        public int RepairLocationID { get; set; }

        [DataObjectField(false)]
        [MapColumn("CSMTLocationID")]
        public int CSMTLocationID { get; set; }

        [DataObjectField(false)]
        [MapColumn("GeneralItemID")]
        public int GeneralItemID { get; set; }

        [DataObjectField(false)]
        [MapColumn("RDCLocationID")]
        public int RDCLocationID { get; set; }

        [DataObjectField(false)]
        [MapColumn("NotificationID")]
        public Int64 NotificationID { get; set; }

        public static List<View_StocktakeResult> FindAll(Int64 NotificationID, string PartCodeStart, string PartCodeEnd)
        {
            return FindAllBySql(String.Format(@"SELECT * FROM [View_StocktakeResult] WHERE NotificationID = {0} AND PartCode >= '{1}' AND PartCode <= '{2}'",
                NotificationID, PartCodeStart, PartCodeEnd));
        }
    }
}
