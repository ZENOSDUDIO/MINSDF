using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class StoreLocation
    {
        public StoreLocation()
        {
            this._Available = true;
        }
    }

    public class S_StoreLocation
    {
        public string LocationName { get; set; }
        public int TypeID { get; set; }
        public int  AvailableIncluded { get; set; }
        public int QIIncluded { get; set; }
        public int BlockIncluded { get; set; }
        public string LogisticsSysSLOC { get; set; }
    }

}
