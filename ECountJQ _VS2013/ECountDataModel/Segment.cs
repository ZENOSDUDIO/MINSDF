using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class Segment
    {
        public Segment()
        {
            _SegmentID = DefaultValue.INT;
            _SegmentCode = DefaultValue.STRING;
            _SegmentName = DefaultValue.STRING;
            _Available = true;
        }
    }
}
