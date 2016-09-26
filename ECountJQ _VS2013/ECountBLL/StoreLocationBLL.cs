using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Data.Common;
using System.Data;


namespace SGM.ECount.BLL
{
    public class StoreLocationBLL:BaseGenericBLL<StoreLocation>
    {
        public StoreLocationBLL():base("StoreLocation")
        {

        }

        public StoreLocationBLL(ECountContext context)
            : base(context, "StoreLocation")
        {

        }

        public StoreLocation GetStoreLocationByKey(StoreLocation info)
        {
            return GetObjectByKey(info);
        }

        public List<StoreLocation> GetStoreLocations()
        {
            return Context.StoreLocation.Include("Plant").Include("StoreLocationType").ToList();
            //return GetObjects().ToList();
        }

        public List<StoreLocation> QueryStoreLocations(StoreLocation info)
        {
            IQueryable<StoreLocation> qryResult = Context.StoreLocation.Include("Plant").Include("StoreLocationType");
            qryResult = qryResult.Where(p => p.Available == true);
           
            if (!string.IsNullOrEmpty(info.LocationName))
            {
                qryResult = qryResult.Where(p => p.LocationName == info.LocationName);
            }
            if (info.Plant != null && info.Plant.PlantID>0)
            {
                qryResult = qryResult.Where(p => p.Plant.PlantID == info.Plant.PlantID);
            }
            if (info.StoreLocationType!=null && info.StoreLocationType.TypeID!=DefaultValue.INT)
            {
                qryResult = qryResult.Where(l => l.StoreLocationType.TypeID == info.StoreLocationType.TypeID);
            }
            return qryResult.OrderBy(s=>s.LogisticsSysSLOC).ToList();
        }

        public StoreLocation AddStoreLocation(StoreLocation location)
        {
            AddObject(location, true);
            return location;
        }

        public void UpdateStoreLocation(StoreLocation location)
        {
            StoreLocation model = GetStoreLocationByKey(location);
            location.Available = model.Available;
            UpdateObject(location, true);
        }

        public void DeleteStoreLocation(StoreLocation location)
        {
            StoreLocation tmpLocation = GetStoreLocationByKey(location);
            tmpLocation.Available = false;
            UpdateStoreLocation(tmpLocation);
        }

        public void DeleteStoreLocations(List<string> ids)
        {
            StringBuilder sbSql = new StringBuilder();
            foreach (string id in ids)
            {
                sbSql.Append(string.Format("Update StoreLocation set Available=0 where LocationID='{0}';", id));
            }
            Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, false);
            //using (Context.Connection)
            //{
            //    Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, true);
            //}
        }

        public bool ExistStoreLocation(StoreLocation model)
        {
            IQueryable<StoreLocation> qryResult = Context.StoreLocation; ;
            if (!string.IsNullOrEmpty(model.LocationName))
            {
                qryResult = qryResult.Where(p => p.LocationName == model.LocationName&&(p.Available== true));
            }
            if (qryResult.Count() > 0)
            {
                return true;
            }
            else
                return false;
        }

        public void ImportStoreLocation(List<S_StoreLocation> list)
        {
            Type[] types = new Type[] { typeof(S_StoreLocation) };
            XmlSerializer xs = new XmlSerializer(typeof(List<S_StoreLocation>), types);
            string itemStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, list);
                itemStr = sw.ToString();
            }
            DbParameter paramItems = Context.CreateDbParameter("@storelocationItems", System.Data.DbType.Xml, itemStr, System.Data.ParameterDirection.Input);

            Context.ExecuteNonQuery("sp_ImportStoreLocation", CommandType.StoredProcedure, paramItems);
        }
    }
}
