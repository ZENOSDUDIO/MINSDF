using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{
    public class BizParamsBLL : BaseGenericBLL<BizParams>
    {
        public BizParamsBLL(ECountContext context)
            : base(context,"BizParams")
        {

        }
        public BizParamsBLL()
            : base("BizParams")
        {
            
        }

        public List<BizParams> GetBizParamsList()
        {
            return GetObjects().OrderBy(o => o.GroupName).ThenBy(o => o.Sequence).ToList();
        }

        public BizParams GetBizParams(BizParams entity)
        {
            return Context.BizParams.FirstOrDefault(o => o.ParamID == entity.ParamID); 
        }

        public void UpdateBizParams(BizParams entity)
        {
            UpdateObject(entity);
        }

        public BizParams GetBizParamByKey(BizParams entity)
        {
            return Context.BizParams.FirstOrDefault(o => string.Equals(o.ParamKey, entity.ParamKey));
        }

        public void UpdateBizParamsList(List<BizParams> entities)
        {
            foreach (var entity in entities)
            {
                UpdateObject(entity,false);
            }
            Context.SaveChanges();
        }

        public int GetMaxStaticStocktakeCount()
        {
            BizParams bizParams = new BizParams { ParamID = 1 };
            bizParams = GetObjectByKey(bizParams);
            return Convert.ToInt32(bizParams.ParamValue);
        }

        public int GetMaxDynamicStocktakeCount()
        {
            BizParams bizParams = new BizParams { ParamID = 1 };
            bizParams = GetObjectByKey(bizParams);
            return Convert.ToInt32(bizParams.ParamValue);
        }

        public void ResetCycleCount()
        {
            string sql = "UPDATE BizParams SET ParamValue=0 WHERE ParamKey='CycledTimes';UPDATE Part SET CycleCountTimes=0,CountTimes=0 ";
            Context.ExecuteNonQuery(sql, System.Data.CommandType.Text);
        }
    }
}
