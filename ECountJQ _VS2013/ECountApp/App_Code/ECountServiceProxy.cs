using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SGM.ECount.Contract.Service;
using System.ServiceModel;
using SGM.ECount.DataModel;
using SGM.Common.Exception;
using System.Threading;
using System.Security.Principal;
using SGM.Common.Utility;
using System.Data;
using System.IO;
using System.IO.Compression;

/// <summary>
/// Summary description for ECountServiceProxy
/// </summary>
public class ECountServiceProxy : ClientBase<IECountService>, IECountService
{
    public string UserName { get; set; }

    public string Password { get; set; }

    #region Constructor...
    public ECountServiceProxy()
        : base("ECountClient")
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public ECountServiceProxy(string endpointConfiguartionName)
        : base(endpointConfiguartionName)
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public ECountServiceProxy(string userName, string Password)
        : base("ECountClient")
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public ECountServiceProxy(string endpointConfiguartionName, string userName, string Password)
        : base(endpointConfiguartionName)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion Constructor......

    #region User ...
    public void Login()
    {
        throw new NotImplementedException();
    }

    public User AddUser(User model)
    {
        return Channel.AddUser(model);
    }

    public void UpdateUser(SGM.ECount.DataModel.User user)
    {
        Channel.UpdateUser(user);
    }

    public void DeleteUsers(List<string> ids)
    {
        Channel.DeleteUsers(ids);
    }

    public List<SGM.ECount.DataModel.User> GetUsers()
    {
        return Channel.GetUsers();
    }

    public User GetUserbyKey(User user)
    {
        //ECountIdentity currentIdentity = Thread.CurrentPrincipal.Identity as ECountIdentity;
        //if (currentIdentity.UserInfo.UserGroup.SysAdmin.Value || currentIdentity.UserInfo.UserID == user.UserID)
        //{
        return Channel.GetUserbyKey(user);
        //}
        //else
        //{
        //    return user;
        //}
    }

    public User GetUserbyName(string userName)
    {
        //IIdentity currentIdentity = Thread.CurrentPrincipal.Identity;
        //if (currentIdentity.Name == userName)
        //{
        return Channel.GetUserbyName(userName);
        //}
        //else
        //{
        //    return null;
        //}
    }

    public bool ExistUser(User user)
    {
        return Channel.ExistUser(user);
    }

    public List<User> QueryUsersByPage(User info)
    {
        return Channel.QueryUsersByPage(info);
    }

    public void AddUserToGroup(int userID, int groupID)
    {
        Channel.AddUserToGroup(userID, groupID);
    }

    public List<SGM.ECount.DataModel.Operation> GetOperations()
    {
        // throw new NotImplementedException();
        return Channel.GetOperations();
    }

    public SGM.ECount.DataModel.Operation GetOperationByKey(SGM.ECount.DataModel.Operation operation)
    {
        // throw new NotImplementedException();
        return Channel.GetOperationByKey(operation);
    }

    public List<SGM.ECount.DataModel.Operation> GetOperationsByOperation(SGM.ECount.DataModel.Operation operation)
    {
        return Channel.GetOperationsByOperation(operation);
    }

    public List<SGM.ECount.DataModel.UserGroup> GetUserGroupsByOperation(SGM.ECount.DataModel.Operation operation)
    {
        List<UserGroup> groups = this.GetUserGroups();//Utils.GetCache("UserGroup") as List<UserGroup>;
        List<UserGroup> authorizedRoles = groups.Where(g => g.Operations.Count(o => o.CommandName == operation.CommandName) > 0).ToList();

        return authorizedRoles;
    }

    public List<SGM.ECount.DataModel.Operation> GetOperationsByUserGroup(SGM.ECount.DataModel.UserGroup group)
    {
        //throw new NotImplementedException();
        return Channel.GetOperationsByUserGroup(group);
    }
    #endregion User ......

    #region UserGroup...
    public List<SGM.ECount.DataModel.UserGroup> GetUserGroups()
    {
        //List<UserGroup> groups = Utils.GetCache(Consts.CACHE_KEY_USER_GROUPS) as List<UserGroup>;
        //if (groups == null || groups.Count > 0)
        //{
        List<UserGroup> groups = Channel.GetUserGroups();
        //    Utils.SetCache(Consts.CACHE_KEY_USER_GROUPS, groups);
        //}
        return groups;
    }

    public List<UserGroup> QueryUserGroups(UserGroup info)
    {
        return Channel.QueryUserGroups(info);
    }

    public UserGroup GetUserGroupByKey(UserGroup info)
    {
        return Channel.GetUserGroupByKey(info);
    }

    public void UpdateUserGroup(UserGroup model)
    {
        Channel.UpdateUserGroup(model);
    }

    public UserGroup AddUserGroup(UserGroup model)
    {
        return Channel.AddUserGroup(model);
    }

    public void DeleteUserGroup(UserGroup model)
    {
        Channel.DeleteUserGroup(model);
    }

    public void DeleteUserGroups(List<string> ids)
    {
        Channel.DeleteUserGroups(ids);
    }

    public bool ExistUserGroup(UserGroup model)
    {
        return Channel.ExistUserGroup(model);
    }
    #endregion UserGroup ......

    #region DifferenceAnalyzeDetails...
    public List<SGM.ECount.DataModel.DifferenceAnalyseDetails> GetDifferenceAnalyseDetails()
    {
        return Channel.GetDifferenceAnalyseDetails();
    }

    public DifferenceAnalyseDetails GetDiffAnalyseDetailstbyID(int detailsID)
    {
        return Channel.GetDiffAnalyseDetailstbyID(detailsID);
    }

    public void AddDiffAnalyseDetail(DifferenceAnalyseDetails detail)
    {
        Channel.AddDiffAnalyseDetail(detail);
    }

    public void UpdateDiffAnalyseDetail(DifferenceAnalyseDetails detail)
    {
        Channel.UpdateDiffAnalyseDetail(detail);
    }

    public void DeleteDiffAnalyseDetail(DifferenceAnalyseDetails detail)
    {
        Channel.DeleteDiffAnalyseDetail(detail);
    }

    public bool ExistDifferenceAnalyse(DifferenceAnalyseDetails diffAnalyseDetails)
    {
        return Channel.ExistDifferenceAnalyse(diffAnalyseDetails);
    }

    public void ImportAnalyseRef(List<View_StocktakeDetails> list, List<long> notiList)
    {
        Channel.ImportAnalyseRef(list,notiList);
    }
    #endregion DifferenceAnalyzeDetails......

    #region DifferenceAnalyzeItem...
    //public List<DifferenceAnalyzeItem> QueryDifferenceAnalyzeItems(DifferenceAnalyzeItem info)
    //{
    //    return Channel.QueryDifferenceAnalyzeItems(info);
    //}

    //public DifferenceAnalyzeItem GetDifferenceAnalyzeItemByKey(DifferenceAnalyzeItem info)
    //{
    //    return Channel.GetDifferenceAnalyzeItemByKey(info);
    //}

    //public void UpdateDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
    //{
    //    Channel.UpdateDifferenceAnalyzeItem(model);
    //}

    //public DifferenceAnalyzeItem AddDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
    //{
    //    return Channel.AddDifferenceAnalyzeItem(model);
    //}

    //public void DeleteDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
    //{
    //    Channel.DeleteDifferenceAnalyzeItem(model);
    //}

    //public void DeleteDifferenceAnalyzeItems(List<string> ids)
    //{
    //    Channel.DeleteDifferenceAnalyzeItems(ids);
    //}

    //public bool ExistDifferenceAnalyzeItem(DifferenceAnalyzeItem model)
    //{
    //    return Channel.ExistDifferenceAnalyzeItem(model);
    //}
    #endregion DifferenceAnalyzeItem ......

    #region StocktakeRequest

    public SGM.ECount.DataModel.StocktakeRequest RequestStocktake(NewStocktakeRequest newRequest)
    {
        return Channel.RequestStocktake(newRequest);
    }

    public void UpdateStocktakeRequest(NewStocktakeRequest request)
    {
        Channel.UpdateStocktakeRequest(request);
    }

    public void UpdateRequest(NewStocktakeRequest request, List<int> removedDetailsList, List<View_StocktakeDetails> changedDetails)
    {
        Channel.UpdateRequest(request, removedDetailsList, changedDetails);
    }

    public void UpdateCachedRequest(NewStocktakeRequest request, string cacheKey, bool submit, bool isRemove)
    {
        Channel.UpdateCachedRequest(request, cacheKey, submit, isRemove);
    }

    public void DeleteStocktake(SGM.ECount.DataModel.StocktakeRequest request)
    {
        throw new NotImplementedException();
    }

    public List<SGM.ECount.DataModel.StocktakeRequest> QueryStocktakeRequest(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd)//(long? requestID, string requestNumber, string userName, int? plantID, string partCode, int? stocktakeType, string partChineseName, DateTime? dateStart, DateTime? dateEnd)
    {
        return Channel.QueryStocktakeRequest(condition, dateStart, dateEnd);//(requestID, requestNumber, userName, plantID, partCode, stocktakeType, partChineseName, dateStart, dateEnd);
    }

    public List<StocktakeRequest> QueryStocktakeRequestByPage(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryStocktakeRequestByPage(condition, dateStart, dateEnd, pageSize, pageNumber, out pageCount, out itemCount);
    }


    public void DeleteStocktakeRequest(long requestID)
    {
        Channel.DeleteStocktakeRequest(requestID);
    }

    public void DeleteRequestBatch(List<StocktakeRequest> reqList)
    {
        Channel.DeleteRequestBatch(reqList);
    }

    //public List<ViewStockTakeRequest> QueryStocktakeReqDetails(long? requestID, string requestNumber, string userName, int? plantID, string partCode, int? stocktakeType, string partChineseName, DateTime? dateStart, DateTime? dateEnd)
    //{
    //    return Channel.QueryStocktakeReqDetails(requestID, requestNumber, userName, plantID, partCode, stocktakeType, partChineseName, dateStart, dateEnd);
    //}

    public List<View_StocktakeDetails> QueryStocktakeReqDetails(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd)
    {
        return Channel.QueryStocktakeReqDetails(condition, dateStart, dateEnd);
    }

    public StocktakeRequest GetRequest(long requestId)
    {
        return Channel.GetRequest(requestId);
    }

    public List<View_StocktakeDetails> QueryRequestDetailsByPage(View_StocktakeDetails condition, List<View_StocktakeDetails> removedDetails, List<View_StocktakeDetails> addedDetails, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryRequestDetailsByPage(condition, removedDetails, addedDetails, dateStart, dateEnd, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public List<ViewPart> GetPartsToCycleCount(out int cycledTimes, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.GetPartsToCycleCount(out cycledTimes, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public StocktakeRequest CreateCycleCount(User user, List<View_StocktakeDetails> deletedDetails, List<View_StocktakeDetails> updatedDetails)
    {
        return Channel.CreateCycleCount(user, deletedDetails, updatedDetails);
    }

    public List<StocktakeRequest> CreateCycleCountByPlant(User user)
    {
        return Channel.CreateCycleCountByPlant(user);
    }

    public List<View_StocktakeDetails> GetNewRequestDetailsByPlant(List<View_StocktakeDetails> filter, List<View_StocktakeDetails> addition, bool isStatic, Plant plant, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.GetNewRequestDetailsByPlant(filter, addition, isStatic, plant, pageSize, pageNumber, out pageCount, out itemCount);
    }


    public List<View_StocktakeDetails> GetNewRequestDetails(View_StocktakeDetails filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.GetNewRequestDetails(filter, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public List<View_StocktakeDetails> GetNotiDetailsByPage(StocktakeNotification notice, List<View_StocktakeDetails> filter, List<View_StocktakeDetails> addition, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.GetNotiDetailsByPage(notice, filter, addition, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public StocktakeNotification GetNotification(StocktakeNotification notification)
    {
        return Channel.GetNotification(notification);
    }

    public bool NotiExistsCSMTPart(StocktakeNotification notification)
    {
        return Channel.NotiExistsCSMTPart(notification);
    }

    public bool NotiExistsRepairPart(StocktakeNotification notification)
    {
        return Channel.NotiExistsRepairPart(notification);
    }

    public StocktakeNotification CreateNotification(StocktakeNotification notification, List<long> removedDetailsList)
    {
        return Channel.CreateNotification(notification, removedDetailsList);
    }

    public void UpdateNotification(StocktakeNotification notification, List<long> removedDetailsList, List<View_StocktakeDetails> changedDetails, bool removeAll)
    {
        Channel.UpdateNotification(notification, removedDetailsList, changedDetails, removeAll);
    }

    public List<View_StocktakeDetails> QueryNotifyDetailsByPage(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryNotifyDetailsByPage(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public List<View_StocktakeItem> QueryStocktakeItem(View_StocktakeItem filter)
    {
        return Channel.QueryStocktakeItem(filter);
    }



    public void ImportAdjustment(List<View_StocktakeItem> list)
    {
        Channel.ImportAdjustment(list);
    }

    public List<View_StocktakeNotification> QueryNotiByPage
    (View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryNotiByPage(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public int RemoveAllPartsFromNoti(long notificationID)
    {
        return Channel.RemoveAllPartsFromNoti(notificationID);
    }

    public void PublishNotification(StocktakeNotification notification, List<StoreLocationType> locationTypes)
    {
        Channel.PublishNotification(notification, locationTypes);
    }

    public void DeleteNotification(List<string> notifications)
    {
        Channel.DeleteNotification(notifications);
    }

    public byte[] ExportNotification(List<String> notification, out string message, out string notiCode)
    {
        byte[] compressedContent = Channel.ExportNotification(notification, out message, out notiCode);
        return SGM.Common.Utility.Utils.DeCompress(compressedContent);
    }

    public void CancelPublish(List<string> notifications)
    {
        Channel.CancelPublish(notifications);
    }

    public List<View_StocktakeResult> GetStocktakeResult(View_StocktakeResult filter,bool loadWorkshopDetails)
    {
        return Channel.GetStocktakeResult(filter,loadWorkshopDetails);
    }


    public List<View_StocktakeResult> GetStocktakeResultOfScope(View_StocktakeResult filter, string startPartCode, string endPartCode)
    {
        return Channel.GetStocktakeResultOfScope(filter, startPartCode, endPartCode);
    }



    public List<View_ResultCSMT> GetCSMTStocktakeResult(View_ResultCSMT filter)
    {

        return Channel.GetCSMTStocktakeResult(filter).ToList();

    }


    public List<View_ResultNoneCSMT> GetNoneCSMTStocktakeResult(View_ResultNoneCSMT filter)
    {
        return Channel.GetNoneCSMTStocktakeResult(filter).ToList();

    }
    public string ImportResult(StocktakeNotification notification, List<View_StocktakeResult> itemList, string cacheKey, bool submit)
    {
        return Channel.ImportResult(notification, itemList, cacheKey, submit);
    }
    public void FillStocktakeResult(StocktakeNotification notification, List<View_StocktakeResult> list)
    {
        Channel.FillStocktakeResult(notification, list);
    }

    public void FillStocktakeAdjustment(List<View_StocktakeResult> list)
    {
        Channel.FillStocktakeAdjustment(list);
    }
    public void ImportStorage(List<S_StorageRecord> list)
    {
        Channel.ImportStorage(list);
    }

    public List<View_StocktakeDetails> QueryFullFilledNotiDetailsByPage(View_StocktakeDetails condition, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryFullFilledNotiDetailsByPage(condition, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public void CreateAnalyseReport(StocktakeNotification notice, User analyzedBy, out string reportCode, out Int64 reportID)//, string cacheKey, bool submit)
    {
        Channel.CreateAnalyseReport(notice, analyzedBy, out reportCode, out reportID);//,cacheKey,submit);
    }

    public void CreateAnalyseRptByCondition(View_StocktakeDetails filter, User analyzedBy, out string reportCode, out Int64 reportID)
    {
        Channel.CreateAnalyseRptByCondition(filter, analyzedBy, out reportCode, out reportID);
    }

    public DataSet QueryAnalyseReport(View_DifferenceAnalyse filter, DateTime? timeStart, DateTime? timeEnd)
    {
        return Channel.QueryAnalyseReport(filter, timeStart, timeEnd);
    }

    public void DeleteAnalyseItems(List<DiffAnalyseReportItem> itemsList)
    {
        Channel.DeleteAnalyseItems(itemsList);
    }
    public DiffAnalyseReport GetAnalyseReport(long reportID)
    {
        return Channel.GetAnalyseReport(reportID);
    }

    public List<View_DiffAnalyseReportDetails> GetDiffAnalyseRptDetails(DiffAnalyseReportItem item, UserGroup userGroup)
    {
        return Channel.GetDiffAnalyseRptDetails(item, userGroup);
    }

    public List<DiffAnalyseReport> GetDiffAnalyseReportsByNoti(StocktakeNotification noti)
    {
        return Channel.GetDiffAnalyseReportsByNoti(noti);
    }

    public List<DiffAnalyseReport> GetDiffAnalyseReports()
    {
        return Channel.GetDiffAnalyseReports();
    }

    public void SaveAnalyseReport(List<View_DiffAnalyseReportDetails> detailsList)
    {
        Channel.SaveAnalyseReport(detailsList);
    }


    public byte[] ExportAnalyseReport(List<int> details)
    {
        byte[] compressedContent = Channel.ExportAnalyseReport(details);
        return SGM.Common.Utility.Utils.DeCompress(compressedContent);
    }

    public byte[] ExportStocktakeResult(StocktakeNotification notification,User userInfo)
    {
        byte[] compressedContent = Channel.ExportStocktakeResult(notification,userInfo);
        return SGM.Common.Utility.Utils.DeCompress(compressedContent);
    }
    public byte[] ExportStocktakeNotice(StocktakeNotification notification, User exportBy, out string notiCode)
    {
        byte[] compressedContent = Channel.ExportStocktakeNotice(notification, exportBy,out notiCode);
        return SGM.Common.Utility.Utils.DeCompress(compressedContent);
    }
    #endregion StocktakeRequest

    #region StocktakeDetails
    public List<View_StocktakeDetails> QueryStocktakeDetailsScope(View_StocktakeDetails stocktakeDetails, string startCode, string endCode)
    {
        return Channel.QueryStocktakeDetailsScope(stocktakeDetails, startCode, endCode);
    }
    #endregion

    #region StocktakeStatus...
    public List<StocktakeStatus> GetStocktakeStatus()
    {
        return Channel.GetStocktakeStatus();
    }

    public List<StocktakeStatus> QueryStocktakeStatuss(StocktakeStatus info)
    {
        return Channel.QueryStocktakeStatuss(info);
    }

    public StocktakeStatus GetStocktakeStatusByKey(StocktakeStatus info)
    {
        return Channel.GetStocktakeStatusByKey(info);
    }

    public void UpdateStocktakeStatus(StocktakeStatus model)
    {
        Channel.UpdateStocktakeStatus(model);
    }

    public StocktakeStatus AddStocktakeStatus(StocktakeStatus model)
    {
        return Channel.AddStocktakeStatus(model);
    }

    public void DeleteStocktakeStatus(StocktakeStatus model)
    {
        Channel.DeleteStocktakeStatus(model);
    }
    #endregion StocktakeStatus ......

    #region StocktakePriority...
    public List<StocktakePriority> GetStocktakePriorities()
    {
        return Channel.GetStocktakePriorities();
    }

    public List<StocktakePriority> QueryStocktakePrioritys(StocktakePriority info)
    {
        return Channel.QueryStocktakePrioritys(info);
    }

    public StocktakePriority GetStocktakePriorityByKey(StocktakePriority info)
    {
        return Channel.GetStocktakePriorityByKey(info);
    }

    public void UpdateStocktakePriority(StocktakePriority model)
    {
        Channel.UpdateStocktakePriority(model);
    }

    public StocktakePriority AddStocktakePriority(StocktakePriority model)
    {
        return Channel.AddStocktakePriority(model);
    }

    public void DeleteStocktakePriority(StocktakePriority model)
    {
        Channel.DeleteStocktakePriority(model);
    }
    #endregion StocktakePriority ......

    #region Plant ...
    public List<SGM.ECount.DataModel.Plant> GetPlants()
    {
        return Channel.GetPlants();
    }

    public Plant GetPlantByKey(Plant info)
    {
        return Channel.GetPlantByKey(info);
    }

    public void UpdatePlant(SGM.ECount.DataModel.Plant plant)
    {
        Channel.UpdatePlant(plant);
        List<Plant> plants = Utils.GetCache(Consts.CACHE_KEY_PLANT) as List<Plant>;
        if (plants != null)
        {

            Plant tmpPlant = plants.SingleOrDefault(p => p.PlantID == plant.PlantID);
            if (tmpPlant != null)
            {
                plants.Remove(tmpPlant);
            }
            plants.Add(plant);
            Utils.SetCache(Consts.CACHE_KEY_PLANT, plants);
        }
    }

    public void DeletePlant(int plantID)
    {
        Channel.DeletePlant(plantID);
        List<Plant> plants = Utils.GetCache(Consts.CACHE_KEY_PLANT) as List<Plant>;
        if (plants != null)
        {
            Plant tmpPlant = plants.SingleOrDefault(p => p.PlantID == plantID);
            if (tmpPlant != null)
            {
                plants.Remove(tmpPlant);
            }
            Utils.SetCache(Consts.CACHE_KEY_PLANT, plants);
        }
    }

    public Plant AddPlant(SGM.ECount.DataModel.Plant plant)
    {
        Plant newPlant = Channel.AddPlant(plant);
        List<Plant> plants = Utils.GetCache(Consts.CACHE_KEY_PLANT) as List<Plant>;
        if (plants != null)
        {
            Plant tmpPlant = plants.SingleOrDefault(p => p.PlantID == plant.PlantID);
            if (tmpPlant != null)
            {
                plants.Remove(tmpPlant);
            }
            plants.Add(plant);
            Utils.SetCache(Consts.CACHE_KEY_PLANT, plants);
        }
        return newPlant;
    }

    #endregion Plant......

    #region Workshop...
    public List<SGM.ECount.DataModel.Workshop> Getworkshop()
    {
        return Channel.Getworkshop();
    }

    public List<SGM.ECount.DataModel.Workshop> GetWorkshopbyPlant(SGM.ECount.DataModel.Plant plant)
    {

        return Channel.GetWorkshopbyPlant(plant);
    }

    public Workshop GetWorkshopbykey(Workshop info)
    {
        return Channel.GetWorkshopbykey(info);
    }

    public void AddWorkshop(SGM.ECount.DataModel.Workshop workshop)
    {
        Channel.AddWorkshop(workshop);
    }

    public void UpdateWorkshop(SGM.ECount.DataModel.Workshop workshop)
    {
        Channel.UpdateWorkshop(workshop);
    }

    public void DeleteWorkshop(SGM.ECount.DataModel.Workshop workshop)
    {
        Channel.DeleteWorkshop(workshop);
    }
    /// <summary>
    /// get workshop list by plantID
    /// </summary>
    /// <param name="plantID">plantID</param>
    /// <returns></returns>
    public List<Workshop> GetWorkshopbyPlantID(int plantID)
    {
        return Channel.GetWorkshopbyPlantID(plantID);
    }

    #endregion Workshop ......

    #region Segment...
    public List<SGM.ECount.DataModel.Segment> GetSegment()
    {
        return Channel.GetSegment();
    }

    public List<SGM.ECount.DataModel.Segment> GetSegmentbyWorkshop(SGM.ECount.DataModel.Workshop workshop)
    {
        return Channel.GetSegmentbyWorkshop(workshop);
    }

    public Segment GetSegmentbykey(Segment info)
    {
        return Channel.GetSegmentbykey(info);
    }

    public void AddSegment(SGM.ECount.DataModel.Segment segment)
    {
        Channel.AddSegment(segment);
    }

    public void UpdateSegment(SGM.ECount.DataModel.Segment segment)
    {
        Channel.UpdateSegment(segment);
    }

    public void DeleteSegment(SGM.ECount.DataModel.Segment segment)
    {
        Channel.DeleteSegment(segment);
    }
    /// <summary>
    /// get segment list by plantid
    /// </summary>
    /// <param name="plantID">plantid</param>
    /// <returns></returns>
    public List<ViewPlantWorkshopSegment> GetSegmentbyPlantID(int plantID)
    {
        return Channel.GetSegmentbyPlantID(plantID);
    }

    /// <summary>
    /// get segments list by workshopid
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public List<Segment> GetSegmentbyWorkshopID(int workshopID)
    {
        return Channel.GetSegmentbyWorkshopID(workshopID);
    }

    public List<Segment> GetSegmentsByWorkshopCodes(List<string> workshopCodes)
    {
        return Channel.GetSegmentsByWorkshopCodes(workshopCodes);
    }
    #endregion Plant,Workshop and Segment... ...

    #region CycleCountLevel...
    public List<CycleCountLevel> QueryCycleCountLevels(CycleCountLevel info)
    {
        return Channel.QueryCycleCountLevels(info);
    }

    public List<CycleCountLevel> QueryCycleCountLevelsByPage(CycleCountLevel info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryCycleCountLevelsByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public CycleCountLevel GetCycleCountLevelByKey(CycleCountLevel info)
    {
        return Channel.GetCycleCountLevelByKey(info);
    }

    public void UpdateCycleCountLevel(CycleCountLevel model)
    {
        Channel.UpdateCycleCountLevel(model);
    }

    public CycleCountLevel AddCycleCountLevel(CycleCountLevel model)
    {
        return Channel.AddCycleCountLevel(model);
    }

    public void DeleteCycleCountLevel(CycleCountLevel model)
    {
        Channel.DeleteCycleCountLevel(model);
    }
    #endregion CycleCountLevel ......

    #region PartCategory...
    public List<PartCategory> QueryPartCategorys(PartCategory info)
    {
        return Channel.QueryPartCategorys(info);
    }

    public List<PartCategory> QueryPartCategorysByPage(PartCategory info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryPartCategorysByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public PartCategory GetPartCategoryByKey(PartCategory info)
    {
        return Channel.GetPartCategoryByKey(info);
    }

    public void UpdatePartCategory(PartCategory model)
    {
        Channel.UpdatePartCategory(model);
    }

    public PartCategory AddPartCategory(PartCategory model)
    {
        return Channel.AddPartCategory(model);
    }

    public void DeletePartCategory(PartCategory model)
    {
        Channel.DeletePartCategory(model);
    }
    #endregion PartCategory ......

    #region PartStatus...
    public List<PartStatus> QueryPartStatuss(PartStatus info)
    {
        return Channel.QueryPartStatuss(info);
    }

    public List<PartStatus> QueryPartStatussByPage(PartStatus info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryPartStatussByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public PartStatus GetPartStatusByKey(PartStatus info)
    {
        return Channel.GetPartStatusByKey(info);
    }

    public void UpdatePartStatus(PartStatus model)
    {
        Channel.UpdatePartStatus(model);
    }

    public PartStatus AddPartStatus(PartStatus model)
    {
        return Channel.AddPartStatus(model);
    }

    public void DeletePartStatus(PartStatus model)
    {
        Channel.DeletePartStatus(model);
    }

    public void DeletePartStatuss(List<string> ids)
    {
        Channel.DeletePartStatuss(ids);
    }

    public bool ExistPartStatus(PartStatus model)
    {
        return Channel.ExistPartStatus(model);
    }
    #endregion PartStatus ......

    #region Part ...
    public List<ViewPart> QueryParts(Part partInfo)//(string partCode, string chineseName, int? plantID, string followup, string book)
    {
        try
        {
            return Channel.QueryParts(partInfo);
        }
        catch (FaultException<ServiceFault> ex)
        {
            ExceptionHandler.HandleException(ex, ExceptionType.SERVICE_PROXY_EXCEPTION);
            return new List<ViewPart>();
        }
    }

    public List<ViewPart> QueryPartsOfScope(string startCode, string endCode)
    {
        return Channel.QueryPartsOfScope(startCode, endCode);
    }
    public List<ViewPart> QueryPartsByGroup(PartGroup group)
    {
        return Channel.QueryPartsByGroup(group);
    }
    public List<ViewPart> QueryUngroupedPartsByPage(Part part, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryUngroupedPartsByPage(part, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public List<ViewPart> QueryUnRequestedPartByPage(StocktakeRequest request, Part filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryUnRequestedPartByPage(request, filter, pageSize, pageNumber, out pageCount, out itemCount);
    }
    public List<ViewPart> QueryPartByPage(Part part, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryPartByPage(part, pageSize, pageNumber, out pageCount, out itemCount);
    }
    public List<ViewPart> QueryPartCodeScope(Part part, string startCode, string endCode)
    {
        return Channel.QueryPartCodeScope(part, startCode, endCode);
    }
    public List<Segment> QuerySegmentByPage(Segment segmentt)
    {
        return Channel.QuerySegmentByPage(segmentt);
    }

    public ViewPart GetViewPartByKey(string partID)
    {
        return Channel.GetViewPartByKey(partID);
    }

    public List<ViewPart> GetViewPartsByPartIDs(List<string> partids)
    {
        return Channel.GetViewPartsByPartIDs(partids);
    }

    public Part GetPartByKey(string partID)
    {
        return Channel.GetPartByKey(partID);
    }

    public List<PartGroup> GetGroupsByPart(Part part)
    {
        return Channel.GetGroupsByPart(part);
    }
    public void UpdatePart(Part part)
    {
        Channel.UpdatePart(part);
    }

    public Part AddPart(Part part)
    {
        return Channel.AddPart(part);
    }

    public void DeletePart(Part part)
    {
        Channel.DeletePart(part);
    }

    public List<Part> GetRelatedParts(string partID)
    {
        return Channel.GetRelatedParts(partID);
    }


    public List<ViewPart> QueryPartsByKey(List<ViewPart> list)
    {
        return Channel.QueryPartsByKey(list);
    }

    public List<ViewPart> QueryPartsByCodePlant(List<ViewPart> list)
    {
        return Channel.QueryPartsByCodePlant(list);
    }

    public byte[] ExportParts(Part filter, out string message)
    {
        return Channel.ExportParts(filter, out message);
    }

    public byte[] ExportSelectedParts(List<string> parts, out string message)
    {
        return Channel.ExportSelectedParts(parts, out message);
    }

    public List<ViewPart> QueryFilteredPartsByPage(View_StocktakeDetails filter, List<ViewPart> filteredParts, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryFilteredPartsByPage(filter, filteredParts, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public void ImportPart(List<ViewPart> list)
    {
        Channel.ImportPart(list);
    }
    #endregion Part ... ...

    #region partGroup...
    /// <summary>
    /// Get all Part Group
    /// </summary>
    /// <returns></returns>
    public List<PartGroup> QueryPartGroups(PartGroup info)
    {
        return Channel.QueryPartGroups(info);
    }

    public PartGroup GetPartGroupByKey(int groupID)
    {
        return Channel.GetPartGroupByKey(groupID);
    }

    public PartGroup AddPartGroup(PartGroup partGroup)
    {
        return Channel.AddPartGroup(partGroup);
    }

    public void UpdatePartGroup(PartGroup partGroup)
    {
        Channel.UpdatePartGroup(partGroup);
    }

    public void DeletePartGroup(int groupID)
    {
        Channel.DeletePartGroup(groupID);
    }

    public void DeletePartGroups(List<int> ids)
    {
        foreach (int id in ids)
        {
            Channel.DeletePartGroup(id);
        }
    }

    public byte[] ExportPartGroup(PartGroup record, out string message)
    {
        return Channel.ExportPartGroup(record, out message);
    }

    #endregion partGroup......

    #region Supplier...
    public int GetSuppliersCount(Supplier info)
    {
        return Channel.GetSuppliersCount(info);
    }
    public List<Supplier> QuerySuppliers(Supplier info)
    {
        return Channel.QuerySuppliers(info);
    }

    public List<Supplier> QuerySuppliersByPage(Supplier info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QuerySuppliersByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public Supplier GetSupplierbykey(Supplier info)
    {
        return Channel.GetSupplierbykey(info);
    }

    public void UpdateSupplier(Supplier model)
    {
        Channel.UpdateSupplier(model);
    }

    public Supplier AddSupplier(Supplier model)
    {
        return Channel.AddSupplier(model);
    }

    public void DeleteSupplier(Supplier model)
    {
        Channel.DeleteSupplier(model);
    }
    public void DeleteSuppliers(List<Supplier> list)
    {
        foreach (Supplier model in list)
        {
            Channel.DeleteSupplier(model);
        }
    }

    public void DeleteSuppliers(List<int> ids)
    {
        Channel.DeleteSuppliers(ids);
    }

    public byte[] ExportSupplier(Supplier record, out string message)
    {
        return Channel.ExportSupplier(record, out message);
    }

    #endregion Supplier ......

    #region ConsignmentPartRecord ...
    public List<ConsignmentPartRecord> QueryConsignmentPartRecords(ConsignmentPartRecord record)
    {
        return Channel.QueryConsignmentPartRecords(record);
    }

    public List<ConsignmentPartRecord> QueryConsignmentPartRecordsByPage(ConsignmentPartRecord record, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryConsignmentPartRecordsByPage(record, pageSize, pageNumber, out pageCount, out itemCount);
    }


    public ConsignmentPartRecord GetConsignmentPartRecordbykey(ConsignmentPartRecord record)
    {
        return Channel.GetConsignmentPartRecordbykey(record);
    }

    public void UpdateConsignmentPartRecord(ConsignmentPartRecord record)
    {
        Channel.UpdateConsignmentPartRecord(record);
    }

    public ConsignmentPartRecord AddConsignmentPartRecord(ConsignmentPartRecord record)
    {
        return Channel.AddConsignmentPartRecord(record);
    }

    public void DeleteConsignmentPartRecord(ConsignmentPartRecord record)
    {
        Channel.DeleteConsignmentPartRecord(record);
    }

    public byte[] ExportConsignmentParts(View_ConsignmentPart record, out string message)
    {
        return Channel.ExportConsignmentParts(record, out message);
    }

    public void ImportConsignmentRecord(List<View_ConsignmentPart> list)
    {
        Channel.ImportConsignmentRecord(list);
    }

    #endregion ConsignmentPartRecord ... ...


    #region PartRepairRecord...
    public List<PartRepairRecord> QueryPartRepairRecords(PartRepairRecord info)
    {
        return Channel.QueryPartRepairRecords(info);
    }

    public List<PartRepairRecord> QueryPartRepairRecordsByPage(PartRepairRecord info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryPartRepairRecordsByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public PartRepairRecord GetPartRepairRecordbykey(PartRepairRecord info)
    {
        return Channel.GetPartRepairRecordbykey(info);
    }

    public void UpdatePartRepairRecord(PartRepairRecord model)
    {
        Channel.UpdatePartRepairRecord(model);
    }

    public PartRepairRecord AddPartRepairRecord(PartRepairRecord model)
    {
        return Channel.AddPartRepairRecord(model);
    }

    public void DeletePartRepairRecord(PartRepairRecord model)
    {
        Channel.DeletePartRepairRecord(model);
    }

    public void DeletePartRepairRecords(List<string> idList)
    {
        Channel.DeletePartRepairRecords(idList);
    }

    public void ImportPartRepairRecord(List<View_PartRepairRecord> list)
    {
        Channel.ImportPartRepairRecord(list);
    }
    #endregion PartRepairRecord ......

    #region SupplierStoreLocation...
    public List<SupplierStoreLocation> QuerySupplierStoreLocations(SupplierStoreLocation info)
    {
        return Channel.QuerySupplierStoreLocations(info);
    }

    public SupplierStoreLocation GetSupplierStoreLocationbykey(SupplierStoreLocation info)
    {
        return Channel.GetSupplierStoreLocationbykey(info);
    }

    public void UpdateSupplierStoreLocation(SupplierStoreLocation model)
    {
        Channel.UpdateSupplierStoreLocation(model);
    }

    public void AddSupplierStoreLocation(SupplierStoreLocation model)
    {
        Channel.AddSupplierStoreLocation(model);
    }

    public void DeleteSupplierStoreLocation(SupplierStoreLocation model)
    {
        Channel.DeleteSupplierStoreLocation(model);
    }
    #endregion SupplierStoreLocation ......

    #region StoreLocationType...
    public List<StoreLocationType> QueryStoreLocationTypes(StoreLocationType info)
    {
        return Channel.QueryStoreLocationTypes(info);
    }

    public List<StoreLocationType> QueryStoreLocationTypesByPage(StoreLocationType info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryStoreLocationTypesByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public StoreLocationType GetStoreLocationTypeByKey(StoreLocationType info)
    {
        return Channel.GetStoreLocationTypeByKey(info);
    }

    public void UpdateStoreLocationType(StoreLocationType model)
    {
        Channel.UpdateStoreLocationType(model);
    }

    public StoreLocationType AddStoreLocationType(StoreLocationType model)
    {
        return Channel.AddStoreLocationType(model);
    }

    public void DeleteStoreLocationType(StoreLocationType model)
    {
        Channel.DeleteStoreLocationType(model);
    }

    public void DeleteStoreLocationTypes(List<string> ids)
    {
        Channel.DeleteStoreLocationTypes(ids);
    }

    public bool ExistStoreLocationType(StoreLocationType model)
    {
        return Channel.ExistStoreLocationType(model);
    }
    #endregion StoreLocationType ......

    //#region SupplierStoretakeItem...
    //public List<SupplierStoretakeItem> QuerySupplierStoretakeItems(SupplierStoretakeItem info)
    //{
    //    return Channel.QuerySupplierStoretakeItems(info);
    //}

    //public SupplierStoretakeItem GetSupplierStoretakeItembykey(SupplierStoretakeItem info)
    //{
    //    return Channel.GetSupplierStoretakeItembykey(info);
    //}

    //public void UpdateSupplierStoretakeItem(SupplierStoretakeItem model)
    //{
    //    Channel.UpdateSupplierStoretakeItem(model);
    //}

    //public void AddSupplierStoretakeItem(SupplierStoretakeItem model)
    //{
    //    Channel.AddSupplierStoretakeItem(model);
    //}

    //public void DeleteSupplierStoretakeItem(SupplierStoretakeItem model)
    //{
    //    Channel.DeleteSupplierStoretakeItem(model);
    //}
    //#endregion SupplierStoretakeItem ......

    #region StocktakeType...
    public List<StocktakeType> GetStocktakeTypes()
    {
        return Channel.GetStocktakeTypes();
    }

    public List<StocktakeType> QueryStocktakeTypes(StocktakeType info)
    {
        return Channel.QueryStocktakeTypes(info);
    }

    public StocktakeType GetStocktakeTypeByKey(StocktakeType info)
    {
        return Channel.GetStocktakeTypeByKey(info);
    }

    public void UpdateStocktakeType(StocktakeType model)
    {
        Channel.UpdateStocktakeType(model);
    }

    public StocktakeType AddStocktakeType(StocktakeType model)
    {
        return Channel.AddStocktakeType(model);
    }

    public void DeleteStocktakeType(StocktakeType model)
    {
        Channel.DeleteStocktakeType(model);
    }

    public void DeleteStocktakeTypes(List<string> ids)
    {
        Channel.DeleteStocktakeTypes(ids);
    }

    public bool ExistStocktakeType(StocktakeType model)
    {
        return Channel.ExistStocktakeType(model);
    }
    #endregion StocktakeType ......

    #region StoreLocation...
    public List<StoreLocation> QueryStoreLocations(StoreLocation info)
    {
        return Channel.QueryStoreLocations(info);
    }

    public List<StoreLocation> QueryStoreLocationsByPage(StoreLocation info, int pageSize, int pageNumber, out int pageCount, out int itemCount)
    {
        return Channel.QueryStoreLocationsByPage(info, pageSize, pageNumber, out pageCount, out itemCount);
    }

    public StoreLocation GetStoreLocationByKey(StoreLocation info)
    {
        return Channel.GetStoreLocationByKey(info);
    }

    public void UpdateStoreLocation(StoreLocation model)
    {
        Channel.UpdateStoreLocation(model);
    }

    public List<StoreLocation> GetStoreLocations()
    {
        return Channel.GetStoreLocations();
    }

    public StoreLocation AddStoreLocation(StoreLocation model)
    {
        return Channel.AddStoreLocation(model);
    }

    public void DeleteStoreLocation(StoreLocation model)
    {
        Channel.DeleteStoreLocation(model);
    }
    public void DeleteStoreLocations(List<string> ids)
    {
        Channel.DeleteStoreLocations(ids);
    }

    public bool ExistStoreLocation(StoreLocation model)
    {
        return Channel.ExistStoreLocation(model);
    }

    public void ImportStoreLocation(List<S_StoreLocation> list)
    {
        Channel.ImportStoreLocation(list);
    }

    public byte[] ExportStoreLocations(StoreLocation record, out string message)
    {
        return Channel.ExportStoreLocations(record, out message);
    }

    #endregion StoreLocation ......

    #region bizParams ....
    public void UpdateBizParams(BizParams model)
    {
        Channel.UpdateBizParams(model);
    }

    public void UpdateBizParamsList(List<BizParams> models)
    {
        Channel.UpdateBizParamsList(models);
    }

    public BizParams GetBizParams(BizParams model)
    {
        return Channel.GetBizParams(model);
    }

    public List<BizParams> GetBizParamsList()
    {
        return Channel.GetBizParamsList();
    }
    public void ResetCycleCount()
    {
        Channel.ResetCycleCount();
    }
    #endregion

    private static ECountServiceProxy _instance;

    //public static ECountServiceProxy Instance()
    //{
    //    if (_instance == null||_instance.State == CommunicationState.Faulted)
    //        _instance = new ECountServiceProxy();
    //    return _instance;
    //}

}
