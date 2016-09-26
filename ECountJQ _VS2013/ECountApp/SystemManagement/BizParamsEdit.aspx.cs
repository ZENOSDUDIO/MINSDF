using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SGM.ECount.DataModel;
using SGM.Common.Cache;
using SGM.Common.Utility;


public partial class SystemManagement_BizParamsEdit : ECountBasePage
{
    private const string TYPE_BOOL = "bool";
    private const string TYPE_INT = "int";
    private const string TYPE_TIME = "time";
    private const string TYPE_DATE = "date";
    private const string TYPE_DAYOFWEEK = "dayofweek";
   //private List<BizParams> bizParamsList;
   protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGridView();
        }
    }

   private void bindGridView()
   {
       //bizParamsList = Service.GetBizParamsList();
       var groupNames = Enumerable.Distinct(from o in BizParamsList select o.GroupName);

       this.GridView1.DataSource = groupNames;
       this.GridView1.DataBind();
   }

    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           //if(BizParamsList == null)
           //    BizParamsList = Service.GetBizParamsList();
           Label group = (Label)e.Row.FindControl("bpGroup");
           GridView gvParams = (GridView)e.Row.FindControl("gvBizParams");
           if (group != null)
           {
               gvParams.DataSource = BizParamsList.Where(o => o.GroupName == group.Text.Trim());
               gvParams.DataBind();
           }
       }
    }

    protected void BizParam_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BizParams param = (BizParams)e.Row.DataItem;
            TextBox textBox = (TextBox)e.Row.FindControl("paramValue");
            CheckBox chkBox = (CheckBox)e.Row.FindControl("checkStatus");
            DropDownList ddlList = (DropDownList)e.Row.FindControl("ddlList");

            switch (param.DataType.ToLower())
            {
                case TYPE_BOOL: //"bool":
                    chkBox.Visible = true;
                    textBox.Visible = false;
                    ddlList.Visible = false;
                    chkBox.Checked = Convert.ToBoolean(param.ParamValue);
                    break;
                case TYPE_DAYOFWEEK: //"dayofweek":
                    chkBox.Visible = false;
                    textBox.Visible = false;
                    ddlList.Visible = true;
                    BindDayofWeekList(ddlList);
                    ddlList.SelectedValue = param.ParamValue;
                    break;
                default:
                    chkBox.Visible = false;
                    textBox.Visible = true;
                    ddlList.Visible = false;
                    break;
            }

            if (param.ReadOnly != null && param.ReadOnly == true)
            {
                textBox.ReadOnly = true;
                textBox.CssClass = "readonly";
            }
        }
    }

   protected void gvBizParams_PreRender(object sender, EventArgs e)
   {
       List<BizParams> objs = new List<BizParams> { new BizParams() };
       this.BindEmptyGridView((GridView)sender, objs);
   }


    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                Save();
                break;
            case "clear":
                Service.ResetCycleCount();
                CacheHelper.RemoveCache(Consts.CACHE_KEY_BIZ_PARAMS);
                bindGridView();
                break;
            default:
                break;
        }

    }

    protected void Save()
    {

        string errorMessage = "";
        CacheHelper.RemoveCache(Consts.CACHE_KEY_BIZ_PARAMS);
        List<BizParams> updateList = new List<BizParams>();
        
        //if (bizParamsList == null)
        //    bizParamsList = Service.GetBizParamsList();

        foreach (GridViewRow row in this.GridView1.Rows)
        {
            GridView gvParams = (GridView)row.FindControl("gvBizParams");
            if (gvParams != null)
            {
                foreach (GridViewRow bpRow in gvParams.Rows)
                {
                    //save each parameter
                    string istr = gvParams.DataKeys[bpRow.DataItemIndex].Value.ToString();
                    if (!string.IsNullOrEmpty(istr))
                    {
                        int paramId = Convert.ToInt32(istr);
                        BizParams param = BizParamsList.FirstOrDefault(o => o.ParamID == paramId);
                        BizParams model = new BizParams 
                        {
                            DataType=param.DataType,
                            GroupName=param.GroupName,
                            ParamDesc=param.ParamDesc,
                            ParamID=param.ParamID,
                            ParamKey=param.ParamKey,
                            ParamValue=param.ParamValue,
                            ReadOnly=param.ReadOnly,
                            Sequence=param.Sequence
                        };
                        //TextBox tbValue = (TextBox)row.Cells[1].FindControl("paramValue");
                        try
                        {
                            TextBox textBox = (TextBox)bpRow.FindControl("paramValue");
                            CheckBox chkBox = (CheckBox)bpRow.FindControl("checkStatus");
                            DropDownList ddlList = (DropDownList)bpRow.FindControl("ddlList");
                            Label     lblError = (Label)bpRow.FindControl("lblError");
                            lblError.Text = "";
                            string value = textBox.Text;
                            if (model.DataType.ToLower() == TYPE_BOOL)//"bool")
                                value = chkBox.Checked.ToString();
                            else if (model.DataType.ToLower() == TYPE_DAYOFWEEK) //"dayofweek")
                                value = ddlList.SelectedValue;

                            if (model.ParamValue.Trim() == value.Trim() || model.ReadOnly==true)
                                continue; //没有更改，不需要更新
                            // value validation
                            if (ValidateParamValue(model, value))
                            {
                                model.ParamValue = value;
                            }
                            else
                            {
                                lblError.Text = "请输入正确格式";
                                // errorMessage += model.ParamDesc + "数据格式不正确，请按照要求输入\\n";
                                continue;
                            }
                        }
                        catch
                        {
                            continue;//获取文本失败，跳过？
                        }

                        try
                        {
                            updateList.Add(model);
                            //Service.UpdateBizParams(model);
                        }
                        catch
                        {
                            errorMessage += "更新参数" + model.ParamDesc + "失败\\n";
                        }
                    }
                }
            }
        }
        if (updateList.Count>0)
        {
            Service.UpdateBizParamsList(updateList);
        }
        CacheHelper.RemoveCache(Consts.CACHE_KEY_BIZ_PARAMS);
        ////if(!string.IsNullOrEmpty(errorMessage))
        ////    RegisterStartupScript("Message", "<script>alert('" +errorMessage + "');</script>");
    }

    private void BindDayofWeekList(DropDownList ddlList)
    {
        ddlList.Items.Add(new ListItem("星期日", "0"));
        ddlList.Items.Add(new ListItem("星期一", "1"));
        ddlList.Items.Add(new ListItem("星期二", "2"));
        ddlList.Items.Add(new ListItem("星期三", "3"));
        ddlList.Items.Add(new ListItem("星期四", "4"));
        ddlList.Items.Add(new ListItem("星期五", "5"));
        ddlList.Items.Add(new ListItem("星期六", "6"));
    }

    private bool ValidateParamValue(BizParams model, string value)
    {
        bool bResult = false;
        bool bValue;
        int iValue;
        DateTime dtValue;
        switch (model.DataType.Trim().ToLower())
        {
            case TYPE_BOOL: //"bool":
                if (bool.TryParse(value, out bValue))
                    bResult = true;
                break;
            case TYPE_INT: //"int":
                if (int.TryParse(value, out iValue))
                    bResult = true;
                break;
            case TYPE_TIME: //"time":
                if (DateTime.TryParse(value, out dtValue))
                    bResult = true;
                break;
            case TYPE_DATE: //"date":
                if (DateTime.TryParse(value, out dtValue))
                    bResult = true;
                break;
            case TYPE_DAYOFWEEK: //"dayofweek":
                if (int.TryParse(value, out iValue))
                    if (iValue > -1 && iValue < 7)
                        bResult = true;
                break;
            default:
                bResult = true;
                break;
        }

        return bResult;
    }
}

    
