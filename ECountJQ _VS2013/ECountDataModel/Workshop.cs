using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class Workshop
    {
        public Workshop()
        {
            this._WorkshopID = DefaultValue.INT;
            this._Available = true;
        }
    }
}
