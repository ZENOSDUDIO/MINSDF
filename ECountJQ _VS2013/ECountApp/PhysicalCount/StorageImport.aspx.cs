using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SGM.ECount.DataModel;

public partial class PhysicalCount_StorageImport : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UCFileUpload1.OnUpload += new EventHandler(UCFileUpload1_OnUpload);
        if (!Page.IsPostBack)
        {
            UCFileUpload1.ValidationSchemaFile = Server.MapPath(@"~/ImportSchema/LogisticSysStorage.xml");
        }
    }
    class StorageRecordView
    {
        public string PartCode { get; set; }
        public string PlantCode { get; set; }
        public string SLOC { get; set; }
    }
    void UCFileUpload1_OnUpload(object sender, EventArgs e)
    {
        string noticeNo = txtNoticeNo.Text.Trim();
        if(string.IsNullOrEmpty(noticeNo))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "selectNotiMsg", "alert('请选择通知单');", true);
            return;
        }

        UploadEventArgs ue = e as UploadEventArgs;
        DataTable dtStorage = ue.ContentTable;
        dtStorage.Columns["序号"].ColumnName = "No";
        dtStorage.Columns["零件号"].ColumnName = "PartNo";
        dtStorage.Columns["工厂代码"].ColumnName = "PlantCode";
        dtStorage.Columns["物流系统存储区域"].ColumnName = "StoreLocation";
        dtStorage.Columns["系统Available"].ColumnName = "Available";
        dtStorage.Columns["系统QI"].ColumnName = "QI";
        dtStorage.Columns["系统Block"].ColumnName = "Block";
        dtStorage.Columns["单价"].ColumnName = "Price";

        bool hasError = false;
        List<S_StorageRecord> recordList = new List<S_StorageRecord>();
        List<ViewPart> partList = new List<ViewPart>();
        List<View_StocktakeDetails> stocktakeDetails = new List<View_StocktakeDetails>();
        dtStorage.DefaultView.Sort = "PartNo";
        dtStorage = dtStorage.DefaultView.ToTable();
        //get part by code, plant
        List<StorageRecordView> storageRecords = new List<StorageRecordView>();
        for (int i = 0; i < dtStorage.Rows.Count; i++)
        {
            StorageRecordView record = new StorageRecordView
            {
                PartCode = dtStorage.Rows[i]["PartNo"].ToString(),
                PlantCode = dtStorage.Rows[i]["PlantCode"].ToString(),
                SLOC = dtStorage.Rows[i]["StoreLocation"].ToString()
            };
            if (!storageRecords.Exists(r => r.PartCode == record.PartCode && r.PlantCode == record.PlantCode && r.SLOC == record.SLOC))
            {
                storageRecords.Add(record);
            }
            else
            {
                string msg = string.Format("库存信息零件号{0}，工厂{1}，存储区域{2}重复", record.PartCode, record.PlantCode, record.SLOC);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
        }
        storageRecords.Clear();
        if (!hasError)
        {
            //partList = Service.QueryPartsByCodePlant(partList);//Key(partList);
            for (int i = 0; i < dtStorage.Rows.Count; i++)
            {
                if (i % 5000 == 0)
                {
                    string startCode = dtStorage.Rows[i]["PartNo"].ToString();
                    int end = i + 4999;
                    if (end >= dtStorage.Rows.Count)
                    {
                        end = dtStorage.Rows.Count - 1;
                    }
                    //View_StocktakeDetails filterStocktakeDetails = new View_StocktakeDetails { NotificationCode = noticeNo };
                    string endCode = dtStorage.Rows[end]["PartNo"].ToString();
                    partList = Service.QueryPartCodeScope(null, startCode, endCode);
                    stocktakeDetails = Service.QueryStocktakeDetailsScope(new View_StocktakeDetails { NotificationCode = noticeNo }, startCode, endCode);
                }
                string sloc = dtStorage.Rows[i]["StoreLocation"].ToString();
                StoreLocation storeLocation = this.StoreLocations.FirstOrDefault(l => l.LogisticsSysSLOC == sloc);
                if (storeLocation == null)//storeLocation invalid
                {
                    string msg = string.Format("物流系统存储区域【{0}】不存在", sloc);//i + 2,
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                }

                View_StocktakeDetails tmpStocktakeDetails = new View_StocktakeDetails
                {
                    PartCode = dtStorage.Rows[i]["PartNo"].ToString(),
                    NotificationCode = noticeNo
                };

                List<View_StocktakeDetails> tmpStocktakeDetailsList = stocktakeDetails.Where(s => s.NotificationCode == tmpStocktakeDetails.NotificationCode && s.PartCode == tmpStocktakeDetails.PartCode).ToList();
                //if (tmpStocktakeDetailsList == null || tmpStocktakeDetailsList.Count == 0)
                //{
                //    string msg = string.Format("通知单【{0}】中不存在零件【{1}】", tmpStocktakeDetails.NotificationCode, tmpStocktakeDetails.PartCode); 
                //    UCFileUpload1.AddErrorInfo(msg);
                //    hasError = true;
                //}

                ViewPart tmpPart = new ViewPart
                {
                    PlantCode = dtStorage.Rows[i]["PlantCode"].ToString(),
                    //DUNS = dtStorage.Rows[i]["DUNS"].ToString(),
                    PartCode = dtStorage.Rows[i]["PartNo"].ToString()
                };

                //ViewPart part = partList.SingleOrDefault(p => p.PartCode == tmpPart.PartCode && p.PlantCode == tmpPart.PlantCode && p.DUNS == tmpPart.DUNS);
                List<ViewPart> tmpPartList = partList.Where(p => p.PartCode == tmpPart.PartCode && p.PlantCode == tmpPart.PlantCode).ToList();
                //if (part == null)//part is invalid
                if (tmpPartList == null || tmpPartList.Count == 0)
                {
                    //string msg = string.Format("工厂【{0}】中不存在零件【{1}】", tmpPart.PlantCode, tmpPart.PartCode);// i + 2, 
                    //UCFileUpload1.AddErrorInfo(msg);
                    //hasError = true;
                }
                else
                {
                    //storeLocation is valid
                    if (storeLocation != null)
                    {
                        foreach (var part in tmpPartList)
                        {
                            S_StorageRecord tmpRecord = new S_StorageRecord
                            {
                                SLOCID = storeLocation.LocationID,
                                PartID = part.PartID
                            };

                            tmpRecord.Available = SGM.Common.Utility.Utils.GetDecimalDbCell(dtStorage.Rows[i]["Available"]);
                            tmpRecord.QI = SGM.Common.Utility.Utils.GetDecimalDbCell(dtStorage.Rows[i]["QI"]);
                            tmpRecord.Block = SGM.Common.Utility.Utils.GetDecimalDbCell(dtStorage.Rows[i]["Block"]);
                            tmpRecord.Price = SGM.Common.Utility.Utils.GetDecimalDbCell(dtStorage.Rows[i]["Price"]);
                            if (tmpStocktakeDetailsList == null || tmpStocktakeDetailsList.Count == 0)
                            {
                                tmpRecord.DetailsID = 0;
                            }
                            else
                            {
                                tmpRecord.DetailsID = Convert.ToInt32(tmpStocktakeDetailsList.FirstOrDefault().DetailsID);
                                recordList.Add(tmpRecord);
                            }
                        }
                    }
                }

            }
        }



        if (!hasError)
        {
            //fill in items
            Service.ImportStorage(recordList);
            BindDataControl(gvStoreage, dtStorage);
            //show information
            this.UCFileUpload1.AddSuccessInfo("上传文件成功", string.Empty, string.Empty);
        }
    }

    protected void gvStoreage_PreRender(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("No");
        dt.Columns.Add("PartNo");
        dt.Columns.Add("PlantCode");
        dt.Columns.Add("StoreLocation");
        dt.Columns.Add("Available");
        dt.Columns.Add("QI");
        dt.Columns.Add("Block");
        dt.Columns.Add("Price");
        dt.Rows.Add(dt.NewRow());
        BindEmptyGridView(gvStoreage, dt);
    }
}
