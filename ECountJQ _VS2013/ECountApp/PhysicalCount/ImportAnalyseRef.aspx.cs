using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SGM.ECount.DataModel;
using SGM.ECount.Service;

public partial class PhysicalCount_ImportAnalyseRef : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UCFileUpload1.ValidationSchemaFile = Server.MapPath(@"~/ImportSchema/AnalyseRef.xml");
        }
        this.UCFileUpload1.OnUpload += new EventHandler(this.ucFileUpload_Upload);
        pagerNotiList.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerNotiList_PageNumberSelect);

    }

    void pagerNotiList_PageNumberSelect(object sender, EventArgs e)
    {
        View_StocktakeDetails condition;
        DateTime? dateStart;
        DateTime? dateEnd;
        DateTime? planDateStart;
        DateTime? planDateEnd;
        int? locationID;
        BuildQueryCondition(out condition, out dateStart, out dateEnd, out planDateStart, out planDateEnd, out locationID);
        QueryNotification(condition, dateStart, dateEnd, planDateStart, planDateEnd, locationID);
    }

    protected void ucFileUpload_Upload(object sender, EventArgs e)
    {
        List<long> notiList = new List<long>();
        foreach (GridViewRow row in gvNotification.Rows)
        {
            if (gvNotification.DataKeys[row.RowIndex] != null)
            {

                CheckBox cbSelect = row.FindControl("cbNotiSelect") as CheckBox;
                if (cbSelect.Checked)
                {
                    long notiID = (long)gvNotification.DataKeys[row.RowIndex]["NotificationID"];
                    notiList.Add(notiID);
                }
            }
        }
        if (notiList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "selectNotiMsg", "alert('请选择通知单');", true);
            return;
        }
        UploadEventArgs ue = e as UploadEventArgs;

        DataTable dtDetails = ue.ContentTable;
        dtDetails.DefaultView.Sort = "零件号";
        dtDetails = dtDetails.DefaultView.ToTable();
        bool isValid = true;
        List<ViewPart> partList = new List<ViewPart>();

        //get part by code, plant, duns
        for (int i = 0; i < dtDetails.Rows.Count; i++)
        {
            ViewPart tmpPart = new ViewPart
            {
                PlantCode = dtDetails.Rows[i]["工厂代码"].ToString(),
                DUNS = dtDetails.Rows[i]["供应商DUNS"].ToString(),
                PartCode = dtDetails.Rows[i]["零件号"].ToString()
            };
            if (!partList.Exists(p => p.PartCode == tmpPart.PartCode && p.PlantCode == tmpPart.PlantCode && p.DUNS == tmpPart.DUNS))
            {
                partList.Add(tmpPart);
            }
        }
        //partList = Service.QueryPartsByKey(partList);

        List<View_StocktakeDetails> detailsList = new List<View_StocktakeDetails>();
        for (int i = 0; i < dtDetails.Rows.Count; i++)
        {
            bool hasError = false;

            View_StocktakeDetails details = new View_StocktakeDetails
            {
                PartCode = dtDetails.Rows[i]["零件号"].ToString(),
                DUNS = dtDetails.Rows[i]["供应商DUNS"].ToString(),
                PartPlantCode = dtDetails.Rows[i]["工厂代码"].ToString(),
                UnRecorded = dtDetails.Rows[i]["未入账"]+""
            };
            details.Wip = SGM.Common.Utility.Utils.GetDecimalDbCell(dtDetails.Rows[i]["Wip"]);
            details.M080 = SGM.Common.Utility.Utils.GetDecimalDbCell(dtDetails.Rows[i]["M080"]);
            
            string msg;
            if (i % 5000 == 0)
            {
                string startCode = details.PartCode;
                int end = i + 4999;
                if (end >= dtDetails.Rows.Count)
                {
                    end = dtDetails.Rows.Count - 1;
                }
                string endCode = dtDetails.Rows[end]["零件号"].ToString();
                partList = Service.QueryPartCodeScope(null, startCode, endCode);
            }
            ViewPart part = partList.FirstOrDefault(p => p.PartCode == details.PartCode && p.PlantCode == details.PartPlantCode && p.DUNS == details.DUNS);
            if (part == null)//part is invalid
            {
                msg = string.Format("工厂为【{0}】，供应商为【{1}】的零件【{2}】不存在", details.PartPlantCode, details.DUNS, details.PartCode);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                details.PartID = part.PartID;
            }

            if (!hasError)
            {
                if (detailsList.Exists(d => d.PartPlantCode == details.PartPlantCode && d.DUNS == details.DUNS && string.Equals(d.PartCode, details.PartCode)))
                {
                    msg = string.Format("工厂为【{0}】，供应商为【{1}】的零件【{2}】数据重复", details.PartPlantCode, details.DUNS, details.PartCode);
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
            Service.ImportAnalyseRef(detailsList, notiList);
            BindDataControl(gvResult, detailsList);
            //show information
            this.UCFileUpload1.AddSuccessInfo("导入成功", string.Empty, string.Empty);
        }
    }


    protected void gvNotification_PreRender(object sender, EventArgs e)
    {

        List<View_StocktakeNotification> list = new List<View_StocktakeNotification> { new View_StocktakeNotification() };
        BindEmptyGridView(gvNotification, list);
    }
    private void BuildQueryCondition(out View_StocktakeDetails condition, out DateTime? dateStart, out DateTime? dateEnd, out DateTime? planDateStart, out DateTime? planDateEnd, out int? locationID)
    {
        condition = new View_StocktakeDetails();
        locationID = null;

        if (!string.IsNullOrEmpty(txtCreateBy.Text.Trim()))
        {
            condition.RequestUser = txtCreateBy.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtPartCName.Text.Trim()))
        {
            condition.PartChineseName = txtPartCName.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtRequestNO.Text.Trim()))
        {
            condition.RequestNumber = txtRequestNO.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtNotificationNO.Text.Trim()))
        {
            condition.NotificationCode = txtNotificationNO.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtPartNO.Text.Trim()))
        {
            condition.PartCode = txtPartNO.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtUserGroup.Text.Trim()))
        {
            condition.RequestUserGroup = txtUserGroup.Text.Trim();
        }
        if (ddlStatus.SelectedIndex > 0)
        {
            condition.Status = Convert.ToInt32(ddlStatus.SelectedValue);
        }

        dateStart = null;
        DateTime tmpDate;
        if (DateTime.TryParse(txtDateStart.Text.Trim(), out tmpDate))
        {
            dateStart = tmpDate;
        }
        dateEnd = null;
        if (DateTime.TryParse(txtDateEnd.Text.Trim(), out tmpDate))
        {
            dateEnd = tmpDate;
        }

        planDateStart = null;
        if (DateTime.TryParse(txtPlanDateStart.Text.Trim(), out tmpDate))
        {
            planDateStart = tmpDate;
        }
        planDateEnd = null;
        if (DateTime.TryParse(txtPlanDateEnd.Text.Trim(), out tmpDate))
        {
            planDateEnd = tmpDate;
        }
    }

    private void Query()
    {
        View_StocktakeDetails condition;
        DateTime? dateStart;
        DateTime? dateEnd;
        DateTime? planDateStart;
        DateTime? planDateEnd;
        int? locationID;
        BuildQueryCondition(out condition, out dateStart, out dateEnd, out planDateStart, out planDateEnd, out locationID);

        QueryNotification(condition, dateStart, dateEnd, planDateStart, planDateEnd, locationID);



    }

    private void QueryNotification(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int? locationID)
    {
        int pageCount;
        int itemCount;
        List<View_StocktakeNotification> notificationList = Service.QueryNotiByPage(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd, pagerNotiList.PageSize, pagerNotiList.CurrentPage, out pageCount, out itemCount);
        gvNotification.DataSource = notificationList;
        gvNotification.DataBind();
        pagerNotiList.TotalRecord = itemCount;
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "query":
                pagerNotiList.CurrentPage = 1;
                Query();
                break;
            default:
                break;
        }
    }
    protected void gvResult_PreRender(object sender, EventArgs e)
    {

    }
}
