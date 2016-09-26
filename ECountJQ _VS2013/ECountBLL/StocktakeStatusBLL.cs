using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{
    public class StocktakeStatusBLL : BaseGenericBLL<StocktakeStatus>
    {
        public StocktakeStatusBLL()
            : base("StocktakeStatus")
        {

        }
        public List<StocktakeStatus> GetStocktakeStatus()
        {
            return _context.StocktakeStatus.ToList();
        }
    }
}
