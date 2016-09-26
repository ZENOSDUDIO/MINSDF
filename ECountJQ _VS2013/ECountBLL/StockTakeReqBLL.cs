using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data.Objects;
using System.Transactions;
using System.Data.EntityClient;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.Data;
using Microsoft.Data.Extensions;
using SGM.Common.Cache;
using SGM.Common.Utility;

namespace SGM.ECount.BLL
{
    public class StockTakeReqBLL : BaseGenericBLL<StocktakeRequest>
    {
        public StockTakeReqBLL()
            : base("StocktakeRequest")
        { }


        public StockTakeReqBLL(ECountContext context)
            : base(context, "StocktakeRequest")
        { }

        public List<StocktakeRequest> GetRequestByUser(User user)
        {
            return new List<StocktakeRequest>();
        }

        //public List<StocktakeRequest> QueryRequest(StocktakeDetails condition, DateTime dateStart, DateTime dateEnd)
        //{
        //    string esqlStr = "SELECT FROM ECountContext.StocktakeRequest AS r INNER JOIN StocktakeType ON r. LEFT OUTER JOIN ECountContext.StocktakeDetails AS d ON  LEFT OUTER JOIN ECountContext.Part AS p ON LEFT OUTER JOIN ECountContext. AS  ON LEFT OUTER JOIN ECountContext. AS  ON LEFT OUTER JOIN ECountContext. AS  ON LEFT OUTER JOIN ECountContext. AS  ON LEFT OUTER JOIN ECountContext. AS  ON";
        //    StocktakeDetailBLL detailBLL = new StocktakeDetailBLL();
        //    string queryWhere = detailBLL.BuildQuery(condition,dateStart,dateEnd);
        //    List<StocktakeRequest> requestList = new ObjectQuery<StocktakeDetails>(queryWhere, _context).Include("Part.PartStatus").Include("Part.Plant").Include("Part.Supplier").Include("Part.PartCategory").Include("Part.CycleCountLevel").Select(d => d.StocktakeRequest).ToList();
        //    return requestList;
        //    //StocktakeRequest r;
        //    //StringBuilder queryWhere = new StringBuilder();
        //    //string queryString = "SELECT VALUE r FROM ECountContext.StocktakeRequest as r";
        //    //if (condition != null)
        //    //{

        //    //    if (condition.StocktakeRequest != null)
        //    //    {
        //    //        if (condition.StocktakeRequest.RequestID != 0)
        //    //        {
        //    //            queryWhere.Append(" r.RequestID='" + condition.StocktakeRequest.RequestID + "' AND ");
        //    //        }
        //    //        if (!string.IsNullOrEmpty(condition.StocktakeRequest.RequestBy.UserName))
        //    //        {
        //    //            queryWhere.Append(" r.RequestBy.UserName='" + condition.StocktakeRequest.RequestBy.UserName + "' AND");
        //    //        }
        //    //        queryWhere.Append(" r.IsStatic=" + condition.StocktakeRequest.IsStatic + " AND");
        //    //    }
        //    //    if (dateStart != null)
        //    //    {
        //    //        queryWhere.Append(" r.DateRequest>=" + dateStart.ToString() + " AND");
        //    //    }
        //    //    if (dateEnd != null)
        //    //    {
        //    //        queryWhere.Append(" r.DateRequest<=" + dateEnd.ToString() + " AND");
        //    //    }
        //    //    //if (condition.Part!=null)
        //    //    //{
        //    //    //    if (!string.IsNullOrEmpty(condition.Part.PartCode))
        //    //    //    {
        //    //    //        queryWhere.Append(" r.StocktakeDetails
        //    //    //    }
        //    //    //}
        //    //}
        //    //if (queryWhere.Length>0)
        //    //{
        //    //    queryString += " WHERE " + queryWhere.ToString();
        //    //}
        //    //queryString = queryString.Substring(0, queryString.Length - 3);
        //    //ObjectQuery<StocktakeRequest> requestQuery = new ObjectQuery<StocktakeRequest>(queryString, _context).Include("RequestBy/UserGroup");
        //    ////ObjectQuery<StocktakeRequest> requests = new ObjectQuery<StocktakeRequest>(queryString, _context).Where(r=>r.StocktakeDetails.Any(
        //    ////requests.Where(r=>r.StocktakeDetails.
        //    //return requestQuery.ToList();
        //    //_context.StocktakeRequest.Include("RequestBy/UserGroup");
        //    //return new List<StocktakeRequest>();
        //}

        public IQueryable<ViewStockTakeRequest> QueryRequestDetailsByPage(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<ViewStockTakeRequest> query = QueryRequestDetails(condition, dateStart, dateEnd).OrderBy(d => d.RequestNumber).ThenBy(d => d.PartCode);
            return GetQueryByPage<ViewStockTakeRequest>(query, pageSize, pageNumber, out pageCount, out itemCount);
        }


        public IQueryable<View_StocktakeDetails> QueryRequestDetails(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL();
            //condition.ReadyForCount = true;
            return bll.QueryDetails(condition, dateStart, dateEnd);
        }

        public IQueryable<View_StocktakeDetails> QueryRequestDetailsByPage(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            string tmp = null;
            return QueryRequestDetailsByPage(condition, dateStart, dateStart, pageSize, pageNumber, out pageCount, out itemCount, false, ref tmp);
        }


        public IQueryable<View_StocktakeDetails> QueryRequestDetailsByPage(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount, bool cacheIt, ref string cacheKey)
        {
            IQueryable<View_StocktakeDetails> query;
            if (cacheIt)
            {
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    List<View_StocktakeDetails> details = CacheHelper.GetCache(cacheKey) as List<View_StocktakeDetails>;
                    if (details != null)
                    {
                        query = details.AsQueryable().OrderBy(d => d.RequestNumber).ThenBy(d => d.PartCode);
                        return GetQueryByPage<View_StocktakeDetails>(query, pageSize, pageNumber, out pageCount, out itemCount);
                    }
                }
                query = QueryRequestDetails(condition, dateStart, dateEnd);
                cacheKey = Guid.NewGuid().ToString();

                CacheHelper.SetCache(cacheKey, query.ToList());
                return GetQueryByPage<View_StocktakeDetails>(query.OrderBy(d => d.RequestNumber).ThenBy(d => d.PartCode), pageSize, pageNumber, out pageCount, out itemCount);


            }
            else
            {
                query = QueryRequestDetails(condition, dateStart, dateEnd).OrderBy(d => d.RequestNumber).ThenBy(d => d.PartCode);
                return GetQueryByPage<View_StocktakeDetails>(query, pageSize, pageNumber, out pageCount, out itemCount);
            }
        }


        public IQueryable<View_StocktakeDetails> QueryRequestDetailsByPage(View_StocktakeDetails condition, List<View_StocktakeDetails> removedDetails, List<View_StocktakeDetails> addedDetails, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<View_StocktakeDetails> query = QueryRequestDetails(condition, dateStart, dateEnd);
            List<View_StocktakeDetails> list = null ;
            if (removedDetails != null && removedDetails.Count > 0)
            {
                list = QueryRequestDetails(condition, null, null).ToList();
                foreach (var item in removedDetails)
                {
                    int index = list.FindIndex(d => d.PartID== item.PartID);
                    if (index >= 0)
                    {
                        list.RemoveAt(index);
                    }
                }
            }
            if (addedDetails != null && addedDetails.Count > 0)
            {
                if (list==null)
                {
                    list = QueryRequestDetails(condition, null, null).ToList();
                }
                foreach (var item in addedDetails)
                {
                    int index = list.FindIndex(d => d.PartID == item.PartID);
                    if (index < 0)
                    {
                        list.Add(item);
                    }
                }
            }
            if (list!=null)
            {
                query = list.AsQueryable(); 
            }
            return GetQueryByPage<View_StocktakeDetails>(query.OrderByDescending(d => d.RequestNumber).ThenBy(d => d.PartCode), pageSize, pageNumber, out pageCount, out itemCount);
        }

        public IQueryable<ViewStockTakeRequest> QueryRequestDetails(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd)
        {
            IQueryable<ViewStockTakeRequest> requestQuery = _context.ViewStockTakeRequestSet;
            requestQuery = BuildQuery(condition, dateStart, dateEnd, requestQuery);
            return requestQuery;
        }

        private IQueryable<ViewStockTakeRequest> BuildQuery(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd, IQueryable<ViewStockTakeRequest> requestQuery)
        {
            if (condition.RequestID != 0)
            {
                requestQuery = requestQuery.Where(vq => vq.RequestID == condition.RequestID);
            }
            if (!string.IsNullOrEmpty(condition.RequestNumber))
            {
                requestQuery = requestQuery.Where(vq => vq.RequestNumber == condition.RequestNumber);
            }
            if (!string.IsNullOrEmpty(condition.UserName))
            {
                requestQuery = requestQuery.Where(vq => vq.UserName == condition.UserName);
            }
            if (condition.PlantID != null)
            {
                requestQuery = requestQuery.Where(vq => vq.PlantID.Value == condition.PlantID.Value);
            }
            if (!string.IsNullOrEmpty(condition.PartCode))
            {
                requestQuery = requestQuery.Where(vq => vq.PartCode == condition.PartCode);
            }
            if (condition.StocktakeType != null)
            {
                requestQuery = requestQuery.Where(vq => vq.StocktakeType.Value == condition.StocktakeType.Value);
            }
            if (!string.IsNullOrEmpty(condition.PartChineseName))
            {
                requestQuery = requestQuery.Where(vq => vq.PartChineseName == condition.PartChineseName);
            }
            if (condition.IsStatic != null)
            {
                requestQuery = requestQuery.Where(vq => vq.IsStatic.Value == condition.IsStatic.Value);
            }
            if (dateStart != null)
            {
                requestQuery = requestQuery.Where(vq => vq.DateRequest >= dateStart);
            }
            if (dateEnd != null)
            {
                requestQuery = requestQuery.Where(vq => vq.DateRequest <= dateEnd);
            }
            return requestQuery;
        }

        public IQueryable<StocktakeRequest> QueryRequest(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd)
        {
            //IQueryable<ViewStockTakeRequest> details = QueryRequestDetails(condition);
            var requestQuery = from d in _context.StocktakeRequest join v in _context.ViewStockTakeRequestSet on d.RequestID equals v.RequestID select new { v, d };

            // string[] requests = details.Select(d => d.RequestID).Distinct().Cast<string>().ToArray();
            //string requestStr = string.Join(",",requests);

            //string esql = string.Format("SELECT VALUE r FROM {0}.StocktakeRequest WHERE r.RequestID IN ({1})", _context.DefaultContainerName,requestStr);
            //string esql = string.Format("SELECT VALUE DISTINCT r FROM {0}.StocktakeRequest AS r LEFT OUTER JOIN {0}.ViewStockTakeRequestSet AS vr ON r.RequestID=vr.RequestID", _context.DefaultContainerName);
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(condition.RequestNumber))
            {
                //strWhere.Append(" vr.RequestNumber='" + condition.RequestNumber + "' AND");
                requestQuery = requestQuery.Where(vq => vq.d.RequestNumber == condition.RequestNumber);
            }
            if (!string.IsNullOrEmpty(condition.UserName))
            {
                //strWhere.Append(" vr.UserName='" + condition.UserName + "' AND");//
                requestQuery = requestQuery.Where(vq => vq.d.RequestBy.UserName == condition.UserName);
            }
            if (condition.PlantID != null)
            {
                //strWhere.Append(" vr.PlantID=" + condition.PlantID + " AND");
                requestQuery = requestQuery.Where(vq => vq.v.PlantID.Value == condition.PlantID.Value);
            }
            if (!string.IsNullOrEmpty(condition.PartCode))
            {
                //strWhere.Append(" vr.PartCode='" + condition.PartCode + "' AND");
                requestQuery = requestQuery.Where(vq => vq.v.PartCode == condition.PartCode);
            }
            if (condition.StocktakeType != null)
            {
                //strWhere.Append(" vr.StocktakeType=" + condition.StocktakeType + " AND");
                requestQuery = requestQuery.Where(vq => vq.v.StocktakeType.Value == condition.StocktakeType.Value);
            }
            if (!string.IsNullOrEmpty(condition.PartChineseName))
            {
                //strWhere.Append(" vr.PartChineseName='" + condition.PartChineseName + "' AND");
                requestQuery = requestQuery.Where(vq => vq.v.PartChineseName == condition.PartChineseName);
            }
            if (condition.IsStatic != null)
            {
                //strWhere.Append(" vr.IsStatic.Value=" + condition.IsStatic.Value + " AND");
                requestQuery = requestQuery.Where(vq => vq.v.IsStatic.Value == condition.IsStatic.Value);
            }
            if (condition.Status != null)
            {
                requestQuery = requestQuery.Where(vq => vq.v.Status == condition.Status);
            }
            if (condition.IsCycleCount != null)
            {
                requestQuery = requestQuery.Where(vq => vq.v.IsCycleCount == condition.IsCycleCount);
            }
            if (dateStart != null)
            {
                requestQuery = requestQuery.Where(vq => vq.v.DateRequest >= dateStart);
            }
            if (dateEnd != null)
            {
                requestQuery = requestQuery.Where(vq => vq.v.DateRequest <= dateEnd);
            }
            return requestQuery.Select(vq => vq.d).Include("RequestBy.UserGroup").Distinct().OrderBy(d => d.RequestNumber);
        }

        public int GetCycledTimes()
        {
            BizParamsBLL bizParamsBLL = new BizParamsBLL(Context);
            BizParams paramCycledTimes = bizParamsBLL.GetBizParamByKey(new BizParams { ParamKey = Consts.BIZ_PARAMS_CYCLEDTIMES });
            int cycledTimes = int.Parse(paramCycledTimes.ParamValue);
            return cycledTimes;//this.QueryRequest(new ViewStockTakeRequest { IsCycleCount = true }, null, null).Count();
        }

        public List<StocktakeRequest> QueryRequestList(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd)
        {
            return QueryRequest(condition, dateStart, dateEnd).ToList();
        }

        public IQueryable<StocktakeRequest> QueryRequestByPage(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            var queryResult = QueryRequest(condition, dateStart, dateEnd).OrderByDescending(d => d.DateRequest);
            return this.GetQueryByPage(queryResult, pageSize, pageNumber, out pageCount, out itemCount);

        }

        public ViewStockTakeRequest BuildQueryCondition(long? requestID, string requestNumber, string userName, int? plantID, string partCode, int? stocktakeType, string partChineseName, DateTime? dateStart, DateTime? dateEnd)
        {
            ViewStockTakeRequest condition = new ViewStockTakeRequest();
            if (requestID != null)
            {
                condition.RequestID = requestID.Value;
            }
            if (!string.IsNullOrEmpty(requestNumber))
            {
                condition.RequestNumber = requestNumber;
            }
            if (!string.IsNullOrEmpty(userName))
            {
                condition.UserName = userName;
            }
            if (plantID != null)
            {
                condition.PlantID = plantID;
            }
            if (!string.IsNullOrEmpty(partCode))
            {
                condition.PartCode = partCode;
            }
            if (stocktakeType != null)
            {
                condition.StocktakeType = stocktakeType.Value;
            }
            if (!string.IsNullOrEmpty(partChineseName))
            {
                condition.PartChineseName = partChineseName;
            }
            return condition;
        }


        public StocktakeRequest GetRequest(StocktakeRequest request)
        {
            return this.GetObjectByKey(request);
        }


        public StocktakeRequest CreateStocktakeRequest(NewStocktakeRequest request)
        {
            return CreateStocktakeRequest(request, true);
        }

        public StocktakeRequest CreateStocktakeRequest(NewStocktakeRequest request, bool retrieveNew)
        {
            ///TO DO:validate stocktake upper bound of request user
            UserBLL userBLL = new UserBLL();
            User requestBy = userBLL.GetUserInfo(new User { UserID = request.RequestBy.Value });
            if (request.IsStatic && request.Details.Count + requestBy.UserGroup.CurrentStaticStocktake > requestBy.UserGroup.MaxStaticStocktake)
            {

            }
            else
            {
                if (!request.IsStatic && request.Details.Count + requestBy.UserGroup.CurrentDynamicStocktake > requestBy.UserGroup.MaxDynamicStocktake)
                {

                }
            }

            Type[] types = new Type[] { typeof(NewStocktakeDetails) };
            XmlSerializer xs = new XmlSerializer(typeof(NewStocktakeRequest), types);
            string reqStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, request);
                reqStr = sw.ToString();
            }

            DbParameter paramXml = Context.CreateDbParameter("Request", System.Data.DbType.Xml, reqStr, ParameterDirection.Input);
            DbParameter paramReqId = Context.CreateDbParameter("RequestID", System.Data.DbType.Int64, DBNull.Value, ParameterDirection.Output);
            Context.ExecuteNonQuery("sp_CreateRequest", CommandType.StoredProcedure, paramXml, paramReqId);

            if (retrieveNew)
            {
                StocktakeRequest newRequest = new StocktakeRequest { RequestID = Convert.ToInt64(paramReqId.Value) };
                return this.GetRequest(newRequest);
            }
            request.RequestID = Convert.ToInt64(paramReqId.Value);
            return request.ConvertToRequest();
        }

        public void UpdateRequest(NewStocktakeRequest request, List<int> removedDetailsList, List<View_StocktakeDetails> changedDetails)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL(Context);

            List<View_StocktakeDetails> detailsList = bll.QueryDetails(new View_StocktakeDetails { RequestID = request.RequestID }, null, null).ToList();
            //removed items
            for (int i = detailsList.Count - 1; i >= 0; i--)
            {
                if (removedDetailsList.Contains(detailsList[i].PartID.Value))
                {
                    detailsList.RemoveAt(i);
                }
            }
            //changed items
            foreach (var item in changedDetails)
            {
                View_StocktakeDetails detail = detailsList.Find(d => d.PartID == item.PartID);
                if (detail != null)
                {
                    detail.StocktakeType = item.StocktakeType;
                    detail.Priority = item.Priority;
                    detail.NotifyComments = item.NotifyComments;
                    detail.DetailsDesc = item.DetailsDesc;
                }
                else
                {
                    detailsList.Add(item);
                }
            }

            if (detailsList != null)
            {
                request.Details = detailsList.Select(d => new NewStocktakeDetails { PartID = d.PartID.ToString(), StocktakePriority = d.Priority.Value, Description = d.DetailsDesc, StocktakeTypeID = d.StocktakeType.Value }).ToList();
            }
            Type[] types = new Type[] { typeof(NewStocktakeDetails) };
            XmlSerializer xs = new XmlSerializer(typeof(NewStocktakeRequest), types);
            string reqStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, request);
                reqStr = sw.ToString();
            }
            DbParameter paramXml = Context.CreateDbParameter("Request", System.Data.DbType.Xml, reqStr, ParameterDirection.Input);
            Context.ExecuteNonQuery("sp_UpdateRequest", CommandType.StoredProcedure, paramXml);
        }

        public void UpdateRequest(NewStocktakeRequest request)
        {
            UpdateRequest(request, null, false, false);
        }


        public void UpdateRequest(NewStocktakeRequest request, string cacheKey, bool submit, bool isRemove)
        {
            List<View_StocktakeDetails> details = new List<View_StocktakeDetails>();
            if (!string.IsNullOrEmpty(cacheKey))
            {
                details = CacheHelper.GetCache(cacheKey) as List<View_StocktakeDetails>;
                if (details != null)
                {
                    if (!isRemove)
                    {
                        foreach (var item in request.Details)
                        {
                            View_StocktakeDetails matchedItem = details.FirstOrDefault(d => d.PartID + "" == item.PartID);
                            if (matchedItem != null)//update
                            {
                                matchedItem.Priority = item.StocktakePriority;
                                matchedItem.StocktakeType = item.StocktakeTypeID;
                                matchedItem.Description = item.Description;
                            }
                            else//add
                            {
                                details.Add(
                                    new View_StocktakeDetails
                                {
                                    PartID = int.Parse(item.PartID),
                                    StocktakeType = item.StocktakeTypeID,
                                    Description = item.Description,
                                    Priority = item.StocktakePriority
                                });
                            }
                        }

                    }
                    else
                    {
                        if (request.Details.Count > 0)//remove
                        {
                            foreach (var item in request.Details)
                            {
                                View_StocktakeDetails matchedItem = details.FirstOrDefault(d => d.PartID + "" == item.PartID);
                                if (matchedItem != null)
                                {
                                    details.Remove(matchedItem);
                                }
                            }
                        }
                        else//request.Details is empty means clear all
                        {
                            details.Clear();
                        }
                    }
                    CacheHelper.SetCache(cacheKey, details);
                }
            }
            if (submit)
            {
                if (!string.IsNullOrEmpty(cacheKey) && details != null)
                {
                    request.Details = details.Select(d => new NewStocktakeDetails { PartID = d.PartID.ToString(), StocktakePriority = d.Priority.Value, Description = d.Description, StocktakeTypeID = d.StocktakeType.Value }).ToList();
                }
                Type[] types = new Type[] { typeof(NewStocktakeDetails) };
                XmlSerializer xs = new XmlSerializer(typeof(NewStocktakeRequest), types);
                string reqStr = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    xs.Serialize(sw, request);
                    reqStr = sw.ToString();
                }
                DbParameter paramXml = Context.CreateDbParameter("Request", System.Data.DbType.Xml, reqStr, ParameterDirection.Input);
                Context.ExecuteNonQuery("sp_UpdateRequest", CommandType.StoredProcedure, paramXml);
            }
            else
            {

            }
        }

        public void DeleteRequest(long requestID)
        {
            DbParameter paramRequestID = Context.CreateDbParameter("requestID", System.Data.DbType.Int64, requestID, ParameterDirection.Input);
            Context.ExecuteNonQuery("sp_DeleteStocktakeRequest", CommandType.StoredProcedure, paramRequestID);
        }


        public void DeleteRequestBatch(List<StocktakeRequest> reqList)
        {
            string requests = string.Join(",", reqList.Select(r => r.RequestID.ToString()).ToArray());
            DbParameter paramRequests = Context.CreateDbParameter("requests", System.Data.DbType.String, requests, ParameterDirection.Input);
            Context.ExecuteNonQuery("sp_DelStocktakeReqBatch", CommandType.StoredProcedure, paramRequests);
        }


        public StocktakeRequest CreateStocktakeRequest(NewStocktakeRequest request, DbTransaction transaction)
        {
            Type[] types = new Type[] { typeof(NewStocktakeDetails) };
            XmlSerializer xs = new XmlSerializer(typeof(NewStocktakeRequest), types);
            string reqStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, request);
                reqStr = sw.ToString();
            }

            DbParameter paramXml = Context.CreateDbParameter("Request", System.Data.DbType.Xml, reqStr, ParameterDirection.Input);
            DbParameter paramReqId = Context.CreateDbParameter("RequestID", System.Data.DbType.Int64, DBNull.Value, ParameterDirection.Output);
            if (transaction != null)
            {
                Context.ExecuteNonQuery("sp_CreateRequest", CommandType.StoredProcedure, transaction, true, paramXml, paramReqId);
            }
            else
            {
                Context.ExecuteNonQuery("sp_CreateRequest", CommandType.StoredProcedure, paramXml, paramReqId);
            }

            StocktakeRequest newRequest = new StocktakeRequest { RequestID = Convert.ToInt64(paramReqId.Value) };
            return newRequest;
        }

        public StocktakeRequest CreateCycleCount(User user, List<View_StocktakeDetails> deletedDetails, List<View_StocktakeDetails> updatedDetails)
        {
            PartBLL partBll = new PartBLL();
            IQueryable<ViewPart> partQry = partBll.GetPartsByPlantToCycleCount();//partBll.GetPartsToCycleCount();
            NewStocktakeRequest request = new NewStocktakeRequest { IsStatic = true, IsCycleCount = true };

            request.RequestBy = user.UserID;

            request.Details = new List<NewStocktakeDetails>();
            foreach (var item in partQry)
            {
                NewStocktakeDetails details = new NewStocktakeDetails
                {
                    PartID = item.PartID.ToString(),
                    StocktakeTypeID = 90,
                    StocktakePriority = 1
                };
                View_StocktakeDetails deletedItem = deletedDetails.Find(d => d.PartID == int.Parse(details.PartID));
                if (deletedItem == null)
                {
                    View_StocktakeDetails updatedItem = updatedDetails.Find(d => d.PartID == int.Parse(details.PartID));
                    if (updatedItem != null)
                    {
                        details.Description = updatedItem.DetailsDesc;
                    }
                    request.Details.Add(details);
                }
            }
            StocktakeRequest result = CreateStocktakeRequest(request);
            BizParamsBLL bizParamsBLL = new BizParamsBLL();
            BizParams param = bizParamsBLL.GetBizParamByKey(new BizParams { ParamKey = Consts.BIZ_PARAMS_CYCLECOUNTED });
            param.ParamValue = "True";
            bizParamsBLL.UpdateBizParams(param);

            BizParams paramCycledTimes = bizParamsBLL.GetBizParamByKey(new BizParams { ParamKey = Consts.BIZ_PARAMS_CYCLEDTIMES });
            int cycledTimes = int.Parse(paramCycledTimes.ParamValue);
            paramCycledTimes.ParamValue = (cycledTimes + 1).ToString();
            bizParamsBLL.UpdateBizParams(paramCycledTimes);
            return result;
        }

        public List<StocktakeRequest> CreateCycleCountByPlant(User user)
        {
            PlantBLL plantBLL = new PlantBLL();
            List<Plant> plants = plantBLL.GetPlants();
            PartBLL partBll = new PartBLL();

            List<ViewPart> partList = partBll.GetPartsByPlantToCycleCount().ToList();//GetPartsToCycleCount().ToList();
            Dictionary<int, List<ViewPart>> dictDetails = new Dictionary<int, List<ViewPart>>();
            foreach (var item in plants)
            {
                int plantID = item.PlantID;
                List<ViewPart> partsByPlant = partList.Where(p => p.PlantID == plantID).ToList();
                if (partsByPlant != null && partsByPlant.Count > 0)
                {
                    dictDetails.Add(plantID, partsByPlant);
                }
            }
            //List<StocktakeRequest> requestList = new List<StocktakeRequest>();
            List<NewStocktakeRequest> requestList = new List<NewStocktakeRequest>();

            //group request by plant
            foreach (var plantID in dictDetails.Keys)
            {
                //StocktakeRequest request = new StocktakeRequest() { IsStatic = true, Plant = new Plant() { PlantID = plantID } };
                NewStocktakeRequest request = new NewStocktakeRequest() { IsStatic = true, PlantID = plantID, IsCycleCount = true };
                request.RequestBy = user.UserID;
                //request.StocktakeDetails = new EntityCollection<StocktakeDetails>();
                request.Details = new List<NewStocktakeDetails>();
                foreach (var item in dictDetails[plantID])
                {
                    //StocktakeDetails details = new StocktakeDetails
                    //{
                    //    Part = new Part { PartID = item.PartID },
                    //    StocktakeType = new StocktakeType { TypeID = 90 },
                    //    StocktakePriority = new StocktakePriority { PriorityID = 1 }
                    //};

                    NewStocktakeDetails details = new NewStocktakeDetails
                    {
                        PartID = item.PartID.ToString(),
                        StocktakeTypeID = 90,
                        StocktakePriority = 1
                    };
                    //request.StocktakeDetails.Add(details);
                    request.Details.Add(details);
                }
                requestList.Add(request);
            }
            List<StocktakeRequest> newRequestList = new List<StocktakeRequest>();
            //create request by plant
            if (requestList.Count > 0)
            {
                using (Context.Connection.CreateConnectionScope())
                {
                    DbTransaction transaction = Context.BeginTransaction();
                    try
                    {
                        foreach (var item in requestList)
                        {
                            StocktakeRequest newRequest = CreateStocktakeRequest(item, transaction);
                            newRequestList.Add(newRequest);
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }

            for (int i = 0; i < newRequestList.Count; i++)
            {
                newRequestList[i] = GetRequest(newRequestList[i]);
            }
            return newRequestList;
        }

        public void CreateStocktakeRequest(string fileContent)
        {
            string filePath = ".csv";
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(fileContent);
                }
            }
            //import file into db
            //delete temp file
            File.Delete(filePath);
        }

        public long ImportRequest(byte[] buffer)
        {
            using (FileStream fs = new FileStream("", FileMode.Create, FileAccess.Write))
            {
                fs.Write(buffer, 0, buffer.Length);
                fs.Close();
            }
            return 0;
            ///
        }
    }
}
