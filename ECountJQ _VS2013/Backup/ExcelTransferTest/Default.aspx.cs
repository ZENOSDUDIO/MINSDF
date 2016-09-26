using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.IO;
using ECount.ExcelTransfer;
using ECount.Infrustructure.Utilities;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            BindData();
       
        }

        public void BindData()
        {
            string message = null;
            DataTable dt = new DataTable();
            GetImportedDataTable(this.FileUpload1, Server.MapPath("/"), out message, out dt);
            this.Label1.Text = message;
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            message = MiscUtil.EnsureDataTableQualify(dt, @"~/XMLFile1.xml", null);
            this.Label2.Text = message.Replace("|", "<BR/>");

        }

        public static bool GetImportedDataTable(FileUpload fuFileUpload, string uploadFolder, out string message,
                                                out DataTable dt)
        {
            return GetImportedDataTable(fuFileUpload, uploadFolder, out message, out dt, new Hashtable());
        }

        public static bool GetImportedDataTable(FileUpload fuFileUpload, string uploadFolder, out string message,
                                                out DataTable dt, Hashtable param)
        {
            dt = new DataTable();
            if (fuFileUpload.PostedFile == null || string.IsNullOrEmpty(fuFileUpload.PostedFile.FileName))
            {
                message = "请选择上传文件";
                return false;
            }

            const int maxLength = 1024 * 1024 * 4; //最大4M，否则内存占用太严重,需要让用户自己拆分文件
            if (fuFileUpload.FileBytes.Length <= 0 || fuFileUpload.FileBytes.Length > maxLength)
            {
                message = "上传文件大小应在4M内";
                return false;
            }

            string mime = fuFileUpload.PostedFile.ContentType;
            //text/csv
            if (!mime.Equals("text/csv", StringComparison.InvariantCultureIgnoreCase) &&
                !mime.Equals("application/vnd.ms-excel", StringComparison.InvariantCultureIgnoreCase) &&
                !fuFileUpload.PostedFile.FileName.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
            {
                message = string.Format("上传文件的格式'{0}'无法识别, 或者文件正在被使用.", mime);
                return false;
            }

            string filename = fuFileUpload.PostedFile.FileName;
            filename = filename.Substring(filename.LastIndexOf('\\') + 1);


            //将上传的文件保存到服务器磁盘
            string tempFileName = Path.Combine(uploadFolder, "temp_" + DateTime.Now.ToFileTime() + filename);
            fuFileUpload.SaveAs(tempFileName);

            try
            {
                //return new ExcelHelper().ImportExcelData(tempFileName, "", out dt, out message, param);
                return ExcelHelper.ImportExcelData(tempFileName, "", out dt, out message, param);
            }
            catch (InvalidDataException)
            {
                dt = null;
                message = "ERROR";
                return false;
            }
        }
    }
}
