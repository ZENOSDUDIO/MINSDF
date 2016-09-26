using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{
    public class WorkshopStocktakeDetailBLL : BaseGenericBLL<WorkshopStocktakeDetail>
    {
        public WorkshopStocktakeDetailBLL()
            : base("WorkshopStocktakeDetail")
        {

        }
        public WorkshopStocktakeDetailBLL(ECountContext context)
            : base(context, "WorkshopStocktakeDetail")
        {

        }

        public IQueryable<WorkshopStocktakeDetail> GetDetailsByItemID(long itemID)
        {
            return Context.WorkshopStocktakeDetail.Where(d => d.ItemID == itemID);
        }
    }
}
