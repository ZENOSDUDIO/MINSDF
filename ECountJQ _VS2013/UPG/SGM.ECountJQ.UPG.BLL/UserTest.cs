using System;
using System.ComponentModel;
using SGM.ECountJQ.UPG.BLL.DBBase;

namespace SGM.ECountJQ.UPG.BLL
{
    [MapTable("User", ConnName = "Test")]
    public class UserTest : Entity<UserTest>
    {
        [DataObjectField(true,true,false)]
        [MapColumn("ID")]
        public int ID { get; set; }

        [DataObjectField(false)]
        [MapColumn("Name")]
        public string Name { get; set; }

        [DataObjectField(false)]
        [MapColumn("Test")]
        public string Test { get; set; }
    }
}
