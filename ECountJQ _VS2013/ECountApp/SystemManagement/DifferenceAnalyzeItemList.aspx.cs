using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class SystemManagement_DifferenceAnalyzeItemList : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridview();
        }
    }

    public void BindGridview()
    {
        List<DifferenceAnalyseDetails> list = new List<DifferenceAnalyseDetails>();
        list = Service.GetDifferenceAnalyseDetails();

        
        gvDifferenceAnalyseDetail.DataSource = list;
        gvDifferenceAnalyseDetail.DataBind();
    }

    protected void DeleteDiffAnalyseDetails()
    {
        List<DifferenceAnalyseDetails> checkedlist = new List<DifferenceAnalyseDetails>();
        CheckBox chk= new CheckBox();

        for (int i = 0; i < gvDifferenceAnalyseDetail.Rows.Count; i++)
        {
            chk = gvDifferenceAnalyseDetail.Rows[i].Cells[0].FindControl("IsCheck") as CheckBox;

            if (chk.Checked)
            {
                DifferenceAnalyseDetails detail = new DifferenceAnalyseDetails();
                detail.DetailsID = int.Parse(gvDifferenceAnalyseDetail.DataKeys[i].Value.ToString());
                checkedlist.Add(detail);
            }
        }

        foreach (var item in checkedlist)
        {
            Service.DeleteDiffAnalyseDetail(item);
        }
        BindGridview();
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "delete":
                DeleteDiffAnalyseDetails();
                break;
            default:
                break;
        }
    }

    protected void gvDifferenceAnalyseDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex>=0)
        {
            LinkButton btnModify = e.Row.Cells[2].FindControl("LinkButton1") as LinkButton;
            DifferenceAnalyseDetails diffAnalyseDetails = e.Row.DataItem as DifferenceAnalyseDetails;
            string script = string.Format("showDialog('DifferenceAnalvzeItemDetails.aspx?Mode=Edit&DetailsID={0}',600,300);return false;", diffAnalyseDetails.DetailsID);
            btnModify.OnClientClick = script;
        }
    }

    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chCheckALL = (CheckBox)sender;

        if (chCheckALL.Checked)
        {
            foreach (GridViewRow dr in gvDifferenceAnalyseDetail.Rows)
            {
                (dr.FindControl("IsCheck") as CheckBox).Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in gvDifferenceAnalyseDetail.Rows)
            {
                (dr.FindControl("IsCheck") as CheckBox).Checked = false;
            }
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = gvDifferenceAnalyseDetail.HeaderRow.FindControl("ChkAll") as CheckBox;
        int iCount = 0;

        foreach (GridViewRow dr in gvDifferenceAnalyseDetail.Rows)
        {
            CheckBox cb = new CheckBox();
            cb = dr.FindControl("IsCheck") as CheckBox;

            if (!cb.Checked)
            {
                break;
            }
            else
            {
                iCount++;
            }
        }

        if (gvDifferenceAnalyseDetail.Rows.Count == iCount)
        {
            chkAll.Checked = true;
        }
        else
        {
            chkAll.Checked = false;
        }
    }
    protected void btnTemp_Click(object sender, EventArgs e)
    {
        BindGridview();
    }
}
