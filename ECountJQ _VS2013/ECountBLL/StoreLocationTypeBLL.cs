using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;

namespace SGM.ECount.BLL
{
    public class StoreLocationTypeBLL : BaseGenericBLL<StoreLocationType>
    {
        public StoreLocationTypeBLL()
            : base("StoreLocationType")
        {

        }
        public StoreLocationTypeBLL(ECountContext context)
            : base(context,"StoreLocationType")
        {

        }

        public List<StoreLocationType> GetStoreLocationType(StoreLocationType info)
        {
            IQueryable<StoreLocationType> qryResult = Context.StoreLocationType;
            if (info != null && !string.IsNullOrEmpty(info.TypeName))
            {
                qryResult = qryResult.Where(p => p.TypeName == info.TypeName);
            }
            return qryResult.ToList();
        }


        public StoreLocationType GetStoreLocationTypeByKey(StoreLocationType info)
        {
            return GetObjectByKey(info);
        }



        public void UpdateStoreLocationType(StoreLocationType model)
        {
            UpdateObject(model, true);
        }

        public StoreLocationType AddStoreLocationType(StoreLocationType model)
        {
            AddObject(model);
            return model;
        }

        public void DeleteStoreLocationTypes(List<string> ids)
        {
            string idlist = FormatIds(ids);
            string sql = string.Format("Delete StoreLocationType where TypeID in ({0});", idlist);

            using (Context.Connection)
            {
                //Exist reference relation  can't delete current StoreLocationType.
                if (Context.ExecuteScalar("Select 1 from StoreLocationType where TypeID in (" + idlist + ")", CommandType.Text) != null)
                {
                    Context.ExecuteNonQuery(sql, CommandType.Text, false);
                }
            }
        }

        public bool ExistStoreLocationType(StoreLocationType model)
        {
            IQueryable<StoreLocationType> qryResult = GetObjects();
            if (!string.IsNullOrEmpty(model.TypeName))
            {
                qryResult = qryResult.Where(p => p.TypeName == model.TypeName);
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
