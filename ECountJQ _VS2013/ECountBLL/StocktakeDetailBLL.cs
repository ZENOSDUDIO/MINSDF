using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data.Objects;
using System.Transactions;
using System.Data.EntityClient;
using SGM.Common.Utility;

namespace SGM.ECount.BLL
{
    public class StocktakeDetailBLL : BaseGenericBLL<StocktakeDetails>
    {
        public StocktakeDetailBLL()
            : base("StocktakeDetailBLL")
        { }

        public StocktakeDetailBLL(ECountContext context)
            : base(context,"StocktakeDetailBLL")
        {

        }

        public List<StocktakeDetails> GetDetailsByUser(StocktakeNotification notification, User user)
        {
            return new List<StocktakeDetails>();
        }

        public List<StocktakeDetails> QueryDetails(StocktakeDetails details, DateTime? dateStart, DateTime? dateEnd)
        {
            return BuildQuery(details, dateStart, dateEnd).ToList();
        }

        public void AddDetails(StocktakeDetails details)
        {
            StocktakeDetails tmpDetails = _context.StocktakeDetails.FirstOrDefault(d => d.StocktakeRequest.RequestID == details.StocktakeRequest.RequestID && details.Part.PartID == d.Part.PartID);

            if (tmpDetails != null)
            {
                _context.DeleteObject(tmpDetails);
            }
            AddObject(details, false);
            _context.SaveChanges();


        }

        public void DeleteDetails(StocktakeDetails details)
        {
            DeleteObject(details, true);
        }

        internal IQueryable<StocktakeDetails> BuildQuery(StocktakeDetails details, DateTime? dateStart, DateTime? dateEnd)
        {
            IQueryable<StocktakeDetails> detailsQry = _context.StocktakeDetails;
            //string queryStr = "SELECT VALUE d FROM ECountContext.StocktakeDetails AS d";
            StringBuilder queryWhere = new StringBuilder();

            if (details != null)
            {
                if (details.StocktakeType != null)
                {
                    if (details.StocktakeType.TypeID != 0)
                    {
                        detailsQry = detailsQry.Where(d => d.StocktakeType.TypeID == details.StocktakeType.TypeID);
                        //queryWhere.Append(" d.StocktakeType.TypeID=" + details.StocktakeType.TypeID + " AND"); 
                    }
                }
                //request filter
                if (details.StocktakeRequest != null)
                {
                    if (!string.IsNullOrEmpty(details.StocktakeRequest.RequestNumber))
                    {
                        detailsQry = detailsQry.Where(d => d.StocktakeRequest.RequestNumber == details.StocktakeRequest.RequestNumber);
                        //queryWhere.Append(" d.StocktakeRequest.RequestNumber='" + details.StocktakeRequest.RequestNumber + "' AND");
                    }
                    if (!string.IsNullOrEmpty(details.StocktakeRequest.RequestBy.UserName))
                    {
                        detailsQry = detailsQry.Where(d => d.StocktakeRequest.RequestBy.UserName == details.StocktakeRequest.RequestBy.UserName);
                        //queryWhere.Append(" d.StocktakeRequest.RequestBy.UserName='" + details.StocktakeRequest.RequestBy.UserName + "' AND");
                    }
                    detailsQry = detailsQry.Where(d => d.StocktakeRequest.IsStatic == details.StocktakeRequest.IsStatic);
                    //queryWhere.Append(" d.StocktakeRequest.IsStatic=" + details.StocktakeRequest.IsStatic + " AND");

                }
                //notification filter
                if (details.StocktakeNotification != null)
                {
                    if (!string.IsNullOrEmpty(details.StocktakeNotification.NotificationCode))
                    {
                        detailsQry = detailsQry.Where(d => d.StocktakeNotification.NotificationCode == details.StocktakeNotification.NotificationCode);
                        //queryWhere.Append(" d.StocktakeNotification.NotificationCode='" + details.StocktakeNotification.NotificationCode + "' AND");
                    }
                }
                if (details.Part != null)
                {
                    if (!string.IsNullOrEmpty(details.Part.PartCode))
                    {

                        detailsQry = detailsQry.Where(d => d.Part.PartCode == details.Part.PartCode);
                        //queryWhere.Append(" d.Part.PartCode='" + details.Part.PartCode + "' AND");
                    }
                }
            }
            if (dateStart != null)
            {
                detailsQry = detailsQry.Where(d => d.StocktakeRequest.DateRequest >= dateStart);
                //queryWhere.Append(" d.StocktakeRequest.DateRequest>=" + dateStart.Value.ToString() + " AND");
            }
            if (dateEnd != null)
            {

                detailsQry = detailsQry.Where(d => d.StocktakeRequest.DateRequest <= dateEnd);
                //queryWhere.Append(" d.StocktakeRequest.DateRequest<=" + dateEnd.Value.ToString() + " AND");
            }
            //}
            //if (queryWhere.Length > 0)
            //{
            //    queryStr += " WHERE " + queryWhere.ToString();
            //} 
            //queryStr = queryStr.Substring(0, queryStr.Length - 3);
            return detailsQry;// queryStr;
        }

        public IQueryable<View_StocktakeDetails> GetNewRequestDetails(bool isStatic)
        {
            View_StocktakeDetails condition = new View_StocktakeDetails { RequestIsStatic = isStatic, ReadyForCount = true };
            IQueryable<View_StocktakeDetails> qryDetails = this.QueryDetails(condition, null, null);
            qryDetails = qryDetails.Where(d => d.NotificationID == null);
            return qryDetails;
        }

        public List<View_StocktakeDetails> GetNewRequestDetails(View_StocktakeDetails filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<View_StocktakeDetails> detailsQry = GetNewRequestDetails(filter);
            detailsQry = detailsQry.OrderBy(d => d.PartPlantCode).ThenBy(d => d.PartCode);
            List<long> detailsIDList = detailsQry.Select(d => d.DetailsID).ToList();
            List<View_StocktakeDetails> result = this.GetQueryByPage(detailsQry, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            return result;
        }
        public IQueryable<View_StocktakeDetails> GetNewRequestDetails(View_StocktakeDetails filter)
        {
            filter.ReadyForCount = true;
            //View_StocktakeDetails condition = new View_StocktakeDetails { RequestIsStatic = isStatic, ReadyForCount = true };

            IQueryable<View_StocktakeDetails> qryDetails = this.QueryDetails(filter, null, null);
            qryDetails = qryDetails.Where(d => d.NotificationID == null);
            return qryDetails;
        }

        public IQueryable<View_StocktakeDetails> GetNewRequestDetails(bool isStatic, Plant plant)
        {
            int plantID = plant.PlantID;
            View_StocktakeDetails condition = new View_StocktakeDetails { RequestIsStatic = isStatic, PartPlantID = plantID, ReadyForCount = true };
            IQueryable<View_StocktakeDetails> detailsQry = this.QueryDetails(condition, null, null);
            detailsQry = detailsQry.Where(d => d.NotificationID == null);
            //IQueryable<View_StocktakeDetails> detailsQry = Context.View_StocktakeDetails.Where(d => d.NotificationID == null && d.RequestIsStatic == isStatic && d.PartPlantID == plantID);
            return detailsQry;
        }

        public List<View_StocktakeDetails> GetNewRequestDetails(bool isStatic, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<View_StocktakeDetails> detailsQry = GetNewRequestDetails(isStatic);
            detailsQry = detailsQry.OrderBy(d => d.PartPlantCode).ThenBy(d => d.PartCode);
            List<long> detailsIDList = detailsQry.Select(d => d.DetailsID).ToList();
            List<View_StocktakeDetails> result = this.GetQueryByPage(detailsQry, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            return result;
        }

        public List<View_StocktakeDetails> GetNewRequestDetails(bool isStatic, Plant plant, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            //IQueryable<View_StocktakeDetails> detailsQry = GetNewRequestDetails(isStatic, plant);
            //detailsQry = detailsQry.OrderBy(d => d.PartPlantCode).ThenBy(d => d.PartCode);
            //List<Guid> detailsIDList = detailsQry.Select(d => d.DetailsID).ToList();
            //List<View_StocktakeDetails> result = this.GetQueryByPage(detailsQry, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            return GetNewRequestDetails(null,null,isStatic, plant, pageSize, pageNumber, out pageCount, out itemCount);
        }


        public List<View_StocktakeDetails> GetNewRequestDetails(List<View_StocktakeDetails> filter,List<View_StocktakeDetails> addition, bool isStatic, Plant plant, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            List<View_StocktakeDetails> list = GetNewRequestDetails(isStatic, plant).ToList();
            //IQueryable<View_StocktakeDetails> detailsQry = GetNewRequestDetails(isStatic, plant);

            if (filter!=null&&filter.Count>0)
            {
                foreach (var item in filter)
                {
                    long detailsID=item.DetailsID;
                    int index = list.FindIndex(d => d.DetailsID == detailsID);
                    if (index>=0)
                    {
                        list.RemoveAt(index);
                    }
                }
                //detailsQry = detailsQry.Except(filter, View_StocktakeDetailsEqualityComparer.Instance); 
            }
            if (addition!=null&&addition.Count>0)
            {
                foreach (var item in addition)
                {
                    long detailsID = item.DetailsID;
                    int index = list.FindIndex(d => d.DetailsID == detailsID);
                    if (index<0)
                    {
                        list.Add(item);
                    }
                }
            }
            IQueryable<View_StocktakeDetails> detailsQry = list.AsQueryable().OrderBy(d => d.RequestNumber).ThenBy(d => d.DefaultPriority);//OrderBy(d => d.PartPlantCode).ThenBy(d => d.PartCode);
            //List<Guid> detailsIDList = detailsQry.Select(d => d.DetailsID).ToList();
            List<View_StocktakeDetails> result = this.GetQueryByPage(detailsQry, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            return result;
        }

        public IQueryable<View_StocktakeDetails> QueryDetails(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd)
        {
            IQueryable<View_StocktakeDetails> detailsQry = Context.View_StocktakeDetails;
            if (condition.RequestID != DefaultValue.INT)
            {
                detailsQry = detailsQry.Where(d => d.RequestID == condition.RequestID);
            }
            if (condition.NotificationID != null)
            {
                detailsQry = detailsQry.Where(d => d.NotificationID == condition.NotificationID);
            }
            if (!string.IsNullOrEmpty(condition.RequestNumber))
            {
                detailsQry = detailsQry.Where(d => d.RequestNumber == condition.RequestNumber);
            }
            if (!string.IsNullOrEmpty(condition.NotificationCode))
            {
                detailsQry = detailsQry.Where(d => d.NotificationCode == condition.NotificationCode);
            }
            if (condition.ReadyForCount != null)
            {
                detailsQry = detailsQry.Where(d => d.ReadyForCount == condition.ReadyForCount);
            }
            if (!string.IsNullOrEmpty(condition.RequestUser))
            {
                detailsQry = detailsQry.Where(d => d.RequestUser == condition.RequestUser.Trim());
            }
            if (condition.PlantID != null)
            {
                detailsQry = detailsQry.Where(d => d.PlantID == condition.PlantID);
            }
            if (condition.NotifyPlantID != null)
            {
                detailsQry = detailsQry.Where(d => d.NotifyPlantID == condition.NotifyPlantID);
            }
            if (condition.PartPlantID != null)
            {
                detailsQry = detailsQry.Where(d => d.PartPlantID == condition.PartPlantID);
            }
            if (!string.IsNullOrEmpty(condition.Workshops))
            {
                detailsQry = detailsQry.Where(d=>(","+d.Workshops+",").Contains(","+condition.Workshops+","));// (d => d.Workshops.Contains(condition.Workshops));
            }
            if (!string.IsNullOrEmpty(condition.Segments))
            {
                detailsQry = detailsQry.Where(d => ("," + d.Segments + ",").Contains("," + condition.Segments + ",")); ;
            }
            if (condition.StocktakeType != null)
            {
                detailsQry = detailsQry.Where(d => d.StocktakeType == condition.StocktakeType);
            }
            if (!string.IsNullOrEmpty(condition.PartCode))
            {
                detailsQry = detailsQry.Where(d => d.PartCode == condition.PartCode.Trim());
            }
            if (!string.IsNullOrEmpty(condition.DUNS))
            {
                detailsQry = detailsQry.Where(d => string.Equals(d.DUNS, condition.DUNS));
            }
            if (dateStart != null)
            {
                detailsQry = detailsQry.Where(d => d.DateRequest >= dateStart);
            }
            if (dateEnd != null)
            {
                detailsQry = detailsQry.Where(d => d.DateRequest <= dateEnd);
            }
            if (!string.IsNullOrEmpty(condition.PartChineseName))
            {
                detailsQry = detailsQry.Where(d => d.PartChineseName == condition.PartChineseName.Trim());
            }
            if (condition.RequestIsStatic != null)
            {
                detailsQry = detailsQry.Where(d => d.RequestIsStatic == condition.RequestIsStatic);
            }
            if (condition.NotifyIsStatic != null)
            {
                detailsQry = detailsQry.Where(d => d.NotifyIsStatic == condition.NotifyIsStatic);
            }
            if (condition.Status != null)
            {
                detailsQry = detailsQry.Where(d => d.Status == condition.Status);
            }
            if (condition.RequestStatus != null)
            {
                detailsQry = detailsQry.Where(d => d.RequestStatus == condition.RequestStatus);
            }
            if (condition.CreatedBy != null)
            {
                detailsQry = detailsQry.Where(d => d.CreatedBy == condition.CreatedBy);
            }
            return detailsQry;
        }


        public IQueryable<View_StocktakeDetails> QueryNotificationDetails(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd)
        {
            //StocktakeDetailBLL bll = new StocktakeDetailBLL();

            IQueryable<View_StocktakeDetails> result = QueryDetails(condition, null, null);
            result = result.Where(d => d.NotificationID != null);
            if (dateStart != null)
            {
                result = result.Where(d => d.DateCreated >= dateStart.Value);
            }
            if (dateEnd != null)
            {
                result = result.Where(d => d.DateCreated <= dateEnd.Value);
            }
            if (planDateStart != null)
            {
                result = result.Where(d => d.PlanDate >= planDateStart);
            }
            if (planDateEnd != null)
            {
                result = result.Where(d => d.PlanDate <= planDateEnd);
            }
            return result;


        }

        public IQueryable<View_StocktakeDetails> QueryNotificationDetails(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd)
        {
            var qry = QueryNotificationDetails(condition, dateStart, dateEnd, planDateStart, planDateEnd);
            if (locationID != null)
            {
                qry = from d in qry join n in Context.StocktakeLocation on d.NotificationID.Value equals n.StocktakeNotification.NotificationID where n.StoreLocation.LocationID == locationID.Value select d;
                //qry = from d in qry join q in QueryNotificationDetails(condition, dateStart, dateEnd, planDateStart, planDateEnd) on d.DetailsID equals q.DetailsID select d;
                return qry;
            }
            else
            {
                return QueryNotificationDetails(condition, dateStart, dateEnd, planDateStart, planDateEnd);
            }
            //var result=qry;
            //if (locationID != null)
            //{
            //    long id = locationID.Value;
            //    var notifications = Context.StocktakeLocation.Where(l => l.StoreLocation.LocationID == id).Select(l => l.StocktakeNotification);
            //    //List<View_StocktakeDetails> list =  result.ToList();
            //    //result = qry.Where(d => notifications.Contains(d.NotificationID.Value)).AsQueryable();
            //    result = from q in qry join n in notifications on q.NotificationID equals n.NotificationID select q;

            //}
            //return result;
        }
        public IQueryable<View_StocktakeDetails> QueryResultDetails(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd)
        {
            IQueryable<View_StocktakeDetails> qry = this.QueryNotificationDetails(condition, dateStart, dateEnd, planDateStart, planDateEnd);
            qry = qry.Where(q => q.Status >= Consts.STOCKTAKE_PUBLISHED);

            //if (condition.LocationID!=null)
            //{
            //    qry = qry.Where(q => q.LocationID == condition.LocationID);
            //}
            return qry;
        }


        public IQueryable<View_StocktakeDetails> QueryResultDetailsByPage(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<View_StocktakeDetails> result = this.QueryResultDetails(condition, dateStart, dateStart, planDateStart, planDateEnd).OrderBy(d => d.NotificationCode).ThenBy(d => d.PartCode);
            return this.GetQueryByPage(result, pageSize, pageNumber, out pageCount, out itemCount);
        }


        public IQueryable<View_StocktakeDetails> QueryNotificationDetailsByPage(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<View_StocktakeDetails> result = this.QueryNotificationDetails(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd).OrderBy(d => d.NotificationCode).ThenBy(d => d.PartCode);
            return this.GetQueryByPage(result, pageSize, pageNumber, out pageCount, out itemCount);
        }


        public IQueryable<View_StocktakeDetails> QueryFullFilledDetails(View_StocktakeDetails condition)
        {
            IQueryable<View_StocktakeDetails> qry = QueryNotificationDetails(condition, null, null, null, null);

            qry = qry.Where(q => q.Status >= Consts.STOCKTAKE_COMPLETE);
            return qry;
        }


        public IQueryable<View_StocktakeDetails> QueryFullFilledDetailsByPage(View_StocktakeDetails condition, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<View_StocktakeDetails> qry = QueryFullFilledDetails(condition).Where(d=>d.ReportID==null).OrderBy(q => q.NotificationCode).ThenBy(q => q.PartCode);
            
            return this.GetQueryByPage(qry, pageSize, pageNumber, out pageCount, out itemCount);
        }


        private class View_StocktakeDetailsEqualityComparer : IEqualityComparer<View_StocktakeDetails>
        {
            public static readonly View_StocktakeDetailsEqualityComparer Instance = new View_StocktakeDetailsEqualityComparer();
            private View_StocktakeDetailsEqualityComparer()
            { }

            #region IEqualityComparer<View_StocktakeDetails> Members

            public bool Equals(View_StocktakeDetails x, View_StocktakeDetails y)
            {
                if (!(x == null || y == null) && x.DetailsID == y.DetailsID)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(View_StocktakeDetails obj)
            {
                return obj.DetailsID.GetHashCode();
            }

            #endregion
        }

        public List<View_StocktakeDetails> QueryDetails(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<View_StocktakeDetails> detailsQry = QueryDetails(condition, dateStart, dateEnd).OrderBy(d => d.RequestNumber).ThenBy(d => d.PartPlantCode).ThenBy(d => d.PartCode);
            return this.GetQueryByPage<View_StocktakeDetails>(detailsQry, pageSize, pageNumber, out pageCount, out itemCount).ToList();
        }

        public IQueryable<View_StocktakeDetails> GetNotificationDetails(StocktakeNotification notification)
        {
            long notificationID = notification.NotificationID;
            IQueryable<View_StocktakeDetails> detailsQry = Context.View_StocktakeDetails.Where(d => d.NotificationID == notificationID);
            return detailsQry;
        }

        public List<View_StocktakeDetails> GetNotiDetailsByPage(StocktakeNotification notification, List<View_StocktakeDetails> filter, List<View_StocktakeDetails> addition, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            List<View_StocktakeDetails> list = GetNotificationDetails(notification).ToList();
            IQueryable<View_StocktakeDetails> detailsQry;
            if (filter!=null&&filter.Count>0)
            {
                foreach (var item in filter)
                {
                    int index = list.FindIndex(d => d.DetailsID == item.DetailsID);
                    if (index>=0)
                    {
                        list.RemoveAt(index);
                    }
                }
            }
            if (addition!=null&&addition.Count>0)
            {

                foreach (var item in addition)
                {
                    int index = list.FindIndex(d => d.DetailsID == item.DetailsID);
                    if (index <0)
                    {
                        list.Add(item);
                    }
                }
            }
            detailsQry = list.AsQueryable().OrderBy(d=>d.WorkLocation);//.OrderBy(d => d.PartCode);
            List<View_StocktakeDetails> result = this.GetQueryByPage(detailsQry, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            return result;
        }

        public IQueryable<View_StocktakeDetails> QueryStocktakeDetailsScope(View_StocktakeDetails stocktakeDetails, string startCode, string endCode)
        {
            var stocktakeDetailsQry = this.QueryStocktakeDetails(stocktakeDetails);
            if (!string.IsNullOrEmpty(startCode))
            {
                stocktakeDetailsQry = stocktakeDetailsQry.Where(p => string.Compare(p.PartCode, startCode) >= 0);
            }
            if (!string.IsNullOrEmpty(endCode))
            {
                stocktakeDetailsQry = stocktakeDetailsQry.Where(p => string.Compare(p.PartCode, endCode) <= 0);
            }
            return stocktakeDetailsQry.OrderBy(p => p.PartCode);
        }

        private IQueryable<View_StocktakeDetails> QueryStocktakeDetails(View_StocktakeDetails stocktakeDetails)
        {
            IQueryable<View_StocktakeDetails> stocktakeQry = Context.View_StocktakeDetails;

            return QueryStocktakeDetails(stocktakeDetails, stocktakeQry);
        }

        private IQueryable<View_StocktakeDetails> QueryStocktakeDetails(View_StocktakeDetails stocktakeDetails, IQueryable<View_StocktakeDetails> stocktakeQry)
        {
            if (stocktakeDetails != null)
            {
                if (!string.IsNullOrEmpty(stocktakeDetails.NotificationCode))
                {
                    stocktakeQry = stocktakeQry.Where(p => p.NotificationCode == stocktakeDetails.NotificationCode);
                }
            }
            return stocktakeQry;
        }
    }
}
