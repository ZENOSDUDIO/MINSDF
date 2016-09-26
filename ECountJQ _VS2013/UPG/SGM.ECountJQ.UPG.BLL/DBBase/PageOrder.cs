using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECountJQ.UPG.BLL.DBBase
{
    [Serializable]
    public class PageOrder
    {
        public string Name { get; set; }

        public OrderDirection Direction { get; set; }
    }

    public enum OrderDirection
    {
        Asc = 0,
        Desc = 1
    }
}
