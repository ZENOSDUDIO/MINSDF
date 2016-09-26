using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SGM.ECount.Contract.Service;
using SGM.Common.Cache;

/// <summary>
/// Summary description for ECountBaseUserControl
/// </summary>
public class ECountBaseUserControl:UserControl
{
    public ECountBasePage Container
    {
        get
        {
            return this.Page as ECountBasePage;
        }
    }

    public PageMode Mode
    {
        get
        {
            if (Request.QueryString["Mode"] != null)
            {
                if (ViewState["PageMode"]==null)
                {
                    PageMode mode = (PageMode)Enum.Parse(typeof(PageMode), Request.QueryString["Mode"]);
                    ViewState["PageMode"] = mode;
                    return mode;
                }
                else
                {
                    return (PageMode)ViewState["PageMode"];
                }
            }
            else
            {
                return PageMode.View;
            }
        }
    }

    public IECountService Service
    {
        get
        {
            return Container.Service;
        }
    }
    public List<Plant> Plants
    {
        get
        {
            return Container.Plants;
        }
    }

    public List<StocktakeType> StocktakeTypes
    {
        get
        {
            return Container.StocktakeTypes;
        }
    }

    public List<StocktakePriority> Priorities
    {
        get
        {
            return Container.Priorities;
        }
    }
    public ECountIdentity CurrentUser
    {
        get
        {
            return Container.CurrentUser;
        }
    }
    /// <summary>
    /// add item into cache
    /// </summary>
    /// <param name="cacheKey">key of cache item</param>
    /// <param name="value">new item</param>
    protected void SetCache(string cacheKey, object value)
    {
        CacheHelper.SetCache(cacheKey, value);
    }

    /// <summary>
    /// retrieve item from cache
    /// </summary>
    /// <param name="cacheKey">cache key</param>
    /// <returns>item founded,null if it's not in cache</returns>
    protected object GetCache(string cacheKey)
    {
        
        return CacheHelper.GetCache(cacheKey);
    }

    public List<CycleCountLevel> _cycleCountLevels;

    public List<CycleCountLevel> CycleCountLevels
    {
        get
        {
            
            return Container.CycleCountLevels;
        }
    }
    public List<StocktakeStatus> StocktakeStatuses
    {
        get
        {
            return Container.StocktakeStatuses;
        }
    }

    #region Data bind utilities

    /// <summary>
    /// Bind data to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="dataSource"></param>
    public void BindDataControl(BaseDataBoundControl dataControl, object dataSource)
    {
        Container.BindDataControl(dataControl, dataSource);
    }


    /// <summary>
    /// Bind data to DataList Control
    /// </summary>
    /// <param name="dataControl"></param>
    /// <param name="dataSource"></param>
    protected void BindDataControl(BaseDataList dataControl, object dataSource)
    {
        Container.BindDataControl(dataControl, dataSource);
    }

    /// <summary>
    /// Bind Stocktake Types to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    protected void BindStocktakeTypes(BaseDataBoundControl dataControl)
    {
        Container.BindStocktakeTypes(dataControl);
    }

    /// <summary>
    /// bind plants to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    protected void BindPlants(BaseDataBoundControl dataControl)
    {
        Container.BindPlants(dataControl);
    }

    /// <summary>
    /// bind stocktake priority to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    protected void BindStocktakePriority(BaseDataBoundControl dataControl)
    {
        this.Container.BindStocktakePriority(dataControl);
    }


    /// <summary>
    /// bind cycle count level to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    protected void BindCycleCountLevel(BaseDataBoundControl dataControl)
    {
        this.Container.BindCycleCountLevel(dataControl);
    }


    /// <summary>
    /// bind stocktake status to DataBoundControl
    /// </summary>
    /// <param name="dataControl"></param>
    protected void BindStocktakeStatus(BaseDataBoundControl dataControl)
    {
        this.Container.BindStocktakeStatus(dataControl);
    }

    protected void BindListControl(ListControl control, string dataValueField, string dataTextField)
    {
        Container.BindListControl(control, dataValueField, dataTextField);
    }

    protected void BindEmptyGridView(GridView gv, object dataSource)
    {
        Container.BindEmptyGridView(gv, dataSource);
    }

    #endregion

}

public enum PageMode
{
    Edit,
    Select,
    View
}
