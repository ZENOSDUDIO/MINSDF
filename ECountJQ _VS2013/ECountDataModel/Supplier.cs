using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class Supplier
    {
        public Supplier()
        {
            _SupplierID = DefaultValue.INT;
            _SupplierName = DefaultValue.STRING;
            _DUNS = DefaultValue.STRING;
        }
    }
}
