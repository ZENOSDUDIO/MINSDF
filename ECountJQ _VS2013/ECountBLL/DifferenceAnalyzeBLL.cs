using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;

namespace SGM.ECount.BLL
{
    //public class DifferenceAnalyzeBLL:BaseGenericBLL<DifferenceAnalyzeItem>
    //{
    //    public DifferenceAnalyzeBLL():base("DifferenceAnalyzeItem")
    //    {

    //    }
    //    public List<DifferenceAnalyzeItem> GetDiferenceAnalyzeItems()
    //    {
    //        return GetObjects().ToList();
    //    }

    //    public List<DifferenceAnalyzeDetails> GetDiferenceAnalyzeDetails(DifferenceAnalyzeItem item)
    //    {
    //        item = GetObjectByKey(item);
    //        item.DifferenceAnalyzeDetails.Load();
    //        return item.DifferenceAnalyzeDetails.ToList();
    //    }


    //    public DifferenceAnalyzeItem GetDifferenceAnalyzeItemByKey(DifferenceAnalyzeItem info)
    //    {
    //        return GetObjectByKey(info);
    //    }

    //    public List<DifferenceAnalyzeItem> QueryDifferenceAnalyzeItems(DifferenceAnalyzeItem info)
    //    {
    //        IQueryable<DifferenceAnalyzeItem> qryResult = Context.DifferenceAnalyzeItem.Include("UserGroup");
    //        return qryResult.ToList();
    //    }

    //    public DifferenceAnalyzeItem AddDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
    //    {
    //        AddObject(model, true);
    //        return model;
    //    }

    //    public void UpdateDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
    //    {
    //        DifferenceAnalyzeItem tempmodel = GetDifferenceAnalyzeItemByKey(model);
    //        model.IsAvailable = tempmodel.IsAvailable;
    //        UpdateObject(model, true);
    //    }


    //    public void DeleteDifferenceAnalyzeItems(List<string> ids)
    //    {
    //        StringBuilder sbSql = new StringBuilder();
    //        foreach (string id in ids)
    //        {
    //            sbSql.Append(string.Format("Update DifferenceAnalyzeItem set IsAvailable=0 where ItemID='{0}';", id));
    //        }
    //        using (Context.Connection)
    //        {
    //            Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, true);
    //        }
    //    }

    //    public bool ExistDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
    //    {
    //        IQueryable<DifferenceAnalyzeItem> qryResult = Context.DifferenceAnalyzeItem; ;
    //        if (!string.IsNullOrEmpty(model.ItemName))
    //        {
    //            qryResult = qryResult.Where(p => p.ItemName == model.ItemName);
    //        }
    //        if (qryResult.Count() > 0)
    //        {
    //            return true;
    //        }
    //        else
    //            return false;
    //    }

    //}
}
