using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using SCS.Web.UI.WebControls;
using SGM.Common.Utility;
using SGM.ECount.DataModel;
using System.Web;
using System.Threading;
using SGM.ECount.Contract.Service;
using System.Configuration;
using SGM.Common.Cache;
using AjaxControlToolkit;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Specialized;
using System.Xml;
using System.Drawing;

/// <summary>
/// Summary description for ECountBasePage
/// </summary>
public class ECountBasePage : Page
{
    protected virtual bool ShowWaitingModal
    {
        get
        {
            return false;
        }
    }
    private class PriorityCacheRefreshAction : ICacheItemRefreshAction
    {
        #region ICacheItemRefreshAction Members

        public void Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    #region const

    #endregion

    public IECountService Service
    {
        get
        {
            IECountService service=Utils.GetService();
//            bool inDMZ = false;
//            if (bool.TryParse(ConfigurationManager.AppSettings["InDMZ"], out inDMZ) && inDMZ)
//            {
//#if DEBUG
//            if (Session["ECountServiceProxy"] == null)
//            {
//                Session["ECountServiceProxy"] = new ECountServiceProxy();
//            }
//            Thread.CurrentPrincipal = new ECountPrincipal(CurrentUser);
//#endif
//                service = Session["ECountServiceProxy"] as IECountService;// as ECountServiceProxy;
//                if (((ECountServiceProxy)service).State == System.ServiceModel.CommunicationState.Faulted)
//                {
//                    service = new ECountServiceProxy();
//                    Session["ECountServiceProxy"] = service;
//                }
//            }
//            else
//            {
//#if DEBUG
//            if (Session["ECountServiceProxy"] == null)
//            {
//                Session["ECountServiceProxy"] = new SGM.ECount.Service.Service(); 
//            }
//            Thread.CurrentPrincipal = new ECountPrincipal(CurrentUser);
//#endif

//                service = Session["ECountServiceProxy"] as IECountService;
//            }

            return service;
        }
    }

    public static IECountService GlobalService
    {
        get
        {
//            IECountService service;
//            bool inDMZ = false;
//            if (bool.TryParse(ConfigurationManager.AppSettings["InDMZ"], out inDMZ) && inDMZ)
//            {
//#if DEBUG
//            if (HttpContext.Current.Session["ECountServiceProxy"] == null)
//            {
//                HttpContext.Current.Session["ECountServiceProxy"] = new ECountServiceProxy();
//            }
//            //Thread.CurrentPrincipal = new ECountPrincipal(CurrentUser);
//#endif
//                service = HttpContext.Current.Session["ECountServiceProxy"] as IECountService;// as ECountServiceProxy;
//                if (((ECountServiceProxy)service).State == System.ServiceModel.CommunicationState.Faulted)
//                {
//                    service = new ECountServiceProxy();
//                    HttpContext.Current.Session["ECountServiceProxy"] = service;
//                }
//            }
//            else
//            {
//#if DEBUG
//            if (HttpContext.Current.Session["ECountServiceProxy"] == null)
//            {
//                HttpContext.Current.Session["ECountServiceProxy"] = new SGM.ECount.Service.Service(); 
//            }
//            //Thread.CurrentPrincipal = new ECountPrincipal(CurrentUser);
//#endif

//                service = HttpContext.Current.Session["ECountServiceProxy"] as IECountService;
//            }

            return Utils.GetService();//service;
        }
    }


    protected enum PageMode
    {
        Edit,
        View,
        Lookup
    }

    protected PageMode Mode
    {
        get
        {
            string mode = Request.QueryString["Mode"];
            if (string.IsNullOrEmpty(mode))
            {
                return PageMode.View;
            }
            PageMode pageMode = (PageMode)Enum.Parse(typeof(PageMode), mode, true);
            return pageMode;
        }
    }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);

        CheckPageAuthorization();
        Toolbar toolbar;
        if (TryGetToolbar(this, out toolbar))
        {
            AuthorizeOperation(toolbar);
        }

        //SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

    }

    private void CheckPageAuthorization()
    {
        string path = Request.RawUrl;
        Operation operation = new Operation { CommandName = path };
        operation = Service.GetOperationByKey(operation);
        if (operation == null)
            return;

        List<UserGroup> roles = Service.GetUserGroupsByOperation(operation);
        if (!roles.Exists(o => o.GroupName == CurrentUser.UserInfo.UserGroup.GroupName))
            Response.Redirect(System.Web.Security.FormsAuthentication.LoginUrl);
    }

    protected SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
    {
        if (SiteMap.CurrentNode == null)
            return SiteMap.CurrentNode;
        SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
        if (!string.IsNullOrEmpty(currentNode.Url))
        {
            SiteMapNode tempNode = currentNode;
            Operation operation = new Operation { CommandName = tempNode.Url };
            operation = Service.GetOperationByKey(operation);
            List<UserGroup> roles = Service.GetUserGroupsByOperation(operation);

            foreach (var item in roles)
            {
                tempNode.Roles.Add(item.GroupName);
            }
        }
        return currentNode;
    }

    private void AuthorizeOperation(Toolbar toolbar)
    {
        // string name = this.GetType().BaseType.Name;
        int count = 0;
        Operation oPage = new Operation { CommandName = Request.RawUrl };
        oPage = Service.GetOperationByKey(oPage);
        if (oPage == null)
            return;
        List<Operation> ops = null;
        if (CurrentUser.UserInfo.UserGroup != null)
            ops = Service.GetOperationsByUserGroup(CurrentUser.UserInfo.UserGroup);
        foreach (ToolbarButton item in toolbar.Items)
        {
            string commandName = item.CommandName;
            Operation operation = null;
            if (ops != null)
            {
                operation = ops.FirstOrDefault(o => string.Compare(o.CommandName, commandName, true) == 0 && o.ParentOperationID == oPage.OperationID);
            }
            if (operation == null)
            {
                item.Visible = false;
                count++;
            }
            else
            {
                item.Visible = true;
            }
        }
        if (count == toolbar.Items.Count)
        {
            toolbar.Visible = false;
        }
    }

    private bool TryGetToolbar(Control control, out Toolbar toolbar)
    {
        if (control is Toolbar)
        {
            toolbar = control as Toolbar;
            return true;
        }
        else
        {
            if (control.Controls.Count > 0)
            {
                foreach (Control child in control.Controls)
                {
                    if (TryGetToolbar(child, out toolbar))
                    {
                        return true;
                    }
                }
                toolbar = null;
                return false;
            }
            else
            {
                toolbar = null;
                return false;
            }
        }
    }

    public ECountBasePage()
    {
    }

    public ECountIdentity CurrentUser
    {
        get
        {
            ECountIdentity identity = Context.User.Identity as ECountIdentity;

            return identity;// new ECountIdentity(userInfo);

        }
    }

    public List<Plant> Plants
    {
        get
        {
            List<Plant> plants = CacheHelper.GetCache(Consts.CACHE_KEY_PLANT) as List<Plant>;
            if (plants == null)
            {
                plants = Service.GetPlants();
                CacheHelper.SetCache(Consts.CACHE_KEY_PLANT, plants, TimeSpan.FromDays(1));
                //SetCache(Consts.CACHE_KEY_PLANT, plants);
            }
            return plants;
        }
    }

    public List<StocktakeType> StocktakeTypes
    {
        get
        {
            List<StocktakeType> stocktakeTypes = CacheHelper.GetCache(Consts.CACHE_KEY_STOCKTAKETYPES) as List<StocktakeType>;
            if (stocktakeTypes == null)
            {
                stocktakeTypes = Service.GetStocktakeTypes();
                CacheHelper.SetCache(Consts.CACHE_KEY_STOCKTAKETYPES, stocktakeTypes);
            }
            return stocktakeTypes;
        }
    }

    public List<StoreLocationType> StoreLocationTypes
    {
        get
        {
            List<StoreLocationType> locationTypes = CacheHelper.GetCache(Consts.CACHE_KEY_STORELOCATION_TYPES) as List<StoreLocationType>;
            if (locationTypes == null)
            {
                locationTypes = Service.QueryStoreLocationTypes(null);
                CacheHelper.SetCache(Consts.CACHE_KEY_STORELOCATION_TYPES, locationTypes);
            }
            return locationTypes;
        }
    }
    public List<StocktakePriority> Priorities
    {
        get
        {
            List<StocktakePriority> priorities = CacheHelper.GetCache(Consts.CACHE_KEY_PRIORITY) as List<StocktakePriority>;
            if (priorities == null)
            {
                priorities = Service.GetStocktakePriorities();
                CacheHelper.SetCache(Consts.CACHE_KEY_PRIORITY, priorities);
            }
            return priorities;
        }
    }


    public List<StoreLocation> StoreLocations
    {
        get
        {
            List<StoreLocation> storeLocations = CacheHelper.GetCache(Consts.CACHE_KEY_STORE_LOCATION) as List<StoreLocation>;
            if (storeLocations == null||storeLocations.Count==0)
            {
                storeLocations = Service.QueryStoreLocations(new StoreLocation());
                CacheHelper.SetCache(Consts.CACHE_KEY_STORE_LOCATION, storeLocations);
            }
            return storeLocations;
        }
    }


    public List<BizParams> BizParamsList
    {
        get
        {
            List<BizParams> paramsList = CacheHelper.GetCache(Consts.CACHE_KEY_BIZ_PARAMS) as List<BizParams>;
            if (paramsList == null)
            {
                paramsList = Service.GetBizParamsList();
                CacheHelper.SetCache(Consts.CACHE_KEY_BIZ_PARAMS, paramsList);

            }
            return paramsList;
        }
    }

    public List<UserGroup> UserGroups
    {
        get
        {
            List<UserGroup> usergroups = CacheHelper.GetCache(Consts.CACHE_KEY_USER_GROUPS) as List<UserGroup>;
            if (usergroups == null)
            {
                usergroups = Service.QueryUserGroups(new UserGroup());
                CacheHelper.SetCache(Consts.CACHE_KEY_USER_GROUPS, usergroups);
            }
            return usergroups;
        }
    }


    public List<CycleCountLevel> _cycleCountLevels;

    public List<CycleCountLevel> CycleCountLevels
    {
        get
        {
            List<CycleCountLevel> cycleCountLevels = CacheHelper.GetCache(Consts.CACHE_KEY_CYCLE_COUNT_LEVEL) as List<CycleCountLevel>;
            if (cycleCountLevels == null)
            {
                cycleCountLevels = Service.QueryCycleCountLevels(new CycleCountLevel());
                CacheHelper.SetCache(Consts.CACHE_KEY_CYCLE_COUNT_LEVEL, cycleCountLevels);
            }
            return cycleCountLevels;
        }
    }

    public List<PartCategory> PartCategorys
    {
        get
        {
            List<PartCategory> partcategorys = CacheHelper.GetCache(Consts.CACHE_KEY_PART_CATEGORY) as List<PartCategory>;
            if (partcategorys == null)
            {
                partcategorys = Service.QueryPartCategorys(new PartCategory());
                CacheHelper.SetCache(Consts.CACHE_KEY_PART_CATEGORY, partcategorys);
            }
            return partcategorys;
        }
    }

    public List<PartStatus> PartStatus
    {
        get
        {
            List<PartStatus> partstatus = CacheHelper.GetCache(Consts.CACHE_KEY_PART_STATUS) as List<PartStatus>;
            if (partstatus == null)
            {
                partstatus = Service.QueryPartStatuss(new PartStatus());
                CacheHelper.SetCache(Consts.CACHE_KEY_PART_STATUS, partstatus);
            }
            return partstatus;
        }
    }

    public List<Supplier> Suppliers
    {
        get
        {
            List<Supplier> suppliers = CacheHelper.GetCache(Consts.CACHE_KEY_SUPPLIER) as List<Supplier>;
            if (suppliers == null)
            {
                suppliers = Service.QuerySuppliers(new Supplier());
                CacheHelper.SetCache(Consts.CACHE_KEY_SUPPLIER, suppliers);
            }
            return suppliers;
        }
    }

    public List<StocktakeStatus> _stocktakeStatuses;
    public List<StocktakeStatus> StocktakeStatuses
    {
        get
        {
            List<StocktakeStatus> stocktakeStatuses = CacheHelper.GetCache(Consts.CACHE_KEY_CYCLE_COUNT_LEVEL) as List<StocktakeStatus>;
            if (stocktakeStatuses == null)
            {
                stocktakeStatuses = Service.GetStocktakeStatus();
                CacheHelper.SetCache(Consts.CACHE_KEY_STOCKTAKE_STATUS, stocktakeStatuses);
            }
            return stocktakeStatuses;
        }
    }

    protected bool PermitStaticRequest()
    {

        BizParams paramDayofWeek = BizParamsList.FirstOrDefault(p => string.Equals(p.ParamKey, "NoStAppDay"));
        BizParams paramTimeStart = BizParamsList.FirstOrDefault(p => string.Equals(p.ParamKey, "NoStAppTimeStart"));
        BizParams paramTimeEnd = BizParamsList.FirstOrDefault(p => string.Equals(p.ParamKey, "NoStAppTimeEnd"));
        if (int.Parse(paramDayofWeek.ParamValue) == (int)DateTime.Now.DayOfWeek)
        {
            DateTime timeStart = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + paramTimeStart.ParamValue);
            DateTime timeEnd = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + paramTimeEnd.ParamValue);
            if (DateTime.Now >= timeStart && DateTime.Now <= timeEnd)
            {
                return false;
            }
        }
        return true;
    }

    #region Data bind utilities

    /// <summary>
    /// Bind data to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="dataSource"></param>
    public void BindDataControl(BaseDataBoundControl dataControl, object dataSource)
    {
        BindDataControl(dataControl, dataSource, false);
    }


    /// <summary>
    /// Bind data to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="dataSource"></param>
    public void BindDataControl(BaseDataBoundControl dataControl, object dataSource, bool addEmptyItem)
    {
        dataControl.DataSource = dataSource;
        dataControl.DataBind();
        if (addEmptyItem)
        {
            ListControl control = dataControl as ListControl;
            if (control != null)
            {
                control.Items.Insert(0, new ListItem(Consts.DROPDOWN_UNSELECTED_TEXT, string.Empty));
            }
        }
    }

    /// <summary>
    /// Bind data to DataList Control
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="dataSource"></param>
    public void BindDataControl(BaseDataList dataControl, object dataSource)
    {
        dataControl.DataSource = dataSource;
        dataControl.DataBind();
    }
    /// <summary>
    /// Bind Stocktake Types to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStocktakeTypes(BaseDataBoundControl dataControl)
    {
        BindStocktakeTypes(dataControl, false);
    }
    /// <summary>
    /// Bind Stocktake Types to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStocktakeTypes(BaseDataBoundControl dataControl, bool addEmptyItem)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "TypeID", "TypeName");
        }
        BindDataControl(dataControl, this.StocktakeTypes, addEmptyItem);
    }

    /// <summary>
    /// bind plants to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindPlants(BaseDataBoundControl dataControl)
    {
        BindPlants(dataControl, false);
    }
    /// <summary>
    /// bind plants to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindPlants(BaseDataBoundControl dataControl, bool addEmptyItem)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "PlantID", "PlantCode");
        }
        BindDataControl(dataControl, this.Plants, addEmptyItem);
    }
    //public static string[] PlantHierarchy
    //{
    //    get
    //    {

    //        return new string[] { "Plant", "Workshop" };
    //    }
    //}

    //public static string[] WorkshopHierarchy
    //{
    //    get
    //    {

    //        return new string[] { "Workshop", "Segment" };
    //    }
    //}

    [WebMethod]
    [ScriptMethod]
    public static CascadingDropDownNameValue[] GetPlantPageMethod(string knownCategoryValues, string category)
    {
        List<Plant> plantList = GlobalService.GetPlants();

        List<CascadingDropDownNameValue> values =
         new List<CascadingDropDownNameValue>();
        values.Add(new CascadingDropDownNameValue(Consts.DROPDOWN_UNSELECTED_TEXT, string.Empty));
        foreach (var plant in plantList)
        {
            values.Add(new CascadingDropDownNameValue(plant.PlantCode, plant.PlantID.ToString()));
        }

        // Perform a simple query against the data document
        return values.ToArray();// AjaxControlToolkit.CascadingDropDown.QuerySimpleCascadingDropDownDocument(doc, PlantHierarchy, knownCategoryValuesDictionary, category);
    }

    [WebMethod]
    [ScriptMethod]
    public static CascadingDropDownNameValue[] GetWorkshopsPageMethod(string knownCategoryValues, string category)
    {
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int plantID;
        if (kv.ContainsKey("PlantCode"))
        {
            List<CascadingDropDownNameValue> values =
                     new List<CascadingDropDownNameValue>();
            if (Int32.TryParse(kv["PlantCode"], out plantID))
            {
                List<Workshop> workshopList = GlobalService.GetWorkshopbyPlantID(plantID);

                values.Add(new CascadingDropDownNameValue(Consts.DROPDOWN_UNSELECTED_TEXT, string.Empty));
                foreach (var item in workshopList)
                {
                    values.Add(new CascadingDropDownNameValue(item.WorkshopCode, item.WorkshopID.ToString()));
                }

            }
            return values.ToArray();
        }
        else
        {
            return null;
        }
        //StringDictionary knownCategoryValuesDictionary = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

        //// Perform a simple query against the data document
        //return AjaxControlToolkit.CascadingDropDown.QuerySimpleCascadingDropDownDocument(doc, WorkshopHierarchy, knownCategoryValuesDictionary, category);
    }


    [WebMethod]
    [ScriptMethod]
    public static CascadingDropDownNameValue[] GetSegmentsPageMethod(string knownCategoryValues, string category)
    {
        StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int workshopID;
        if (kv.ContainsKey("WorkshopCode"))
        {
            List<CascadingDropDownNameValue> values =
                     new List<CascadingDropDownNameValue>();
            if (Int32.TryParse(kv["WorkshopCode"], out workshopID))
            {
                List<Segment> segmentList = GlobalService.GetSegmentbyWorkshopID(workshopID);

                values.Add(new CascadingDropDownNameValue(Consts.DROPDOWN_UNSELECTED_TEXT, string.Empty));
                foreach (var item in segmentList)
                {
                    values.Add(new CascadingDropDownNameValue(item.SegmentCode, item.SegmentID.ToString()));
                }

            }
            return values.ToArray();
        }
        else
        {
            return null;
        }
    }


    /// <summary>
    /// bind Workshops to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="plant"></param>
    public void BindWorkshops(BaseDataBoundControl dataControl, Plant plant)
    {
        BindWorkshops(dataControl, plant, false);
    }

    /// <summary>
    /// bind Workshops to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="plant"></param>
    public void BindWorkshops(BaseDataBoundControl dataControl, Plant plant, bool addEmptyItem)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "WorkshopID", "WorkshopCode");
        }
        List<Workshop> workshops = Service.GetWorkshopbyPlant(plant);
        BindDataControl(dataControl, workshops, addEmptyItem);
    }


    /// <summary>
    /// bind Segments to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="plant"></param>
    public void BindSegments(BaseDataBoundControl dataControl)
    {
        BindSegments(dataControl, null);
    }

    /// <summary>
    /// bind Segments to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="plant"></param>
    public void BindSegments(BaseDataBoundControl dataControl, Workshop workshop)
    {
        BindSegments(dataControl, workshop, false);
    }

    /// <summary>
    /// bind Segments to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="plant"></param>
    public void BindSegments(BaseDataBoundControl dataControl, Workshop workshop, bool addEmptyItem)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "SegmentID", "SegmentCode");
        }
        List<Segment> segments;
        if (workshop == null)
        {
            segments = Service.GetSegment();
        }
        else
        {
            segments = Service.GetSegmentbyWorkshop(workshop);
        }
        BindDataControl(dataControl, segments, addEmptyItem);
    }

    /// <summary>
    /// bind stocktake priority to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStocktakePriority(BaseDataBoundControl dataControl)
    {
        BindStocktakePriority(dataControl, false);
    }

    /// <summary>
    /// bind stocktake priority to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStocktakePriority(BaseDataBoundControl dataControl, bool addEmptyItem)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "PriorityID", "PriorityName");
        }
        BindDataControl(dataControl, this.Priorities, addEmptyItem);
    }


    /// <summary>
    /// bind cycle count level to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindCycleCountLevel(BaseDataBoundControl dataControl)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "LevelID", "LevelName");
        }
        BindDataControl(dataControl, this.CycleCountLevels);
    }


    /// <summary>
    /// bind stocktake status to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStocktakeStatus(BaseDataBoundControl dataControl, bool insertEmptyValue)
    {

        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "StatusID", "StatusName");
        }
        BindDataControl(dataControl, this.StocktakeStatuses);
    }

    /// <summary>
    /// bind stocktake status to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStocktakeStatus(BaseDataBoundControl dataControl)
    {
        BindStocktakeStatus(dataControl, false);
    }

    /// <summary>
    /// bind store location to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStoreLocation(BaseDataBoundControl dataControl)
    {
        BindStoreLocation(dataControl, false);
    }
    /// <summary>
    /// bind store location to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStoreLocation(BaseDataBoundControl dataControl,bool addEmptyItem)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "LocationID", "LocationName");
        }
        BindDataControl(dataControl, this.StoreLocations,addEmptyItem);
    }
    /// <summary>
    /// bind store location type to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStoreLocationType(BaseDataBoundControl dataControl)
    {
        BindStoreLocationType(dataControl, false);
    }
    /// <summary>
    /// bind store location type to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindStoreLocationType(BaseDataBoundControl dataControl, bool addEmptyItem)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "TypeID", "TypeName");
        }
        BindDataControl(dataControl, this.StoreLocationTypes, addEmptyItem);
    }

    /// <summary>
    /// bind usergroups to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    public void BindUserGroup(BaseDataBoundControl dataControl)
    {
        if (dataControl is ListControl)
        {
            ListControl control = (ListControl)dataControl;
            BindListControl(control, "GroupID", "GroupName");
        }
        BindDataControl(dataControl, this.UserGroups);
    }

    public void BindListControl(ListControl control, string dataValueField, string dataTextField)
    {
        control.DataValueField = dataValueField;
        control.DataTextField = dataTextField;
    }

    public void BindEmptyGridView(GridView gv, object dataSource)
    {
        if (gv.Rows.Count == 0)
        {
            BindDataControl(gv, dataSource);
            gv.Rows[0].Visible = false;
        }
    }

    #endregion

    /// <summary>
    /// bind data to dropdownlist control
    /// </summary>
    /// <param name="ddl">control reference</param>
    /// <param name="type">tag type</param>
    /// <param name="filter"></param>
    public void BindDropDownList(DropDownList ddl, DropDownType type, params string[] filter)
    {
        switch (type)
        {
            case DropDownType.Plant:
                List<Plant> plants = Service.GetPlants();
                ddl.DataTextField = "PlantCode";
                ddl.DataValueField = "PlantID";
                ddl.DataSource = plants;
                break;
            case DropDownType.PartCategory:
                List<PartCategory> pcs = Service.QueryPartCategorys(new PartCategory());
                ddl.DataTextField = "CategoryName";
                ddl.DataValueField = "CategoryID";
                ddl.DataSource = pcs;
                break;
            case DropDownType.PartStatus:
                List<PartStatus> pss = Service.QueryPartStatuss(new PartStatus());
                ddl.DataTextField = "StatusName";
                ddl.DataValueField = "StatusID";
                ddl.DataSource = pss;
                break;
            case DropDownType.Workshop:
                List<Workshop> workshops = Service.GetWorkshopbyPlantID(int.Parse(filter[0]));
                ddl.DataTextField = "WorkshopCode";
                ddl.DataValueField = "WorkshopID";
                ddl.DataSource = workshops;
                break;
            case DropDownType.Segment:
                List<Segment> ss = Service.GetSegmentbyWorkshopID(int.Parse(filter[0]));
                ddl.DataTextField = "SegmentName";
                ddl.DataValueField = "SegmentID";
                ddl.DataSource = ss;
                break;
            case DropDownType.CycleCountLevel:
                List<CycleCountLevel> ccls = Service.QueryCycleCountLevels(new CycleCountLevel());
                ddl.DataTextField = "LevelName";
                ddl.DataValueField = "LevelID";
                ddl.DataSource = ccls;
                break;

            case DropDownType.PartGroup:
                List<PartGroup> pgs = Service.QueryPartGroups(new PartGroup());
                ddl.DataTextField = "GroupName";
                ddl.DataValueField = "GroupID";
                ddl.DataSource = pgs;
                break;
            case DropDownType.StoreLocationType:
                List<StoreLocationType> slts = Service.QueryStoreLocationTypes(new StoreLocationType());
                ddl.DataTextField = "TypeName";
                ddl.DataValueField = "TypeID";
                ddl.DataSource = slts;
                break;
            case DropDownType.StocktakePriority:
                List<StocktakePriority> stps = Service.GetStocktakePriorities();
                ddl.DataTextField = "PriorityName";
                ddl.DataValueField = "PriorityID";
                ddl.DataSource = stps;
                break;
            case DropDownType.StoreLocation:
                List<StoreLocation> sls = Service.GetStoreLocations();
                ddl.DataTextField = "LocationName";
                ddl.DataValueField = "LocationID";
                ddl.DataSource = sls;
                break;
        }
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--", ""));
    }


    protected void MergeCell(GridView gv, int keyCellIndex, List<int> indexList, Color foreColor, Color alternateForeColor, Color backgroudColor, Color alternateBgColor)
    {
        string text = string.Empty;
        int count = 0;
        Dictionary<string, int> dict = new Dictionary<string, int>();

        // loop through all rows to get row counts
        foreach (GridViewRow row in gv.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (row.Cells[keyCellIndex].Text == text)
                {
                    count++;
                }
                else
                {
                    if (count > 0)
                    {
                        dict.Add(text, count);
                    }
                    text = row.Cells[keyCellIndex].Text;
                    count = 1;
                }
            }
        }

        if (count >0)
        {
            dict.Add(text, count);
        }


        // loop through all rows again to set rowspan
        text = "";
        Color currentBgColor = foreColor;// Color.FromName("#F7F6F3");
        Color currentForeColor = backgroudColor;// Color.FromName("#333333");

        foreach (GridViewRow gvr in gv.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                if (gvr.Cells[keyCellIndex].Text == text)
                {
                    //foreach (var i in indexList)
                    for (int i = indexList.Count - 1; i >= 0; i--)
                    {
                        gvr.Cells.Remove(gvr.Cells[i]);
                    }

                }
                else
                {
                    text = gvr.Cells[keyCellIndex].Text;
                    count = Int32.Parse(dict[text].ToString());
                    if (count > 1)
                    {
                        foreach (var i in indexList)
                        {
                            gvr.Cells[i].RowSpan = count;
                        }
                    }
                    // Start of new row, alternate color
                    currentBgColor = SwapColor(currentBgColor, backgroudColor, alternateBgColor);
                    currentForeColor = SwapColor(currentForeColor, foreColor, alternateForeColor);
                }
                // Set background color
                gvr.BackColor = currentBgColor;
                gvr.ForeColor = currentForeColor;
            }
        }
    }


    protected Color SwapColor(Color originalColor, Color color, Color alternateColor)
    {
        return (originalColor == color) ? alternateColor : color;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (ShowWaitingModal)
        {
            ToolkitScriptManager.RegisterOnSubmitStatement(this, this.GetType(), "ShowWaitingModal", "showWaitingModal(); return true;");
            //    if(Page.IsPostBack && !Page.ClientScript.IsStartupScriptRegistered("CloseWatingModal"))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWatingModal", "closeWaitingModal();", true);
            //}
        }

        if (!Page.IsPostBack)
        {
            if (!CurrentUser.UserInfo.LastModified.HasValue || CurrentUser.UserInfo.LastModified.Value.AddDays(90) < DateTime.Now)
                Response.Redirect("~/ChangePwd.aspx");
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        if (ShowWaitingModal && Page.IsPostBack && !Page.ClientScript.IsStartupScriptRegistered("CloseWatingModal"))
        {
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWatingModal", "closeWaitingModal();", true);
        }
    }

}

public enum DropDownType : int
{
    Plant = 0,
    PartCategory,
    PartStatus,
    Workshop,
    Segment,
    CycleCountLevel,
    Supplier = 6,
    PartGroup = 7,
    StoreLocationType = 8,
    StocktakePriority = 9,
    StoreLocation
}
