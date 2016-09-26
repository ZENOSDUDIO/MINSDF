using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;

namespace SGM.ECount.BLL
{
    public class StocktakeTypeBLL:BaseGenericBLL<StocktakeType>
    {
        public StocktakeTypeBLL()
            : base("StocktakeType")
        {

        }
        public List<StocktakeType> GetStocktakeTypes()
        {
            return _context.StocktakeType.ToList();
        }


        public StocktakeType GetStocktakeTypeByKey(StocktakeType info)
        {
            return GetObjectByKey(info);
        }

        public List<StocktakeType> QueryStocktakeTypes(StocktakeType info)
        {
            IQueryable<StocktakeType> qryResult = Context.StocktakeType;
            qryResult = qryResult.Where(p => p.Available == true);
            if (!string.IsNullOrEmpty(info.TypeName))
            {
                qryResult = qryResult.Where(p => p.TypeName == info.TypeName);
            }
            return qryResult.OrderBy(p => p.DefaultPriority).ToList();
        }

        public StocktakeType AddStocktakeType(StocktakeType model)
        {
            AddObject(model, true);
            return model;
        }

        public void UpdateStocktakeType(StocktakeType model)
        {
            StocktakeType tempmodel = GetStocktakeTypeByKey(model);
            model.Available = tempmodel.Available;
            UpdateObject(model, true);
        }


        public void DeleteStocktakeTypes(List<string> ids)
        {
            StringBuilder sbSql = new StringBuilder();
            foreach (string id in ids)
            {
                sbSql.Append(string.Format("Update StocktakeType set Available=0 where TypeID='{0}';", id));
            }
            using (Context.Connection)
            {
                Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, false);
            }
        }

        public bool ExistStocktakeType(StocktakeType model)
        {
            IQueryable<StocktakeType> qryResult = Context.StocktakeType; ;
            if (!string.IsNullOrEmpty(model.TypeName))
            {
                qryResult = qryResult.Where(p => p.TypeName == model.TypeName&&p.Available==true);
            }
            if (qryResult.Count() > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
