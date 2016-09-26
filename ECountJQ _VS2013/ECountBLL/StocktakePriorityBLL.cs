using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{
    public class StocktakePriorityBLL:BaseGenericBLL<StocktakePriority>
    {
        public StocktakePriorityBLL():base("StocktakePriority")
        {

        }
        public List<StocktakePriority> GetPriorities()
        {
            return _context.StocktakePriority.ToList();
        }
    }
}
