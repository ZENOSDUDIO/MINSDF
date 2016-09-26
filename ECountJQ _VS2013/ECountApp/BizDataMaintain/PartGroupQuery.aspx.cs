using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Text;

public partial class BizDataMaintain_PartGroupQuery : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "零件组管理";
        if (!IsPostBack)
        {
            bindGridView();
        }
    }


    private void bindGridView()
    {
        GridView1.DataKeyNames = new string[] { "GroupID","GroupName" };
        GridView1.PageSize = 10;
        PartGroup info = new PartGroup();
        if (!string.IsNullOrEmpty(this.txtGroupName.Text.Trim()))
        {
            info.GroupName = this.txtGroupName.Text.Trim();
        }
        List<PartGroup> pgs = Service.QueryPartGroups(info);
        GridView1.DataSource = pgs;
        GridView1.DataBind();//将控件及其所有子控件绑定到指定的数据源
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    protected void ExportPartGroup()
    {
        string errorMessage;
        PartGroup group = new PartGroup();

        if (!string.IsNullOrEmpty(txtGroupName.Text.Trim()))
        {
            group.GroupName = txtGroupName.Text.ToString();
        }

        byte[] buffer = Service.ExportPartGroup(group, out errorMessage);
        if (string.IsNullOrEmpty(errorMessage) && buffer.Length > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=partgroups.csv");
            Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.Flush();
            Response.End();
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "add":
                Response.Redirect("PartGroupEdit.aspx");
                break;
            case "delete":
                lnkDelete_Click(null, null);
                break;
            case "query":
                butQuery_Click(null, null);
                break;
            case "export":
                ExportPartGroup();
                break;
            default:
                break;
        }
    }

    protected void butQuery_Click(object sender, EventArgs e)
    {
        bindGridView();
    }

    /// <summary>
    /// 新增分组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect( string.Format("PartGroupEdit.aspx"));
    }

    /// <summary>
    /// 分组编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void lnkEdit_Click(object sender, EventArgs e)
    //{
    //    if (checkSelectedRow())
    //    {
    //        GridViewBind();
    //        return;
    //    }       

    //    for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
    //    {
    //        GridViewRow row = GridView1.Rows[i];
    //        bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
    //        if (isChecked)
    //        {
    //            string groupID = GridView1.DataKeys[row.RowIndex]["GroupID"].ToString();
    //            Response.Redirect(string.Format("PartGroupEdit.aspx?GroupID={0}", groupID));
    //        }
    //    }
    //    bindGridView();
    //}

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        List<int> list = new List<int>();
        string message = string.Empty;
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                int groupID = int.Parse(GridView1.DataKeys[row.RowIndex]["GroupID"].ToString());
                string groupName = GridView1.DataKeys[row.RowIndex]["GroupName"].ToString();
                //Part part = new Part();
                //if (part.PartGroup == null)
                //    part.PartGroup = new PartGroup();
                //part.PartGroup.GroupID = groupID;
                ////先判断该分组是否含有零件
                ////如果有 则提示不能删除
                //if (Service.QueryParts(part).Count > 0)
                //{
                //    message += groupName + ";";
                //    continue;
                //}
                //else
                //{
                    list.Add(groupID);
                //}
            }
        }
        foreach (int id in list)
        {
            Service.DeletePartGroup(id);
        }
        //Response.Write("<script>alert('" + message + "分组有零件不能删除.')</script>");
        //ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "DelAlert", "alert('" + message + "分组有零件不能删除.')", true);
        bindGridView();
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<SGM.ECount.DataModel.PartGroup> pgs = new List<PartGroup> { new PartGroup() };
        this.BindEmptyGridView(this.GridView1, pgs);
    }

    private bool checkSelectedRow()
    {
        int selectedRowCount = 0;
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                selectedRowCount = selectedRowCount + 1;
            }
        }
        if (selectedRowCount == 0)
        {
            Response.Write("<script>alert('请选择一个分组.')</script>");
            return true;
        }
        if (selectedRowCount > 1)
        {
            Response.Write("<script>alert('只能选择一个分组.')</script>");
            return true;
        }
        return false;
    }

}
