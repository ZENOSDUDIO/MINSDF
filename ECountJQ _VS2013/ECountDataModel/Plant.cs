using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class Plant
    {
        public Plant()
        {
            this._PlantID = DefaultValue.INT;
            this._Available = true;
        }
    }
}
