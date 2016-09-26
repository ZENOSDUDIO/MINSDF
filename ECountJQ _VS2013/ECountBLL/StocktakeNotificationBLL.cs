using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Data.EntityClient;
using System.Data.Common;
using System.Data;
using SGM.Common.Cache;
using SGM.Common.Utility;

namespace SGM.ECount.BLL
{
    public class StocktakeNotificationBLL : BaseGenericBLL<StocktakeNotification>
    {
        public StocktakeNotificationBLL()
            : base("StocktakeNotification")
        {

        }

        public IQueryable<StocktakeDetails> GetDetails(bool isStatic)
        {
            IQueryable<StocktakeDetails> detailsQry = Context.StocktakeDetails.Where(d => d.StocktakeNotification == null && d.StocktakeNotification == null && d.StocktakeRequest.IsStatic == isStatic);
            return detailsQry;
        }


        public StocktakeNotification CreateNotification(StocktakeNotification notification, List<long> removedDetailsList, bool retrieveNew)
        {
            List<View_StocktakeDetails> detailsList = null;
            if (notification.Plant != null)//notification by plant
            {
                StocktakeDetailBLL bll = new StocktakeDetailBLL();
                IQueryable<View_StocktakeDetails> details = bll.GetNewRequestDetails(notification.IsStatic.Value, notification.Plant);
                detailsList = details.ToList();
            }
            else
            {
                StocktakeDetailBLL bll = new StocktakeDetailBLL();
                IQueryable<View_StocktakeDetails> details = bll.GetNewRequestDetails(notification.IsStatic.Value);

                detailsList = details.ToList();
            }
            for (int i = detailsList.Count - 1; i >= 0; i--)
            {
                if (removedDetailsList.Contains(detailsList[i].DetailsID))
                {
                    detailsList.RemoveAt(i);
                }
            }
            foreach (var item in notification.DetailsView)
            {
                View_StocktakeDetails detail = detailsList.Find(d => d.DetailsID == item.DetailsID);
                if (detail != null)
                {
                    detail.NotifyComments = item.NotifyComments;
                }
            }
            notification.DetailsView = detailsList;

            S_StocktakeNotification snoti = notification.MakeSerializable();
            Type[] types = new Type[] { typeof(NewStocktakeDetails) };
            XmlSerializer xs = new XmlSerializer(typeof(S_StocktakeNotification), types);
            string notificationStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, snoti);
                notificationStr = sw.ToString();
            }

            #region serialize manually
          
            #endregion

            DbParameter paramXml = Context.CreateDbParameter("Notification", System.Data.DbType.Xml, notificationStr, System.Data.ParameterDirection.Input);
            DbParameter paramNotificationId = Context.CreateDbParameter("NotificationID", System.Data.DbType.Int64, null, System.Data.ParameterDirection.Output);
            DbParameter paramNotificationNumber = Context.CreateDbParameter("notificationNumber", System.Data.DbType.String, null, System.Data.ParameterDirection.Output);
            paramNotificationNumber.Size = 30;
            Context.ExecuteNonQuery("sp_CreateNotification", System.Data.CommandType.StoredProcedure, paramXml, paramNotificationId, paramNotificationNumber);

            if (retrieveNew)
            {
                StocktakeNotification newNotification = new StocktakeNotification { NotificationID = Convert.ToInt64(paramNotificationId.Value) };
                return this.GetNotification(newNotification);
            }
            notification.NotificationID = long.Parse(paramNotificationId.Value.ToString());
            return notification;
        }

        public bool ExistsRepairPart(StocktakeNotification notification)
        {
            string sql = "SELECT TOP 1 1 FROM View_StocktakeDetails, PartRepairRecord WHERE PartRepairRecord.PartID=View_StocktakeDetails.PartID AND PartRepairRecord.Available=1 AND View_StocktakeDetails.NotificationID=" + notification.NotificationID.ToString();
            object obj = Context.ExecuteScalar(sql, CommandType.Text);
            return obj != null;
        }


        public bool ExistsCSMTPart(StocktakeNotification notification)
        {
            string sql = "SELECT TOP 1 1 FROM View_StocktakeDetails,ConsignmentPartRecord  WHERE ConsignmentPartRecord.PartID=View_StocktakeDetails.PartID AND ConsignmentPartRecord.Available=1 AND View_StocktakeDetails.NotificationID=" + notification.NotificationID.ToString();
            object obj = Context.ExecuteScalar(sql, CommandType.Text);
            return obj != null;
        }

        public void UpdateNotification(StocktakeNotification notification, List<long> removedDetailsList, List<View_StocktakeDetails> changedDetails)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL(Context);

            List<View_StocktakeDetails> detailsList = bll.GetNotificationDetails(notification).ToList();
            //removed items
            for (int i = detailsList.Count - 1; i >= 0; i--)
            {
                if (removedDetailsList.Contains(detailsList[i].DetailsID))
                {
                    detailsList.RemoveAt(i);
                }
            }
            //changed items
            foreach (var item in changedDetails)
            {
                View_StocktakeDetails detail = detailsList.Find(d => d.DetailsID == item.DetailsID);
                if (detail != null)
                {
                    detail.NotifyComments = item.NotifyComments;
                }
                else
                {
                    detailsList.Add(item);
                }
            }

            notification.DetailsView = detailsList;

            S_StocktakeNotification snoti = notification.MakeSerializable();
            Type[] types = new Type[] { typeof(NewStocktakeDetails) };
            XmlSerializer xs = new XmlSerializer(typeof(S_StocktakeNotification), types);
            string notificationStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, snoti);
                notificationStr = sw.ToString();
            }


            DbParameter paramXml = Context.CreateDbParameter("Notification", System.Data.DbType.Xml, notificationStr, System.Data.ParameterDirection.Input);
            DbParameter paramNotificationId = Context.CreateDbParameter("notificationID", System.Data.DbType.Int64, notification.NotificationID, System.Data.ParameterDirection.Input);
            Context.ExecuteNonQuery("sp_UpdateNotification", System.Data.CommandType.StoredProcedure, paramXml, paramNotificationId);

            //if (retrieveNew)
            //{
            //    StocktakeNotification newNotification = new StocktakeNotification { NotificationID = Convert.ToInt64(paramNotificationId.Value) };
            //    return this.GetNotification(newNotification);
            //}
            //notification.NotificationID = long.Parse(paramNotificationId.Value.ToString());
            //return notification;
        }

        public int RemoveAllParts(long notificationID)
        {
            DbParameter paramNotificationId = Context.CreateDbParameter("NotificationID", System.Data.DbType.Int64, notificationID, System.Data.ParameterDirection.Input);
            return Context.ExecuteNonQuery("sp_RemoveAllPartsFromNoti", System.Data.CommandType.StoredProcedure, paramNotificationId);

        }

        public StocktakeNotification GetNotification(StocktakeNotification notification)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL();
            StocktakeNotification noti = Context.StocktakeNotification.Include("Publisher").Include("Creator").Include("StocktakeStatus").Include("Plant").FirstOrDefault(n => n.NotificationID == notification.NotificationID);
            noti.StocktakeLocations.Clear();
            List<StocktakeLocation> list = LoadLocations(noti);
            for (int i = 0; i < list.Count; i++)
            {
                noti.StocktakeLocations.Add(list[i]);
            }
            //noti.DetailsView = new List<View_StocktakeDetails>();
            //IQueryable<View_StocktakeDetails> detailsQry = bll.GetNotificationDetails(notification);
            //noti.StocktakeDetails.Clear();
            //noti.DetailsView = new List<View_StocktakeDetails>();
            //noti.DetailsView.AddRange(detailsQry);
            return noti;
        }

        public List<View_StocktakeDetails> GetNotificationlist(String  notification)
        {
            long noti = int.Parse(notification);
            IQueryable<View_StocktakeDetails> query = Context.View_StocktakeDetails.Where(c => c.NotificationID == noti);

            return query.ToList();
        }



        private List<StocktakeLocation> LoadLocations(StocktakeNotification noti)
        {
            return Context.StocktakeLocation.Include("StoreLocationType").Where(l => l.StocktakeNotification.NotificationID == noti.NotificationID).ToList();
        }





        public IQueryable<View_StocktakeNotification> QueryNotification(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd)
        {
            var result = from n in Context.View_StocktakeNotification join d in Context.View_StocktakeDetails on n.NotificationID equals d.NotificationID select new { n, d };
            if (condition.Published != null)
            {
                result = result.Where(nd => nd.n.Published == condition.Published);
            }
            if (!string.IsNullOrEmpty(condition.NotificationCode))
            {
                result = result.Where(nd => nd.n.NotificationCode == condition.NotificationCode);
            }
            if (dateStart != null)
            {
                result = result.Where(nd => nd.n.DateCreated >= dateStart.Value);
            }
            if (dateEnd != null)
            {
                result = result.Where(nd => nd.n.DateCreated <= dateEnd.Value);
            }
            if (planDateStart != null)
            {
                result = result.Where(nd => nd.n.PlanDate >= planDateStart);
            }
            if (planDateEnd != null)
            {
                result = result.Where(nd => nd.n.PlanDate <= planDateEnd);
            }
            if (!string.IsNullOrEmpty(condition.PartCode))
            {
                result = result.Where(nd => nd.d.PartCode == condition.PartCode);
            }
            if (!string.IsNullOrEmpty(condition.PartChineseName))
            {
                result = result.Where(nd => nd.d.PartChineseName == condition.PartChineseName);
            }
            if (condition.Status != null)
            {
                result = result.Where(nd => nd.n.Status == condition.Status);
            }
            if (!string.IsNullOrEmpty(condition.RequestUser))
            {
                result = result.Where(nd => nd.d.RequestUser == condition.RequestUser);
            }
            if (!string.IsNullOrEmpty(condition.RequestUserGroup))
            {
                result = result.Where(nd => nd.d.RequestUserGroup == condition.RequestUserGroup);
            }

            if (!string.IsNullOrEmpty(condition.RequestNumber))
            {
                result = result.Where(nd => nd.d.RequestNumber == condition.RequestNumber);
            }

            return result.Select(nd => nd.n).Distinct();
        }

        public IQueryable<View_StocktakeNotification> QueryNotification(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd)
        {
            var result = QueryNotification(condition, dateStart, dateEnd, planDateStart, planDateEnd);
            if (locationID != null)
            {
                result = from r in result join l in Context.StocktakeLocation on r.NotificationID equals l.StocktakeNotification.NotificationID where l.StoreLocation.LocationID == locationID select r;
            }
            return result.Distinct();
        }

        //public IQueryable<View_StocktakeNotification> QueryResult(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd)
        //{
        //    IQueryable<View_StocktakeNotification> qry = QueryNotification(condition, dateStart, dateEnd, planDateStart, planDateEnd);
        //    qry = qry.Where(q => q.Status >= Consts.STOCKTAKE_PUBLISHED);
        //    return qry;
        //}

        //public IQueryable<View_StocktakeNotification> QueryResultByPage(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        //{
        //    IQueryable<View_StocktakeNotification> result = this.QueryResult(condition, dateStart, dateEnd, planDateStart, planDateEnd).OrderBy(d => d.NotificationCode);
        //    return this.GetQueryByPage(result, pageSize, pageNumber, out pageCount, out itemCount);
        //}



        //public IQueryable<View_StocktakeNotification> QueryNotificationByPage(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        //{
        //    IQueryable<View_StocktakeNotification> result = this.QueryNotification(condition, dateStart, dateEnd, planDateStart, planDateEnd).OrderBy(d => d.NotificationCode);
        //    return this.GetQueryByPage(result, pageSize, pageNumber, out pageCount, out itemCount);
        //}


        public IQueryable<View_StocktakeNotification> QueryNotificationByPage(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<View_StocktakeNotification> result = this.QueryNotification(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd).OrderByDescending(d => d.NotificationCode);
            return this.GetQueryByPage(result, pageSize, pageNumber, out pageCount, out itemCount);
        }

        public void Publish(StocktakeNotification notification, List<StoreLocationType> locationTypes)
        {
            string slocs = string.Empty;
            if (locationTypes != null && locationTypes.Count > 0)
            {
                string[] locationTypeArray = locationTypes.Select(l => l.TypeID.ToString()).ToArray();
                slocs = string.Join(",", locationTypeArray);
            }
            else
            {
                return;
            }
            DbParameter paramNotificationID = Context.CreateDbParameter("@notificationID", System.Data.DbType.Int32, notification.NotificationID, System.Data.ParameterDirection.Input);
            DbParameter paramPublishby = Context.CreateDbParameter("@publishBy", System.Data.DbType.Int32, notification.Publisher.UserID, System.Data.ParameterDirection.Input);
            DbParameter paramPlanDate = Context.CreateDbParameter("@planDate", System.Data.DbType.DateTime, notification.PlanDate, System.Data.ParameterDirection.Input);
            DbParameter paramIsEmergent = Context.CreateDbParameter("@isEmergent", System.Data.DbType.Boolean, notification.IsEmergent, System.Data.ParameterDirection.Input);
            DbParameter paramSLOCS = Context.CreateDbParameter("@slocs", System.Data.DbType.String, slocs, System.Data.ParameterDirection.Input);

            Context.ExecuteNonQuery("sp_PublishNotification", System.Data.CommandType.StoredProcedure, paramNotificationID, paramPublishby, paramPlanDate, paramIsEmergent, paramSLOCS);
        }

        public void CancelPublish(long notificationID)
        {
            DbParameter paramNotificationID = Context.CreateDbParameter("@notificationID", System.Data.DbType.Int64, notificationID, System.Data.ParameterDirection.Input);
            Context.ExecuteNonQuery("sp_DeleteNotification", System.Data.CommandType.StoredProcedure, paramNotificationID);
        }

        public void BatchCancelPublish(List<string> notifications)
        {
            if (notifications.Count > 0)
            {
                string notis = string.Join(",", notifications.ToArray());
                DbParameter paramNotifications = Context.CreateDbParameter("@notifications", System.Data.DbType.String, notis, System.Data.ParameterDirection.Input);
                Context.ExecuteNonQuery("sp_BatchCancelNotiPublish", System.Data.CommandType.StoredProcedure, paramNotifications);
            }
        }

        public void Delete(long notificationID)
        {
            DbParameter paramNotificationID = Context.CreateDbParameter("@notificationID", System.Data.DbType.Int64, notificationID, System.Data.ParameterDirection.Input);
            Context.ExecuteNonQuery("sp_DeleteNotification", System.Data.CommandType.StoredProcedure, paramNotificationID);
        }

        public void DeleteBatch(List<string> notifications)
        {
            if (notifications.Count > 0)
            {
                string notis = string.Join(",", notifications.ToArray());
                DbParameter paramNotifications = Context.CreateDbParameter("@notifications", System.Data.DbType.String, notis, System.Data.ParameterDirection.Input);
                Context.ExecuteNonQuery("sp_DeleteNotificationBatch", System.Data.CommandType.StoredProcedure, paramNotifications);
            }
        }

        public IQueryable<View_StocktakeResult> GetStocktakeResult(View_StocktakeResult filter)
        {
            IQueryable<View_StocktakeResult> qry = Context.View_StocktakeResult;//.Where(r => r.NotificationID == id);
            if (filter.NotificationID != null)
            {
                qry = qry.Where(r => r.NotificationID == filter.NotificationID);
            }
            if (!string.IsNullOrEmpty(filter.CSMTDUNS) && !string.IsNullOrEmpty(filter.RepairDUNS) && !string.IsNullOrEmpty(filter.DUNS))
            {
                qry = qry.Where(r => r.CSMTDUNS != null && r.CSMTDUNS == filter.CSMTDUNS || r.RepairDUNS != null && r.RepairDUNS == filter.RepairDUNS || r.DUNS == filter.DUNS);
            }
            if (filter.PlantID != null)
            {
                int plantID = filter.PlantID.Value;
                qry = qry.Where(r => r.PlantID == plantID);
                if (!string.IsNullOrEmpty(filter.Workshops))
                {
                    string workshops = filter.Workshops;
                    qry = qry.Where(r => ("," + r.Workshops + ",").IndexOf("," + workshops + ",") >= 0);
                }
                if (!string.IsNullOrEmpty(filter.Segments))
                {
                    string segments = filter.Segments;
                    qry = qry.Where(r => ("," + r.Segments + ",").IndexOf("," + segments + ",") >= 0);
                }
            }
            if (filter.Fulfilled!=null)
            {
                qry = qry.Where(r => r.Fulfilled == filter.Fulfilled);
            }
            if (filter.Status != null)
            {
                qry = qry.Where(r => r.Status >= filter.Status);
            }
            return qry;
        }

        public IQueryable<View_StocktakeResult> GetStocktakeResultOfScope(View_StocktakeResult filter, string startPartCode, string endPartCode)
        {
            var qry = GetStocktakeResult(filter);
            qry = qry.OrderBy(r => r.PartCode);
            if (!string.IsNullOrEmpty(startPartCode))
            {
                qry = qry.Where(r => string.Compare(r.PartCode, startPartCode) >= 0); 
            }
            if (!string.IsNullOrEmpty(endPartCode))
            {
                qry = qry.Where(r => string.Compare(r.PartCode, endPartCode) <= 0); 
            }
            return qry;
        }

        public IQueryable<View_ResultCSMT> GetCSMTStocktakeResult(View_ResultCSMT filter)
        {
            IQueryable<View_ResultCSMT> qry = Context.View_ResultCSMT;//.Where(r => r.NotificationID == id);
            if (filter.NotificationID != null)
            {
                qry = qry.Where(r => r.NotificationID == filter.NotificationID);
            }
            if (filter.CSMTFulfilled!=null)
            {
                qry = qry.Where(r => r.CSMTFulfilled == filter.CSMTFulfilled);
            }
            if (filter.PlantID != null)
            {
                int plantID = filter.PlantID.Value;
                qry = qry.Where(r => r.PlantID == plantID);
                if (!string.IsNullOrEmpty(filter.Workshops))
                {
                    string workshops = filter.Workshops;
                    qry = qry.Where(r => ("," + r.Workshops + ",").IndexOf("," + workshops + ",") >= 0);
                }
                if (!string.IsNullOrEmpty(filter.Segments))
                {
                    string segments = filter.Segments;
                    qry = qry.Where(r => ("," + r.Segments + ",").IndexOf("," + segments + ",") >= 0);
                }
            }
            if (!string.IsNullOrEmpty(filter.CSMTDUNS) || !string.IsNullOrEmpty(filter.DUNS))
            {
                qry = qry.Where(r => !string.IsNullOrEmpty(filter.CSMTDUNS) && r.CSMTDUNS == filter.CSMTDUNS || !string.IsNullOrEmpty(filter.DUNS) && r.DUNS == filter.DUNS);
            }
            if (filter.Status != null)
            {
                qry = qry.Where(r => r.Status >= filter.Status);
            }
            return qry.OrderBy(r => r.WorkLocation);
        }

        public IQueryable<View_ResultNoneCSMT> GetNoneCSMTStocktakeResult(View_ResultNoneCSMT filter)
        {
            IQueryable<View_ResultNoneCSMT> qry = Context.View_ResultNoneCSMT;//.Where(r => r.NotificationID == id);
            if (filter.NotificationID != null)
            {
                qry = qry.Where(r => r.NotificationID == filter.NotificationID);
            }
            if (filter.NoneCSMTFulfilled != null)
            {
                qry = qry.Where(r => r.NoneCSMTFulfilled == filter.NoneCSMTFulfilled);
            }
            if (filter.PlantID != null)
            {
                int plantID = filter.PlantID.Value;
                qry = qry.Where(r => r.PlantID == plantID);
                if (!string.IsNullOrEmpty(filter.Workshops))
                {
                    string workshops = filter.Workshops;
                    qry = qry.Where(r => ("," + r.Workshops + ",").IndexOf("," + workshops + ",") >= 0);
                }
                if (!string.IsNullOrEmpty(filter.Segments))
                {
                    string segments = filter.Segments;
                    qry = qry.Where(r => ("," + r.Segments + ",").IndexOf("," + segments + ",") >= 0);
                }
            }
            if (!string.IsNullOrEmpty(filter.RepairDUNS) || !string.IsNullOrEmpty(filter.DUNS))
            {
                qry = qry.Where(r => !string.IsNullOrEmpty(filter.RepairDUNS) && r.RepairDUNS == filter.RepairDUNS || !string.IsNullOrEmpty(filter.DUNS) && r.DUNS == filter.DUNS);
            }
            if (filter.Status != null)
            {
                qry = qry.Where(r => r.Status >= filter.Status);
            }
            return qry.OrderBy(r => r.WorkLocation);
        }

        public IQueryable<View_ResultNoneCSMT> GetStocktakeResult(View_ResultCSMT filter)
        {
            IQueryable<View_ResultNoneCSMT> qry = Context.View_ResultNoneCSMT;//.Where(r => r.NotificationID == id);
            //if (filter.NotificationID != null)
            //{
            //    qry = qry.Where(r => r.NotificationID == filter.NotificationID);
            //}
            //if (filter.PlantID != null)
            //{
            //    int plantID = filter.PlantID.Value;
            //    qry = qry.Where(r => r.PlantID == plantID);
            //    if (!string.IsNullOrEmpty(filter.Workshops))
            //    {
            //        string workshops = filter.Workshops;
            //        qry = qry.Where(r => ("," + r.Workshops + ",").IndexOf("," + workshops + ",") >= 0);
            //    }
            //}
            //if (!string.IsNullOrEmpty(filter.RepairDUNS) && !string.IsNullOrEmpty(filter.DUNS))
            //{
            //    qry = qry.Where(r => !string.IsNullOrEmpty(filter.RepairDUNS) && r.RepairDUNS == filter.RepairDUNS || !string.IsNullOrEmpty(filter.DUNS) && r.DUNS == filter.DUNS);
            //}
            //if (filter.Status != null)
            //{
            //    qry = qry.Where(r => r.Status >= filter.Status);
            //}
            return qry;
        }

        public string ImportResult(StocktakeNotification notification, List<View_StocktakeResult> itemList, string cacheKey, bool submit)
        {
            List<View_StocktakeResult> list = itemList;
            if (!(submit && string.IsNullOrEmpty(cacheKey)))
            {
                if (string.IsNullOrEmpty(cacheKey))
                {
                    cacheKey = Guid.NewGuid().ToString();
                }
                list = CacheHelper.GetCache(cacheKey) as List<View_StocktakeResult>;
                if (list == null)
                {
                    list = itemList;
                    CacheHelper.SetCache(cacheKey, list);
                }
                else
                {
                    list.AddRange(itemList);
                }
            }
            if (submit)
            {
                FillResult(notification, list);
                CacheHelper.RemoveCache(cacheKey);
            }
            else
            {
                CacheHelper.SetCache(cacheKey, list);
            }
            return cacheKey;
        }

        public void FillResult(StocktakeNotification notification, List<View_StocktakeResult> itemList)
        {

            //foreach (var item in itemList)
            //{
            //    if (item.SGMItemID!=null)
            //    {
            //        foreach (var detail in item.WorkshopDetails)
            //        {
            //            if (detail.Store!=null)
            //            {
            //                item.Store += detail.Store;
            //            }
            //            if (detail.Line != null)
            //            {
            //                item.Line += detail.Line;
            //            }
            //            if (detail.Machining != null)
            //            {
            //                item.Machining += detail.Machining;
            //            }
            //            if (detail.SGMQI != null)
            //            {
            //                item.SGMQI+= detail.SGMQI;
            //            }
            //            if (detail.SGMBlock != null)
            //            {
            //                item.SGMBlock += detail.SGMBlock;
            //            }
            //        }
            //    }
            //}
            // SGM ITEMS
            List<S_StocktakeItem> list = new List<S_StocktakeItem>();
            var qrySGM = from r in itemList
                         where r.SGMItemID != null && r.SGMFillinBy != null
                         select new { r.SGMItemID, r.Machining, r.Line, r.Store, r.SGMQI, r.SGMBlock, r.StartCSN, r.EndCSN, r.SGMFillinBy, r.SGMFillinTime };
            foreach (var item in qrySGM)
            {
                list.Add(new S_StocktakeItem
                {
                    ItemID = item.SGMItemID.Value,
                    Block = item.SGMBlock,
                    EndCSN = item.EndCSN,
                    Line = item.Line,
                    Machining = item.Machining,
                    QI = item.SGMQI,
                    StartCSN = item.StartCSN,
                    Store = item.Store,
                    FillinBy = item.SGMFillinBy,
                    FillinTime =( item.SGMFillinTime??DateTime.Now).ToString()
                });
            }

            //supplier items
            List<S_SupplierStocktakeItem> supplierList = new List<S_SupplierStocktakeItem>();
            var qrySupplier = (
                from r in itemList
                where r.RDCItemID != DefaultValue.LONG && r.RDCFillinBy!=null
                select new S_SupplierStocktakeItem
                {
                    ItemID = r.RDCItemID,
                    Available = r.RDCAvailable,
                    QI = r.RDCQI,
                    Block = r.RDCBlock,
                    FillinBy = r.RDCFillinBy,
                    FillinTime = (r.RDCFillinTime??DateTime.Now).ToString()
                }//RDC
                ).Union(
                from r in itemList
                where r.RepairItemID != null && r.RepairFillinBy != null
                select new S_SupplierStocktakeItem
                {
                    ItemID = r.RepairItemID.Value,
                    Available = r.RepairAvailable,
                    QI = r.RepairQI,
                    Block = r.RepairBlock,
                    FillinBy = r.RepairFillinBy,
                    FillinTime = (r.RepairFillinTime??DateTime.Now).ToString()
                }//Repair
                ).Union(
                from r in itemList
                where r.CSMTItemID != null && r.CSMTFillinBy != null
                select new S_SupplierStocktakeItem
                {
                    ItemID = r.CSMTItemID.Value,
                    Available = r.CSMTAvailable,
                    QI = r.CSMTQI,
                    Block = r.CSMTBlock,
                    FillinBy = r.CSMTFillinBy,
                    FillinTime = (r.CSMTFillinTime??DateTime.Now).ToString()
                }//Consigment
                ).Union(
                from r in itemList
                where r.GeneralItemID != null && r.GenFillinBy != null
                select new S_SupplierStocktakeItem
                {
                    ItemID = r.GeneralItemID.Value,
                    Available = r.GeneralAvailable,
                    QI = r.GeneralQI,
                    Block = r.GeneralBlock,
                    FillinBy = r.GenFillinBy,
                    FillinTime = (r.GenFillinTime??DateTime.Now).ToString()
                }//general
                );
            supplierList = qrySupplier.ToList();
            //foreach (var item in qrySupplier)
            //{
            //    supplierList.Add(new S_SupplierStocktakeItem
            //    {
            //        Available = item.Available,
            //        Block = item.Block,
            //        ItemID = item.ItemID,
            //        QI = item.QI
            //    });
            //}


            string itemStr = string.Empty;
            if (itemList.Count > 0)
            {
                Type[] types = new Type[] { typeof(S_StocktakeItem) };
                XmlSerializer xs = new XmlSerializer(typeof(List<S_StocktakeItem>), types);
                using (StringWriter sw = new StringWriter())
                {
                    xs.Serialize(sw, list);
                    itemStr = sw.ToString();
                }
            }

            string supplierItemStr = string.Empty;
            if (supplierList.Count > 0)
            {
                Type[] supplierTypes = new Type[] { typeof(S_SupplierStocktakeItem) };
                XmlSerializer suppliserXS = new XmlSerializer(typeof(List<S_SupplierStocktakeItem>), supplierTypes);
                using (StringWriter sw = new StringWriter())
                {
                    suppliserXS.Serialize(sw, supplierList);
                    supplierItemStr = sw.ToString();
                }
            }
            var itemsEnum = itemList.Where(i => i.WorkshopDetails!=null).SelectMany(i => i.WorkshopDetails);
            List<WorkshopStocktakeDetail> workshopDetails = new List<WorkshopStocktakeDetail>();
            if (itemsEnum!=null)
            {
                workshopDetails = itemsEnum.ToList();
            }
            //itemList.SelectMany(i => i.WorkshopDetails).ToList();

            string workshopDetailStr = Utils.SerializeToString<List<WorkshopStocktakeDetail>>(workshopDetails);
            DbParameter paramWorkshopDetails = Context.CreateDbParameter("@workshopDetails", System.Data.DbType.Xml, DBNull.Value, System.Data.ParameterDirection.Input);
            if (!string.IsNullOrEmpty(workshopDetailStr))
            {
                paramWorkshopDetails.Value = workshopDetailStr;
            }

            DbParameter paramID = Context.CreateDbParameter("@notificationID", System.Data.DbType.Int64, notification.NotificationID, System.Data.ParameterDirection.Input);
            DbParameter paramSGMItems = Context.CreateDbParameter("@sgmItems", System.Data.DbType.Xml, DBNull.Value, System.Data.ParameterDirection.Input);
            if (!string.IsNullOrEmpty(itemStr))
            {
                paramSGMItems.Value = itemStr;
            }
            DbParameter paramSupplierItems = Context.CreateDbParameter("@supplierItems", System.Data.DbType.Xml, DBNull.Value, System.Data.ParameterDirection.Input);
            if (!string.IsNullOrEmpty(supplierItemStr))
            {
                paramSupplierItems.Value = supplierItemStr;
            }
            Context.ExecuteNonQuery("sp_FillStocktakeResult", CommandType.StoredProcedure, paramID, paramSGMItems, paramSupplierItems,paramWorkshopDetails);
        }

        public void FillAdjustment(List<View_StocktakeResult> itemList)
        {

            // SGM ITEMS
            List<S_StocktakeItem> list = new List<S_StocktakeItem>();
            var qrySGM = from r in itemList
                         where r.SGMItemID != null
                         select new
                         {
                             r.SGMItemID,
                             r.SGMAvailableAdjust,
                             r.SGMQIAdjust,
                             r.SGMBlockAdjust
                         };
            foreach (var item in qrySGM)
            {
                list.Add(new S_StocktakeItem
                {
                    ItemID = item.SGMItemID.Value,
                    BlockAdjust = item.SGMBlockAdjust,
                    AvailableAdjust = item.SGMAvailableAdjust,
                    QIAdjust = item.SGMQIAdjust
                });
            }

            //supplier items
            List<S_SupplierStocktakeItem> supplierList = new List<S_SupplierStocktakeItem>();
            var qrySupplier = (
                from r in itemList
                where r.RDCItemID != null
                select new S_SupplierStocktakeItem
                {
                    ItemID = r.RDCItemID,
                    AvailableAdjust = r.RDCAvailableAdjust,
                    QIAdjust = r.RDCQIAdjust,
                    BlockAdjust = r.RDCBlockAdjust
                }//RDC
                ).Union(
                from r in itemList
                where r.RepairItemID != null
                select new S_SupplierStocktakeItem
                {
                    ItemID = r.RepairItemID.Value,
                    AvailableAdjust = r.RepairAvailableAdjust,
                    QIAdjust = r.RepairQIAdjust,
                    BlockAdjust = r.RepairBlockAdjust
                }//Repair
                ).Union(
                from r in itemList
                where r.CSMTItemID != null
                select new S_SupplierStocktakeItem
                {
                    ItemID = r.CSMTItemID.Value,
                    AvailableAdjust = r.CSMTAvailableAdjust,
                    QIAdjust = r.CSMTQIAdjust,
                    BlockAdjust = r.CSMTBlockAdjust
                }//Consigment
                ).Union(
                from r in itemList where r.GeneralItemID != null select new S_SupplierStocktakeItem { ItemID = r.GeneralItemID.Value, AvailableAdjust = r.GenAvailableAdjust, QIAdjust = r.GenQIAdjust, BlockAdjust = r.GenBlockAdjust }//general
                );
            supplierList = qrySupplier.ToList();
            //foreach (var item in qrySupplier)
            //{
            //    supplierList.Add(new S_SupplierStocktakeItem
            //    {
            //        AvailableAdjust = item.AvailableAdjust,
            //        BlockAdjust = item.BlockAdjust,
            //        ItemID = item.ItemID,
            //        QIAdjust = item.QIAdjust
            //    });
            //}


            string itemStr = string.Empty;
            if (itemList.Count > 0)
            {
                Type[] types = new Type[] { typeof(S_StocktakeItem) };
                XmlSerializer xs = new XmlSerializer(typeof(List<S_StocktakeItem>), types);
                using (StringWriter sw = new StringWriter())
                {
                    xs.Serialize(sw, list);
                    itemStr = sw.ToString();
                }
            }

            string supplierItemStr = string.Empty;
            if (supplierList.Count > 0)
            {
                Type[] supplierTypes = new Type[] { typeof(S_SupplierStocktakeItem) };
                XmlSerializer suppliserXS = new XmlSerializer(typeof(List<S_SupplierStocktakeItem>), supplierTypes);
                using (StringWriter sw = new StringWriter())
                {
                    suppliserXS.Serialize(sw, supplierList);
                    supplierItemStr = sw.ToString();
                }
            }

            //DbParameter paramID = Context.CreateDbParameter("@notificationID", System.Data.DbType.Int64, notification.NotificationID, System.Data.ParameterDirection.Input);
            DbParameter paramSGMItems = Context.CreateDbParameter("@sgmItems", System.Data.DbType.Xml, DBNull.Value, System.Data.ParameterDirection.Input);
            if (!string.IsNullOrEmpty(itemStr))
            {
                paramSGMItems.Value = itemStr;
            }
            DbParameter paramSupplierItems = Context.CreateDbParameter("@supplierItems", System.Data.DbType.Xml, DBNull.Value, System.Data.ParameterDirection.Input);
            if (!string.IsNullOrEmpty(supplierItemStr))
            {
                paramSupplierItems.Value = supplierItemStr;
            }
            Context.ExecuteNonQuery("sp_FillStocktakeAdjust", CommandType.StoredProcedure, paramSGMItems, paramSupplierItems);
        }

        public string CreateAnalyseReport(StocktakeNotification notice, User analyzedBy, out string reportCode, out Int64 reportID, string cacheKey, bool submit)
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                cacheKey = Guid.NewGuid().ToString();
            }
            List<View_StocktakeDetails> list = CacheHelper.GetCache(cacheKey) as List<View_StocktakeDetails>;
            if (list == null)
            {
                list = notice.DetailsView;
                CacheHelper.SetCache(cacheKey, list);
            }
            else
            {
                list.AddRange(notice.DetailsView);
            }
            if (submit)
            {
                CreateAnalyseReport(notice, analyzedBy, out  reportCode, out  reportID);
                CacheHelper.RemoveCache(cacheKey);
            }
            else
            {
                CacheHelper.SetCache(cacheKey, list);
                reportID = -1;
                reportCode = string.Empty;
            }
            return cacheKey;
        }

        public void CreateAnalyseReport(StocktakeNotification notice, User analyzedBy, out string reportCode, out Int64 reportID)
        {
            //notice.DetailsView = (from d in notice.DetailsView select new View_StocktakeDetails { DetailsID = d.DetailsID }).ToList();
            List<long> list = notice.DetailsView.Select(d => d.DetailsID).ToList();
            string detailsIDStr = Utils.SerializeToString(list);

            //string detailsIDStr = string.Empty;
            //foreach (var item in notice.DetailsView)
            //{
            //    detailsIDStr += "," + item.DetailsID;
            //}
            //if (!string.IsNullOrEmpty(detailsIDStr))
            //{
            //    detailsIDStr = detailsIDStr.Substring(1);
            //}
            DbParameter paramDetails = Context.CreateDbParameter("@details", System.Data.DbType.Xml, detailsIDStr, System.Data.ParameterDirection.Input);
            //paramDetails.Size = 80000;

            DbParameter paramNotiID = Context.CreateDbParameter("@noticeID", System.Data.DbType.Int64, notice.NotificationID, System.Data.ParameterDirection.Input);
            DbParameter paramAnalysedBy = Context.CreateDbParameter("@analysedBy", System.Data.DbType.Int32, analyzedBy.UserID, System.Data.ParameterDirection.Input);
            DbParameter paramReportCode = Context.CreateDbParameter("@reportCode", System.Data.DbType.String, DBNull.Value, System.Data.ParameterDirection.InputOutput);
            paramReportCode.Size = 20;
            DbParameter paramReportID = Context.CreateDbParameter("@reportID", System.Data.DbType.Int64, DBNull.Value, System.Data.ParameterDirection.InputOutput);
            Context.ExecuteNonQuery("sp_CreateAnalyseReport", CommandType.StoredProcedure, paramNotiID, paramDetails, paramAnalysedBy, paramReportCode, paramReportID);

            reportCode = paramReportCode.Value.ToString();
            reportID = Convert.ToInt64(paramReportID.Value);
        }


        public void CreateAnalyseRptByCondition(View_StocktakeDetails filter, User analyzedBy, out string reportCode, out Int64 reportID)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL();

            var qry = bll.QueryFullFilledDetails(filter);
            StocktakeNotification noti = new StocktakeNotification();
            noti.DetailsView = qry.ToList();
            reportCode = string.Empty;
            reportID = DefaultValue.LONG;
            if (noti.DetailsView != null && noti.DetailsView.Count > 0)
            {
                noti.NotificationID = noti.DetailsView[0].NotificationID.Value;
                //noti.NotificationID = filter.NotificationID.Value;
                CreateAnalyseReport(noti, analyzedBy, out reportCode, out reportID);
            }
        }

        public DataSet QueryAnalyseReport(View_DifferenceAnalyse filter, DateTime? timeStart, DateTime? timeEnd)
        {
            string notiCode = null;
            if (filter!=null)
            {
                notiCode = filter.NotificationCode;
            }
            DbParameter paramNotificationCode = Context.CreateDbParameter("@notificationCode", DbType.String, notiCode, ParameterDirection.Input);
            DataTable dt = Context.LoadDataTable("sp_GetAnalysisDetails1", CommandType.StoredProcedure, paramNotificationCode);
            string rowFilter = dt.DefaultView.RowFilter;
            if (!string.IsNullOrEmpty(filter.PartCode))
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "PartCode='" + filter.PartCode + "'";
            }
            if (!string.IsNullOrEmpty(filter.PartChineseName))
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "PartChineseName='" + filter.PartChineseName + "'";
            }
            if (!string.IsNullOrEmpty(filter.FollowUp))
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "FollowUp='" + filter.FollowUp + "'";
            }
            if (!string.IsNullOrEmpty(filter.AnalysedByUser))
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "AnalysedByUser='" + filter.AnalysedByUser + "'";
            }
            if (!string.IsNullOrEmpty(filter.Specs))
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "Specs='" + filter.Specs + "'";
            }
            if (filter.Status != null)
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "status=" + filter.Status;
            }
            if (!string.IsNullOrEmpty(filter.PartPlantCode))
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "PartPlantCode='" + filter.PartPlantCode + "'";
            }
            if (!string.IsNullOrEmpty(filter.Workshops))
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "Workshops like '%" + filter.Workshops + "%'";
            }
            if (!string.IsNullOrEmpty(filter.Segments))
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "Segments like '%" + filter.Segments + "%'";
            }
            if (timeStart != null)
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "TimeCreated>='" + timeStart.Value + "'";
            }
            if (timeEnd != null)
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                rowFilter += "TimeCreated<='" + timeEnd.Value + "'";
            }
            if (filter.ItemID != DefaultValue.LONG)
            {
                if (!string.IsNullOrEmpty(rowFilter))
                {
                    rowFilter += " AND ";
                }
                //rowFilter += "AvailableItem=" + filter.ItemID;
                rowFilter += "ItemID=" + filter.ItemID;
            }
            //if (!string.IsNullOrEmpty(filter.ProfitLossFilter))
            //{

            //    if (!string.IsNullOrEmpty(rowFilter))
            //    {
            //        rowFilter += " AND ";
            //    }
            //    rowFilter += "DiffAmount" + filter.ProfitLossFilter;
            //}
            //if (!string.IsNullOrEmpty(filter.DiffFilter))
            //{
            //    if (!string.IsNullOrEmpty(rowFilter))
            //    {
            //        rowFilter += " AND ";
            //    }
            //    rowFilter += "DiffAmount" + filter.DiffFilter;
            //}
            if (!string.IsNullOrEmpty(filter.TimesFilter))
            {
                string sql = "SELECT STUFF(( SELECT DISTINCT TOP 100 PERCENT ',' + CAST(PartID AS VARCHAR(50)) FROM    (SELECT PartID FROM dbo.View_StocktakeResult ";
                string tmpFilter = string.Empty;
                if (!string.IsNullOrEmpty(filter.ProfitLossFilter))
                {
                    tmpFilter += " WHERE DiffAmount " + filter.ProfitLossFilter;
                }
                if (!string.IsNullOrEmpty(filter.DiffFilter))
                {
                    if (!string.IsNullOrEmpty(tmpFilter))
                    {
                        tmpFilter += " AND ";
                    }
                    else
                    {
                        tmpFilter += " WHERE ";
                    }
                    tmpFilter += " DiffAmount " + filter.DiffFilter;
                }
                sql += tmpFilter + " GROUP BY PartID HAVING COUNT(1) " + filter.TimesFilter + ") AS t2 FOR XML PATH('') ), 1, 2, '') +''";
                string parts = Context.ExecuteScalar(sql, CommandType.Text).ToString();

                if (!string.IsNullOrEmpty(parts))
                {
                    //parts = parts.Replace("'{", "Convert('").Replace("}'", "', 'System.Guid')");
                    if (parts.StartsWith(","))
                    {
                        parts = parts.Substring(1);
                    }
                    if (!string.IsNullOrEmpty(rowFilter))
                    {
                        rowFilter += " AND ";
                    }
                    rowFilter += " PartID IN(" + parts + ") ";
                }
            }
            dt.DefaultView.RowFilter = rowFilter;
            DataTable filteredTable = dt.DefaultView.ToTable();

            #region summary columns
            DataColumn colDiffAmount = new DataColumn("DiffAmount", typeof(decimal));
            DataColumn colDiffSum = new DataColumn("DiffSum", typeof(decimal));
            //Available
            DataColumn colAvailable = new DataColumn("Available", typeof(decimal));
            colAvailable.Expression = "ISNULL(SGMAvailable,0)+ISNULL(RDCAvailable,0)+ISNULL(RepairAvailable,0)";
            DataColumn colSysAvailable = new DataColumn("SysAvailable", typeof(decimal));
            colSysAvailable.Expression = "ISNULL(Sys_Available_SGM,0)+ISNULL(Sys_Available_RDC,0)+ISNULL(Sys_Available_Repair,0)";
            DataColumn colAvailableDiff = new DataColumn("AvailableDiff", typeof(decimal));
            colDiffAmount.Expression = "AvailableDiff";
            colAvailableDiff.Expression = "ISNULL(Available,0)-ISNULL(SysAvailable,0)";
            DataColumn colAvailableDiffSum = new DataColumn("AvailableDiffSum", typeof(decimal));

            //QI
            DataColumn colQI = new DataColumn("QI", typeof(decimal));
            colQI.Expression = "ISNULL(SGMQI,0)+ISNULL(RDCQI,0)+ISNULL(RepairQI,0)";
            DataColumn colSysQI = new DataColumn("SysQI", typeof(decimal));
            colSysQI.Expression = "ISNULL(Sys_QI_SGM,0)+ISNULL(Sys_QI_RDC,0)+ISNULL(Sys_QI_Repair,0)";
            DataColumn colQIDiff = new DataColumn("QIDiff", typeof(decimal));
            colDiffAmount.Expression += "+[QIDiff]";
            colQIDiff.Expression = "ISNULL(QI,0)-ISNULL(SysQI,0)";
            DataColumn colQIDiffSum = new DataColumn("QIDiffSum", typeof(decimal));

            //Block
            DataColumn colBlock = new DataColumn("Block", typeof(decimal));
            colBlock.Expression = "ISNULL(SGMBlock,0)+ISNULL(RDCBlock,0)+ISNULL(RepairBlock,0)";
            DataColumn colSysBlock = new DataColumn("SysBlock", typeof(decimal));
            colSysBlock.Expression = "ISNULL(Sys_Block_SGM,0)+ISNULL(Sys_Block_RDC,0)+ISNULL(Sys_Block_Repair,0)";
            DataColumn colBlockDiff = new DataColumn("BlockDiff", typeof(decimal));
            colDiffAmount.Expression += "+[BlockDiff]";
            colBlockDiff.Expression = "ISNULL(Block,0)-ISNULL(SysBlock,0)";
            DataColumn colBlockDiffSum = new DataColumn("BlockDiffSum", typeof(decimal));

            //filteredTable.Columns.Add(colSysAvailable);
            //filteredTable.Columns.Add(colSysBlock);
            //filteredTable.Columns.Add(colSysQI);

            #endregion

            //add columns for each duns
            int availableItemIndex = filteredTable.Columns.IndexOf("AvailableItem");
            int qiItemIndex = filteredTable.Columns.IndexOf("QIItem");
            int count = qiItemIndex - availableItemIndex;

            for (int i = 1; i < count; i++)
            {
                colAvailable.Expression += "+ISNULL([" + filteredTable.Columns[availableItemIndex + i].ColumnName + "],0)";
                colQI.Expression += "+ISNULL([" + filteredTable.Columns[availableItemIndex + i + count].ColumnName + "],0)";
                colBlock.Expression += "+ISNULL([" + filteredTable.Columns[availableItemIndex + i + count * 2].ColumnName + "],0)";

                for (int j = 0; j < 3; j++)
                {
                    int tmpIndex = availableItemIndex + j * count + i;
                    string tmpColName = filteredTable.Columns[tmpIndex].ColumnName;

                    DataColumn colSys = filteredTable.Columns[tmpColName + "_Sys"];//new DataColumn(tmpColName + "_Sys", typeof(Int32));
                    DataColumn colDiff = new DataColumn(tmpColName + "_Diff", typeof(decimal));
                    colDiff.Expression = "[" + tmpColName + "]-[" + colSys.ColumnName + "]";
                    DataColumn colAdjust = new DataColumn(tmpColName + "_Adjust", typeof(Int32));
                    //filteredTable.Columns.Add(colSys);
                    filteredTable.Columns.Add(colAdjust);
                    filteredTable.Columns.Add(colDiff);
                    DataColumn colDunsDiffSum = new DataColumn(tmpColName + "_DiffSum", typeof(decimal));
                    colDunsDiffSum.Expression = "[" + colDiff.ColumnName + "]*Price";
                    filteredTable.Columns.Add(colDunsDiffSum);

                    if (tmpColName.EndsWith("_Available"))
                    {
                        colAvailableDiffSum.Expression += ((string.IsNullOrEmpty(colAvailableDiffSum.Expression)) ? "" : "+") + "ISNULL([" + colDiff.ColumnName + "]*Price,0)";
                    }

                    if (tmpColName.EndsWith("_QI"))
                    {
                        colQIDiffSum.Expression += ((string.IsNullOrEmpty(colQIDiffSum.Expression)) ? "" : "+") + "ISNULL([" + colDiff.ColumnName + "]*Price,0)";
                    }

                    if (tmpColName.EndsWith("_Block"))
                    {
                        colBlockDiffSum.Expression += ((string.IsNullOrEmpty(colBlockDiffSum.Expression)) ? "" : "+") + "ISNULL([" + colDiff.ColumnName + "]*Price,0)";
                    }
                }
            }

            #region add summary columns
            filteredTable.Columns.Add(colAvailable);
            filteredTable.Columns.Add(colBlock);
            filteredTable.Columns.Add(colQI);

            filteredTable.Columns.Add(colSysAvailable);
            filteredTable.Columns.Add(colSysBlock);
            filteredTable.Columns.Add(colSysQI);


            #endregion

            #region add calculation columns
            DataColumn colSGMAvailableDiff = new DataColumn("SGMAvailableDiff", typeof(decimal), "SGMAvailable-Sys_Available_SGM");
            DataColumn colSGMAvailableDiffSum = new DataColumn("SGMAvailableDiffSum", typeof(decimal), "SGMAvailableDiff*Price");
            DataColumn colRDCAvailableDiff = new DataColumn("RDCAvailableDiff", typeof(decimal), "RDCAvailable-Sys_Available_RDC");
            DataColumn colRDCAvailableDiffSum = new DataColumn("RDCAvailableDiffSum", typeof(decimal), "RDCAvailableDiff*Price");
            DataColumn colRepairAvailableDiff = new DataColumn("RepairAvailableDiff", typeof(decimal), "RepairAvailable-Sys_Available_Repair");
            DataColumn colRepairAvailableDiffSum = new DataColumn("RepairAvailableDiffSum", typeof(decimal), "RepairAvailableDiff*Price");

            DataColumn colSGMQIDiff = new DataColumn("SGMQIDiff", typeof(decimal), "SGMQI-Sys_QI_SGM");
            DataColumn colSGMQIDiffSum = new DataColumn("SGMQIDiffSum", typeof(decimal), "SGMQIDiff*Price");
            DataColumn colRDCQIDiff = new DataColumn("RDCQIDiff", typeof(decimal), "RDCQI-Sys_QI_RDC");
            DataColumn colRDCQIDiffSum = new DataColumn("RDCQIDiffSum", typeof(decimal), "RDCQIDiff*Price");
            DataColumn colRepairQIDiff = new DataColumn("RepairQIDiff", typeof(decimal), "RepairQI-Sys_QI_Repair");
            DataColumn colRepairQIDiffSum = new DataColumn("RepairQIDiffSum", typeof(decimal), "RepairQIDiff*Price");
            DataColumn colSGMBlockDiff = new DataColumn("SGMBlockDiff", typeof(decimal), "SGMBlock-Sys_Block_SGM");
            DataColumn colSGMBlockDiffSum = new DataColumn("SGMBlockDiffSum", typeof(decimal), "SGMBlockDiff*Price");
            DataColumn colRDCBlockDiff = new DataColumn("RDCBlockDiff", typeof(decimal), "RDCBlock-Sys_Block_RDC");
            DataColumn colRDCBlockDiffSum = new DataColumn("RDCBlockDiffSum", typeof(decimal), "RDCBlockDiff*Price");
            DataColumn colRepairBlockDiff = new DataColumn("RepairBlockDiff", typeof(decimal), "RepairBlock-Sys_Block_Repair");
            DataColumn colRepairBlockDiffSum = new DataColumn("RepairBlockDiffSum", typeof(decimal), "RepairBlockDiff*Price");
            filteredTable.Columns.Add(colSGMAvailableDiff);
            filteredTable.Columns.Add(colSGMAvailableDiffSum);
            filteredTable.Columns.Add(colSGMBlockDiff);
            filteredTable.Columns.Add(colSGMBlockDiffSum);
            filteredTable.Columns.Add(colSGMQIDiff);
            filteredTable.Columns.Add(colSGMQIDiffSum);
            filteredTable.Columns.Add(colRDCAvailableDiff);
            filteredTable.Columns.Add(colRDCAvailableDiffSum);
            filteredTable.Columns.Add(colRDCBlockDiff);
            filteredTable.Columns.Add(colRDCBlockDiffSum);
            filteredTable.Columns.Add(colRDCQIDiff);
            filteredTable.Columns.Add(colRDCQIDiffSum);
            filteredTable.Columns.Add(colRepairAvailableDiff);
            filteredTable.Columns.Add(colRepairAvailableDiffSum);
            filteredTable.Columns.Add(colRepairBlockDiff);
            filteredTable.Columns.Add(colRepairBlockDiffSum);
            filteredTable.Columns.Add(colRepairQIDiff);
            filteredTable.Columns.Add(colRepairQIDiffSum);

            colAvailableDiffSum.Expression += ((string.IsNullOrEmpty(colAvailableDiffSum.Expression)) ? "" : "+") + "ISNULL([" + colSGMAvailableDiffSum.ColumnName + "],0)+ISNULL([" + colRDCAvailableDiffSum.ColumnName + "],0)+ISNULL([" + colRepairAvailableDiffSum.ColumnName + "],0)";
            colQIDiffSum.Expression += ((string.IsNullOrEmpty(colQIDiffSum.Expression)) ? "" : "+") + "ISNULL([" + colSGMQIDiffSum.ColumnName + "],0)+ISNULL([" + colRDCQIDiffSum.ColumnName + "],0)+ISNULL([" + colRepairQIDiffSum.ColumnName + "],0)";
            colBlockDiffSum.Expression += ((string.IsNullOrEmpty(colBlockDiffSum.Expression)) ? "" : "+") + "ISNULL([" + colSGMBlockDiffSum.ColumnName + "],0)+ISNULL([" + colRDCBlockDiffSum.ColumnName + "],0)+ISNULL([" + colRepairBlockDiffSum.ColumnName + "],0)";

            filteredTable.Columns.Add(colAvailableDiff);
            filteredTable.Columns.Add(colBlockDiff);
            filteredTable.Columns.Add(colQIDiff);
            filteredTable.Columns.Add(colAvailableDiffSum);
            filteredTable.Columns.Add(colBlockDiffSum);
            filteredTable.Columns.Add(colQIDiffSum);
            filteredTable.Columns.Add(colDiffAmount);
            colDiffSum.Expression = "ISNULL([" + colDiffAmount.ColumnName + "]*Price,0)";
            filteredTable.Columns.Add(colDiffSum);
            #endregion
            string diffFilter = string.Empty;
            if (!string.IsNullOrEmpty(filter.ProfitLossFilter))
            {
                diffFilter += "DiffAmount" + filter.ProfitLossFilter;
            }
            if (!string.IsNullOrEmpty(filter.DiffFilter))
            {
                if (!string.IsNullOrEmpty(diffFilter))
                {
                    diffFilter += " AND ";
                }
                diffFilter += "DiffAmount" + filter.DiffFilter;
            }
            if (!string.IsNullOrEmpty(filter.DiffSumFilter))
            {
                if (!string.IsNullOrEmpty(diffFilter))
                {
                    diffFilter += " AND ";
                }
                diffFilter += "DiffSum" + filter.DiffSumFilter;
            }
            if (!string.IsNullOrEmpty(diffFilter))
            {
                filteredTable.DefaultView.RowFilter = diffFilter; 
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(filteredTable.DefaultView.ToTable());//(filteredTable);
            return ds;
        }

        public void DeleteAnalyseItems(List<DiffAnalyseReportItem> itemsList)
        {
            if (itemsList.Count > 0)
            {
                Type[] types = new Type[] { typeof(DiffAnalyseReportItem) };
                string itemsStr = string.Empty;
                //XmlSerializer xs = new XmlSerializer(typeof(List<DiffAnalyseReportItem>), types);
                //using (StringWriter sw = new StringWriter())
                //{
                //    xs.Serialize(sw, itemsList);
                //    itemsStr = sw.ToString();
                //}
                itemsStr = Utils.SerializeToString<List<DiffAnalyseReportItem>>(types, itemsList);

                DbParameter paramItems = Context.CreateDbParameter("@items", System.Data.DbType.Xml, itemsStr, System.Data.ParameterDirection.Input);
                Context.ExecuteNonQuery("sp_DeleteAnalyseItems", CommandType.StoredProcedure, paramItems);
            }
        }


        public DataSet GetAnalyseItem(DiffAnalyseReportItem item)
        {
            return QueryAnalyseReport(new View_DifferenceAnalyse { ItemID = item.ItemID }, null, null);
        }

        public IQueryable<View_StocktakeItem> QueryStocktakeItem(View_StocktakeItem filter)
        {
            IQueryable<View_StocktakeItem> qry = Context.View_StocktakeItem;

            if (!string.IsNullOrEmpty(filter.PartCode))
            {
                qry = from q in qry where string.Equals(q.PartCode, filter.PartCode) select q;
            }
            if (!string.IsNullOrEmpty(filter.PartPlantCode))
            {
                qry = from q in qry where string.Equals(q.PartPlantCode, filter.PartPlantCode) select q;
            }

            if (!string.IsNullOrEmpty(filter.DUNS))
            {
                qry = from q in qry where string.Equals(q.DUNS, filter.DUNS) select q;
            }


            if (filter.LogisticsSysSLOC != null && !string.IsNullOrEmpty(filter.LogisticsSysSLOC))
            {
                qry = from q in qry where string.Equals(q.LogisticsSysSLOC, filter.LogisticsSysSLOC) select q;
            }
            if (!string.IsNullOrEmpty(filter.NotificationCode))
            {
                qry = from q in qry where string.Equals(q.NotificationCode, filter.NotificationCode) select q;
            }
            return qry;
        }

        public void ImportAdjustment(List<View_StocktakeItem> list)
        {
            Type[] types = new Type[] { typeof(View_StocktakeItem) };
            string itemsStr = Utils.SerializeToString<List<View_StocktakeItem>>(types, list);

            DbParameter paramItems = Context.CreateDbParameter("@adjustItems", System.Data.DbType.Xml, itemsStr, System.Data.ParameterDirection.Input);
            Context.ExecuteNonQuery("sp_ImportAdjustment", CommandType.StoredProcedure, paramItems);
        }
    }
}
