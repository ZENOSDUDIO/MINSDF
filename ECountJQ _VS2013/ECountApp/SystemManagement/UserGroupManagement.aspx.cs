using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SGM.Common.Cache;
using SGM.Common.Utility;

public partial class SystemManagement_UserGroupManagement : ECountBasePage
{
    private const string SessionOperations = "SessionOperations";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindDropDownList(this.ddlPlantID, DropDownType.Plant);
            //BindDropDownList(this.ddlStoreLocationID, DropDownType.StoreLocation);
            BindStoreLocationType(ddlStoreLocationType, true);
            if (Request.QueryString["groupid"] != null)
            {
                bindBaseData(Request.QueryString["groupid"].ToString());
            }
            else
            {
                Session.Remove(SessionOperations);
                bindGridView();
            }
        }
    }

    private void bindGridView()
    {
        List<Operation> objs = Service.GetOperations();
        this.GridView1.DataSource = objs;
        this.GridView1.DataBind();
    }

    private void bindGridView(GridView gView, Operation operation)
    {
        List<Operation> objs = Service.GetOperationsByOperation(operation);
        gView.DataSource = objs;
        gView.DataBind();
    }

    private void bindBaseData(string groupID)
    {
        UserGroup model = Service.GetUserGroupByKey(new UserGroup { GroupID = int.Parse(groupID) });
        #region binding user group value
        this.hidGroupID.Value = model.GroupID.ToString();
        this.txtGroupName.Text = model.GroupName;
        this.chkShowAllLocation.Checked = (bool)model.ShowAllLocation;
        this.chkSysAdmin.Checked = Convert.ToBoolean(model.SysAdmin);
        this.chkAnalyzeAll.Checked = Convert.ToBoolean(model.AnalyzeAll);
        this.chkFillinAllLocation.Checked = Convert.ToBoolean(model.FillinAllLocation);

        ddlStoreLocationType.SelectedValue = model.StoreLocationType == null ? string.Empty : model.StoreLocationType.TypeID.ToString();
        //ddlStoreLocationID.SelectedValue = model.StoreLocation == null ? "" : model.StoreLocation.LocationID.ToString();

        this.txtMaxDynamicStocktake.Text = model.MaxDynamicStocktake == null ? "" : model.MaxDynamicStocktake.Value.ToString();
        this.txtMaxStaticStocktake.Text = model.MaxStaticStocktake == null ? "" : model.MaxStaticStocktake.ToString();
        this.txtCurrentDynamicStocktake.Text = model.CurrentDynamicStocktake == null ? "" : model.CurrentDynamicStocktake.ToString();
        this.txtCurrentStaticStocktake.Text = model.CurrentStaticStocktake == null ? "" : model.CurrentStaticStocktake.ToString();
        #endregion
        Session.Remove(SessionOperations);
        Session[SessionOperations] = model.Operations;
        bindGridView();
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                btnSave_Click(null, null);
                CurrentUser.RefreshUserProfile();
                CacheHelper.RemoveCache(Consts.CACHE_KEY_USER_GROUPS);
                break;
            case "return":
                Session.Remove(SessionOperations);
                Response.Write("<script>window.location.href='UserGroupList.aspx';</script>");
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserGroup userGroup = new UserGroup();
        #region Set the user group values
        userGroup.GroupName = this.txtGroupName.Text.Trim();
        userGroup.ShowAllLocation = this.chkShowAllLocation.Checked;
        //model.DUNS = this.txtSupplier.Text.Trim();
        userGroup.SysAdmin = this.chkSysAdmin.Checked;
        userGroup.AnalyzeAll = this.chkAnalyzeAll.Checked;
        userGroup.FillinAllLocation = this.chkFillinAllLocation.Checked;
        int iTake;
        if (int.TryParse(this.txtMaxDynamicStocktake.Text.Trim(), out iTake))
            userGroup.MaxDynamicStocktake = iTake;
        if (int.TryParse(this.txtMaxStaticStocktake.Text.Trim(), out iTake))
            userGroup.MaxStaticStocktake = iTake;
        if (int.TryParse(this.txtCurrentDynamicStocktake.Text.Trim(), out iTake))
            userGroup.CurrentDynamicStocktake = iTake;
        if (int.TryParse(this.txtCurrentStaticStocktake.Text.Trim(), out iTake))
            userGroup.CurrentStaticStocktake = iTake;
        try
        {
            StoreLocationType slType = new StoreLocationType
            {
                TypeID=Convert.ToInt32(ddlStoreLocationType.SelectedValue),
                TypeName=ddlStoreLocationType.SelectedItem.Text
            };
            userGroup.StoreLocationType = slType;
            //StoreLocation stloc = new StoreLocation();
            //stloc.LocationID = Convert.ToInt32(ddlStoreLocationID.SelectedValue);
            //stloc.LocationName = ddlStoreLocationID.SelectedItem.ToString();
            //model.StoreLocation = stloc;
        }
        catch
        {
            //userGroup.StoreLocation = null;
            userGroup.StoreLocationType = null;
        }
        
        #endregion

        #region Get Operations
        foreach (var oper in userGroup.Operations)
        {
            userGroup.Operations.Remove(oper);
        }

        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chkOp = (CheckBox)row.FindControl("chkSelected");
            object oid = ((GridView)row.Parent.Parent).DataKeys[row.RowIndex].Value;
            Operation op = new Operation();
            op.OperationID = Convert.ToInt32(oid);
            if (chkOp.Checked)
                userGroup.Operations.Add(op);

            GridView gvToolbar = (GridView)row.FindControl("gvOperation");
            if (gvToolbar != null)
            {
                foreach (GridViewRow gvRow in gvToolbar.Rows)
                {
                    Operation oper = new Operation();
                    oper.OperationID = Convert.ToInt32(gvToolbar.DataKeys[gvRow.RowIndex].Value);
                    CheckBox chk = (CheckBox)gvRow.FindControl("chkSelected");
                    if (chk.Checked)
                        userGroup.Operations.Add(oper);
                }
            }
        }
        #region two gridview implementation

        #endregion

        Session.Remove(SessionOperations);
        Session[SessionOperations] = userGroup.Operations;

        #endregion
        if (this.hidGroupID.Value.Length > 0)
        {
            userGroup.GroupID = int.Parse(this.hidGroupID.Value);
            Service.UpdateUserGroup(userGroup);
        }
        else
        {
            UserGroup temp = new UserGroup();
            temp.GroupName = userGroup.GroupName;
            if (Service.ExistUserGroup(temp))
            {
                RegisterStartupScript("Message", "<script>alert('该用户组名称已存在');</script>");
                return;
            }
            else
            {
                userGroup.CreateDate = DateTime.Now;
                userGroup = Service.AddUserGroup(userGroup);
                // this.hidGroupID.Value = model.GroupID.ToString();
                Response.Write("<script>window.location.href='UserGroupList.aspx';</script>");
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }

    //protected void ddlPlantID_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (!string.IsNullOrEmpty(this.ddlPlantID.SelectedValue))
    //    {
    //        BindDropDownList(this.ddlWorkshopID, DropDownType.Workshop, this.ddlPlantID.SelectedValue);
    //        #region storelocation
    //        //StoreLocation info = new StoreLocation();
    //        //info.Plant = new Plant();
    //        //info.Plant.PlantID = int.Parse(this.ddlPlantID.SelectedValue);
    //        //List<StoreLocation> sls = Service.QueryStoreLocations(info);
    //        //this.ddlStoreLocationID.DataSource = sls;
    //        //ddlStoreLocationID.DataTextField = "LocationName";
    //        //ddlStoreLocationID.DataValueField = "LocationID";
    //        //this.ddlStoreLocationID.DataBind();
    //        //ddlStoreLocationID.Items.Insert(0, new ListItem("请选择", ""));
    //        #endregion
    //    }
    //}

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        string argu = linkButton.CommandArgument;
        int opID;
        GridViewRow row = (GridViewRow)linkButton.NamingContainer;
        GridView gView = ((GridView)row.Parent.Parent);
        if (int.TryParse(argu, out opID))
        {
            Operation op = new Operation();
            op.OperationID = opID;
            bindGridView(gView, op);
        }
    }

    protected void chkSelected_CheckedChanged(Object sender, EventArgs e)
    {
        #region two gridviews implementation
        //List<Operation> ops = (List<Operation>)Session[SessionOperations];
        //System.Data.Objects.DataClasses.EntityCollection<Operation> ops = (System.Data.Objects.DataClasses.EntityCollection<Operation>)Session[SessionOperations];
        //if (ops == null)
        //    ops = new System.Data.Objects.DataClasses.EntityCollection<Operation>();
        #endregion

        CheckBox chkOp = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkOp.NamingContainer;
        GridView gvToolbar = (GridView)row.FindControl("gvOperation");
        if (gvToolbar != null)
        {
            foreach (GridViewRow gvRow in gvToolbar.Rows)
            {
                CheckBox chk = (CheckBox)gvRow.FindControl("chkSelected");
                if (chk != null)
                    chk.Checked = chkOp.Checked;
            }
        }

        #region Two GridView Implementation
        //object oid = ((GridView)row.Parent.Parent).DataKeys[row.RowIndex].Value;//row.Cells[1].Text;
        //int opId ;
        //bool bResult;

        //Operation op = new Operation();
        //if (int.TryParse(oid.ToString(), out opId))
        //{
        //    op.OperationID = opId;
        //}
        //op = Service.GetOperationByKey(op);  
        //if (chkOp.Checked)
        //{
        //    if (!ops.ToList().Exists(o => o.OperationID == op.OperationID))
        //        ops.Add(op);
        //}
        //else
        //{
        //    if (ops.ToList().Exists(o => o.OperationID == op.OperationID))
        //        bResult = ops.Remove(ops.FirstOrDefault(o => o.OperationID == op.OperationID));
        //}
        //// Toolbars     
        //List<Operation> subOps = Service.GetOperationsByOperation(op);
        //foreach (Operation subOp in subOps)
        //{
        //    if (chkOp.Checked)
        //    {
        //        if (!ops.ToList().Exists(o => o.OperationID == subOp.OperationID))
        //            ops.Add(subOp);
        //    }
        //    else
        //    {
        //        if (ops.ToList().Exists(o => o.OperationID == subOp.OperationID))
        //            bResult = ops.Remove(ops.FirstOrDefault(o => o.OperationID == subOp.OperationID));
        //    }
        //}

        //Session[SessionOperations] = ops;

        //if (row.FindControl("gvOperation") != null)
        //    bindGridView((GridView)row.FindControl("gvOperation"), op);
        #endregion
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //List<Operation> ops = (List<Operation>)Session[SessionOperations];
            System.Data.Objects.DataClasses.EntityCollection<Operation> ops = (System.Data.Objects.DataClasses.EntityCollection<Operation>)Session[SessionOperations];
            int opId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OperationId"));
            if (ops != null)
            {
                if (ops.ToList().Exists(o => o.OperationID == opId))
                    ((CheckBox)e.Row.FindControl("chkSelected")).Checked = true;
                else
                    ((CheckBox)e.Row.FindControl("chkSelected")).Checked = false;
            }

            GridView gView = (GridView)e.Row.FindControl("gvOperation");
            if (gView != null)
            {
                Operation op = new Operation();
                op.OperationID = opId;
                bindGridView(gView, op);
            }
        }

    }
}
