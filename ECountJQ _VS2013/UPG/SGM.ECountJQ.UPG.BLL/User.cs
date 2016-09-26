using System;
using System.ComponentModel;
using SGM.ECountJQ.UPG.BLL.DBBase;

namespace SGM.ECountJQ.UPG.BLL
{
    [MapTable("User", ConnName = Const.ConnName)]
    public class User : Entity<User>
    {
        [DataObjectField(true, true, true, 10)]
        [MapColumn("UserID")]
        public int UserID { get; set; }

        [DataObjectField(false, false, false, 20)]
        [MapColumn("UserName")]
        public string UserName { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("UserGroupID")]
        public int UserGroupID { get; set; }

        [DataObjectField(false, false, false, 50)]
        [MapColumn("Password")]
        public string Password { get; set; }

        [DataObjectField(false, false, false, 20)]
        [MapColumn("DUNS")]
        public string DUNS { get; set; }

        [DataObjectField(false, false, false, 20)]
        [MapColumn("ConsignmentDUNS")]
        public string ConsignmentDUNS { get; set; }

        [DataObjectField(false, false, false, 20)]
        [MapColumn("RepairDUNS")]
        public string RepairDUNS { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("Available")]
        public bool Available { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("SegmentID")]
        public int SegmentID { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("WorkShopID")]
        public int WorkShopID { get; set; }

        [DataObjectField(false, false, false, 20)]
        [MapColumn("CreateDate")]
        public DateTime CreateDate { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("PlantID")]
        public int PlantID { get; set; }

        [DataObjectField(false, false, false, 10)]
        [MapColumn("RetryTimes")]
        public int RetryTimes { get; set; }

        [DataObjectField(false, false, false, 20)]
        [MapColumn("LastModified")]
        public DateTime LastModified { get; set; }

        [DataObjectField(false, false, false, 20)]
        [MapColumn("LastLogon")]
        public DateTime LastLogon { get; set; }
    }
}
