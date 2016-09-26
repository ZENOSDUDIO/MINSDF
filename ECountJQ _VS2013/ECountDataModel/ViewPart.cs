using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGM.ECount.DataModel
{
    public partial class ViewPart
    {
        [DataMember]
        private int? _RowNumber;
        public int? RowNumber
        {
            get { return this._RowNumber; }
            set { this._RowNumber = value; }
        }
    }
}
