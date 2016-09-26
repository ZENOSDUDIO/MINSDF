using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;

namespace SGM.ECount.BLL
{
    public class PartStatusBLL:BaseGenericBLL<PartStatus>
    {
        public PartStatusBLL():base("PartStatus")
        {

        }

        public List<PartStatus> GetPartStatus()
        {           
            return GetObjects().ToList();
        }


        public List<PartStatus> QueryPartStatusAvailable(PartStatus info)
        {
            IQueryable<PartStatus> partStatusQry = GetObjects();
            partStatusQry = partStatusQry.Where(p => p.Available == true);
            if (!string.IsNullOrEmpty(info.StatusName))
            {
                partStatusQry = partStatusQry.Where(p => p.StatusName == info.StatusName);
            }
            return partStatusQry.ToList();
        }

        public PartStatus GetPartStatusByKey(PartStatus status)
        {
            return GetObjectByKey(status);
        }

        public void DeletePartStatus(PartStatus status)
        {
            PartStatus partStatus = _context.PartStatus.First(p => p.StatusID == status.StatusID);
            partStatus.Available = false;
            UpdatePartStatus(partStatus);
            //DeleteObject(status);
        }

        public void DeletePartStatuss(List<string> ids)
        {
            StringBuilder sbSql = new StringBuilder();
            foreach (string id in ids)
            {
                sbSql.Append(string.Format("Update PartStatus set Available=0 where StatusID='{0}';", id));
            }
            using (Context.Connection)
            {
                Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, false);
            }
        }

        public void UpdatePartStatus(PartStatus status)
        {
            UpdateObject(status, true);
        }

        public PartStatus AddPartStatus(PartStatus model)
        {
            AddObject(model);
            return model;
        }

        public bool ExistPartStatus(PartStatus model)
        {
            IQueryable<PartStatus> partStatusQry = GetObjects();
            if (!string.IsNullOrEmpty(model.StatusName))
            {
                partStatusQry = partStatusQry.Where(p => p.StatusName == model.StatusName);
            }
            if (partStatusQry.Count() > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
