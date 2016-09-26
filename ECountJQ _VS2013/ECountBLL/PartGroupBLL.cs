using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using Microsoft.Data.Extensions;
using System.Data.Common;
using System.Data.EntityClient;
using SGM.Common.Utility;
using System.Data.Objects.DataClasses;

namespace SGM.ECount.BLL
{
    public class PartGroupBLL : BaseGenericBLL<PartGroup>
    {
        public PartGroupBLL()
            : base("PartGroup")
        {
        }


        public PartGroup GetPartGroupByKey(PartGroup group)
        {
            return Context.PartGroup.Include("GroupParts").FirstOrDefault(pg => pg.GroupID == group.GroupID);
        }

        public List<PartGroup> QueryPartGroups(PartGroup info)
        {
            IQueryable<PartGroup> partGroupQry = this.Context.PartGroup;

            if (partGroupQry != null)
            {
                if (!string.IsNullOrEmpty(info.GroupName))
                {
                    partGroupQry = partGroupQry.Where(p => p.GroupName == info.GroupName);
                }
            }
            return partGroupQry.ToList();
        }

        public List<PartGroup> GetPartGroupsByPage(int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<PartGroup> partGroupQry = this.Context.PartGroup.OrderBy(pg => pg.GroupID);
            return GetQueryByPage(partGroupQry, pageSize, pageNumber, out pageCount,out itemCount).ToList();
        }
       
        public PartGroup AddPartGroup(PartGroup partGroup)
        {

            Type[] types = new Type[] { typeof(EntityCollection<GroupPartRelation>), typeof(Part) };
            string partGroupStr = Utils.SerializeToString(types, partGroup);
            return Context.AddPartGroup(partGroupStr).ToList()[0];

        }
        
        public void UpdatePartGroup(PartGroup partGroup)
        {
            //Type[] types = new Type[] { typeof(List<Part>), typeof(Part) };
            string partGroupStr = Utils.SerializeToString( partGroup);
            Context.UpdatePartGroup(partGroupStr, partGroup.GroupID);
        }
        public void DeletePartGroup(PartGroup partGroup)
        {
            string sql = "DELETE FROM GroupPartRelation WHERE GroupID=@groupID;DELETE FROM PartGroup WHERE GroupID=@groupID";
           DbParameter paramID = Context.CreateDbParameter("@groupID", System.Data.DbType.Int32, partGroup.GroupID, System.Data.ParameterDirection.Input);
           Context.ExecuteNonQuery(sql, System.Data.CommandType.Text, paramID);
        }

    }
}
