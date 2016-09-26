using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Data;
using ECount.ExcelTransfer;
using System.Collections;
using AjaxControlToolkit;

public partial class Common_UCFileUpload : System.Web.UI.UserControl
{
    public event EventHandler OnUpload;//(object sender, EventArgs e);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string ValidationSchemaFile
    {
        get
        {
            if (ViewState["ValidationSchemaFile"] == null)
            {
                return null;
            }
            return ViewState["ValidationSchemaFile"].ToString();
        }
        set { ViewState["ValidationSchemaFile"] = value; }
    }

    public void AddSuccessInfo(string message, string linkValue, string clickScript)
    {
        panelUploading.Style["display"] = "none";
        bllResultInfo.ForeColor = Color.Black;
        if (!string.IsNullOrEmpty(linkValue))
        {
            bllResultInfo.DisplayMode = BulletedListDisplayMode.HyperLink; 
        }
        else
        {
            bllResultInfo.DisplayMode = BulletedListDisplayMode.Text;
        }
        ListItem item = new ListItem(message, linkValue);
        if (!string.IsNullOrEmpty(clickScript))
        {
            item.Attributes["onclick"] = clickScript;
        }
        bllResultInfo.Items.Add(item);

    }

    public void AddErrorInfo(string message)
    {
        panelUploading.Style["display"] = "none";
        bllResultInfo.DisplayMode = BulletedListDisplayMode.Text;
        bllResultInfo.ForeColor = Color.Red;
        bllResultInfo.Items.Add(new ListItem(message));
    }

    public void ClearResult()
    {
        bllResultInfo.Items.Clear();
    }

    public Stream GetPostedStream()
    {
        return fileImport.PostedFile.InputStream;
    }
    public Stream PostedStream
    {
        get
        {
            if (fileImport.PostedFile != null)
            {
                fileImport.PostedFile.InputStream.Seek(0, SeekOrigin.Begin);
                return fileImport.PostedFile.InputStream;
            }
            return null;
        }
    }
    public void HideUploadingModal()
    {
        panelUploading_ModalPopupExtender.Hide();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ClearResult();
        try
        {
            string message = null;
            DataTable dt = new DataTable();
            if (ExcelHelper.GetImportedDataTable(this.fileImport.PostedFile, Server.MapPath(""), out message, out dt, new Hashtable(), ValidationSchemaFile))
            {
                UploadEventArgs args = new UploadEventArgs { ContentTable = dt };
                OnUpload(this, args);
            }
            else
            {
                if (!string.IsNullOrEmpty(message))
                {
                    string[] errorMsg = message.Split('&');
                    AddErrorInfo(errorMsg[0]);
                    if (errorMsg.Length>1)
                    {
                        string[] errorContent = errorMsg[1].Split('|');
                        for (int i = 0; i < errorContent.Length; i++)
                        {
                            AddErrorInfo(errorContent[i]);
                        } 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClearResult();
            AddErrorInfo(ex.Message);
        }
        finally
        {
            HideUploadingModal();
        }
    }

}

public class UploadEventArgs : EventArgs
{
    public DataTable ContentTable { get; set; }
}
