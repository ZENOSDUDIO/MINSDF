using System;
using System.ComponentModel;
using SGM.ECountJQ.UPG.BLL.DBBase;
using System.Collections;
using System.Collections.Generic;

namespace SGM.ECountJQ.UPG.BLL
{
    [DataObject]
    [MapTable("StocktakeDetails", ConnName = Const.ConnName)]
    public class StocktakeDetails : Entity<StocktakeDetails>
    {
        #region Properties

        [DataObjectField(true, true, false, 10)]
        [MapColumn("DetailsID")]
        public Int64 DetailsID { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("PartID")]
        public int PartID { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("StocktakeType")]
        public int StocktakeType { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("Priority")]
        public int Priority { get; set; }

        [DataObjectField(false, false, false, 50)]
        [MapColumn("Description")]
        public string Description { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("ReadyForCount")]
        public bool ReadyForCount { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("NoticeID")]
        public Int64 NoticeID { get; set; }

        #endregion

        public static List<StocktakeDetails> FindAllByNotification(StocktakeNotification notification)
        {
            if (notification == null)
            {
                return null;
            }

            return FindAllByNotification(notification.NotificationID);
        }

        public static List<StocktakeDetails> FindAllByNotification(Int64 notificationId)
        {
            return FindAllByWhere(" NoticeID = " + notificationId);
        }
    }
}
