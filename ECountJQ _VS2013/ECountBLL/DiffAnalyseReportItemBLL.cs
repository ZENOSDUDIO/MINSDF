using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{
    class DiffAnalyseReportItemBLL:BaseGenericBLL<DiffAnalyseReportItem>
    {
        public DiffAnalyseReportItemBLL()
            : base("DiffAnalyseReportItem")
        {

        }
        public DiffAnalyseReportItem GetReportItem(long itemID)
        {
            DiffAnalyseReportItem itemInfo = Context.DiffAnalyseReportItem.Include("DiffAnalyseReportDetails").FirstOrDefault(i => i.ItemID == itemID);
            return itemInfo;
        }
    }
}
