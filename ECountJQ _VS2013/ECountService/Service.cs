using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.Contract.Service;
using System.ServiceModel;
using SGM.ECount.BLL;
using SGM.Common.Exception;
using SGM.ECount.DataModel;
using System.Threading;
using System.Data;
using System.IO;
using ECount.ExcelTransfer;
using SGM.Common.Utility;
using System.Reflection;
using System.IO.Compression;
using System.Web;


namespace SGM.ECount.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service : IECountService
    {
        #region BLLs

        //public StocktakeDetailBLL StocktakeDetailBLL
        //{
        //    get
        //    {
        //        return new StocktakeDetailBLL();
        //    }
        //}
        //private StockTakeReqBLL _stockTakeReqBLL;

        //public StockTakeReqBLL StockTakeReqBLL
        //{
        //    get
        //    {
        //        return new StockTakeReqBLL();
        //    }
        //}


        //public PartBLL PartBLL
        //{
        //    get
        //    {
        //        return new PartBLL();
        //    }
        //}

        //public PlantBLL PlantBLL
        //{
        //    get
        //    {
        //        return new PlantBLL();
        //    }
        //}

        //public PartGroupBLL PartGroupBLL
        //{
        //    get
        //    {
        //        return new PartGroupBLL();
        //    }
        //}


        //public ConsignmentPartBLL ConsignmentPartBLL
        //{
        //    get
        //    {
        //        return new ConsignmentPartBLL();
        //    }
        //}

        #endregion

        #region User...
        public void Login()
        {
            //throw new Exception("login ex");
        }

        public User AddUser(User model)
        {
            UserBLL bll = new UserBLL();
            using (bll.Context)
            {
                return bll.AddUser(model);
            }
        }

        public void AddUserToGroup(int userID, int groupID)
        {
            UserBLL busi = new UserBLL();
            using (busi.Context)
            {
                User user = new User { UserID = userID, UserGroup = new UserGroup { GroupID = groupID } };
                busi.AddToGroup(user.UserGroup, user);
            }
        }

        public void UpdateUser(SGM.ECount.DataModel.User user)
        {
            UserBLL bll = new UserBLL();
            using (bll.Context)
            {
                bll.UpdateUser(user);
            }
        }

        public List<User> QueryUsersByPage(User info)
        {
            UserBLL bll = new UserBLL();
            using (bll.Context)
            {
                List<User> list = bll.QueryUsersByPage(info).ToList();
                return list;
            }
        }

        public void DeleteUsers(List<string> ids)
        {
            UserBLL busi = new UserBLL();
            using (busi.Context)
            {
                busi.DeleteUsers(ids);
            }
        }

        public List<SGM.ECount.DataModel.User> GetUsers()
        {
            UserBLL busi = new UserBLL();
            using (busi.Context)
            {
                return busi.GetUsers();
            }
        }

        public bool ExistUser(User user)
        {
            UserBLL bll = new UserBLL();
            using (bll.Context)
            {
                return bll.ExistUser(user);
            }
        }

        public User GetUserbyKey(User user)
        {
            UserBLL busi = new UserBLL();
            using (busi.Context)
            {
                return busi.GetUserInfo(user);
            }
        }

        public User GetUserbyName(string userName)
        {
            UserBLL busi = new UserBLL();
            using (busi.Context)
            {
                return busi.GetUserByName(userName);
            }
        }

        public SGM.ECount.DataModel.Operation GetOperationByKey(SGM.ECount.DataModel.Operation operation)
        {
            OperationBLL bll = new OperationBLL();
            using (bll.Context)
            {
                return bll.GetOperationbyKey(operation);
            }
        }

        public List<SGM.ECount.DataModel.Operation> GetOperations()
        {

            OperationBLL bll = new OperationBLL();
            using (bll.Context)
            {
                return bll.GetOperations();
            }
        }

        public List<SGM.ECount.DataModel.Operation> GetOperationsByOperation(SGM.ECount.DataModel.Operation operation)
        {
            OperationBLL bll = new OperationBLL();
            using (bll.Context)
            {
                return bll.GetOperationsbyOperation(operation);
            }
        }

        public List<SGM.ECount.DataModel.UserGroup> GetUserGroupsByOperation(SGM.ECount.DataModel.Operation operation)
        {
            OperationBLL bll = new OperationBLL();
            using (bll.Context)
            {
                return bll.GetUserGroupbyOperation(operation);
            }
        }

        public List<SGM.ECount.DataModel.Operation> GetOperationsByUserGroup(SGM.ECount.DataModel.UserGroup group)
        {

            OperationBLL bll = new OperationBLL();
            using (bll.Context)
            {
                return bll.GetOperationsbyUserGroup(group);
            }
        }
        #endregion User......

        #region UserGroup...
        public List<SGM.ECount.DataModel.UserGroup> GetUserGroups()
        {
            UserGroupBLL bll = new UserGroupBLL();
            using (bll.Context)
            {
                return bll.GetUserGroups();
            }
        }

        public List<UserGroup> QueryUserGroups(UserGroup info)
        {
            UserGroupBLL bll = new UserGroupBLL();
            using (bll.Context)
            {
                return bll.QueryUserGroups(info);
            }
        }

        public UserGroup GetUserGroupByKey(UserGroup info)
        {

            UserGroupBLL bll = new UserGroupBLL();
            using (bll.Context)
            {
                return bll.GetUserGroupByKey(info);
            }
        }

        public void UpdateUserGroup(UserGroup model)
        {

            UserGroupBLL bll = new UserGroupBLL();
            using (bll.Context)
            {
                bll.UpdateUserGroup(model);
            }
        }

        public UserGroup AddUserGroup(UserGroup model)
        {

            UserGroupBLL bll = new UserGroupBLL();
            using (bll.Context)
            {
                bll.AddUserGroup(model);
            }
            return model;
        }

        public void DeleteUserGroup(UserGroup model)
        {
            //throw new NotImplementedException();
            List<string> ids = new List<string>();
            ids.Add(model.GroupID.ToString());
            UserGroupBLL bll = new UserGroupBLL();
            using (bll.Context)
            {
                bll.DeleteUserGroups(ids);
            }
        }

        public void DeleteUserGroups(List<string> ids)
        {
            UserGroupBLL bll = new UserGroupBLL();
            using (bll.Context)
            {
                bll.DeleteUserGroups(ids);
            }
        }

        public bool ExistUserGroup(UserGroup model)
        {
            // throw new NotImplementedException();
            return new UserGroupBLL().ExistUserGroup(model);
        }
        #endregion UserGroup ......

        #region StocktakeRequest


        public StocktakeRequest RequestStocktake(NewStocktakeRequest newRequest)
        {
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.CreateStocktakeRequest(newRequest);
            }
        }

        public StocktakeRequest CreateCycleCount(User user, List<View_StocktakeDetails> deletedDetails, List<View_StocktakeDetails> updatedDetails)
        {
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.CreateCycleCount(user, deletedDetails, updatedDetails);
            }
        }

        public List<StocktakeRequest> CreateCycleCountByPlant(User user)
        {
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.CreateCycleCountByPlant(user);
            }
        }

        //public void UpdateStocktake(StocktakeRequest request)
        //{
        //    StockTakeReqBLL busi = new StockTakeReqBLL();
        //    using (busi.Context)
        //    {

        //        busi.UpdateStocktakeRequest(request);
        //    }
        //}

        public void UpdateStocktakeRequest(NewStocktakeRequest request)
        {
            StockTakeReqBLL bll = new StockTakeReqBLL();
            using (bll.Context)
            {
                bll.UpdateRequest(request);
            }
        }


        public void UpdateCachedRequest(NewStocktakeRequest request, string cacheKey, bool submit, bool isRemove)
        {
            StockTakeReqBLL bll = new StockTakeReqBLL();
            using (bll.Context)
            {
                bll.UpdateRequest(request, cacheKey, submit, isRemove);
            }
        }

        public void DeleteStocktake(StocktakeRequest request)
        {

        }

        public void DeleteRequestBatch(List<StocktakeRequest> reqList)
        {
            StockTakeReqBLL bll = new StockTakeReqBLL();
            using (bll.Context)
            {
                bll.DeleteRequestBatch(reqList);
            }
        }

        //public List<StocktakeRequest> QueryStocktakeRequest(long? requestID, string requestNumber, string userName, int? plantID, string partCode, int? stocktakeType, string partChineseName, DateTime? dateStart, DateTime? dateEnd)
        //{
        //    StockTakeReqBLL busi = new StockTakeReqBLL();
        //    using (busi.Context)
        //    {
        //        ViewStockTakeRequest condition = busi.BuildQueryCondition(requestID, requestNumber, userName, plantID, partCode, stocktakeType, partChineseName, dateStart, dateEnd);
        //        return busi.QueryRequest(condition).ToList();
        //    }
        //}
        public List<StocktakeRequest> QueryStocktakeRequest(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd)
        {
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.QueryRequestList(condition, dateStart, dateEnd).ToList();
            }
        }

        public int GetCycledTimes()
        {
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.GetCycledTimes();
            }
        }

        public List<StocktakeRequest> QueryStocktakeRequestByPage(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.QueryRequestByPage(condition, dateStart, dateEnd, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            }
        }

        public void DeleteStocktakeRequest(long requestID)
        {
            StockTakeReqBLL bll = new StockTakeReqBLL();
            using (bll.Context)
            {
                bll.DeleteRequest(requestID);
            }
        }
        //public List<View_StocktakeDetails> QueryStocktakeReqDetails(long? requestID, string requestNumber, string userName, int? plantID, string partCode, int? stocktakeType, string partChineseName, DateTime? dateStart, DateTime? dateEnd)
        //{
        //    StockTakeReqBLL busi = new StockTakeReqBLL();
        //    using (busi.Context)
        //    {
        //        View_StocktakeDetails condition = busi.BuildQueryCondition(requestID, requestNumber, userName, plantID, partCode, stocktakeType, partChineseName, dateStart, dateEnd);

        //        return busi.QueryRequestDetails(View_StocktakeDetails, dateStart, dateEnd).ToList();
        //    }
        //}

        public List<View_StocktakeDetails> QueryStocktakeReqDetails(View_StocktakeDetails condtion, DateTime? dateStart, DateTime? dateEnd)
        {
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.QueryRequestDetails(condtion, dateStart, dateEnd).ToList();
            }
        }

        public StocktakeRequest GetRequest(long requestId)
        {
            StocktakeRequest request = new StocktakeRequest { RequestID = requestId };
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.GetRequest(request);
            }
        }

        //public List<View_StocktakeDetails> QueryRequestDetailsByPage(View_StocktakeDetails condition, List<View_StocktakeDetails> removedDetails, List<View_StocktakeDetails> addedDetails, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        //{
        //    StockTakeReqBLL busi = new StockTakeReqBLL();
        //    using (busi.Context)
        //    {
        //        return busi.QueryRequestDetailsByPage(condition,removedDetails,addedDetails, dateStart, dateEnd, pageSize, pageNumber, out pageCount, out itemCount).ToList();
        //    }
        //}


        public List<View_StocktakeDetails> QueryRequestDetailsByPage(View_StocktakeDetails condition, List<View_StocktakeDetails> RemovedDetails, List<View_StocktakeDetails> AddedDetails, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            StockTakeReqBLL busi = new StockTakeReqBLL();
            using (busi.Context)
            {
                return busi.QueryRequestDetailsByPage(condition, RemovedDetails, AddedDetails, dateStart, dateEnd, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            }
        }

        //public List<View_StocktakeDetails> QueryRequestDetailsByPage(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        //{
        //    StockTakeReqBLL bll = new StockTakeReqBLL();
        //    using (bll.Context)
        //    {
        //        return bll.QueryRequestDetailsByPage(condition, dateStart, dateEnd, pageSize, pageNumber, out pageCount, out itemCount).ToList();
        //    }
        //}

        public List<ViewPart> GetPartsToCycleCount(out int cycledTimes, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            PartBLL bll = new PartBLL();
            StockTakeReqBLL reqBLL = new StockTakeReqBLL(bll.Context);
            using (bll.Context)
            {
                cycledTimes = reqBLL.GetCycledTimes();
                return bll.GetPartsToCycleCount(pageSize, pageNumber, out pageCount, out itemCount);
            }
        }

        public void ResetCycleCount()
        {
            BizParamsBLL bll = new BizParamsBLL();
            using (bll.Context)
            {
                bll.ResetCycleCount();
            }
        }

        public List<View_StocktakeDetails> GetNewRequestDetailsByPlant(List<View_StocktakeDetails> filter, List<View_StocktakeDetails> addition, bool isStatic, Plant plant, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL();
            using (bll.Context)
            {
                return bll.GetNewRequestDetails(filter, addition, isStatic, plant, pageSize, pageNumber, out pageCount, out itemCount);
            }
        }

        public List<View_StocktakeDetails> GetNotiDetailsByPage(StocktakeNotification notice, List<View_StocktakeDetails> filter, List<View_StocktakeDetails> addition, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL();
            using (bll.Context)
            {
                return bll.GetNotiDetailsByPage(notice, filter, addition, pageSize, pageNumber, out pageCount, out itemCount);
            }
        }

        public bool NotiExistsRepairPart(StocktakeNotification notification)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.ExistsRepairPart(notification);
            }
        }


        public bool NotiExistsCSMTPart(StocktakeNotification notification)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.ExistsCSMTPart(notification);
            }
        }

        //public List<View_StocktakeDetails> GetNewRequestDetails(bool isStatic, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        //{
        //    StocktakeDetailBLL bll = new StocktakeDetailBLL();
        //    using (bll.Context)
        //    {
        //        return bll.GetNewRequestDetails(isStatic, pageSize, pageNumber, out pageCount, out itemCount);
        //    }
        //}

        public List<View_StocktakeDetails> GetNewRequestDetails(View_StocktakeDetails filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL();
            using (bll.Context)
            {
                return bll.GetNewRequestDetails(filter, pageSize, pageNumber, out pageCount, out itemCount);
            }
        }

        public StocktakeNotification GetNotification(StocktakeNotification notification)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.GetNotification(notification);
            }
        }
        public StocktakeNotification CreateNotification(StocktakeNotification notification, List<long> removedDetailsList)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.CreateNotification(notification, removedDetailsList, false);
            }
        }


        public void UpdateNotification(StocktakeNotification notification, List<long> removedDetailsList, List<View_StocktakeDetails> changedDetails, bool removeAll)
        {
            if (removeAll)
            {
                this.RemoveAllPartsFromNoti(notification.NotificationID);
            }
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.UpdateNotification(notification, removedDetailsList, changedDetails);
            }
        }


        public void UpdateRequest(NewStocktakeRequest request, List<int> removedDetailsList, List<View_StocktakeDetails> changedDetails)
        {

            StockTakeReqBLL bll = new StockTakeReqBLL();
            using (bll.Context)
            {
                bll.UpdateRequest(request, removedDetailsList, changedDetails);
            }
        }

        public List<View_StocktakeDetails> QueryNotifyDetailsByPage(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL();
            using (bll.Context)
            {
                return bll.QueryNotificationDetailsByPage(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            }
        }


        public List<View_StocktakeNotification> QueryNotiByPage(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.QueryNotificationByPage(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            }
        }

        public int RemoveAllPartsFromNoti(long notificationID)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.RemoveAllParts(notificationID);
            }
        }

        public void PublishNotification(StocktakeNotification notification, List<StoreLocationType> locationTypes)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.Publish(notification, locationTypes);
            }
        }


        public void DeleteNotification(List<string> notifications)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.DeleteBatch(notifications);
            }
        }


        public byte[] ExportNotification(List<String> notification, out string message, out string notiCode)
        {
            byte[] byteArray = new byte[0];
            StocktakeNotification noti = new StocktakeNotification();
            foreach (var item in notification)
            {
                noti.NotificationID = long.Parse(item);
            }
            StocktakeNotificationBLL busi = new StocktakeNotificationBLL();
            noti = busi.GetNotification(noti);
            notiCode = noti.NotificationCode;
            List<View_StocktakeDetails> list = new List<View_StocktakeDetails>();
            using (busi.Context)
            {
                for (int i = 0; i < notification.Count; i++)
                {
                    list.AddRange(busi.GetNotificationlist(notification[i]).OrderBy(n => n.WorkLocation));
                }
            }
            ConsignmentPartBLL bll = new ConsignmentPartBLL();
            using (bll.Context)
            {
                foreach (var item in list)
                {
                    //item.PartID;
                    List<ConsignmentPartRecord> records = bll.QueryRecords(new ConsignmentPartRecord { Part = new Part { PartID = item.PartID.Value } }).ToList();
                    if (records.Count > 0)
                    {
                        foreach (var record in records)
                        {
                            item.CSMTDUNS += record.Supplier.SupplierName + "/";
                        }
                    }
                }
            }
            byteArray = ExportNotiToExel(list, out message);
            return byteArray;
        }

        private byte[] ExportNotiToExel(List<View_StocktakeDetails> list, out string msg)
        {
            string schemaFile = "盘点通知明细模板.xls";
            string tmpFile;
            PrepareExportTemp(ref schemaFile, out tmpFile);
            File.Copy(schemaFile, tmpFile);
            File.SetAttributes(tmpFile, FileAttributes.Normal);
            DataSet ds = Utils.ListToDataSet(list);
            DataTable dt = ds.Tables[0];
            dt.TableName = "盘点通知";
            //    dt = Utils.AddAutoIncreaseColumn(dt, "SNO", 0, 1);
            Dictionary<string, string> mapping = new Dictionary<string, string>();
            #region field mappings
            mapping.Add("F1", "AutoGeneratedSerialNo");
            mapping.Add("F2", "NotificationCode");
            mapping.Add("F3", "PartCode");
            mapping.Add("F4", "PartChineseName");
            mapping.Add("F5", "DUNS");
            mapping.Add("F6", "PartPlantCode");
            mapping.Add("F7", "Workshops");
            mapping.Add("F8", "Segments");
            mapping.Add("F9", "WorkLocation");
            mapping.Add("F10", "CategoryName");
            mapping.Add("F11", "CSMTDUNS");
            mapping.Add("F12", "RequestUser");
            mapping.Add("F13", "TypeName");
            #endregion
            try
            {

                if (ExcelHelper.WriteExcel(tmpFile, ds, out msg, mapping))
                {

                    byte[] content = File.ReadAllBytes(tmpFile);

                    if (HttpContext.Current == null)
                    {
                        return Utils.Compress(content);
                    }
                    else
                    {
                        return content;
                    }
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }

        private byte[] ExportNotiToCSV(List<View_StocktakeDetails> list, out string message)
        {

            string fileName;
            string schemaFile = "StocktakeNotification.xml";
            string tmpFile;
            PrepareExportTemp(ref schemaFile, out tmpFile);
            string tmpDir = Path.GetDirectoryName(schemaFile) + @"\";//Path.GetFullPath(@"ExportSchema\");
            byte[] byteArray = MaterializeToBytes<View_StocktakeDetails>(list, schemaFile, tmpDir, out fileName, out message);

            return byteArray;
        }

        public void CancelPublish(List<string> notifications)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.BatchCancelPublish(notifications);
            }
        }

        public List<View_StocktakeResult> GetStocktakeResult(View_StocktakeResult filter, bool loadWorkshopDetails)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                List<View_StocktakeResult> list = bll.GetStocktakeResult(filter).OrderBy(r => r.WorkLocation).ToList();
                if (loadWorkshopDetails)
                {
                    WorkshopStocktakeDetailBLL detailBLL = new WorkshopStocktakeDetailBLL(bll.Context);
                    for (int i = 0; i < list.Count; i++)
                    {
                        View_StocktakeResult item = list[i];
                        if (item.SGMItemID != null)
                        {
                            item.WorkshopDetails = detailBLL.GetDetailsByItemID(item.SGMItemID.Value).ToList();
                        }
                    }
                }
                return list;
            }
        }

        public List<View_StocktakeResult> GetStocktakeResultOfScope(View_StocktakeResult filter, string startPartCode, string endPartCode)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                List<View_StocktakeResult> list = bll.GetStocktakeResultOfScope(filter, startPartCode, endPartCode).ToList();

                WorkshopStocktakeDetailBLL detailBLL = new WorkshopStocktakeDetailBLL(bll.Context);
                for (int i = 0; i < list.Count; i++)
                {
                    View_StocktakeResult item = list[i];
                    if (item.SGMItemID != null)
                    {
                        item.WorkshopDetails = detailBLL.GetDetailsByItemID(item.SGMItemID.Value).ToList();
                    }
                }
                return list;
            }
        }

        public List<View_ResultCSMT> GetCSMTStocktakeResult(View_ResultCSMT filter)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.GetCSMTStocktakeResult(filter).ToList();
            }
        }


        public List<View_ResultNoneCSMT> GetNoneCSMTStocktakeResult(View_ResultNoneCSMT filter)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                List<View_ResultNoneCSMT> list = bll.GetNoneCSMTStocktakeResult(filter).ToList();
                WorkshopStocktakeDetailBLL detailBLL = new WorkshopStocktakeDetailBLL(bll.Context);
                for (int i = 0; i < list.Count; i++)
                {
                    View_ResultNoneCSMT item = list[i];
                    if (item.Workshops != null)
                    {
                        string[] workshops = item.Workshops.Trim().Trim(',').Split(',');
                    }


                    if (item.SGMItemID.HasValue)
                    {
                        item.WorkshopDetails = detailBLL.GetDetailsByItemID(item.SGMItemID.Value).ToList();
                    }
                }
                return list;
            }
        }

        public string ImportResult(StocktakeNotification notification, List<View_StocktakeResult> itemList, string cacheKey, bool submit)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.ImportResult(notification, itemList, cacheKey, submit);
            }
        }

        public void FillStocktakeResult(StocktakeNotification notification, List<View_StocktakeResult> list)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.FillResult(notification, list);
            }
        }

        public void FillStocktakeAdjustment(List<View_StocktakeResult> list)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.FillAdjustment(list);
            }
        }

        public void ImportStorage(List<S_StorageRecord> list)
        {
            StorageRecordBLL bll = new StorageRecordBLL();
            using (bll.Context)
            {
                bll.ImportStorage(list);
            }
        }

        public List<View_StocktakeDetails> QueryFullFilledNotiDetailsByPage(View_StocktakeDetails condition, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            StocktakeDetailBLL bll = new StocktakeDetailBLL();
            using (bll.Context)
            {
                return bll.QueryFullFilledDetailsByPage(condition, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            }

        }

        public List<View_StocktakeItem> QueryStocktakeItem(View_StocktakeItem filter)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.QueryStocktakeItem(filter).ToList();
            }
        }

        public void ImportAdjustment(List<View_StocktakeItem> list)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.ImportAdjustment(list);
            }
        }

        public void CreateAnalyseReport(StocktakeNotification notice, User analyzedBy, out string reportCode, out Int64 reportID)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.CreateAnalyseReport(notice, analyzedBy, out reportCode, out reportID);
            }
        }

        public void CreateAnalyseRptByCondition(View_StocktakeDetails filter, User analyzedBy, out string reportCode, out Int64 reportID)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.CreateAnalyseRptByCondition(filter, analyzedBy, out reportCode, out reportID);
            }
        }

        public DataSet QueryAnalyseReport(View_DifferenceAnalyse filter, DateTime? timeStart, DateTime? timeEnd)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                return bll.QueryAnalyseReport(filter, timeStart, timeEnd);
            }
        }

        public void DeleteAnalyseItems(List<DiffAnalyseReportItem> itemsList)
        {
            StocktakeNotificationBLL bll = new StocktakeNotificationBLL();
            using (bll.Context)
            {
                bll.DeleteAnalyseItems(itemsList);
            }
        }


        public DiffAnalyseReport GetAnalyseReport(long reportID)
        {
            DiffAnalyseReportBLL bll = new DiffAnalyseReportBLL();
            using (bll.Context)
            {
                return bll.GetAnalyseReport(reportID);
            }
        }

        public List<DifferenceAnalyseDetails> GetDifferenceAnalyseDetails()
        {
            DifferenceAnalyseDetailsBLL busi = new DifferenceAnalyseDetailsBLL();
            using (busi.Context)
            {
                return busi.GetDifferenceAnalyseDetails();
            }
        }

        public bool ExistDifferenceAnalyse(DifferenceAnalyseDetails diffAnalyseDetails)
        {
            DifferenceAnalyseDetailsBLL busi = new DifferenceAnalyseDetailsBLL();
            using (busi.Context)
            {
                return busi.ExistDifferenceAnalyse(diffAnalyseDetails);
            }
        }

        public void AddDiffAnalyseDetail(DifferenceAnalyseDetails detail)
        {
            DifferenceAnalyseDetailsBLL bll = new DifferenceAnalyseDetailsBLL();
            using (bll.Context)
            {
                bll.AddDiffAnalyseDetail(detail);
            }
        }

        public void DeleteDiffAnalyseDetail(DifferenceAnalyseDetails detail)
        {
            DifferenceAnalyseDetailsBLL bll = new DifferenceAnalyseDetailsBLL();

            using (bll.Context)
            {
                bll.DeleteDiffAnalyseDetail(detail);
            }
        }

        public DifferenceAnalyseDetails GetDiffAnalyseDetailstbyID(int detailsID)
        {
            DifferenceAnalyseDetailsBLL bll = new DifferenceAnalyseDetailsBLL();
            using (bll.Context)
            {
                return bll.GetDiffAnalyseDetailstbyID(detailsID);
            }
        }

        //public DiffAnalyseReportItem GetAnalyseItem(long reportID)
        //{
        //    DiffAnalyseReportBLL bll = new DiffAnalyseReportBLL();
        //    using (bll.Context)
        //    {
        //        return bll.GetAnalyseReport(reportID);
        //    }
        //}

        public List<View_DiffAnalyseReportDetails> GetDiffAnalyseRptDetails(DiffAnalyseReportItem item, UserGroup userGroup)
        {
            DiffAnalyseReportDetailsBLL bll = new DiffAnalyseReportDetailsBLL();
            using (bll.Context)
            {
                return bll.GetDetails(item, userGroup);
            }
        }

        public List<DiffAnalyseReport> GetDiffAnalyseReportsByNoti(StocktakeNotification noti)
        {
            DiffAnalyseReportBLL bll = new DiffAnalyseReportBLL();
            using (bll.Context)
            {
                return bll.GetReportsByNotification(noti);
            }
        }

        public List<DiffAnalyseReport> GetDiffAnalyseReports()
        {
            DiffAnalyseReportBLL bll = new DiffAnalyseReportBLL();
            using (bll.Context)
            {
                return bll.GetReportsList();
            }
        }

        public void SaveAnalyseReport(List<View_DiffAnalyseReportDetails> detailsList)
        {
            DiffAnalyseReportDetailsBLL bll = new DiffAnalyseReportDetailsBLL();
            using (bll.Context)
            {
                bll.SaveAnalyseReport(detailsList);
            }
        }

        protected bool LocationAccessDenied(StoreLocation location, bool aiqIncluded, User userInfo)
        {
            return (!aiqIncluded
                || userInfo.UserGroup.StoreLocationType != null
                    && userInfo.UserGroup.StoreLocationType.TypeID != location.StoreLocationType.TypeID
                || userInfo.UserGroup.StoreLocationType == null
                    && (userInfo.UserGroup.ShowAllLocation == null
                        || !userInfo.UserGroup.ShowAllLocation.Value));
        }
        public byte[] ExportStocktakeResult(StocktakeNotification notification, User userInfo)
        {
            byte[] byteArray = new byte[0];
            string schemaFile = "实盘结果模板.xls";
            string tmpFile;
            PrepareExportTemp(ref schemaFile, out tmpFile);
            File.Copy(schemaFile, tmpFile);
            File.SetAttributes(tmpFile, FileAttributes.Normal);
            List<View_StocktakeResult> list = GetStocktakeResult(new View_StocktakeResult { NotificationID = notification.NotificationID }, true);
            Type t = typeof(View_StocktakeResult);
            List<StoreLocation> storeLocations = GetStoreLocations();
            List<PropertyInfo> props = t.GetProperties().Where(p => p.MemberType == MemberTypes.Property && p.CanRead && p.CanWrite).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                View_StocktakeResult item = list[i];
                StoreLocation location = new StoreLocation();
                if (item.SGMItemID != null)
                {
                    location = storeLocations.Find(l => l.LocationID == item.SGMLocationID);

                    if (location != null)
                    {
                        bool availableExcluded = LocationAccessDenied(location, location.AvailableIncluded.Value, userInfo);
                        bool qiExcluded = LocationAccessDenied(location, location.QIIncluded.Value, userInfo);
                        bool blockExcluded = LocationAccessDenied(location, location.BlockIncluded.Value, userInfo);
                        if (availableExcluded)
                        {
                            item.Store = null;
                            item.Line = null;
                            item.Machining = null;


                        }
                        if (qiExcluded)
                        {
                            item.SGMQI = null;
                        }
                        if (blockExcluded)
                        {
                            item.SGMBlock = null;
                        }
                        if (availableExcluded && blockExcluded && qiExcluded)//unauthorized to view sgm location
                        {
                            item.StartCSN = null;
                            item.EndCSN = null;
                        }
                        else
                        {
                            List<WorkshopStocktakeDetail> details = item.WorkshopDetails;
                            if (userInfo.Workshop != null)
                            {
                                details = new List<WorkshopStocktakeDetail>();
                                WorkshopStocktakeDetail detail = item.WorkshopDetails.Find(d => d.WorkshopCode == userInfo.Workshop.WorkshopCode);
                                if (detail != null)
                                {
                                    details.Add(detail);
                                }
                            }
                            
                            if (details != null && details.Count > 0)//split by workshops
                            {
                                
                                list.RemoveAt(i);
                                foreach (var detailItem in details)
                                {
                                    View_StocktakeResult newItem = new View_StocktakeResult();
                                    foreach (var p in props)
                                    {
                                        p.SetValue(newItem, p.GetValue(item, null), null);
                                    }
                                    newItem.StartCSN = detailItem.StartCSN;
                                    newItem.EndCSN = detailItem.EndCSN;
                                    newItem.Store = detailItem.Store;
                                    newItem.Machining = detailItem.Machining;
                                    newItem.Line = detailItem.Line;
                                    newItem.SGMAvailable = newItem.Store.GetValueOrDefault() + newItem.Machining.GetValueOrDefault() + newItem.Line.GetValueOrDefault();
                                    newItem.SGMQI = detailItem.SGMQI;
                                    newItem.SGMBlock = detailItem.SGMBlock;
                                    newItem.Workshops = detailItem.WorkshopCode;
                                    newItem.WorkshopDetails = null;
                                    list.Add(newItem);
                                }
                            }
                        }
                    }

                }


                if (item.GeneralItemID == null || item.GeneralItemID == DefaultValue.LONG)
                {
                    item.GeneralAvailable = null;
                    item.GeneralBlock = null;
                    item.GeneralQI = null;

                }
                else
                {
                    location = storeLocations.Find(l => l.LocationID == item.GenerLocationID);

                    if (location != null)
                    {
                        if (LocationAccessDenied(location, location.AvailableIncluded.Value, userInfo))
                        {

                            item.GeneralAvailable = null;
                        }
                        if (LocationAccessDenied(location, location.QIIncluded.Value, userInfo))
                        {
                            item.GeneralQI = null;
                        }
                        if (LocationAccessDenied(location, location.BlockIncluded.Value, userInfo))
                        {
                            item.GeneralBlock = null;
                        }

                    }
                }


                if (item.RepairItemID == null || item.RepairItemID == DefaultValue.LONG)
                {

                    item.RepairAvailable = null;
                    item.RepairBlock = null;
                    item.RepairQI = null;
                }
                else
                {
                    location = storeLocations.Find(l => l.LocationID == item.RepairLocationID);

                    if (location != null)
                    {
                        if (LocationAccessDenied(location, location.AvailableIncluded.Value, userInfo))
                        {

                            item.RepairAvailable = null;
                        }
                        if (LocationAccessDenied(location, location.QIIncluded.Value, userInfo))
                        {
                            item.RepairQI = null;
                        }
                        if (LocationAccessDenied(location, location.BlockIncluded.Value, userInfo))
                        {
                            item.RepairBlock = null;
                        }
                    }
                }


                //TextBox txtRDCBlock = e.Row.FindControl("txtRDCBlock") as TextBox;
                //TextBox txtRDCQI = e.Row.FindControl("txtRDCQI") as TextBox;
                //TextBox txtRDCAvailable = e.Row.FindControl("txtRDCAvailable") as TextBox;
                if (item.RDCItemID == DefaultValue.LONG)
                {
                    item.RDCAvailable = null;
                    item.RDCBlock = null;
                    item.RDCQI = null;

                }
                else
                {
                    location = storeLocations.Find(l => l.LocationID == item.RDCLocationID);


                    if (location != null)
                    {
                        if (LocationAccessDenied(location, location.AvailableIncluded.Value, userInfo))
                        {

                            item.RDCAvailable = null;
                        }
                        if (LocationAccessDenied(location, location.QIIncluded.Value, userInfo))
                        {
                            item.RDCQI = null;
                        }
                        if (LocationAccessDenied(location, location.BlockIncluded.Value, userInfo))
                        {
                            item.RDCBlock = null;
                        }
                    }
                }

                if (item.CSMTItemID == null || item.CSMTItemID == DefaultValue.LONG)
                {
                    item.CSMTAvailable = null;
                    item.CSMTQI = null;
                    item.CSMTBlock = null;
                }
                else
                {
                    location = storeLocations.Find(l => l.LocationID == item.CSMTLocationID);

                    if (location != null)
                    {
                        if (LocationAccessDenied(location, location.AvailableIncluded.Value, userInfo))
                        {

                            item.CSMTAvailable = null;
                        }
                        if (LocationAccessDenied(location, location.QIIncluded.Value, userInfo))
                        {
                            item.CSMTQI = null;
                        }
                        if (LocationAccessDenied(location, location.BlockIncluded.Value, userInfo))
                        {
                            item.CSMTBlock = null;
                        }
                    }
                }
            }
            list = list.OrderBy(r => r.PartCode).ToList();
            //DataSet ds = Utils.ListToDataSet(list);
            //DataTable dt = ds.Tables[0];//Utils.ListToDataTable2<View_StocktakeResult>(list);

            DataTable dt = Utils.ListToDataTable2<View_StocktakeResult>(list);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            dt.TableName = "实盘结果";
            Dictionary<string, string> mapping = new Dictionary<string, string>();
            dt.Columns.Add("SUM", typeof(decimal), "ISNULL(SGMAvailable,0)+ISNULL(SGMQI,0)+ISNULL(SGMBlock,0)+ISNULL(RDCAvailable,0)+ISNULL(RDCQI,0)+ISNULL(RDCBlock,0)+ISNULL(RepairAvailable,0)+ISNULL(RepairQI,0)+ISNULL(RepairBlock,0)+ISNULL(CSMTAvailable,0)+ISNULL(CSMTQI,0)+ISNULL(CSMTBlock,0)");
            #region field mappings
            mapping.Add("F1", "PartCode");
            mapping.Add("F2", "PartPlantCode");
            mapping.Add("F3", "PartChineseName");
            mapping.Add("F4", "DUNS");
            mapping.Add("F5", "CategoryName");
            mapping.Add("F6", "RequestNumber");
            mapping.Add("F7", "RequestUser");
            mapping.Add("F8", "TypeName");
            mapping.Add("F9", "SUM");
            mapping.Add("F10", "Store");
            mapping.Add("F11", "Line");
            mapping.Add("F12", "Machining");
            mapping.Add("F13", "SGMBlock");
            mapping.Add("F14", "StartCSN");
            mapping.Add("F15", "EndCSN");
            mapping.Add("F16", "Workshops");
            mapping.Add("F17", "Segments");
            mapping.Add("F18", "SGMFillinUserName");
            mapping.Add("F19", "SGMFillinTime");

            mapping.Add("F20", "RDCAvailable");
            mapping.Add("F21", "RDCBlock");
            mapping.Add("F22", "RDCFillinUserName");
            mapping.Add("F23", "RDCFillinTime");

            mapping.Add("F24", "RepairAvailable");
            mapping.Add("F25", "RepairBlock");
            mapping.Add("F26", "RepairDUNS");
            mapping.Add("F27", "RepairFillinUserName");
            mapping.Add("F28", "RepairFillinTime");

            mapping.Add("F29", "CSMTAvailable");
            mapping.Add("F30", "CSMTBlock");
            mapping.Add("F31", "CSMTDUNS");
            mapping.Add("F32", "CSMTFillinUserName");
            mapping.Add("F33", "CSMTFillinTime");

            mapping.Add("F34", "WorkLocation");
            #endregion

            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            string msg;
            try
            {
                if (ExcelHelper.WriteExcel(tmpFile, ds, out msg, mapping))
                {

                    byte[] content = File.ReadAllBytes(tmpFile);

                    if (HttpContext.Current == null)
                    {
                        return Utils.Compress(content);
                    }
                    else
                    {
                        return content;
                    }
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }

        public byte[] ExportStocktakeNotice(StocktakeNotification notification, User exportBy, out string notiCode)
        {
            byte[] byteArray = new byte[0];
            string schemaFile = "盘点通知模板.xls";
            string tmpFile;
            PrepareExportTemp(ref schemaFile, out tmpFile);
            File.Copy(schemaFile, tmpFile);
            File.SetAttributes(tmpFile, FileAttributes.Normal);
            StocktakeNotificationBLL notiBLL = new StocktakeNotificationBLL();
            notification = notiBLL.GetNotification(notification);
            notiCode = notification.NotificationCode;
            List<string> locationTypes = notification.StocktakeLocations.Select(l => l.StoreLocationType.TypeName).Distinct().ToList();
            List<View_StocktakeDetails> list = new List<View_StocktakeDetails>();
            StocktakeDetailBLL bll = new StocktakeDetailBLL();
            using (bll.Context)
            {
                list = bll.GetNotificationDetails(notification).OrderBy(n => n.WorkLocation).ToList();
            }
            //List<View_StocktakeResult> list = GetStocktakeResult(new View_StocktakeResult { NotificationID = notification.NotificationID });
            //DataTable dt = Utils.ListToDataTable(list);

            Dictionary<string, string> mapping = new Dictionary<string, string>();
            DataSet ds = Utils.ListToDataSet(list);
            DataTable dt = ds.Tables[0];
            dt.TableName = "盘点通知";
            dt.Columns.Add("RowNum");
            dt.Columns.Add("F10");
            dt.Columns.Add("F13");
            //dt.Columns.Add("F17");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RowNum"] = Convert.ToString(i + 1);
            }

            if (locationTypes.Count > 0)
            {
                DataRow foot1 = dt.NewRow();
                foot1["F13"] = "盘点区域：";
                int i = 14;
                foreach (var item in locationTypes)
                {
                    string colName = "F" + i;
                    dt.Columns.Add(colName);
                    foot1[colName] = item;
                    i++;
                    mapping.Add(colName, colName);
                }
                dt.Rows.Add(foot1);
            }
            DataRow foot2 = dt.NewRow();
            foot2["RowNum"] = "盘点原因：";
            //DataRow foot1 = dt.NewRow();
            //foot1["F13"] = "SGM现场";
            //foot1["F17"] = "RDC";
            //DataRow foot2 = dt.NewRow();
            //foot2["F13"] = "外协加工商";
            //foot2["F17"] = "返修加工商";
            DataRow foot3 = dt.NewRow();
            foot3["RowNum"] = "发自：" + exportBy.UserName;
            foot3["F14"] = "盘点主管签字  _____________";
            DataRow foot4 = dt.NewRow();
            foot4["F14"] = "实际盘点时间  _____________";
            DataRow foot5 = dt.NewRow();
            foot5["RowNum"] = "以上车间、工位、库位信息来自MH,仅供参考；请盘点时须保证盘点区域完整性！！！";

            dt.Rows.Add(foot2);
            dt.Rows.Add(foot3);
            dt.Rows.Add(foot4);
            dt.Rows.Add(foot5);

            //dt.Columns.Add("SUM", typeof(Int32), "ISNULL(SGMAvailable,0)+ISNULL(SGMQI,0)+ISNULL(SGMBlock,0)+ISNULL(RDCAvailable,0)+ISNULL(RDCQI,0)+ISNULL(RDCBlock,0)+ISNULL(RepairAvailable,0)+ISNULL(RepairQI,0)+ISNULL(RepairBlock,0)+ISNULL(CSMTAvailable,0)+ISNULL(CSMTQI,0)+ISNULL(CSMTBlock,0)");
            #region field mappings
            mapping.Add("F1", "RowNum");
            mapping.Add("F2", "PartCode");
            mapping.Add("F3", "PartPlantCode");

            mapping.Add("F4", "Workshops");
            mapping.Add("F5", "Segments");
            mapping.Add("F6", "PartChineseName");
            mapping.Add("F7", "DUNS");
            mapping.Add("F8", "RequestUser");
            mapping.Add("F9", "TypeName");
            mapping.Add("F10", "Dloc");
            mapping.Add("F13", "F13");
            mapping.Add("F12", "WorkLocation");

            mapping.Add("F19", "CategoryName");
            mapping.Add("F20", "Specs");

            #endregion


            ConsignmentPartBLL cpBll = new ConsignmentPartBLL();
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    if (row["PartID"] != DBNull.Value)
                    {

                        //item.PartID;
                        List<ConsignmentPartRecord> records = cpBll.QueryRecords(new ConsignmentPartRecord { Part = new Part { PartID = Convert.ToInt32(row["PartID"]) } }).ToList();
                        if (records.Count > 0)
                        {
                            foreach (var record in records)
                            {
                                row["Workshops"] += "/" + record.Supplier.SupplierName;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            string msg;
            try
            {
                Dictionary<string, KeyValuePair<string, string>> dictRanges = new Dictionary<string, KeyValuePair<string, string>>();
                if (notification.IsStatic.Value)
                {
                    dictRanges.Add("A1:A1", new KeyValuePair<string, string>("F1", "静态盘点通知"));
                }
                else
                {
                    dictRanges.Add("A1:A1", new KeyValuePair<string, string>("F1", "动态盘点通知"));
                }
                dictRanges.Add("A3:A3", new KeyValuePair<string, string>("F1", "NO:" + notification.NotificationCode));
                dictRanges.Add("C3:C3", new KeyValuePair<string, string>("F1", "盘点时间：" + notification.PlanDate.Value.ToString()));
                //dictRanges.Add("H3:H3", new KeyValuePair<string, string>("F8", (notification.IsStatic??true)?"静态":"动态"));
                if (ExcelHelper.WriteExcel(tmpFile, ds, out msg, mapping) && ExcelHelper.UpdateExcel(tmpFile, "盘点通知", dictRanges, out msg))
                {

                    byte[] content = File.ReadAllBytes(tmpFile);

                    if (HttpContext.Current == null)
                    {
                        return Utils.Compress(content);
                    }
                    else
                    {
                        return content;
                    }
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }

        private static void PrepareExportTemp(ref string schemaFile, out string tmpFile)
        {
            string path = string.Empty;
            if (HttpContext.Current != null)
            {
                path = HttpContext.Current.Server.MapPath("~") + @"\ExportSchema\";
            }
            else
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"ExportSchema\";
            }
            schemaFile = path + schemaFile;
            tmpFile = path + Path.GetRandomFileName() + ".xls";
        }
        #endregion StocktakeRequest

        #region DifferenceAnalyzeDetails ...
        //public List<SGM.ECount.DataModel.DifferenceAnalyzeDetails> GetDifferenceAnalyzeDetails(SGM.ECount.DataModel.DifferenceAnalyzeItem item)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddDifferenceAnalyzeDetails(SGM.ECount.DataModel.DifferenceAnalyzeDetails detail)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateDifferenceAnalyzeDetail(SGM.ECount.DataModel.DifferenceAnalyzeDetails detail)
        //{
        //    throw new NotImplementedException();
        //}
        public void UpdateDiffAnalyseDetail(DifferenceAnalyseDetails detail)
        {
            DifferenceAnalyseDetailsBLL bll = new DifferenceAnalyseDetailsBLL();
            using (bll.Context)
            {
                bll.UpdateDiffAnalyseDetail(detail);
            }
        }

        //public void DeleteDifferenceAnalyzeDetail(SGM.ECount.DataModel.DifferenceAnalyzeDetails detail)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion DifferenceAnalyzeDetails ... ...

        #region DifferenceAnalyzeItem...
        //public List<DifferenceAnalyzeItem> QueryDifferenceAnalyzeItems(DifferenceAnalyzeItem info)
        //{
        //    DifferenceAnalyzeBLL busi = new DifferenceAnalyzeBLL();
        //    using (busi.Context)
        //    {
        //        return busi.QueryDifferenceAnalyzeItems(info);
        //    }
        //}

        //public DifferenceAnalyzeItem GetDifferenceAnalyzeItemByKey(DifferenceAnalyzeItem info)
        //{
        //    DifferenceAnalyzeBLL busi = new DifferenceAnalyzeBLL();
        //    using (busi.Context)
        //    {
        //        return busi.GetDifferenceAnalyzeItemByKey(info);
        //    }
        //}

        //public void UpdateDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
        //{
        //    DifferenceAnalyzeBLL busi = new DifferenceAnalyzeBLL();
        //    using (busi.Context)
        //    {
        //        busi.UpdateDifferenceAnalyzeItem(model);
        //    }
        //}

        //public DifferenceAnalyzeItem AddDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
        //{
        //    DifferenceAnalyzeBLL busi = new DifferenceAnalyzeBLL();
        //    using (busi.Context)
        //    {
        //        return busi.AddDifferenceAnalyzeItem(model);
        //    }
        //}

        //public void DeleteDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteDifferenceAnalyzeItems(List<string> ids)
        //{
        //    DifferenceAnalyzeBLL busi = new DifferenceAnalyzeBLL();
        //    using (busi.Context)
        //    {
        //        busi.DeleteDifferenceAnalyzeItems(ids);
        //    }
        //}

        //public bool ExistDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
        //{
        //    DifferenceAnalyzeBLL busi = new DifferenceAnalyzeBLL();
        //    using (busi.Context)
        //    {
        //        return busi.ExistDifferenceAnalyzeItem(model);
        //    }
        //}
        #endregion DifferenceAnalyzeItem ......

        #region StocktakeStatus...
        public List<StocktakeStatus> GetStocktakeStatus()
        {
            StocktakeStatusBLL busi = new StocktakeStatusBLL();
            using (busi.Context)
            {
                return busi.GetStocktakeStatus();
            }
        }

        public List<StocktakeStatus> QueryStocktakeStatuss(StocktakeStatus info)
        {
            throw new NotImplementedException();
        }

        public StocktakeStatus GetStocktakeStatusByKey(StocktakeStatus info)
        {
            throw new NotImplementedException();
        }

        public void UpdateStocktakeStatus(StocktakeStatus model)
        {
            throw new NotImplementedException();
        }

        public StocktakeStatus AddStocktakeStatus(StocktakeStatus model)
        {
            throw new NotImplementedException();
        }

        public void DeleteStocktakeStatus(StocktakeStatus model)
        {
            throw new NotImplementedException();
        }
        #endregion StocktakeStatus ......

        #region StocktakePriority...
        public List<StocktakePriority> GetStocktakePriorities()
        {
            StocktakePriorityBLL busi = new StocktakePriorityBLL();
            using (busi.Context)
            {
                return busi.GetPriorities();
            }
        }

        public List<StocktakePriority> QueryStocktakePrioritys(StocktakePriority info)
        {
            throw new NotImplementedException();
        }

        public List<StocktakePriority> QueryStocktakePrioritysByPage(StocktakePriority info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            throw new NotImplementedException();
        }

        public StocktakePriority GetStocktakePriorityByKey(StocktakePriority info)
        {
            throw new NotImplementedException();
        }

        public void UpdateStocktakePriority(StocktakePriority model)
        {
            throw new NotImplementedException();
        }

        public StocktakePriority AddStocktakePriority(StocktakePriority model)
        {
            throw new NotImplementedException();
        }

        public void DeleteStocktakePriority(StocktakePriority model)
        {
            throw new NotImplementedException();
        }
        #endregion StocktakePriority ......

        #region Plant...
        public List<SGM.ECount.DataModel.Plant> GetPlants()
        {
            PlantBLL busi = new PlantBLL();
            using (busi.Context)
            {
                return busi.GetPlants();
            }
        }

        public Plant GetPlantByKey(Plant info)
        {
            PlantBLL bll = new PlantBLL();
            using (bll.Context)
            {
                return bll.GetPlantbykey(info);
            }

        }

        public void UpdatePlant(SGM.ECount.DataModel.Plant plant)
        {
            //this.PlantBLL.UpdatePlant(plant);
            PlantBLL busi = new PlantBLL();
            using (busi.Context)
            {
                busi.UpdatePlant(plant);
            }
        }

        public void DeletePlant(int plantID)
        {
            PlantBLL busi = new PlantBLL();
            using (busi.Context)
            {
                busi.DeletePlant(new Plant { PlantID = plantID });
            }
        }

        public Plant AddPlant(SGM.ECount.DataModel.Plant plant)
        {
            PlantBLL busi = new PlantBLL();
            using (busi.Context)
            {
                return busi.AddPlant(plant);
            }
        }
        #endregion Plant... ...

        #region Workshop...
        public List<SGM.ECount.DataModel.Workshop> Getworkshop()
        {
            WorkshopBLL busi = new WorkshopBLL();
            using (busi.Context)
            {
                return busi.GetWorkshops();
            }
        }

        public List<SGM.ECount.DataModel.Workshop> GetWorkshopbyPlant(SGM.ECount.DataModel.Plant plant)
        {
            WorkshopBLL busi = new WorkshopBLL();
            using (busi.Context)
            {
                return busi.GetWorkshopbyPlant(plant);
            }
        }

        public Workshop GetWorkshopbykey(Workshop info)
        {
            WorkshopBLL bll = new WorkshopBLL();
            using (bll.Context)
            {
                return bll.GetWorkshopbykey(info);
            }
        }

        public void AddWorkshop(SGM.ECount.DataModel.Workshop workshop)
        {
            WorkshopBLL busi = new WorkshopBLL();

            using (busi.Context)
            {

                busi.AddWorkshop(workshop);
            }
        }

        public void UpdateWorkshop(SGM.ECount.DataModel.Workshop workshop)
        {
            WorkshopBLL bll = new WorkshopBLL();
            using (bll.Context)
            {
                bll.UpdateWorkshop(workshop);
            }
        }

        public void DeleteWorkshop(SGM.ECount.DataModel.Workshop workshop)
        {
            WorkshopBLL bll = new WorkshopBLL();
            using (bll.Context)
            {
                bll.DeleteWorkshop(workshop);
            }
        }

        public List<Workshop> GetWorkshopbyPlantID(int plantID)
        {
            WorkshopBLL busi = new WorkshopBLL();
            using (busi.Context)
            {
                return busi.GetWorkshopbyPlantID(plantID);
            }
        }

        #endregion Workshop... ...

        #region Segment...
        public List<SGM.ECount.DataModel.Segment> GetSegment()
        {
            SegmentBLL bll = new SegmentBLL();
            using (bll.Context)
            {
                return bll.GetSegments();
            }

        }

        public List<SGM.ECount.DataModel.Segment> GetSegmentbyWorkshop(SGM.ECount.DataModel.Workshop workshop)
        {
            SegmentBLL busi = new SegmentBLL();
            using (busi.Context)
            {

                return busi.GetSegmentbyWorkshop(workshop);
            }
        }

        public Segment GetSegmentbykey(Segment info)
        {
            SegmentBLL bll = new SegmentBLL();

            using (bll.Context)
            {
                return bll.GetSegmentbykey(info);
            }
        }

        public void AddSegment(SGM.ECount.DataModel.Segment segment)
        {
            SegmentBLL bll = new SegmentBLL();

            using (bll.Context)
            {
                bll.AddSegment(segment);
            }

        }

        public void UpdateSegment(SGM.ECount.DataModel.Segment segment)
        {
            SegmentBLL bll = new SegmentBLL();
            using (bll.Context)
            {
                bll.UpdateSegment(segment);
            }
        }

        public void DeleteSegment(SGM.ECount.DataModel.Segment segment)
        {
            SegmentBLL bll = new SegmentBLL();
            using (bll.Context)
            {
                bll.DeleteSegment(segment);
            }
        }

        public List<Segment> GetSegmentbyWorkshopID(int workshopID)
        {
            SegmentBLL busi = new SegmentBLL();
            using (busi.Context)
            {

                return busi.GetSegmentbyWorkshopID(workshopID);
            }
        }

        public List<ViewPlantWorkshopSegment> GetSegmentbyPlantID(int plantID)
        {
            SegmentBLL busi = new SegmentBLL();
            using (busi.Context)
            {
                return busi.GetSegmentbyPlantID(plantID);
            }
        }

        public List<Segment> GetSegmentsByWorkshopCodes(List<string> workshopCodes)
        {
            SegmentBLL busi = new SegmentBLL();
            using (busi.Context)
            {
                return busi.GetSegmentsByWorkshopCodes(workshopCodes);
            }
        }

        public List<Segment> QuerySegmentByPage(Segment segment)
        {
            SegmentBLL busi = new SegmentBLL();
            using (busi.Context)
            {
                List<Segment> segmentList = busi.QuerySegmentByPage(segment).ToList();
                return segmentList;
            }
        }
        #endregion Segment... ...

        #region CycleCountLevel...
        public List<CycleCountLevel> QueryCycleCountLevels(CycleCountLevel info)
        {
            CycleCountLevelBLL busi = new CycleCountLevelBLL();
            using (busi.Context)
            {
                return busi.GetCycleCountLevel(info);
            }
        }

        public List<CycleCountLevel> QueryCycleCountLevelsByPage(CycleCountLevel info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            throw new NotImplementedException();
        }

        public CycleCountLevel GetCycleCountLevelByKey(CycleCountLevel info)
        {
            CycleCountLevelBLL busi = new CycleCountLevelBLL();
            using (busi.Context)
            {
                return busi.GetCycleCountLevelByKey(info);
            }
        }

        public void UpdateCycleCountLevel(CycleCountLevel model)
        {
            CycleCountLevelBLL busi = new CycleCountLevelBLL();
            using (busi.Context)
            {
                busi.UpdateCycleCountLevel(model);
            }
        }

        public CycleCountLevel AddCycleCountLevel(CycleCountLevel model)
        {
            CycleCountLevelBLL busi = new CycleCountLevelBLL();
            using (busi.Context)
            {
                return busi.AddCycleCountLevel(model);
            }
        }

        public void DeleteCycleCountLevel(CycleCountLevel model)
        {
            CycleCountLevelBLL busi = new CycleCountLevelBLL();
            using (busi.Context)
            {
                busi.DeleteCycleCountLevel(model);
            }
        }
        #endregion CycleCountLevel ......

        #region Part_PartSegment...
        public Part AddPart(Part part)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                return busi.AddPart(part);
            }
        }

        public void DeletePart(Part part)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                busi.DeletePart(part);
            }
        }

        /// <summary>
        /// update one part object
        /// </summary>
        /// <param name="part"></param>
        public void UpdatePart(Part part)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                busi.UpdatePart(part);
            }
        }

        public List<Part> GetRelatedParts(string partID)
        {
            Part part = new Part { PartID = int.Parse(partID) };
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                List<Part> partList = busi.GetRelatedParts(part);
                if (partList == null || partList.Count == 0)
                {
                    part = GetPartByKey(partID);
                    if (part != null)
                    {
                        partList.Add(part);
                    }
                }
                return partList;
            }
        }

        public List<ViewPart> QueryParts(Part partInfo)//(string partCode, string chineseName, int? plantID, string followup, string book)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                return busi.QueryParts(partInfo).ToList();
            }
        }


        public List<ViewPart> QueryPartsByGroup(PartGroup group)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                List<ViewPart> partList = busi.QueryPartsByGroup(group).ToList();
                return partList;
            }
        }

        public List<ViewPart> QueryPartByPage(Part part, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                List<ViewPart> partList = busi.QueryPartByPage(part, pageSize, pageNumber, out pageCount, out itemCount).ToList();
                return partList;
            }
        }
        public List<ViewPart> QueryPartCodeScope(Part part, string startCode, string endCode)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                List<ViewPart> partList = busi.QueryPartCodeScope(part, startCode, endCode).ToList();
                return partList;
            }
        }

        public List<View_StocktakeDetails> QueryStocktakeDetailsScope(View_StocktakeDetails stocktakeDetails, string startCode, string endCode)
        {
            StocktakeDetailBLL busi = new StocktakeDetailBLL();
            using (busi.Context)
            {
                List<View_StocktakeDetails> stocktakeDetailsList = busi.QueryStocktakeDetailsScope(stocktakeDetails, startCode, endCode).ToList();
                return stocktakeDetailsList;
            }
        }
        public List<ViewPart> QueryUnRequestedPartByPage(StocktakeRequest request, Part filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                List<ViewPart> partList = (List<ViewPart>)busi.QueryUnRequestedPartByPage(request, filter, pageSize, pageNumber, out pageCount, out itemCount);
                return partList;
            }
        }

        public List<ViewPart> QueryUngroupedPartsByPage(Part part, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                List<ViewPart> partList = busi.QueryUngroupedPartsByPage(part, pageSize, pageNumber, out pageCount, out itemCount).ToList();
                return partList;
            }
        }

        public List<ViewPart> QueryPartsByKey(List<ViewPart> list)
        {
            PartBLL bll = new PartBLL();
            using (bll.Context)
            {
                return bll.QueryPartsByKey(list);
            }
        }


        public List<ViewPart> QueryPartsOfScope(string startCode, string endCode)
        {
            PartBLL bll = new PartBLL();
            using (bll.Context)
            {
                return bll.QueryPartofScope(startCode, endCode).ToList();
            }
        }

        public List<ViewPart> QueryPartsByCodePlant(List<ViewPart> list)
        {
            PartBLL bll = new PartBLL();
            using (bll.Context)
            {
                return bll.QueryPartsByCodePlant(list);
            }
        }

        public Part GetPartByKey(string partID)
        {
            Part partInfo = new Part { PartID = int.Parse(partID) };
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                partInfo = busi.GetPartbyKey(partInfo);
                return partInfo;
            }
        }

        public List<ViewPart> GetViewPartsByPartIDs(List<string> partids)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                return busi.GetViewPartsByPartIDs(partids);
            }
        }


        public List<PartGroup> GetGroupsByPart(Part part)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                return busi.GetGroupsByPart(part).ToList();
            }
        }

        public ViewPart GetViewPartByKey(string partID)
        {
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                return busi.GetViewPartByKey(partID);
            }
        }

        public byte[] ExportParts(Part filter, out string message)
        {
            byte[] byteArray = new byte[0];
            PartBLL busi = new PartBLL();
            using (busi.Context)
            {
                List<ViewPart> list = busi.QueryParts(filter).OrderBy(p => p.PartCode).ThenBy(p => p.PlantCode).ThenBy(p => p.DUNS).ToList();
                string fileName;
                string schemaFile = "Part.xml";//Path.GetFullPath(@"ExportSchema\Part.xml");
                string tmpFile;
                int count = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].RowNumber = i + 1;
                }
                PrepareExportTemp(ref schemaFile, out tmpFile);
                string tmpDir = Path.GetDirectoryName(schemaFile) + @"\";//Path.GetFullPath(@"ExportSchema\");

                byteArray = MaterializeToBytes2<ViewPart>(list, schemaFile, tmpDir, out fileName, out message);
                list.Clear();
                if (byteArray.Length > 1024000)
                {
                    GC.Collect();
                }
                return byteArray;
            }
        }

        public void ImportPart(List<ViewPart> list)
        {
            PartBLL bll = new PartBLL();
            using (bll.Context)
            {
                bll.ImportPart(list);
            }
        }

        public void ImportAnalyseRef(List<View_StocktakeDetails> list, List<long> notiList)
        {
            DiffAnalyseReportBLL bll = new DiffAnalyseReportBLL();
            using (bll.Context)
            {
                bll.ImportRefData(list, notiList);
            }
        }

        public byte[] ExportAnalyseReport(List<int> details)
        {
            DiffAnalyseReportBLL bll = new DiffAnalyseReportBLL();
            using (bll.Context)
            {
                string schemaFile = "差异分析模板.xls";//Path.GetFullPath(@"ExportSchema\差异分析模板.xls");
                //string path = Path.GetFullPath(@"ExportSchema\");
                string tmpFile;// = path + Path.GetRandomFileName() + ".xls";
                PrepareExportTemp(ref schemaFile, out tmpFile);
                File.Copy(schemaFile, tmpFile);
                File.SetAttributes(tmpFile, FileAttributes.Normal);
                DataSet ds = bll.GetReports(details);//new DataSet();

                //List<string> columns = new List<string> { "PN", "零件名称", "工厂", "车间", "工段", "申请人", "申请类别", "返修", "外协", "UNIT PRICE", "盘点级别", "外协duns", "盘点日期", "SGM", "RDC", "DUNS" };
                //for (int i = 0; i < ds.Tables.Count; i++)
                //{
                //    DataTable dt = ds.Tables[i];
                //    for (int j = dt.Columns.Count - 1; j >= 0; j--)
                //    {
                //        if (!columns.Contains(dt.Columns[j].ColumnName))
                //        {
                //            dt.Columns.RemoveAt(j);
                //        }
                //    }
                //}
                string msg;
                try
                {
                    if (ExcelHelper.WriteExcel(tmpFile, ds, out msg))
                    {

                        byte[] content = File.ReadAllBytes(tmpFile);
                        if (HttpContext.Current == null)
                        {
                            return Utils.Compress(content);
                        }
                        else
                        {
                            return content;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    File.Delete(tmpFile);
                }
            }
        }

        public byte[] ExportSelectedParts(List<string> parts, out string message)
        {
            byte[] byteArray = new byte[0];
            PartBLL busi = new PartBLL();

            using (busi.Context)
            {
                List<ViewPart> list = busi.GetViewPartsByPartIDs(parts).OrderBy(p => p.PartCode).ThenBy(p => p.PlantCode).ThenBy(p => p.DUNS).ToList();
                string fileName;
                string schemaFile = "Part.xml";//Path.GetFullPath(@"ExportSchema\Part.xml");
                string tmpFile;
                PrepareExportTemp(ref schemaFile, out tmpFile);
                string tmpDir = Path.GetDirectoryName(schemaFile) + @"\";//Path.GetFullPath(@"ExportSchema\");

                byteArray = MaterializeToBytes<ViewPart>(list, schemaFile, tmpDir, out fileName, out message);
                list.Clear();
                return byteArray;
            }
        }

        private byte[] MaterializeToBytes2<T>(List<T> list, string schemaFile, string tmpDir, out string fileName, out string message)
        {
            byte[] byteArray = new byte[0];
            DataTable dt = DataSetLinqOperators.CopyToDataTable<T>(list);//Utils.ListToDataTable2(list);
            if (ExcelHelper.TransferDataTableToCSVFile(dt, tmpDir, schemaFile, out fileName, out message))
            {
                dt.Clear();
                fileName = tmpDir + fileName;
                FileInfo fi = new FileInfo(fileName);
                using (FileStream fs = fi.OpenRead())
                {
                    //Read all bytes into an array from the specified file.
                    int nBytes = (int)fs.Length;
                    byteArray = new byte[nBytes];
                    int nBytesRead = fs.Read(byteArray, 0, nBytes);
                    fs.Close();

                }
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            return byteArray;
        }

        private byte[] MaterializeToBytes<T>(List<T> list, string schemaFile, string tmpDir, out string fileName, out string message)
        {
            byte[] byteArray = new byte[0];
            DataTable dt = Utils.ListToDataSet(list).Tables[0];
            if (ExcelHelper.TransferDataTableToCSVFile(dt, tmpDir, schemaFile, out fileName, out message))
            {
                dt.Clear();
                fileName = tmpDir + fileName;
                FileInfo fi = new FileInfo(fileName);
                using (FileStream fs = fi.OpenRead())
                {
                    //Read all bytes into an array from the specified file.
                    int nBytes = (int)fs.Length;
                    byteArray = new byte[nBytes];
                    int nBytesRead = fs.Read(byteArray, 0, nBytes);
                    fs.Close();

                }
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            return byteArray;
        }
        public List<ViewPart> QueryFilteredPartsByPage(View_StocktakeDetails filter, List<ViewPart> filteredParts, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            PartBLL bll = new PartBLL();
            using (bll.Context)
            {
                return bll.QueryFilteredPartsByPage(filter, filteredParts, pageSize, pageNumber, out pageCount, out itemCount).ToList();
            }
        }

        #endregion Part_PartCategory_PartStatus...

        #region PartCategory...
        public List<PartCategory> QueryPartCategorys(PartCategory info)
        {
            PartCategoryBLL busi = new PartCategoryBLL();
            using (busi.Context)
            {
                return busi.GetPartCategories(info);
            }
        }

        public List<PartCategory> QueryPartCategorysByPage(PartCategory info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            throw new NotImplementedException();
        }

        public PartCategory GetPartCategoryByKey(PartCategory info)
        {
            PartCategoryBLL busi = new PartCategoryBLL();
            using (busi.Context)
            {
                return busi.GetPartCategoryByKey(info);
            }
        }

        public void UpdatePartCategory(PartCategory model)
        {
            PartCategoryBLL busi = new PartCategoryBLL();
            using (busi.Context)
            {
                busi.UpdatePartCategory(model);
            }
        }

        public PartCategory AddPartCategory(PartCategory model)
        {
            PartCategoryBLL busi = new PartCategoryBLL();
            using (busi.Context)
            {
                return busi.AddPartCategory(model);
            }
        }

        public void DeletePartCategory(PartCategory model)
        {
            PartCategoryBLL busi = new PartCategoryBLL();
            using (busi.Context)
            {
                busi.DeletePartCategory(model);
            }
        }
        #endregion PartCategory ......

        #region PartStatus...
        public List<PartStatus> QueryPartStatuss(PartStatus info)
        {
            PartStatusBLL bll = new PartStatusBLL();
            using (bll.Context)
            {
                try
                {
                    return bll.QueryPartStatusAvailable(info);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleBLLException(ex);
                    return new List<SGM.ECount.DataModel.PartStatus>();
                }
            }
        }

        public List<PartStatus> QueryPartStatussByPage(PartStatus info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            throw new NotImplementedException();
        }

        public PartStatus GetPartStatusByKey(PartStatus info)
        {
            PartStatusBLL busi = new PartStatusBLL();
            using (busi.Context)
            {
                return busi.GetPartStatusByKey(info);
            }
        }

        public void UpdatePartStatus(PartStatus model)
        {
            PartStatusBLL busi = new PartStatusBLL();
            using (busi.Context)
            {
                busi.UpdatePartStatus(model);
            }
        }

        public PartStatus AddPartStatus(PartStatus model)
        {
            PartStatusBLL busi = new PartStatusBLL();
            using (busi.Context)
            {
                return busi.AddPartStatus(model);
            }
        }

        public void DeletePartStatus(PartStatus model)
        {
            PartStatusBLL busi = new PartStatusBLL();
            using (busi.Context)
            {
                busi.DeletePartStatus(model);
            }
        }

        public void DeletePartStatuss(List<string> ids)
        {
            PartStatusBLL busi = new PartStatusBLL();
            using (busi.Context)
            {
                busi.DeletePartStatuss(ids);
            }
        }

        public bool ExistPartStatus(PartStatus model)
        {
            PartStatusBLL bll = new PartStatusBLL();
            using (bll.Context)
            {
                return bll.ExistPartStatus(model);
            }
        }
        #endregion PartStatus ......

        #region PartGroup...
        public PartGroup GetPartGroupByKey(int groupID)
        {
            PartGroupBLL busi = new PartGroupBLL();
            using (busi.Context)
            {
                return busi.GetPartGroupByKey(new PartGroup { GroupID = groupID });
            }
        }

        public List<PartGroup> QueryPartGroups(PartGroup info)
        {
            PartGroupBLL busi = new PartGroupBLL();
            using (busi.Context)
            {
                return busi.QueryPartGroups(info);
            }
        }

        public PartGroup AddPartGroup(PartGroup partGroup)
        {
            PartGroupBLL busi = new PartGroupBLL();
            using (busi.Context)
            {
                return busi.AddPartGroup(partGroup);
            }
        }

        public void UpdatePartGroup(PartGroup partGroup)
        {
            PartGroupBLL busi = new PartGroupBLL();
            using (busi.Context)
            {
                busi.UpdatePartGroup(partGroup);
            }
        }

        public void DeletePartGroup(int groupID)
        {
            PartGroupBLL busi = new PartGroupBLL();
            using (busi.Context)
            {
                busi.DeletePartGroup(new PartGroup { GroupID = groupID });
            }
        }

        public byte[] ExportPartGroup(PartGroup record, out string message)
        {
            byte[] byteArray = new byte[0];
            PartGroupBLL busi = new PartGroupBLL();

            using (busi.Context)
            {
                List<PartGroup> list = busi.QueryPartGroups(record);

                string fileName;
                string schemaFile = "PartGroups.xml";
                string tmpFile;
                PrepareExportTemp(ref schemaFile, out tmpFile);
                string tmpDir = Path.GetDirectoryName(schemaFile) + @"\";
                byteArray = MaterializeToBytes<PartGroup>(list, schemaFile, tmpDir, out fileName, out message);

                return byteArray;
            }
        }

        #endregion PartGroup... ...

        #region Supplier...
        public int GetSuppliersCount(Supplier info)
        {
            SupplierBLL busi = new SupplierBLL();
            using (busi.Context)
            {
                return busi.GetSuppliersCount(info);
            }
        }

        public List<Supplier> QuerySuppliers(Supplier info)
        {
            SupplierBLL busi = new SupplierBLL();
            using (busi.Context)
            {
                return busi.QuerySupplier(info).ToList();
            }
        }
        public List<Supplier> QuerySuppliersByPage(Supplier info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            SupplierBLL busi = new SupplierBLL();
            return busi.QuerySupplierByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
        }

        public Supplier GetSupplierbykey(Supplier info)
        {
            SupplierBLL busi = new SupplierBLL();
            using (busi.Context)
            {
                return busi.GetSupplierbykey(info);
            }
        }

        public void UpdateSupplier(Supplier model)
        {
            SupplierBLL busi = new SupplierBLL();
            using (busi.Context)
            {
                busi.UpdateSupplier(model);
            }
        }

        public Supplier AddSupplier(Supplier model)
        {
            SupplierBLL busi = new SupplierBLL();
            using (busi.Context)
            {
                busi.AddSupplier(model);
                return model;
            }
        }

        public void DeleteSupplier(Supplier model)
        {
            SupplierBLL busi = new SupplierBLL();
            using (busi.Context)
            {
                busi.DeleteSupplier(model);
            }
        }
        public void DeleteSuppliers(List<int> ids)
        {
            SupplierBLL busi = new SupplierBLL();
            using (busi.Context)
            {
                busi.DeleteSuppliers(ids);
            }
        }
        #endregion Supplier ......

        #region ConsignmentPartRecord ...
        public ConsignmentPartRecord GetConsignmentPartRecordbykey(ConsignmentPartRecord record)
        {
            ConsignmentPartBLL busi = new ConsignmentPartBLL();
            using (busi.Context)
            {
                return busi.GetConsignmentPartRecordbykey(record);
            }
        }

        public List<ConsignmentPartRecord> QueryConsignmentPartRecordsByPage(ConsignmentPartRecord record, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            ConsignmentPartBLL busi = new ConsignmentPartBLL();
            using (busi.Context)
            {
                return busi.GetRecordsByPage(record, pageSize, pageNumber, out pageCount, out itemCount);
            }
        }

        public void UpdateConsignmentPartRecord(ConsignmentPartRecord record)
        {
            ConsignmentPartBLL busi = new ConsignmentPartBLL();
            using (busi.Context)
            {
                busi.UpdateConsignmentPartRecord(record);
            }
        }

        public void DeleteConsignmentPartRecord(ConsignmentPartRecord record)
        {
            ConsignmentPartBLL busi = new ConsignmentPartBLL();
            using (busi.Context)
            {
                busi.DeleteConsignmentPartRecord(record);
            }
        }

        public List<ConsignmentPartRecord> QueryConsignmentPartRecords(ConsignmentPartRecord record)
        {
            ConsignmentPartBLL busi = new ConsignmentPartBLL();
            using (busi.Context)
            {
                return QueryConsignmentPartRecords(record);
            }
        }

        public ConsignmentPartRecord AddConsignmentPartRecord(ConsignmentPartRecord record)
        {
            ConsignmentPartBLL busi = new ConsignmentPartBLL();
            using (busi.Context)
            {
                return busi.AddConsignmentPartRecord(record);
            }
        }

        public List<ConsignmentPartRecord> QueryRecords(ConsignmentPartRecord filter)
        {
            ConsignmentPartBLL bll = new ConsignmentPartBLL();
            using (bll.Context)
            {
                return bll.QueryRecords(filter).ToList(); ;
            }
        }

        public byte[] ExportConsignmentParts(View_ConsignmentPart record, out string message)
        {
            byte[] byteArray = new byte[0];
            ConsignmentPartBLL busi = new ConsignmentPartBLL();
            using (busi.Context)
            {
                List<View_ConsignmentPart> list = busi.GetViewConsignment(record);

                string fileName;
                string schemaFile = "ConsignmentParts.xml";//Path.GetFullPath(@"ExportSchema\ConsignmentParts.xml");
                string tmpFile;
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].RowNumber = i + 1;
                }
                PrepareExportTemp(ref schemaFile, out tmpFile);
                string tmpDir = Path.GetDirectoryName(schemaFile) + @"\";//Path.GetFullPath(@"ExportSchema\");
                byteArray = MaterializeToBytes<View_ConsignmentPart>(list, schemaFile, tmpDir, out fileName, out message);

                return byteArray;
            }
        }

        public void ImportConsignmentRecord(List<View_ConsignmentPart> list)
        {
            ConsignmentPartBLL bll = new ConsignmentPartBLL();
            using (bll.Context)
            {
                bll.ImportConsignmentRecord(list);
            }
        }

        #endregion ConsignmentPartRecord ... ...

        #region PartRepairRecord...
        public List<PartRepairRecord> QueryPartRepairRecords(PartRepairRecord info)
        {
            PartRepairRecordBLL busi = new PartRepairRecordBLL();
            using (busi.Context)
            {
                return busi.QueryPartRepairRecords(info).ToList();
            }
        }

        public List<PartRepairRecord> QueryPartRepairRecordsByPage(PartRepairRecord info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            PartRepairRecordBLL busi = new PartRepairRecordBLL();
            using (busi.Context)
            {
                return busi.GetPartRepairRecordsByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
            }
        }

        public PartRepairRecord GetPartRepairRecordbykey(PartRepairRecord info)
        {
            PartRepairRecordBLL busi = new PartRepairRecordBLL();
            using (busi.Context)
            {
                return busi.GetPartRepairRecordbykey(info);
            }
        }

        public void UpdatePartRepairRecord(PartRepairRecord model)
        {
            PartRepairRecordBLL busi = new PartRepairRecordBLL();
            using (busi.Context)
            {
                busi.UpdatePartRepairRecord(model);
            }
        }

        public PartRepairRecord AddPartRepairRecord(PartRepairRecord model)
        {
            PartRepairRecordBLL busi = new PartRepairRecordBLL();
            using (busi.Context)
            {
                return busi.AddPartRepairRecord(model);
            }
        }

        public void DeletePartRepairRecord(PartRepairRecord model)
        {
            PartRepairRecordBLL busi = new PartRepairRecordBLL();
            using (busi.Context)
            {
                busi.DeletePartRepairRecord(model);
            }
        }

        public void DeletePartRepairRecords(List<string> idList)
        {
            PartRepairRecordBLL busi = new PartRepairRecordBLL();
            using (busi.Context)
            {
                busi.DeletePartRepairRecords(idList);
            }
        }

        public void ImportPartRepairRecord(List<View_PartRepairRecord> list)
        {
            PartRepairRecordBLL bll = new PartRepairRecordBLL();
            using (bll.Context)
            {
                bll.ImportPartRepairRecord(list);
            }
        }
        #endregion PartRepairRecord ......

        #region SupplierStoreLocation...
        public List<SupplierStoreLocation> QuerySupplierStoreLocations(SupplierStoreLocation info)
        {
            throw new NotImplementedException();
        }

        public SupplierStoreLocation GetSupplierStoreLocationbykey(SupplierStoreLocation info)
        {
            throw new NotImplementedException();
        }

        public void UpdateSupplierStoreLocation(SupplierStoreLocation model)
        {
            throw new NotImplementedException();
        }

        public void AddSupplierStoreLocation(SupplierStoreLocation model)
        {
            throw new NotImplementedException();
        }

        public void DeleteSupplierStoreLocation(SupplierStoreLocation model)
        {
            throw new NotImplementedException();
        }
        #endregion SupplierStoreLocation ......

        #region StoreLocationType...
        public List<StoreLocationType> QueryStoreLocationTypes(StoreLocationType info)
        {
            StoreLocationTypeBLL busi = new StoreLocationTypeBLL();
            using (busi.Context)
            {
                return busi.GetStoreLocationType(info);
            }
        }

        public List<StoreLocationType> QueryStoreLocationTypesByPage(StoreLocationType info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            throw new NotImplementedException();
        }

        public StoreLocationType GetStoreLocationTypeByKey(StoreLocationType info)
        {
            StoreLocationTypeBLL busi = new StoreLocationTypeBLL();
            using (busi.Context)
            {
                return busi.GetStoreLocationTypeByKey(info);
            }
        }

        public void UpdateStoreLocationType(StoreLocationType model)
        {
            StoreLocationTypeBLL busi = new StoreLocationTypeBLL();
            using (busi.Context)
            {
                busi.UpdateStoreLocationType(model);
            }
        }

        public StoreLocationType AddStoreLocationType(StoreLocationType model)
        {
            StoreLocationTypeBLL busi = new StoreLocationTypeBLL();
            using (busi.Context)
            {
                return busi.AddStoreLocationType(model);
            }
        }

        public void DeleteStoreLocationType(StoreLocationType model)
        {
            throw new NotImplementedException();
        }

        public void DeleteStoreLocationTypes(List<string> ids)
        {
            StoreLocationTypeBLL busi = new StoreLocationTypeBLL();
            using (busi.Context)
            {
                busi.DeleteStoreLocationTypes(ids);
            }
        }

        public bool ExistStoreLocationType(StoreLocationType model)
        {
            StoreLocationTypeBLL busi = new StoreLocationTypeBLL();
            using (busi.Context)
            {
                return busi.ExistStoreLocationType(model);
            }
        }
        #endregion StoreLocationType ......

        #region StoreLocation...
        public List<StoreLocation> QueryStoreLocations(StoreLocation info)
        {
            StoreLocationBLL busi = new StoreLocationBLL();
            using (busi.Context)
            {
                return busi.QueryStoreLocations(info);
            }
        }

        public List<StoreLocation> QueryStoreLocationsByPage(StoreLocation info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            throw new NotImplementedException();
        }

        public List<StoreLocation> GetStoreLocations()
        {
            return new StoreLocationBLL().GetStoreLocations();
        }

        public StoreLocation GetStoreLocationByKey(StoreLocation info)
        {
            StoreLocationBLL busi = new StoreLocationBLL();
            using (busi.Context)
            {
                return busi.GetStoreLocationByKey(info);
            }
        }

        public void UpdateStoreLocation(StoreLocation model)
        {
            StoreLocationBLL busi = new StoreLocationBLL();
            using (busi.Context)
            {
                busi.UpdateStoreLocation(model);
            }
        }

        public StoreLocation AddStoreLocation(StoreLocation model)
        {
            StoreLocationBLL busi = new StoreLocationBLL();
            using (busi.Context)
            {
                return busi.AddStoreLocation(model);
            }
        }

        public void DeleteStoreLocation(StoreLocation model)
        {
            throw new NotImplementedException();
        }
        public void DeleteStoreLocations(List<string> ids)
        {
            StoreLocationBLL busi = new StoreLocationBLL();
            using (busi.Context)
            {
                busi.DeleteStoreLocations(ids);
            }
        }

        public bool ExistStoreLocation(StoreLocation model)
        {
            StoreLocationBLL busi = new StoreLocationBLL();
            using (busi.Context)
            {
                return busi.ExistStoreLocation(model);
            }
        }

        public void ImportStoreLocation(List<S_StoreLocation> list)
        {
            StoreLocationBLL bll = new StoreLocationBLL();
            using (bll.Context)
            {
                bll.ImportStoreLocation(list);
            }
        }

        public byte[] ExportStoreLocations(StoreLocation record, out string message)
        {
            byte[] byteArray = new byte[0];
            StoreLocationBLL busi = new StoreLocationBLL();

            using (busi.Context)
            {
                List<StoreLocation> list = QueryStoreLocations(record);

                List<S_StoreLocation> recordlist = new List<S_StoreLocation>();

                for (int i = 0; i < list.Count; i++)
                {
                    S_StoreLocation storeloc = new S_StoreLocation();
                    storeloc.LocationName = list[i].LocationName;
                    storeloc.TypeID = list[i].StoreLocationType.TypeID;
                    storeloc.LogisticsSysSLOC = list[i].LogisticsSysSLOC;
                    storeloc.AvailableIncluded = Convert.ToInt32(list[i].AvailableIncluded);
                    storeloc.QIIncluded = Convert.ToInt32(list[i].QIIncluded);
                    storeloc.BlockIncluded = Convert.ToInt32(list[i].BlockIncluded);

                    recordlist.Add(storeloc);
                }
                if (list.Count == 0)
                {
                    message = "无导出数据";
                }
                else
                {
                    string fileName;
                    string schemaFile = "StoreLocations.xml";
                    string tmpFile;
                    PrepareExportTemp(ref schemaFile, out tmpFile);
                    string tmpDir = Path.GetDirectoryName(schemaFile) + @"\";
                    byteArray = MaterializeToBytes<S_StoreLocation>(recordlist, schemaFile, tmpDir, out fileName, out message);
                }

                return byteArray;
            }
        }

        #endregion StoreLocation ......

        public class SupplierView
        {
            public string No { get; set; }
            public string SupplierName { get; set; }
            public string Duns { get; set; }
            public string PhoneNumber1 { get; set; }
            public string PhoneNumber2 { get; set; }
            public string fax { get; set; }
            public string Description { get; set; }
        }

        public byte[] ExportSupplier(Supplier record, out string message)
        {
            byte[] byteArray = new byte[0];
            SupplierBLL busi = new SupplierBLL();

            using (busi.Context)
            {
                List<Supplier> list = busi.QuerySupplier(new Supplier { DUNS = record.DUNS, SupplierName = record.SupplierName }).ToList();
                List<SupplierView> recordlist = new List<SupplierView>();

                for (int i = 0; i < list.Count; i++)
                {
                    SupplierView supplier = new SupplierView();
                    supplier.No = (i + 1).ToString();
                    if (!string.IsNullOrEmpty(list[i].SupplierName.ToString()))
                    {
                        supplier.SupplierName = list[i].SupplierName.ToString();
                    }
                    if (!string.IsNullOrEmpty(list[i].DUNS.ToString()))
                    {
                        supplier.Duns = list[i].DUNS.ToString();
                    }
                    if (!string.IsNullOrEmpty(list[i].PhoneNumber1))
                    {
                        supplier.PhoneNumber1 = list[i].PhoneNumber1;
                    }
                    if (!string.IsNullOrEmpty(list[i].PhoneNumber2))
                    {
                        supplier.PhoneNumber2 = list[i].PhoneNumber2;
                    }

                    if (!string.IsNullOrEmpty(list[i].Fax))
                    {
                        supplier.fax = list[i].Fax.ToString();
                    }
                    if (!string.IsNullOrEmpty(list[i].Description))
                    {
                        supplier.Description = list[i].Description.ToString();
                    }

                    recordlist.Add(supplier);
                }
                string fileName;
                string schemaFile = "Suppliers.xml";//Path.GetFullPath(@"ExportSchema\ConsignmentParts.xml");
                string tmpFile;
                PrepareExportTemp(ref schemaFile, out tmpFile);
                string tmpDir = Path.GetDirectoryName(schemaFile) + @"\";//Path.GetFullPath(@"ExportSchema\");
                byteArray = MaterializeToBytes<SupplierView>(recordlist, schemaFile, tmpDir, out fileName, out message);

                return byteArray;
            }
        }

        //#region SupplierStoretakeItem...
        //public List<SupplierStoretakeItem> QuerySupplierStoretakeItems(SupplierStoretakeItem info)
        //{
        //    throw new NotImplementedException();
        //}

        //public SupplierStoretakeItem GetSupplierStoretakeItembykey(SupplierStoretakeItem info)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateSupplierStoretakeItem(SupplierStoretakeItem model)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddSupplierStoretakeItem(SupplierStoretakeItem model)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteSupplierStoretakeItem(SupplierStoretakeItem model)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion SupplierStoretakeItem ......

        #region StocktakeType...
        public List<SGM.ECount.DataModel.StocktakeType> GetStocktakeTypes()
        {
            StocktakeTypeBLL busi = new StocktakeTypeBLL();
            using (busi.Context)
            {

                return busi.GetStocktakeTypes();
            }
        }

        public List<StocktakeType> QueryStocktakeTypes(StocktakeType info)
        {
            StocktakeTypeBLL busi = new StocktakeTypeBLL();
            using (busi.Context)
            {
                return busi.QueryStocktakeTypes(info);
            }
        }

        public StocktakeType GetStocktakeTypeByKey(StocktakeType info)
        {
            StocktakeTypeBLL busi = new StocktakeTypeBLL();
            using (busi.Context)
            {
                return busi.GetStocktakeTypeByKey(info);
            }
        }

        public void UpdateStocktakeType(StocktakeType model)
        {
            StocktakeTypeBLL busi = new StocktakeTypeBLL();
            using (busi.Context)
            {
                busi.UpdateStocktakeType(model);
            }
        }

        public StocktakeType AddStocktakeType(StocktakeType model)
        {
            StocktakeTypeBLL busi = new StocktakeTypeBLL();
            using (busi.Context)
            {
                return busi.AddStocktakeType(model);
            }
        }

        public void DeleteStocktakeType(StocktakeType model)
        {
            throw new NotImplementedException();
        }

        public void DeleteStocktakeTypes(List<string> ids)
        {
            StocktakeTypeBLL busi = new StocktakeTypeBLL();
            using (busi.Context)
            {
                busi.DeleteStocktakeTypes(ids);
            }
        }

        public bool ExistStocktakeType(StocktakeType model)
        {
            StocktakeTypeBLL busi = new StocktakeTypeBLL();
            using (busi.Context)
            {
                return busi.ExistStocktakeType(model);
            }
        }
        #endregion StocktakeType ......

        #region bizParams ....
        public void UpdateBizParams(BizParams model)
        {
            BizParamsBLL busi = new BizParamsBLL();
            using (busi.Context)
            {
                busi.UpdateBizParams(model);
            }
        }

        public void UpdateBizParamsList(List<BizParams> models)
        {
            BizParamsBLL busi = new BizParamsBLL();
            using (busi.Context)
            {
                busi.UpdateBizParamsList(models);
            }
        }

        public BizParams GetBizParams(BizParams model)
        {
            BizParamsBLL busi = new BizParamsBLL();
            using (busi.Context)
            {
                return busi.GetBizParams(model);
            }
        }

        public List<BizParams> GetBizParamsList()
        {
            BizParamsBLL busi = new BizParamsBLL();
            using (busi.Context)
            {
                return busi.GetBizParamsList();
            }
        }
        #endregion
    }
}
