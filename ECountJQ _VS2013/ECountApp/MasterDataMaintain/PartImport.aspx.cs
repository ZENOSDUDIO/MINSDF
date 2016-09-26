using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using System.Data;

public partial class MasterDataMaintain_PartImport : ECountBasePage
{
    public bool BatchUpdate
    {
        get
        {
            if (ViewState["BatchUpdate"] == null)
            {
                return false;
            }
            return (bool)ViewState["BatchUpdate"];
        }
        set { ViewState["BatchUpdate"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UCFileUpload1.OnUpload += new EventHandler(UCFileUpload1_OnUpload);

        if (!Page.IsPostBack)
        {
            UCFileUpload1.ValidationSchemaFile = Server.MapPath(@"~/ImportSchema/PartImport.xml");
            if (!string.IsNullOrEmpty(Request.QueryString["BatchUpdate"]))
            {
                BatchUpdate = bool.Parse(Request.QueryString["BatchUpdate"]);
                if (BatchUpdate)
                {
                    mvFields.ActiveViewIndex = 0;
                }
            }
        }
    }

    void UCFileUpload1_OnUpload(object sender, EventArgs e)
    {
        UploadEventArgs ue = e as UploadEventArgs;
        DataTable dtPart = ue.ContentTable;
        dtPart.Columns["序号"].ColumnName = "RowNumber";
        dtPart.Columns["零件号"].ColumnName = "PartCode";
        dtPart.Columns["零件名称"].ColumnName = "PartChineseName";
        dtPart.Columns["工位"].ColumnName = "WorkLocation";
        dtPart.Columns["库位"].ColumnName = "Dloc";
        dtPart.Columns["循环盘点级别"].ColumnName = "LevelName";
        dtPart.Columns["已循环盘点次数"].ColumnName = "CycleCountTimes";
        dtPart.Columns["车型"].ColumnName = "Specs";
        dtPart.Columns["FollowUp"].ColumnName = "Followup";
        dtPart.Columns["工厂"].ColumnName = "PlantCode";
        dtPart.Columns["车间"].ColumnName = "Workshops";
        dtPart.Columns["工段"].ColumnName = "Segments";
        dtPart.Columns["物料类别"].ColumnName = "CategoryName";
        dtPart.Columns["物料状态"].ColumnName = "StatusName";
        dtPart.Columns["DUNS"].ColumnName = "DUNS";

        dtPart.Columns.Add("UserName", typeof(string));
        dtPart.DefaultView.Sort = "PartCode";
        dtPart = dtPart.DefaultView.ToTable();
        bool hasError = false;
        List<ViewPart> partList = new List<ViewPart>();
        List<ViewPart> list = new List<ViewPart>();
        for (int i = 0; i < dtPart.Rows.Count; i++)
        {
            DataRow row = dtPart.Rows[i];
            bool rowError = false;
            ViewPart part = new ViewPart();
            if (!BatchUpdate)
            {
                Plant plant = this.Plants.SingleOrDefault(p =>string.Equals(p.PlantCode , dtPart.Rows[i]["PlantCode"].ToString(), StringComparison.OrdinalIgnoreCase) );

                CycleCountLevel cycleClevel = this.CycleCountLevels.SingleOrDefault(c => c.LevelName == dtPart.Rows[i]["LevelName"].ToString());
                PartStatus partstatus = this.PartStatus.SingleOrDefault(p => p.StatusName == dtPart.Rows[i]["StatusName"].ToString());
                Supplier supplier = this.Suppliers.SingleOrDefault(s => string.Equals(s.DUNS, dtPart.Rows[i]["DUNS"].ToString()));

                part.PartCode = dtPart.Rows[i]["PartCode"].ToString();
                if (supplier == null)
                {
                    string msg = string.Format("零件【{0}】的供应商DUNS【{1}】不存在", part.PartCode, dtPart.Rows[i]["DUNS"] + "");
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                    rowError = true;
                }
                else
                {
                    part.SupplierID = supplier.SupplierID;
                }

                if (plant == null)
                {
                    string msg = string.Format("零件【{0}】的工厂【{1}】不存在", part.PartCode, dtPart.Rows[i]["PlantCode"] + "");
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                    rowError = true;
                }
                else
                {
                    part.PlantID = plant.PlantID;
                }
                if (cycleClevel == null)
                {
                    string msg = string.Format("零件【{0}】的循环盘点级别【{1}】不存在", part.PartCode, dtPart.Rows[i]["LevelName"] + "");
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                    rowError = true;
                }
                else
                {
                    part.CycleCountLevel = cycleClevel.LevelID;
                }

                if (dtPart.Rows[i]["CategoryName"] != DBNull.Value && !string.IsNullOrEmpty(dtPart.Rows[i]["CategoryName"].ToString().Trim()))
                {
                    PartCategory partcategory = this.PartCategorys.SingleOrDefault(p =>string.Equals(p.CategoryName,dtPart.Rows[i]["CategoryName"].ToString(),StringComparison.OrdinalIgnoreCase));
                    if (partcategory == null)
                    {
                        string msg = string.Format("零件【{0}】的物料类别【{1}】不存在", part.PartCode, dtPart.Rows[i]["CategoryName"] + "");
                        UCFileUpload1.AddErrorInfo(msg);
                        hasError = true;
                        rowError = true;
                    }
                    else
                    {
                        part.CategoryID = partcategory.CategoryID;
                    }
                }
                if (partstatus == null)
                {
                    string msg = string.Format("零件【{0}】的物料状态【{1}】不存在", part.PartCode, dtPart.Rows[i]["StatusName"] + "");
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                    rowError = true;
                }
                else
                {
                    part.PartStatus = partstatus.StatusID;
                }

                part.PartChineseName = dtPart.Rows[i]["PartChineseName"].ToString();
                part.WorkLocation = dtPart.Rows[i]["WorkLocation"].ToString();
                part.Dloc = dtPart.Rows[i]["Dloc"].ToString();
                part.Specs = dtPart.Rows[i]["Specs"].ToString();
                part.FollowUp = dtPart.Rows[i]["Followup"].ToString();
                part.Workshops = dtPart.Rows[i]["Workshops"].ToString();
                part.Segments = dtPart.Rows[i]["Segments"].ToString();
                if (!string.IsNullOrEmpty(dtPart.Rows[i]["CycleCountTimes"].ToString()))
                {
                    part.CycleCountTimes = short.Parse(dtPart.Rows[i]["CycleCountTimes"].ToString());
                }

                part.UpdateBy = CurrentUser.UserInfo.UserID;
                dtPart.Rows[i]["UserName"] = CurrentUser.UserInfo.UserName;
            }
            else
            {
                string plantCode = row["PlantCode"] + "";
                string partCode = row["PartCode"] + "";
                string duns = row["DUNS"] + "";
                //ViewPart tmpPart = new ViewPart { PlantCode = plantCode, PartCode = partCode, DUNS = duns };

                if (i % 1000 == 0)
                {
                    string startCode = partCode;
                    int end = i + 999;
                    if (end >= dtPart.Rows.Count)
                    {
                        end = dtPart.Rows.Count - 1;
                    }
                    string endCode = dtPart.Rows[end]["PartCode"].ToString();
                    list = Service.QueryPartsOfScope(startCode, endCode);
                }


                //List<ViewPart> list = new List<ViewPart> { tmpPart };
                //list = Service.QueryPartsByKey(list);
                part = list.Find(p => p.PartCode == partCode && p.PlantCode == plantCode && p.DUNS == duns);
                if (part==null)//(list == null || list.Count == 0)
                {

                    string msg = string.Format("工厂为【{0}】，供应商为【{1}】的零件【{2}】不存在",  plantCode,duns, partCode);
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                    rowError = true;
                }
                else
                {
                    //part = list[0];
                    part.UpdateBy = CurrentUser.UserInfo.UserID;
                    switch (rblFields.SelectedIndex)
                    {
                        case 0:
                            part.FollowUp = row["Followup"].ToString();
                            break;
                        case 1:
                            part.Specs = row["Specs"].ToString();
                            break;
                        case 2:
                            part.Segments = row["Segments"].ToString();
                            part.WorkLocation = row["WorkLocation"].ToString();
                            if (row["CategoryName"] != DBNull.Value && !string.IsNullOrEmpty(row["CategoryName"].ToString().Trim()))
                            {
                                PartCategory partcategory = this.PartCategorys.SingleOrDefault(p => string.Equals(p.CategoryName, row["CategoryName"].ToString(), StringComparison.OrdinalIgnoreCase));
                                if (partcategory == null)
                                {
                                    string msg = string.Format("零件【{0}】的物料类别【{1}】不存在", part.PartCode, row["CategoryName"] + "");
                                    UCFileUpload1.AddErrorInfo(msg);
                                    hasError = true;
                                    rowError = true;
                                }
                                else
                                {
                                    part.CategoryID = partcategory.CategoryID;
                                }
                            }
                            else
                            {
                                part.CategoryID = null;
                            }
                            break;                        
                        default:
                            break;
                    }
                }
            }

            if (!rowError)
            {        
                partList.Add(part);
            }
        }

        if (!hasError)
        {
            Service.ImportPart(partList);
         
            BindDataControl(gvParts, dtPart);
            this.UCFileUpload1.AddSuccessInfo("上传文件成功", string.Empty, string.Empty);
        }
    }

    protected void gvParts_PreRender(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RowNumber");
        dt.Columns.Add("PartCode");
        dt.Columns.Add("PartChineseName");
        dt.Columns.Add("WorkLocation");
        dt.Columns.Add("Dloc");
        dt.Columns.Add("LevelName");
        dt.Columns.Add("Specs");
        dt.Columns.Add("Followup");
        dt.Columns.Add("PlantCode");
        dt.Columns.Add("Workshops");
        dt.Columns.Add("Segments");
        dt.Columns.Add("CategoryName");
        dt.Columns.Add("CycleCountTimes");
        dt.Columns.Add("StatusName");
        dt.Columns.Add("DUNS");
        //dt.Columns.Add("UserName");
        dt.Rows.Add(dt.NewRow());
        BindEmptyGridView(gvParts, dt);
    }


    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (e.CommandName == "return")
        {
            Response.Redirect("PartsQuery.aspx");
        }
    }
}
