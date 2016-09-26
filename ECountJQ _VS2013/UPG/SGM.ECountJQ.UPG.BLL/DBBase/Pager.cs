using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECountJQ.UPG.BLL.DBBase
{
    [Serializable]
    public class Pager
    {
        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int PageCount
        {
            get
            {
                return TotalCount / PageSize + (TotalCount % PageSize != 0 ? 1 : 0);
            }
        }

        public PageOrder Order { get; set; }
    }
}
