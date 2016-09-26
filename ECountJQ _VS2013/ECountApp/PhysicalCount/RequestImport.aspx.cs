using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using SGM.ECount.DataModel;
using System.Drawing;
using SGM.ECount.Utility;
using AjaxControlToolkit;
using System.Data;
using SGM.Common.Utility;
using SGM.Common.Cache;


public partial class PhysicalCount_RequestImport : ECountBasePage
{
    List<ViewStockTakeRequest> _detailsList;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UCFileUpload1.ValidationSchemaFile = Server.MapPath(@"~/ImportSchema/StocktakeRequest.xml");
        }
        this.UCFileUpload1.OnUpload += new EventHandler(this.ucFileUpload_Upload);
    }

    /// <summary>
    /// check: 
    /// part exists
    /// current user can request this part
    /// repeat parts
    /// stocktake type exists
    /// priority exists
    /// show error details of each row
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    //private bool ValidateDetails(string content)
    //{
    //    _detailsList = new List<ViewStockTakeRequest>();
    //    List<int> altHashCode = new List<int>();
    //    List<StocktakePriority> altPriority = base.Priorities;
    //    List<StocktakeType> altType = base.StocktakeTypes;
    //    string[][] importString;
    //    bool isValid = true;
    //    if (content != null)
    //    {
    //        importString = CSVUtiltiy.SplitCSV(content);
    //        for (int i = 1; i < importString.Length; i++)
    //        {
    //            string error = string.Empty;
    //            string rowNumber = "第" + (i + 1) + "行";
    //            string partcode = importString[i][0];


    //            if (string.IsNullOrEmpty(partcode))
    //            {
    //                error += rowNumber + "零件号为空；";
    //            }
    //            string plantcode = importString[i][1];
    //            if (string.IsNullOrEmpty(plantcode))
    //            {
    //                error += error.Length > 0 ? "工厂编码为空；" : rowNumber + "工厂编码为空；";
    //            }
    //            string duns = importString[i][2];
    //            if (string.IsNullOrEmpty(duns))
    //            {
    //                error += error.Length > 0 ? "供应商DUNS为空；" : rowNumber + "供应商DUNS为空；";
    //            }
    //            if (error.Length == 0)
    //            {
    //                Part info = new Part();
    //                info.PartCode = partcode.Trim();
    //                info.Plant = new Plant();
    //                info.Plant.PlantCode = plantcode.Trim();
    //                info.Supplier = new Supplier();
    //                info.Supplier.DUNS = duns.Trim();
    //                if (Service.QueryParts(info).Count == 0)
    //                {
    //                    error += error.Length > 0 ? "零件不存在；" : rowNumber + "零件不存在；";
    //                }
    //                // add some split charactors to make sure it's unique
    //                int hc = (info.PartCode + "^" + info.Plant.PlantCode + "`" + info.Supplier.DUNS).GetHashCode();
    //                if (!altHashCode.Contains(hc))
    //                {
    //                    altHashCode.Add(hc);
    //                }
    //                else
    //                {
    //                    error += error.Length > 0 ? "零件已重复存在；" : rowNumber + "零件已重复存在；";
    //                }
    //            }
    //            string typename = importString[i][3];
    //            if (string.IsNullOrEmpty(typename))
    //            {
    //                error += error.Length > 0 ? "盘点类型为空；" : rowNumber + "盘点类型为空；";
    //            }
    //            else
    //            {
    //                if (!altType.Exists(p => p.TypeName == typename))
    //                {
    //                    error += error.Length > 0 ? "盘点类型非法；" : rowNumber + "盘点类型非法；";
    //                }
    //            }

    //            string priorityname = importString[i][4];
    //            if (string.IsNullOrEmpty(priorityname))
    //            {
    //                error += error.Length > 0 ? "盘点优先级为空；" : rowNumber + "盘点优先级为空；";
    //            }
    //            else
    //            {
    //                if (!altPriority.Exists(p => p.PriorityName == priorityname))
    //                {
    //                    error += error.Length > 0 ? "盘点优先级非法；" : rowNumber + "盘点优先级非法；";
    //                }
    //            }
    //            if (error.Length == 0)
    //            {
    //                _detailsList.Add(new ViewStockTakeRequest { PartCode = importString[i][0], PlantName = importString[i][1], DUNS = importString[i][2], TypeName = importString[i][3], PriorityName = importString[i][4] });
    //            }
    //            ///TODO: if invalid, add error details of each row into bulleted list
    //            else
    //            {
    //                isValid = false;
    //                //bllResultInfo.Items.Add(error + "\r\n");
    //                this.UCFileUpload1.AddErrorInfo(error + "\r\n");
    //            }
    //        }
    //    }
    //    BindDataControl(gvResult, _detailsList);
    //    return isValid;
    //}

    protected void gvResult_PreRender(object sender, EventArgs e)
    {
        List<View_StocktakeDetails> detailsList = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
        BindEmptyGridView(gvResult, detailsList);
    }

    protected void ucFileUpload_Upload(object sender, EventArgs e)
    {
        UploadEventArgs ue = e as UploadEventArgs;
        DataTable dtDetails = ue.ContentTable;
        dtDetails.DefaultView.Sort = "零件号";
        dtDetails = dtDetails.DefaultView.ToTable();
        bool isValid = true;
        List<ViewPart> partList = new List<ViewPart>();

        //get part by code, plant, duns
        //for (int i = 0; i < dtDetails.Rows.Count; i++)
        //{
        //    ViewPart tmpPart = new ViewPart
        //    {
        //        PlantCode = dtDetails.Rows[i]["工厂代码"].ToString(),
        //        DUNS = dtDetails.Rows[i]["供应商DUNS"].ToString(),
        //        PartCode = dtDetails.Rows[i]["零件号"].ToString()
        //    };
        //    if (!partList.Exists(p => string.Equals(p.PartCode , tmpPart.PartCode, StringComparison.OrdinalIgnoreCase) && string.Equals(p.PlantCode ,tmpPart.PlantCode , StringComparison.OrdinalIgnoreCase)&& string.Equals(p.DUNS ,tmpPart.DUNS, StringComparison.OrdinalIgnoreCase)))
        //    {
        //        partList.Add(tmpPart);
        //    }
        //}

        List<View_StocktakeDetails> detailsList = new List<View_StocktakeDetails>();
        for (int i = 0; i < dtDetails.Rows.Count; i++)
        {
            bool hasError = false;
            View_StocktakeDetails details = new View_StocktakeDetails
            {
                RowNumber = int.Parse(dtDetails.Rows[i]["序号"].ToString()),
                PartCode = dtDetails.Rows[i]["零件号"].ToString(),
                DUNS = dtDetails.Rows[i]["供应商DUNS"].ToString(),
                PartPlantCode = dtDetails.Rows[i]["工厂代码"].ToString(),
                TypeName = dtDetails.Rows[i]["申请类别"].ToString(),
                PriorityName = dtDetails.Rows[i]["紧急程度"].ToString(),
                Description = dtDetails.Rows[i]["备注"].ToString()
            };
            string msg;
            if (i % 5000 == 0)
            {
                string startCode = details.PartCode;
                int end = i + 4999;
                if (end >= dtDetails.Rows.Count)
                {
                    end = dtDetails.Rows.Count -1 ;
                }
                string endCode = dtDetails.Rows[end]["零件号"].ToString();
                partList = Service.QueryPartCodeScope(null, startCode, endCode);
            }
            ViewPart part = partList.FirstOrDefault(p => string.Equals(p.PartCode,details.PartCode, StringComparison.OrdinalIgnoreCase) && string.Equals(p.PlantCode ,details.PartPlantCode, StringComparison.OrdinalIgnoreCase) && string.Equals(p.DUNS ,details.DUNS, StringComparison.OrdinalIgnoreCase));
            if (part == null)//part is invalid
            {
                msg = string.Format("第{0}行，该零件不存在", i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                details.PartID = part.PartID;
            }
            StocktakePriority priority = Priorities.FirstOrDefault(p => string.Equals(p.PriorityName ,details.PriorityName, StringComparison.OrdinalIgnoreCase));
            if (priority == null)
            {
                msg = string.Format("第{0}行，紧急程度信息非法", i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                details.Priority = priority.PriorityID;
            }
            StocktakeType type = StocktakeTypes.FirstOrDefault(t => string.Equals(t.TypeName, details.TypeName, StringComparison.OrdinalIgnoreCase));
            if (type == null)
            {
                msg = string.Format("第{0}行，申请类别信息非法", i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                details.StocktakeType = type.TypeID;
            }
            if (!hasError)
            {
                if (detailsList.Exists(d => string.Equals(d.TypeName ,details.TypeName, StringComparison.OrdinalIgnoreCase) && string.Equals(d.PriorityName , details.PriorityName, StringComparison.OrdinalIgnoreCase) && string.Equals(d.PartPlantCode , details.PartPlantCode, StringComparison.OrdinalIgnoreCase) && string.Equals(d.DUNS , details.DUNS, StringComparison.OrdinalIgnoreCase) && string.Equals(d.PartCode, details.PartCode, StringComparison.OrdinalIgnoreCase)))
                {
                    msg = string.Format("第{0}行，数据重复", i + 2);
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                }
                else
                {
                    detailsList.Add(details);
                }
            }
            isValid = isValid && !hasError;
        }

        if (isValid)
        {
            NewStocktakeRequest newRequest = new NewStocktakeRequest
            {
                IsStatic = (rblIsStatic.SelectedIndex == 1),
                RequestBy = CurrentUser.UserInfo.UserID,
                IsCycleCount = false,
                Details = (from d in detailsList
                           select new NewStocktakeDetails
                           {
                               PartID = d.PartID.ToString(),
                               StocktakePriority = d.Priority.Value,
                               StocktakeTypeID = d.StocktakeType.Value,
                               Description = d.Description
                           }).ToList()
            };
            int currentDynAmount = 0;
            int currentStAmount = 0;
            foreach (var item in UserGroups)
            {
                currentDynAmount += item.CurrentDynamicStocktake ?? 0;
                currentStAmount += item.CurrentStaticStocktake ?? 0;
            }
            int maxUserCount;
            int currentCount;
            int maxCount;
            if (!newRequest.IsStatic)
            {
                BizParams param = BizParamsList.Find(p => p.ParamKey == "MaxDynamic");
                maxCount = int.Parse(param.ParamValue);
                maxUserCount = CurrentUser.UserInfo.UserGroup.MaxDynamicStocktake ?? 0;
                currentCount = CurrentUser.UserInfo.UserGroup.CurrentDynamicStocktake.Value;
                int dynOverflowAmount = currentCount + newRequest.Details.Count - maxUserCount;
                if (dynOverflowAmount > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "maxOverflow", "alert('超过当前用户组动态盘点上限" + dynOverflowAmount + "个')", true);
                    return;
                }
                dynOverflowAmount = currentDynAmount + newRequest.Details.Count - maxCount;
                if (dynOverflowAmount > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "maxOverflow", "alert('超过动态盘点总数上限" + dynOverflowAmount + "个')", true);
                    return;
                }
            }

            if (newRequest.IsStatic)
            {
                BizParams param = BizParamsList.Find(p => p.ParamKey == "MaxStatic");
                maxCount = int.Parse(param.ParamValue);
                maxUserCount = CurrentUser.UserInfo.UserGroup.MaxStaticStocktake.Value;

                currentCount = CurrentUser.UserInfo.UserGroup.CurrentStaticStocktake.Value;
                int stOverflowAmount = currentCount + newRequest.Details.Count - maxUserCount;
                if (stOverflowAmount > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "maxOverflow", "alert('超过当前用户组静态盘点上限" + stOverflowAmount + "个')", true);
                    return;
                }

                stOverflowAmount = currentStAmount + newRequest.Details.Count - maxCount;
                if (stOverflowAmount > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "maxOverflow", "alert('超过静态盘点总数上限" + stOverflowAmount + "个')", true);
                    return;
                }
            }
            //fill in items
            StocktakeRequest request = Service.RequestStocktake(newRequest);
            BindDataControl(gvResult, detailsList);
            CurrentUser.RefreshUserProfile();
            CacheHelper.RemoveCache(Consts.CACHE_KEY_USER_GROUPS);
            //show information
            this.UCFileUpload1.AddSuccessInfo("盘点申请成功，申请单号" + request.RequestNumber, string.Empty, string.Empty);
        }
    }


    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (e.CommandName == "return")
        {
            Response.Redirect("StocktakeReqList.aspx");
        }
    }
}

