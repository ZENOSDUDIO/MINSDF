﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class SystemManagement_PartStatusList :ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            bindGridView();
        }
    }

    protected void btnTemp_Click(object sender, EventArgs e)
    {
        this.bindGridView();
    }

    private void bindGridView()
    {
        PartStatus filter = new PartStatus();

        List<PartStatus> objs = Service.QueryPartStatuss(filter);
        this.GridView1.DataSource = objs;
        this.GridView1.DataBind();
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "add":
                Response.Redirect("PartStatusEdit.aspx");
                break;
            case "delete":
                butDelete_Click(null, null);
                break;
            default:
                break;
        }
    }

    protected void butAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("PartStatusEdit.aspx"));
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        //string opName = linkButton.CommandName;
        string url;
        url = string.Format("PartStatusEdit.aspx?statusid={0}", linkButton.CommandArgument);
        Response.Redirect(url);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "javascript:return showdata('" + GridView1.DataKeys[e.Row.RowIndex]["StatusID"].ToString() + "');");
        }
    }

    protected void butDelete_Click(object sender, EventArgs e)
    {
        List<string> ids = new List<string>();
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                string sid = GridView1.DataKeys[row.RowIndex]["StatusID"].ToString();
                //PartStatus obj = new PartStatus();
                //obj.StatusID = int.Parse(sid);
                //Service.DeletePartStatus(obj);
                ids.Add(sid);
            }
        }
        if (ids.Count > 0)
        {
            Service.DeletePartStatuss(ids);
            bindGridView();
        }
        else
        {
            RegisterStartupScript("Message", "<script>alert('请标记删除.');</script>");
        }

    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<PartStatus> objs = new List<PartStatus> { new PartStatus() };
        this.BindEmptyGridView(this.GridView1, objs);
    }
}