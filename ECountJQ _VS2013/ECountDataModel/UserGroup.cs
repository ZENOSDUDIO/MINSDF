using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            this.GroupID = DefaultValue.INT;
            this.Available = true;
            this.CurrentStaticStocktake = 0;
            this.CurrentDynamicStocktake= 0;
        }
    }
}
