using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;


public partial class BizDataMaintain_PartEdit : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "零件编辑";
        if (!IsPostBack)
        {
            if (Request.QueryString["partid"] != null)
            {
                Part model = Service.GetPartByKey(Request.QueryString["partid"]);
                this.PartDetails1.PartInfo = model;
            }
            //else
            //{
            //    this.PartDetails1.
            //}
        }
    }
}
