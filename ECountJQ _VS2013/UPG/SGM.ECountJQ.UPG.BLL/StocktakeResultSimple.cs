using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECountJQ.UPG.BLL.DBBase;
using System.ComponentModel;

namespace SGM.ECountJQ.UPG.BLL
{
    [MapTable("StocktakeResultSimple", ConnName = Const.ConnName)]
    public class StocktakeResultSimple : Entity<StocktakeResultSimple>
    {
        [DataObjectField(false)]
        [MapColumn("NoticeID")]
        public Int64 NoticeID { get; set; }

        [DataObjectField(false)]
        [MapColumn("DetailsID")]
        public Int64 DetailsID { get; set; }

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
        public string PlantCode { get; set; }

        [DataObjectField(false)]
        [MapColumn("LogisticsSysSLOC")]
        public string LogisticsSysSLOC { get; set; }

        [DataObjectField(false)]
        [MapColumn("LocationID")]
        public int LocationID { get; set; }

        [DataObjectField(false)]
        [MapColumn("DUNS")]
        public string DUNS { get; set; }

        [DataObjectField(false)]
        [MapColumn("TypeID")]
        public int TypeID { get; set; }

        [DataObjectField(false)]
        [MapColumn("ItemID")]
        public Int64 ItemID { get; set; }

        public static List<StocktakeResultSimple> FindAll(Int64 noticeId, Int64 minPartID, Int64 maxPartID)
        {
            List<StocktakeResultSimple> list = new List<StocktakeResultSimple>();

            LoadData(list, ExecuteDataSet("sp_UPG_GetStocktakeResultSimple", new object[] { noticeId, minPartID, maxPartID }));

            return list;
        }
    }
}
