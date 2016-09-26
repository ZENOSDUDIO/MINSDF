using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECountJQ.UPG.BLL.DBBase;

namespace SGM.ECountJQ.UPG.Web.Pages
{
    public partial class GridViewPager : System.Web.UI.UserControl
    {
        private Pager _Pager;
        public Pager Pager
        {
            get
            {
                if (ViewState["Pager_" + this.ID] == null)
                {
                    _Pager = new Pager();
                    _Pager.PageIndex = 1;
                    _Pager.PageSize = int.Parse(ddlPageSize.SelectedValue);
                    _Pager.Order = new PageOrder();
                    ViewState["Pager_" + this.ID] = _Pager;
                }

                return ViewState["Pager_" + this.ID] as Pager;
            }
            set
            {
                _Pager = value;
                ViewState["Pager_" + this.ID] = _Pager;
            }
        }

        public delegate void PageIndexChangedEventHandler(object sender, EventArgs e);
        public event PageIndexChangedEventHandler PageIndexChanged;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponentsState();
            }
        }

        protected void OnPageIndexChanged(object sender, EventArgs e)
        {
            InitComponentsState();
        }

        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            Pager.PageIndex = Pager.PageIndex + 1;
            PageIndexChanged(sender, e);
            InitComponentsState();
        }

        protected void lbtnPrev_Click(object sender, EventArgs e)
        {
            Pager.PageIndex = Pager.PageIndex - 1;
            PageIndexChanged(sender, e);
            InitComponentsState();
        }

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            Pager.PageIndex = 1;
            PageIndexChanged(sender, e);
            InitComponentsState();
        }

        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            Pager.PageIndex = Pager.PageCount;
            PageIndexChanged(sender, e);
            InitComponentsState();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pager.PageIndex = 1;
            Pager.PageSize = int.Parse(ddlPageSize.SelectedValue);
            PageIndexChanged(sender, e);
            InitComponentsState();
        }

        protected void txtPageIndex_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPageIndex.Text))
            {
                int index = int.Parse(txtPageIndex.Text);
                if (index < 1)
                {
                    index = 1;
                }
                if (index > Pager.PageCount)
                {
                    index = Pager.PageCount;
                }
                Pager.PageIndex = index;
                PageIndexChanged(sender, e);
                InitComponentsState();
            }
        }

        private void InitComponentsState()
        {
            lbtnNext.Enabled = this.Pager.PageCount != 0 && this.Pager.PageIndex != this.Pager.PageCount;
            lbtnLast.Enabled = this.Pager.PageCount != 0 && this.Pager.PageIndex != this.Pager.PageCount;
            lbtnFirst.Enabled = this.Pager.PageIndex != 1;
            lbtnPrev.Enabled = this.Pager.PageIndex != 1;
            ddlPageSize.SelectedValue = this.Pager.PageSize.ToString();
            txtPageIndex.Text = this.Pager.PageIndex.ToString();
        }

        public void SetPager(Pager pg)
        {
            this.Pager = pg;
            InitComponentsState();
        }
    }
}