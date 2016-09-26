using System;
using System.ComponentModel;
using SGM.ECountJQ.UPG.BLL.DBBase;

namespace SGM.ECountJQ.UPG.BLL
{
    [Serializable]
    [MapTable("StocktakeNotification", ConnName = "SGM.ECountJQ.UPG")]
    public class StocktakeNotification : Entity<StocktakeNotification>
    {
        #region Properties

        [DataObjectField(true, true)]
        [MapColumn("NotificationID")]
        public Int64 NotificationID { get; set; }

        [DataObjectField(false)]
        [MapColumn("NotificationCode")]
        public string NotificationCode { get; set; }

        [DataObjectField(false)]
        [MapColumn("PlantID")]
        public int PlantID { get; set; }

        [DataObjectField(false)]
        [MapColumn("Published")]
        public bool Published { get; set; }

        [DataObjectField(false)]
        [MapColumn("IsStatic")]
        public bool IsStatic { get; set; }

        [DataObjectField(false, false, true)]
        [MapColumn("PlanDate")]
        public DateTime? PlanDate { get; set; }

        [DataObjectField(false)]
        [MapColumn("IsEmergent")]
        public bool IsEmergent { get; set; }

        [DataObjectField(false)]
        [MapColumn("CreatedBy")]
        public int CreatedBy { get; set; }

        [DataObjectField(false, false, true)]
        [MapColumn("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [DataObjectField(false)]
        [MapColumn("PublishedBy")]
        public int PublishedBy { get; set; }

        [DataObjectField(false, false, true)]
        [MapColumn("DatePublished")]
        public DateTime? DatePublished { get; set; }

        [DataObjectField(false)]
        [MapColumn("Status")]
        public int Status { get; set; }

        [DataObjectField(false, false, true)]
        [MapColumn("DateCounted")]
        public DateTime? DateCounted { get; set; }

        #endregion

        public static StocktakeNotification Find(Int64 id)
        {
            return FindByIdentity(id);
        }
    }
}
