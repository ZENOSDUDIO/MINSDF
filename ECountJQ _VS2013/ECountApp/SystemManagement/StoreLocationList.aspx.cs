using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using System.Text;

public partial class SystemManagement_StoreLocationList : ECountBasePage
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "javascript:return showdata('" + GridView1.DataKeys[e.Row.RowIndex]["LocationID"].ToString() + "');");
        }
    }

    private void bindGridView()
    {
        StoreLocation filter = new StoreLocation();

        List<StoreLocation> objs = Service.QueryStoreLocations(filter);
        this.GridView1.DataSource = objs;
        this.GridView1.DataBind();
    }

    protected void QueryStorelocation()
    {
        if (string.IsNullOrEmpty(this.txtStorelocation.Text))
        {
            bindGridView();
        }
        else
        {
            StoreLocation sLocation = new StoreLocation();
            sLocation.LocationName = this.txtStorelocation.Text.ToString();

            List<StoreLocation> list = new List<StoreLocation>();
            list = Service.QueryStoreLocations(sLocation);
            GridView1.DataSource = list;
            GridView1.DataBind();
        }

    }

    private void ExportStoreLocations()
    {
        string errorMessage;
        StoreLocation storeloc = new StoreLocation();

        if (!string.IsNullOrEmpty(txtStorelocation.Text.Trim()))
        {
            storeloc.LocationName = txtStorelocation.Text.ToString();
        }

        byte[] buffer = Service.ExportStoreLocations(storeloc, out errorMessage);
        if (string.IsNullOrEmpty(errorMessage) && buffer.Length > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=storelocations.csv");
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
                Response.Redirect("StoreLocationEdit.aspx");
                break;
            case "delete":
                butDelete_Click(null, null);
                break;
            case "search":
                QueryStorelocation();
                break;
            case "export":
                 ExportStoreLocations();
                break;
            case "import":
                string url;
                url = string.Format("StoreLocationImport.aspx");
                Response.Redirect(url);
                break;

            default:
                break;
        }
    }

    protected void butAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("StoreLocationEdit.aspx"));
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        string url;
        url = string.Format("StoreLocationEdit.aspx?locationid={0}", linkButton.CommandArgument);
        Response.Redirect(url);
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
                string sid = GridView1.DataKeys[row.RowIndex]["LocationID"].ToString();
                ids.Add(sid);
            }
        }
        if (ids.Count > 0)
        {
            Service.DeleteStoreLocations(ids);
            bindGridView();           
        }

    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<StoreLocation> objs = new List<StoreLocation> { new StoreLocation() };
        this.BindEmptyGridView(this.GridView1, objs);
    }
}
