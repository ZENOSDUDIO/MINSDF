using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECountJQ.UPG.BLL;
using SGM.ECountJQ.UPG.BLL.DBBase;
using XStocktakeNotification = SGM.ECountJQ.UPG.BLL.StocktakeNotification;
using System.Text;
using ECountUser = SGM.ECountJQ.UPG.BLL.User;

public partial class UPG_StocktakeResult : System.Web.UI.Page
{
    public Dictionary<int, string> Users
    {
        get
        {
            if (ViewState["Users"] == null)
            {
                ViewState["Users"] = FindAllUsers();
            }

            return ViewState["Users"] as Dictionary<int, string>;
        }
        set
        {
            ViewState["Users"] = value;
        }
    }

    protected struct Fields
    {
        public const string NotificationID = "NotificationID";
        public const string Status = "Status";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        if (ckNotificationList.Checked)
        {
            BindNotificationListData();
        }

        if (ckNotificationDetil.Checked)
        {
            BindNotificationDetailsData();
        }
    }

    protected void gvpNotification_PageIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private string BuildNotificationWhere()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" 1=1");
        if (!string.IsNullOrEmpty(txtNotificationCode.Text))
        {
            sb.AppendFormat(" AND NotificationCode LIKE '{0}%'", txtNotificationCode.Text.Trim());
        }

        return sb.ToString();
    }

    private void BindNotificationListData()
    {
        Pager pg = gvpNotification.Pager;
        pg.Order.Name = "DateCreated";
        pg.Order.Direction = OrderDirection.Desc;
        gvNotification.DataSource = XStocktakeNotification.FindAllByPage(pg, BuildNotificationWhere());
        gvNotification.DataBind();
        gvpNotification.Pager = pg;
    }

    private void BindNotificationDetailsData()
    {
        Pager pg = gvpDetails.Pager;
        gvDetails.DataSource = StocktakeDetails.FindAllByPage(pg, string.Empty);
        gvDetails.DataBind();
        gvpDetails.SetPager(pg);
    }

    protected void gvpDetails_PageIndexChanged(object sender, EventArgs e)
    {
        BindNotificationDetailsData();
    }

    private Dictionary<int, string> FindAllUsers()
    {
        Dictionary<int, string> dict = new Dictionary<int, string>();

        List<ECountUser> users = ECountUser.FindAll();
        if (users != null && users.Count != 0)
        {
            foreach (ECountUser user in users)
            {
                dict.Add(user.UserID, user.UserName);
            }
        }

        return dict;
    }

    protected string GetUserName(object userId)
    {
        if (userId == null || string.IsNullOrEmpty(userId.ToString()) || this.Users == null || this.Users.Keys.Count == 0)
        {
            return string.Empty;
        }
        int id = int.Parse(userId.ToString());
        return this.Users.ContainsKey(id) ? this.Users[id] : string.Empty;
    }
}