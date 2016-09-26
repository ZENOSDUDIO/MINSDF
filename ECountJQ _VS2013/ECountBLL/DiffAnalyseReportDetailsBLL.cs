using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data.Common;
using System.Data;
using SGM.Common.Utility;

namespace SGM.ECount.BLL
{
    public class DiffAnalyseReportDetailsBLL : BaseGenericBLL<DiffAnalyseReportDetails>
    {

        public DiffAnalyseReportDetailsBLL()
            : base("DiffAnalyseReportDetails")
        {

        }
        public List<View_DiffAnalyseReportDetails> GetDetails(DiffAnalyseReportItem item, UserGroup userGroup)
        {
            var query = Context.View_DiffAnalyseReportDetails.Where(d => d.ItemID == item.ItemID);
            if (userGroup != null)
            {
                query = query.Where(d => d.UserGroupID == userGroup.GroupID);
            }
            return query.OrderBy(d => d.GroupName).ThenBy(d => d.Description).ToList();
        }

        public void SaveAnalyseReport(List<View_DiffAnalyseReportDetails> detailsList)
        {
            if (detailsList.Count > 0)
            {
                Type[] types = new Type[] { typeof(View_DiffAnalyseReportDetails) };
                string detailsStr = string.Empty;
                detailsStr = Utils.SerializeToString<List<View_DiffAnalyseReportDetails>>(types, detailsList);

                DbParameter paramItems = Context.CreateDbParameter("@details", System.Data.DbType.Xml, detailsStr, System.Data.ParameterDirection.Input);
                Context.ExecuteNonQuery("sp_SaveAnalyseResult", CommandType.StoredProcedure, paramItems);
            }
        }

    }
}
