using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SGM.Common.Exception;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using System.Security.Cryptography.X509Certificates;
using SGM.ECount.DataModel;
using System.Security.Permissions;
using System.Data;

namespace SGM.ECount.Contract.Service
{
    [ServiceContract(ConfigurationName = "ECountService", Namespace = "urn:SGM.ECount"), ExceptionShielding("Service Exception Policy")]
    public interface IECountService
    {
        #region User...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void Login();

        [OperationContract, FaultContract(typeof(ServiceFault)), PrincipalPermission(SecurityAction.Demand, Role = "Administraor")]
        User AddUser(User model);

        [OperationContract, FaultContract(typeof(ServiceFault)), PrincipalPermission(SecurityAction.Demand, Role = "Administraor")]
        void UpdateUser(User user);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<User> QueryUsersByPage(User info);

        [OperationContract, FaultContract(typeof(ServiceFault)), PrincipalPermission(SecurityAction.Demand, Role = "Administraor")]
        void DeleteUsers(List<string> ids);

        [OperationContract, FaultContract(typeof(ServiceFault)), PrincipalPermission(SecurityAction.Demand, Role = "Administraor")]
        List<User> GetUsers();

        [OperationContract, FaultContract(typeof(ServiceFault)), PrincipalPermission(SecurityAction.Demand, Role = "Administraor")]
        void AddUserToGroup(int userID, int groupID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        User GetUserbyKey(User user);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        User GetUserbyName(string userName);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool ExistUser(User user);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Operation GetOperationByKey(Operation operation);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Operation> GetOperations();
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Operation> GetOperationsByOperation(Operation operation);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<UserGroup> GetUserGroupsByOperation(Operation operation);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Operation> GetOperationsByUserGroup(UserGroup group);
        #endregion User......

        #region UserGroup...

        [OperationContract, FaultContract(typeof(ServiceFault)), PrincipalPermission(SecurityAction.Demand, Role = "Administraor")]
        List<UserGroup> GetUserGroups();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<UserGroup> QueryUserGroups(UserGroup info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        UserGroup GetUserGroupByKey(UserGroup info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateUserGroup(UserGroup model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        UserGroup AddUserGroup(UserGroup model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteUserGroup(UserGroup model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteUserGroups(List<string> ids);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool ExistUserGroup(UserGroup model);
        #endregion UserGroup ......

        #region StocktakePriority...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StocktakePriority> GetStocktakePriorities();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StocktakePriority> QueryStocktakePrioritys(StocktakePriority info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakePriority GetStocktakePriorityByKey(StocktakePriority info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateStocktakePriority(StocktakePriority model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakePriority AddStocktakePriority(StocktakePriority model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStocktakePriority(StocktakePriority model);
        #endregion StocktakePriority ......

        #region StocktakeStatus...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StocktakeStatus> GetStocktakeStatus();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StocktakeStatus> QueryStocktakeStatuss(StocktakeStatus info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeStatus GetStocktakeStatusByKey(StocktakeStatus info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateStocktakeStatus(StocktakeStatus model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeStatus AddStocktakeStatus(StocktakeStatus model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStocktakeStatus(StocktakeStatus model);
        #endregion StocktakeStatus ......

        #region stocktake
        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //StocktakeRequest RequestStocktake(User user, StocktakeRequest request);

        #region Request
        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeRequest RequestStocktake(NewStocktakeRequest newRequest);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateStocktakeRequest(NewStocktakeRequest request);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateRequest(NewStocktakeRequest request, List<int> removedDetailsList, List<View_StocktakeDetails> changedDetails);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateCachedRequest(NewStocktakeRequest request, string cacheKey, bool submit, bool isRemove);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStocktake(StocktakeRequest request);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        //List<StocktakeRequest> QueryStocktakeRequest(long? requestID, string requestNumber, string userName, int? plantID, string partCode, int? stocktakeType, string partChineseName, DateTime? dateStart, DateTime? dateEnd);
        List<StocktakeRequest> QueryStocktakeRequest(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StocktakeRequest> QueryStocktakeRequestByPage(ViewStockTakeRequest condition, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStocktakeRequest(long requestID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteRequestBatch(List<StocktakeRequest> reqList);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeDetails> QueryStocktakeReqDetails(View_StocktakeDetails condtion, DateTime? dateStart, DateTime? dateEnd);

        

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeDetails> QueryRequestDetailsByPage(View_StocktakeDetails condition, List<View_StocktakeDetails> removedDetails, List<View_StocktakeDetails> addedDetails, DateTime? dateStart, DateTime? dateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeRequest GetRequest(long requestId);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeDetails> GetNewRequestDetailsByPlant(List<View_StocktakeDetails> filter, List<View_StocktakeDetails> addition, bool isStatic, Plant plant, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeDetails> GetNotiDetailsByPage(StocktakeNotification notice, List<View_StocktakeDetails> filter, List<View_StocktakeDetails> addition, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeDetails> GetNewRequestDetails(View_StocktakeDetails filter, int pageSize, int pageNumber, out int pageCount, out int itemCount); 
        #endregion



        #region Notification
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> GetPartsToCycleCount(out int cycledTimes,int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeRequest CreateCycleCount(User user, List<View_StocktakeDetails> deletedDetails, List<View_StocktakeDetails> updatedDetails);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StocktakeRequest> CreateCycleCountByPlant(User user);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeNotification GetNotification(StocktakeNotification notification);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool NotiExistsRepairPart(StocktakeNotification notification);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool NotiExistsCSMTPart(StocktakeNotification notification);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeNotification CreateNotification(StocktakeNotification notification, List<long> removedDetailsList);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateNotification(StocktakeNotification notification, List<long> removedDetailsList, List<View_StocktakeDetails> changedDetails, bool removeAll);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeDetails> QueryNotifyDetailsByPage(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeItem> QueryStocktakeItem(View_StocktakeItem filter);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ImportAdjustment(List<View_StocktakeItem> list);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ImportAnalyseRef(List<View_StocktakeDetails> list, List<long> notiList);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeNotification> QueryNotiByPage(View_StocktakeDetails condition,int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        int RemoveAllPartsFromNoti(long notificationID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void PublishNotification(StocktakeNotification notification, List<SGM.ECount.DataModel.StoreLocationType> locationTypes);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteNotification(List<string> notifications);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportNotification(List<String> notification, out string message, out string notiCode);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void CancelPublish(List<string> notifications); 
        #endregion

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeResult> GetStocktakeResult(View_StocktakeResult filter,bool loadWorkshopDetails);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeResult> GetStocktakeResultOfScope(View_StocktakeResult filter, string startPartCode, string endPartCode);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_ResultCSMT> GetCSMTStocktakeResult(View_ResultCSMT filter);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_ResultNoneCSMT> GetNoneCSMTStocktakeResult(View_ResultNoneCSMT filter);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void FillStocktakeResult(StocktakeNotification notification, List<View_StocktakeResult> list);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void FillStocktakeAdjustment( List<View_StocktakeResult> list);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ImportStorage(List<S_StorageRecord> list);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        string ImportResult(StocktakeNotification notification, List<View_StocktakeResult> itemList, string cacheKey, bool submit);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeDetails> QueryFullFilledNotiDetailsByPage(View_StocktakeDetails condition, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void CreateAnalyseReport(StocktakeNotification notice, User analyzedBy, out string reportCode, out Int64 reportID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void CreateAnalyseRptByCondition(View_StocktakeDetails filter, User analyzedBy, out string reportCode, out Int64 reportID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        DataSet QueryAnalyseReport(View_DifferenceAnalyse filter,DateTime? timeStart,DateTime? timeEnd);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteAnalyseItems(List<DiffAnalyseReportItem> itemsList);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        DiffAnalyseReport GetAnalyseReport(long reportID);


        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_DiffAnalyseReportDetails> GetDiffAnalyseRptDetails(DiffAnalyseReportItem item, UserGroup userGroup);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<DiffAnalyseReport> GetDiffAnalyseReportsByNoti(StocktakeNotification noti);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<DiffAnalyseReport> GetDiffAnalyseReports();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void SaveAnalyseReport(List<View_DiffAnalyseReportDetails> detailsList);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportAnalyseReport(List<int> details);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportStocktakeResult(StocktakeNotification notification, User userInfo);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportStocktakeNotice(StocktakeNotification notification, User exportBy,out string notiCode);

        #endregion

        #region plant...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Plant> GetPlants();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Plant GetPlantByKey(Plant info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdatePlant(Plant plant);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeletePlant(int plantID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Plant AddPlant(Plant plant);
        #endregion

        #region workshop
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Workshop> GetWorkshopbyPlantID(int plantID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Workshop> Getworkshop();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Workshop> GetWorkshopbyPlant(Plant plant);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Workshop GetWorkshopbykey(Workshop info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void AddWorkshop(Workshop workshop);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateWorkshop(Workshop workshop);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteWorkshop(Workshop workshop);

        #endregion

        #region segment

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPlantWorkshopSegment> GetSegmentbyPlantID(int plantID);


        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Segment> GetSegmentbyWorkshopID(int workshopID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Segment> GetSegment();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Segment GetSegmentbykey(Segment info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Segment> GetSegmentbyWorkshop(Workshop workshop);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void AddSegment(Segment segment);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateSegment(Segment segment);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteSegment(Segment segment);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Segment> QuerySegmentByPage(Segment segment);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Segment> GetSegmentsByWorkshopCodes(List<string> workshopCodes);
        #endregion

        #region CycleCountLevel...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<CycleCountLevel> QueryCycleCountLevels(CycleCountLevel info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<CycleCountLevel> QueryCycleCountLevelsByPage(CycleCountLevel info, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        CycleCountLevel GetCycleCountLevelByKey(CycleCountLevel info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateCycleCountLevel(CycleCountLevel model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        CycleCountLevel AddCycleCountLevel(CycleCountLevel model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteCycleCountLevel(CycleCountLevel model);
        #endregion CycleCountLevel ......

        #region DifferenceAnalyzeDetails ...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<DifferenceAnalyseDetails> GetDifferenceAnalyseDetails();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void AddDiffAnalyseDetail(DifferenceAnalyseDetails detail);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateDiffAnalyseDetail(DifferenceAnalyseDetails detail);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteDiffAnalyseDetail(DifferenceAnalyseDetails detail);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        DifferenceAnalyseDetails GetDiffAnalyseDetailstbyID(int detailsID);


        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool ExistDifferenceAnalyse(DifferenceAnalyseDetails diffAnalyseDetails);

        #endregion DifferenceAnalyzeDetails......

        #region DifferenceAnalyzeItem...
        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //List<DifferenceAnalyzeItem> QueryDifferenceAnalyzeItems(DifferenceAnalyzeItem info);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //DifferenceAnalyzeItem GetDifferenceAnalyzeItemByKey(DifferenceAnalyzeItem info);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //void UpdateDifferenceAnalyzeItem(DifferenceAnalyzeItem model);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //DifferenceAnalyzeItem AddDifferenceAnalyzeItem(DifferenceAnalyzeItem model);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //void DeleteDifferenceAnalyzeItem(DifferenceAnalyzeItem model);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //void DeleteDifferenceAnalyzeItems(List<string> ids);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //bool ExistDifferenceAnalyzeItem(DifferenceAnalyzeItem model);
        #endregion DifferenceAnalyzeItem ......

        #region PartCategory...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<PartCategory> QueryPartCategorys(PartCategory info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<PartCategory> QueryPartCategorysByPage(PartCategory info, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        PartCategory GetPartCategoryByKey(PartCategory info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdatePartCategory(PartCategory model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        PartCategory AddPartCategory(PartCategory model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeletePartCategory(PartCategory model);
        #endregion PartCategory ......

        #region PartStatus...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<PartStatus> QueryPartStatuss(PartStatus info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<PartStatus> QueryPartStatussByPage(PartStatus info, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        PartStatus GetPartStatusByKey(PartStatus info);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<PartGroup> GetGroupsByPart(Part part);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdatePartStatus(PartStatus model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        PartStatus AddPartStatus(PartStatus model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeletePartStatus(PartStatus model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeletePartStatuss(List<string> ids);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool ExistPartStatus(PartStatus model);
        #endregion PartStatus ......

        #region Part
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryParts(Part partInfo);// string partCode,string chineseName,int? plantID,string followup,string book);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryPartsOfScope(string startCode, string endCode);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryPartByPage(Part part, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryUnRequestedPartByPage(StocktakeRequest request, Part filter, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Part GetPartByKey(string partID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> GetViewPartsByPartIDs(List<string> partids);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryPartsByGroup(PartGroup group);
  
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryUngroupedPartsByPage(Part part, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdatePart(Part part);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Part AddPart(Part part);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeletePart(Part part);
       
        [OperationContract, FaultContract(typeof(ServiceFault))]
        ViewPart GetViewPartByKey(string partID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Part> GetRelatedParts(string partID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryPartsByKey(List<ViewPart> list);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryPartsByCodePlant(List<ViewPart> list);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryPartCodeScope(Part part, string startCode, string endCode);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<View_StocktakeDetails> QueryStocktakeDetailsScope(View_StocktakeDetails stocktakeDetails, string startCode, string endCode);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportParts(Part filter, out string message);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ImportPart(List<ViewPart> list);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportSelectedParts(List<string> parts, out string message);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ViewPart> QueryFilteredPartsByPage(View_StocktakeDetails filter, List<ViewPart> filteredParts, int pageSize, int pageNumber, out int pageCount, out int itemCount);
        #endregion

        #region Supplier...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Supplier> QuerySuppliers(Supplier info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        int GetSuppliersCount(Supplier info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<Supplier> QuerySuppliersByPage(Supplier info, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Supplier GetSupplierbykey(Supplier info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateSupplier(Supplier model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        Supplier AddSupplier(Supplier model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteSupplier(Supplier model);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteSuppliers(List<int> ids);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportSupplier(Supplier record, out string message);
        #endregion Supplier ......

        #region PartGroup...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<PartGroup> QueryPartGroups(PartGroup info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        PartGroup GetPartGroupByKey(int groupID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        PartGroup AddPartGroup(PartGroup partGroup);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdatePartGroup(PartGroup partGroup);
        [OperationContract, FaultContract(typeof(ServiceFault))]

        void DeletePartGroup(int groupID);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportPartGroup(PartGroup filter, out string message);

        #endregion PartGroup ... ...

        #region ConsignmentPartRecord ...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ConsignmentPartRecord> QueryConsignmentPartRecords(ConsignmentPartRecord record);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<ConsignmentPartRecord> QueryConsignmentPartRecordsByPage(ConsignmentPartRecord record, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        ConsignmentPartRecord GetConsignmentPartRecordbykey(ConsignmentPartRecord record);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateConsignmentPartRecord(ConsignmentPartRecord record);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        ConsignmentPartRecord AddConsignmentPartRecord(ConsignmentPartRecord record);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteConsignmentPartRecord(ConsignmentPartRecord record);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportConsignmentParts(View_ConsignmentPart record, out string message);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ImportConsignmentRecord(List<View_ConsignmentPart> list);

        #endregion ConsignmentPartRecord ... ...

        #region PartRepairRecord...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<PartRepairRecord> QueryPartRepairRecords(PartRepairRecord info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<PartRepairRecord> QueryPartRepairRecordsByPage(PartRepairRecord info, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        PartRepairRecord GetPartRepairRecordbykey(PartRepairRecord info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdatePartRepairRecord(PartRepairRecord model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        PartRepairRecord AddPartRepairRecord(PartRepairRecord model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeletePartRepairRecord(PartRepairRecord model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeletePartRepairRecords(List<string> guids);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ImportPartRepairRecord(List<View_PartRepairRecord> list);
        #endregion PartRepairRecord ......

        #region SupplierStoreLocation...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<SupplierStoreLocation> QuerySupplierStoreLocations(SupplierStoreLocation info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        SupplierStoreLocation GetSupplierStoreLocationbykey(SupplierStoreLocation info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateSupplierStoreLocation(SupplierStoreLocation model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void AddSupplierStoreLocation(SupplierStoreLocation model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteSupplierStoreLocation(SupplierStoreLocation model);
        #endregion SupplierStoreLocation ......

        #region StoreLocationType...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StoreLocationType> QueryStoreLocationTypes(StoreLocationType info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StoreLocationType> QueryStoreLocationTypesByPage(StoreLocationType info, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StoreLocationType GetStoreLocationTypeByKey(StoreLocationType info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateStoreLocationType(StoreLocationType model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StoreLocationType AddStoreLocationType(StoreLocationType model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStoreLocationType(StoreLocationType model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStoreLocationTypes(List<string> ids);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool ExistStoreLocationType(StoreLocationType model);

  
        #endregion StoreLocationType ......

        #region StoreLocation...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<SGM.ECount.DataModel.StoreLocation> QueryStoreLocations(SGM.ECount.DataModel.StoreLocation info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<SGM.ECount.DataModel.StoreLocation> QueryStoreLocationsByPage(SGM.ECount.DataModel.StoreLocation info, int pageSize, int pageNumber, out int pageCount, out int itemCount);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        SGM.ECount.DataModel.StoreLocation GetStoreLocationByKey(SGM.ECount.DataModel.StoreLocation info);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<SGM.ECount.DataModel.StoreLocation> GetStoreLocations();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateStoreLocation(SGM.ECount.DataModel.StoreLocation model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        SGM.ECount.DataModel.StoreLocation AddStoreLocation(SGM.ECount.DataModel.StoreLocation model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStoreLocation(SGM.ECount.DataModel.StoreLocation model);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStoreLocations(List<string> ids);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool ExistStoreLocation(SGM.ECount.DataModel.StoreLocation model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ImportStoreLocation(List<SGM.ECount.DataModel.S_StoreLocation> list);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        byte[] ExportStoreLocations(SGM.ECount.DataModel.StoreLocation record, out string message);

        #endregion StoreLocation ......

        #region SupplierStoretakeItem...
        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //List<SupplierStoretakeItem> QuerySupplierStoretakeItems(SupplierStoretakeItem info);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //SupplierStoretakeItem GetSupplierStoretakeItembykey(SupplierStoretakeItem info);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //void UpdateSupplierStoretakeItem(SupplierStoretakeItem model);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //void AddSupplierStoretakeItem(SupplierStoretakeItem model);

        //[OperationContract, FaultContract(typeof(ServiceFault))]
        //void DeleteSupplierStoretakeItem(SupplierStoretakeItem model);
        #endregion SupplierStoretakeItem ......

        #region StocktakeType...
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StocktakeType> GetStocktakeTypes();
        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<StocktakeType> QueryStocktakeTypes(StocktakeType info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeType GetStocktakeTypeByKey(StocktakeType info);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateStocktakeType(StocktakeType model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        StocktakeType AddStocktakeType(StocktakeType model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStocktakeType(StocktakeType model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void DeleteStocktakeTypes(List<string> ids);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        bool ExistStocktakeType(StocktakeType model);
        #endregion StocktakeType ......

        #region bizParams ....
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateBizParams(BizParams model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void UpdateBizParamsList(List<BizParams> models);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        BizParams GetBizParams(BizParams model);

        [OperationContract, FaultContract(typeof(ServiceFault))]
        List<BizParams> GetBizParamsList();

        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ResetCycleCount();
        #endregion

    }
}
