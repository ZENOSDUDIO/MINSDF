using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class User
    {
         public User()
        {
            this.UserID = DefaultValue.INT;
            this._Available = true;
        }
    }
}
