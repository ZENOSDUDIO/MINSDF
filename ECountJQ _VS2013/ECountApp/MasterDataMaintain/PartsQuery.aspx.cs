using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.MobileControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Xml.Xsl;
using System.Data;
using System.Threading;
using ECount.ExcelTransfer;
using System.Text;

public partial class MasterDataMaintain_PartsQuery : ECountBasePage
{
    public List<int> SelectedParts
    {
        get
        {
            if (Session["PartsQuery_SelectedParts"] == null)
            {
                Session["PartsQuery_SelectedParts"] = new List<int>();
            }
            return Session["PartsQuery_SelectedParts"] as List<int>;
        }
        set
        {
            Session["PartsQuery_SelectedParts"] = value;
        }
    }
    public Part Filter
    {
        get
        {
            if (Session["PartsQuery_Filter"] == null)
            {
                Session["PartsQuery_Filter"] = new Part();
            }
            return Session["PartsQuery_Filter"] as Part;
        }
        set
        {
            Session["PartsQuery_Filter"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "零件管理";
        //this.AspPager1.PageSizeChange += new BizDataMaintain_AspPager.PageSizeChangeEventHandler(AspPager1_PageSizeChange);
        this.AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);
        if (!IsPostBack)
        {
            SelectedParts = null;
            bindDDLControl();
        }
    }

    void AspPager1_PageNumberSelect(object sender, EventArgs e)
    {
        RefreshSelectedParts();
        bindGridView();
    }

    void AspPager1_PageSizeChange(object sender, EventArgs e)
    {
        RefreshSelectedParts();
        bindGridView();
    }

    private void bindDDLControl()
    {
        //BindDropDownList(this.ddlPlantID, DropDownType.Plant);

        //ddlPlantID_SelectedIndexChanged(null, null);
        BindDropDownList(this.ddlCategoryID, DropDownType.PartCategory);
        BindDropDownList(this.ddlPartStatus, DropDownType.PartStatus);
        //BindDropDownList(this.ddlCycleCountLevel, DropDownType.CycleCountLevel);
        BindPlants(this.ddlPlantID);
        BindCycleCountLevel(this.ddlCycleCountLevel);
    }

    private void bindGridView()
    {
        Part part = getPartFilter();

        int pageCount;
        int itemCount;
        List<SGM.ECount.DataModel.ViewPart> parts = Service.QueryPartByPage(part, this.AspPager1.PageSize, this.AspPager1.SelectPageNumber, out pageCount, out itemCount);
        Filter = part;
        this.AspPager1.TotalPage = pageCount;
        this.AspPager1.TotalRecord = itemCount;
        this.gvParts.DataSource = parts;
        this.gvParts.DataBind();
    }

    private Part getPartFilter()
    {
        Part part = new Part();

        if (!string.IsNullOrEmpty(CurrentUser.UserInfo.DUNS))
        {
            part.Supplier = new Supplier { DUNS = CurrentUser.UserInfo.DUNS };
        }
        if (!string.IsNullOrEmpty(this.txtPartCode.Text))
        {
            part.PartCode = this.txtPartCode.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtPartChineseName.Text))
        {
            part.PartChineseName = this.txtPartChineseName.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtUpdateBy.Text))
        {
            //part.UpdateBy = new User { UserName = this.txtUpdateBy.Text.Trim() };
            part.UpdateBy = Service.GetUserbyName(this.txtUpdateBy.Text.Trim());
        }
        if (!string.IsNullOrEmpty(this.txtSpecs.Text))
        {
            part.Specs = this.txtSpecs.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtWorkLocation.Text))
        {
            part.WorkLocation = this.txtWorkLocation.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtFollowUp.Text))
        {
            part.FollowUp = this.txtFollowUp.Text.Trim();
        }

        if (!string.IsNullOrEmpty(this.ddlPlantID.SelectedValue))
        {
            part.Plant = new Plant();
            part.Plant.PlantID = int.Parse(this.ddlPlantID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.ddlWorkshopID.SelectedValue))
        {
            part.Workshops = this.ddlWorkshopID.SelectedItem.ToString();
        }

        if (!string.IsNullOrEmpty(this.ddlSegmentID.SelectedValue))
        {
            part.Segments = this.ddlSegmentID.SelectedItem.Text.ToString();
        }

        part.PartStatus = new PartStatus();
        if (this.ddlPartStatus.SelectedValue.Length > 0)
        {
            part.PartStatus.StatusID = int.Parse(this.ddlPartStatus.SelectedValue);
        }
        else
        {
            part.PartStatus = null;
        }
        part.Supplier = new Supplier();
        if (!string.IsNullOrEmpty(this.txtDUNS.Text))
        {
            part.Supplier.DUNS = this.txtDUNS.Text.Trim();
        }
        else
        {
            part.Supplier = null;
        }

        part.PartCategory = new PartCategory();
        if (!string.IsNullOrEmpty(this.ddlCategoryID.SelectedValue))
        {
            part.PartCategory.CategoryID = int.Parse(this.ddlCategoryID.SelectedValue.Trim());
        }
        else
        {
            part.PartCategory = null;
        }

        part.CycleCountLevel = new CycleCountLevel();
        if (this.ddlCycleCountLevel.SelectedValue.ToString() != "--")
        {
            part.CycleCountLevel.LevelID = int.Parse(this.ddlCycleCountLevel.SelectedValue.Trim());
        }
        else
        {
            part.CycleCountLevel = null;
        }

        return part;
    }

    //查询零件
    protected void Query()
    {
        bindGridView();
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "add":
                Response.Redirect("PartEdit.aspx");
                break;
            case "query":
                AspPager1.CurrentPage = 1;
                SelectedParts = null;
                Query();
                break;
            //case "export":
            //    RefreshSelectedParts();
            //    ExportItems();
            //    break;
            case "exportAll":
                //AspPager1.CurrentPage = 1;
                //SelectedParts = null;
                //Query();
                if (gvParts.Rows.Count > 0 && gvParts.Rows[0].Visible)
                {
                    ExportAllItems();
                }
                break;
            
            case "delete":
                DeleteParts();
                break;
            case "import":
                string url;
                url = string.Format("PartImport.aspx");
                Response.Redirect(url);
                break;
            case "batchUpdate":
                Response.Redirect("PartImport.aspx?BatchUpdate=true");
                break;
            default:
                break;
        }
    }

   public void DeleteParts()
    {
        List<Part> checkedList = new List<Part>();
        CheckBox chk;

        for (int i = 0; i < gvParts.Rows.Count; i++)
        {
            chk = gvParts.Rows[i].Cells[0].FindControl("ChkSelected") as CheckBox;

            if (chk.Checked)
            {
                Part partinfo = new Part();
                partinfo.PartID = (int)gvParts.DataKeys[i].Value;
                checkedList.Add(partinfo);
            }
        }

        foreach (var item in checkedList)
        {
            Service.DeletePart(item);
        }
        bindGridView();
    }

    private void ExportAllItems()
    {
        Part part = Filter;// getPartFilter();
        string errorMessage;
        byte[] buffer = Service.ExportParts(part, out errorMessage);
        if (string.IsNullOrEmpty(errorMessage) && buffer.Length > 0)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=part.csv");
            Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            Response.Charset = "gb2312";


            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.Flush();

            Response.End();
        }
  
    }

    private void ExportParts(byte[] content)
    {
        //byte[] buffer = Service.ExportParts(part, out errorMessage);
        if (content.Length > 0)
        {
            //string tmpDir = Server.MapPath(@"~/ExportSchema/");
            //string filePath = tmpDir + Path.GetRandomFileName().TrimEnd('.') + ".csv";
            //using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            //{
            //    fs.Write(buffer, 0, buffer.Length);
            //    fs.Close();
            //}
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=part.csv");
            Response.ContentEncoding = Encoding.GetEncoding("utf-8");

            //Response.WriteFile(filePath);
            Response.OutputStream.Write(content, 0, content.Length);
            Response.Flush();
            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}
            Response.End();
        }
        //Part part = getPartFilter();
        //int pageCount;
        //int itemCount;
        //List<ViewPart> parts = Service.QueryPartByPage(part, 100, 1, out pageCount, out itemCount);

        ////10 threads
        //int loopCount = (pageCount - 1) / 10 + 1;
        //int completedCount = 0;
        //for (int i = 0; i < 10; i++)
        //{
        //    ThreadPool.QueueUserWorkItem(new WaitCallback(
        //    delegate(object state)
        //    {
        //        int startIndex = Convert.ToInt32(state);
        //        int endIndex = startIndex + loopCount;
        //        endIndex = (endIndex > pageCount) ? pageCount : endIndex;
        //        for (int j = startIndex + 1; j <= endIndex; j++)
        //        {
        //            int tmpPageCount;
        //            int tmpItemCount;
        //            List<ViewPart> tmpParts = Service.QueryPartByPage(part, 100, j, out tmpPageCount, out tmpItemCount);
        //            lock (this)
        //            {
        //                parts.AddRange(tmpParts);
        //            }
        //        }
        //        lock (this)
        //        {
        //            completedCount++;
        //        }
        //    }), i * loopCount);
        //}

        //while (completedCount < 10)
        //{
        //    Thread.Sleep(2000);
        //}
        //if (parts != null && parts.Count > 0)
        //{
        //    ExportParts(parts);
        //}
    }


    //export selected item
    //private void ExportItems()
    //{
    //    if (SelectedParts.Count > 0)
    //    {

    //        List<string> list = (from p in SelectedParts select p.ToString()).ToList();// new List<string>();
    //        //for (int i = 0; i <= this.gvParts.Rows.Count - 1; i++)    
    //        //{
    //        //    GridViewRow row = gvParts.Rows[i];
    //        //    bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
    //        //    if (isChecked)
    //        //    {
    //        //        string partid = gvParts.DataKeys[row.RowIndex]["PartID"].ToString();
    //        //        list.Add(partid);
    //        //    }
    //        //}
    //        string msg;
    //        byte[] buffer = Service.ExportSelectedParts(list, out msg);
    //        if (string.IsNullOrEmpty(msg))
    //        {
    //            ExportParts(buffer);
    //        }
    //        //List<ViewPart> parts = Service.GetViewPartsByPartIDs(list);

    //        //ExportParts(parts);
    //    }
    //}

    private void ExportParts(List<ViewPart> parts)
    {
        //Dictionary<string, string> columns = ExcelUtil.GetExportListColumnInfo("ViewPart");
        //Type t = typeof(ViewPart);
        //System.Reflection.PropertyInfo[] pis = t.GetProperties();
        //List<string> listColumns = new List<string>();
        //List<string> listProperties = new List<string>();
        //foreach (PropertyInfo pi in pis)
        //{
        //    if (columns.Keys.Contains<string>(pi.Name))
        //    {
        //        listColumns.Add(columns[pi.Name]);
        //        listProperties.Add(pi.Name);
        //    }
        //}
        if (parts.Count > 0)
        {
            //string xlsFilePath = Server.MapPath("~/App_Themes/");
            //ExcelUtil.ExportExcel(xlsFilePath, listColumns, listProperties, ExcelUtil.ListToDataTable<ViewPart>(parts));
            string fileName;
            string message;
            string schemaFile = Server.MapPath(@"~/ExportSchema/Part.xml");
            string tmpDir = Server.MapPath(@"~/ExportSchema/");

            if (ExcelHelper.TransferDataTableToCSVFile(ExcelUtil.ListToDataTable<ViewPart>(parts), tmpDir, schemaFile, out fileName, out message))
            {
                fileName = tmpDir + fileName;

                Response.Clear();
                Response.Buffer = true;
                //Response.ContentType = "application/vnd.ms-excel";
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";filetype=excel");
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=part.csv");
                Response.ContentEncoding = Encoding.GetEncoding("utf-8");

                Response.WriteFile(fileName);
                Response.Flush();

                if (System.IO.File.Exists(fileName))
                    System.IO.File.Delete(fileName);
                Response.End();
            }
        }
    }

    //导向编辑页面
    protected void lnkEditPart_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        //string opName = linkButton.CommandName;
        string url;
        url = string.Format("PartEdit.aspx?partid={0}", linkButton.CommandArgument);
        Response.Redirect(url);
        //PageOperator.OpenWindow(this, url, "800", "1200");
    }

    //选择工厂
    protected void ddlPlantID_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (this.ddlPlantID.SelectedValue.Length > 0)
        //{
        //    BindDropDownList(this.ddlWorkshopID, DropDownType.Workshop, this.ddlPlantID.SelectedValue);
        //}
        //else
        //{
        //    ddlWorkshopID.Items.Clear();
        //}
        //ddlWorkshopID_SelectedIndexChanged(null, null);

        if (this.ddlPlantID.SelectedItem.ToString() != "--")
        {
            ddlWorkshopID.Items.Clear();
            ddlWorkshopID.Items.Insert(0, "--");
            ddlSegmentID.Items.Clear();
            ddlSegmentID.Items.Insert(0, "--");
            Plant plant = new Plant();
            plant.PlantID = int.Parse(this.ddlPlantID.SelectedValue);
            BindWorkshops(this.ddlWorkshopID, plant);
        }
        else
        {
            ddlWorkshopID.Items.Clear();
            ddlWorkshopID.Items.Insert(0, "--");
            ddlSegmentID.Items.Clear();
            ddlSegmentID.Items.Insert(0, "--");
        }

    }

    //选择车间
    protected void ddlWorkshopID_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (this.ddlWorkshopID.SelectedValue.Length > 0)
        //{
        //    ddlSegmentID.Items.Clear();
        //    BindDropDownList(this.ddlSegmentID, DropDownType.Segment, this.ddlWorkshopID.SelectedValue);
        //}
        //else
        //{
        //    ddlSegmentID.Items.Clear();
        //}

        if (this.ddlWorkshopID.SelectedItem.ToString() != "--")
        {
            ddlSegmentID.Items.Clear();
            ddlSegmentID.Items.Insert(0, "--");
            Workshop workshop = new Workshop();
            workshop.WorkshopID = int.Parse(this.ddlWorkshopID.SelectedValue);
            BindSegments(ddlSegmentID, workshop);
        }
        else
        {
            ddlSegmentID.Items.Clear();
            ddlSegmentID.Items.Insert(0, "--");
        }
    }


    protected void gvParts_PreRender(object sender, EventArgs e)
    {
        List<SGM.ECount.DataModel.ViewPart> parts = new List<ViewPart> { new ViewPart() };
        this.BindEmptyGridView(this.gvParts, parts);
    }
    protected void RefreshSelectedParts()
    {
        for (int i = 0; i < gvParts.Rows.Count; i++)
        {
            CheckBox ChkSelected = gvParts.Rows[i].FindControl("ChkSelected") as CheckBox;
            int partID = (int)gvParts.DataKeys[i].Value;
            if (ChkSelected.Checked)
            {
                if (!SelectedParts.Contains(partID))
                {
                    SelectedParts.Add(partID);
                }
            }
            else
            {
                if (SelectedParts.Contains(partID))
                {
                    SelectedParts.Remove(partID);
                }
            }
        }
    }
    protected void gvParts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType== DataControlRowType.DataRow)
        {
            CheckBox ChkSelected = e.Row.FindControl("ChkSelected") as CheckBox;
            int partID = (int)gvParts.DataKeys[e.Row.RowIndex].Value;
            if (SelectedParts.Contains(partID))
            {
                ChkSelected.Checked = true;
            }
        }
    }
}
