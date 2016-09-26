using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGM.ECount.DataModel
{
    public partial class View_ResultNoneCSMT
    {
        [DataMember]
        public List<WorkshopStocktakeDetail> WorkshopDetails { get; set; }

        [DataMember]
        private int? _RowNumber;
        public int? RowNumber
        {
            get { return this._RowNumber; }
            set { this._RowNumber = value; }
        }
    }
}
