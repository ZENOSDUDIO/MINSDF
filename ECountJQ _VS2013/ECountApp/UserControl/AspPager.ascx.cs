using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BizDataMaintain_AspPager : System.Web.UI.UserControl
{
    //显示10个页码
    private const int MaxiumPageCount = 10;
    //声明事件委托
    public delegate void PageSizeChangeEventHandler(object sender, EventArgs e);
    public delegate void PageNumberSelectEventHandler(object sender, EventArgs e);
    //定义事件
    public event PageSizeChangeEventHandler PageSizeChange;
    public event PageNumberSelectEventHandler PageNumberSelect;


    protected void Page_Load(object sender, EventArgs e)
    {
        InitComponentsState();
    }


    //监视事件
    protected void OnPageSizeChange(object sender, EventArgs e)
    {
        //if (PageSizeChange != null)
        //{
            this.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);

            if (this.TotalRecord - (this.TotalRecord / this.PageSize) * this.PageSize > 0)
                this.TotalPage = this.TotalRecord / this.PageSize + 1;
            else
                this.TotalPage = this.TotalRecord / this.PageSize;

            //if ((StartIndex / PageSize) + 1 < TotalPage)
            //    this.CurrentPage = (StartIndex / PageSize) + 1;
            //else
            //    this.CurrentPage = TotalPage;
            this.CurrentPage = 1;

            InitComponentsState();
            this.SelectPageNumber = 1;
            if (this.TotalRecord>0)
            {
                PageNumberSelect(sender, e); 
            }
            //PageSizeChange(sender, e);
        //}
    }

    protected void OnPageNumberSelect(object sender, EventArgs e)
    {
        if (PageNumberSelect != null)
        {
            LinkButton btSender = (LinkButton)sender;
            string strCommandName = btSender.CommandArgument;

            int i;
            try
            {
                i = Convert.ToInt32(strCommandName);
                this.SelectPageNumber = i;
                //this.lbCurrentPage.Text = i.ToString();
                //this.txtDestinationPage.Text = i.ToString();
                CurrentPage = i;
                StartIndex = (CurrentPage - 1) * PageSize;
                InitComponentsState();
            }
            catch
            {
                return;
            }
            PageNumberSelect(sender, e);
        }
    }

    protected void DestinationPageChange_Click(object sender, EventArgs e)
    {
        try
        {
            int i = int.Parse(this.txtDestinationPage.Text);
            if (i > TotalPage)
            {
                this.SelectPageNumber = TotalPage;
            }
            else if (i < 0)
            {
                this.SelectPageNumber = 1;
            }
            else
                this.SelectPageNumber = i;
            this.txtDestinationPage.Text = this.SelectPageNumber.ToString();
            this.CurrentPage = this.SelectPageNumber;
            PageNumberSelect(sender, e);
        }
        catch
        {
        }
    }

    protected void PreviousPage_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToInt16(this.lbCurrentPage.Text) > 1)
            if (CurrentPage>1)
            {
                //int i;
                //i = Convert.ToInt16(this.lbCurrentPage.Text) - 1;
                //this.lbCurrentPage.Text = i.ToString();
                //CurrentPage = i;
                CurrentPage--;
                StartIndex = (CurrentPage - 1) * PageSize;
                InitComponentsState();

                this.SelectPageNumber = CurrentPage;
                PageNumberSelect(sender, e);
            }
        }
        catch (System.Exception)
        {
            return;
        }
    }

    protected void FirstPage_Click(object sender, EventArgs e)
    {
        //this.lbCurrentPage.Text = "1";
        //txtDestinationPage.Text = "1";
        this.CurrentPage = 1;
        StartIndex = 0;
        InitComponentsState();

        this.SelectPageNumber = 1;
        PageNumberSelect(sender, e);
    }

    protected void LastPage_Click(object sender, EventArgs e)
    {
        //this.lbCurrentPage.Text = this.lbTotalPage.Text;
        //txtDestinationPage.Text = this.lbTotalPage.Text;
        this.CurrentPage = this.TotalPage;// Convert.ToInt16(this.lbTotalPage.Text);
        StartIndex = (CurrentPage - 1) * PageSize;
        InitComponentsState();

        this.SelectPageNumber = CurrentPage;
        PageNumberSelect(sender, e);
    }

    protected void NextPage_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToInt16(this.lbCurrentPage.Text) < Convert.ToInt16(this.lbTotalPage.Text))
                if (CurrentPage < this.TotalPage)
            {
                //int i;
                //i = Convert.ToInt16(this.lbCurrentPage.Text) + 1;
                //this.lbCurrentPage.Text = i.ToString();
                //txtDestinationPage.Text = i.ToString();
                //CurrentPage = i;
                CurrentPage++;
                StartIndex = (CurrentPage - 1) * PageSize;
                InitComponentsState();

                this.SelectPageNumber = CurrentPage;
                PageNumberSelect(sender, e);
            }

        }
        catch (System.Exception)
        {
            return;
        }
    }


    #region private methods...
    //获取当前显示的开始页码
    private int GetStartPage(int CurrentPage, int TotalPage)
    {
        int nStartPage = CurrentPage - (MaxiumPageCount / 2);
        if (nStartPage <= 0)
            return 1;
        return nStartPage;
    }
    //获取当前显示的结束页码
    private int GetEndPage(int StartPage, int CurrentPage, int TotalPage)
    {
        int nEndPage = StartPage + MaxiumPageCount - 1;
        if (nEndPage >= TotalPage)
            return TotalPage;
        return nEndPage;
    }
   
    private void InitComponentsState()
    {
        if (CurrentPage <= 1)
            this.bnPreviousPage.Enabled = false;
        else
            this.bnPreviousPage.Enabled = true;

        if (CurrentPage >= TotalPage)
            this.bnNextPage.Enabled = false;
        else
            this.bnNextPage.Enabled = true;

        if (CurrentPage == 1)
            this.bnFirstPage.Enabled = false;
        else
            this.bnFirstPage.Enabled = true;

        if (CurrentPage >= TotalPage)
            this.bnLastPage.Enabled = false;
        else
            this.bnLastPage.Enabled = true;

        AddPageSelector();
    }

    private void AddPageSelector()
    {
        TableCell td = (TableCell)this.FindControl("PageSelector");
        if (td != null)
        {
            td.Controls.Clear();

            int nCurrentPage = CurrentPage;
            int nTotalPage = TotalPage;

            int nStartPage = GetStartPage(nCurrentPage, nTotalPage);
            int nEndPage = GetEndPage(nStartPage, nCurrentPage, nTotalPage);
            for (int i = nStartPage; i <= nEndPage; i++)
            {
                LinkButton btSelector = new LinkButton();
                btSelector.CausesValidation = false;
                btSelector.Text = i.ToString() + "  ";
                btSelector.ID = "PageSelector" + i;
                btSelector.Click += new EventHandler(OnPageNumberSelect);
                btSelector.CommandArgument = i.ToString();
                if (i == CurrentPage)
                    btSelector.Enabled = false;
                else
                    btSelector.Enabled = true;
                td.Controls.Add(btSelector);
            }
        }
    }
    #endregion private methods......

    #region propertys ...
    private int _PageSize = 10;
    public int PageSize
    {
        get
        {
            try
            {
                return System.Convert.ToInt32(this.ddlPageSize.SelectedValue);
            }
            catch (System.Exception)
            {
                return 10;
            }
        }
        set
        {
            _PageSize = value;
            this.ddlPageSize.SelectedValue = value.ToString();
        }
    }

    private int _SelectPageNumber = 1;
    public int SelectPageNumber
    {
        get
        {
            return _SelectPageNumber;
        }
        set
        {
            _SelectPageNumber = value;
        }
    }

    public int StartIndex
    {
        get
        {
            String s = (String)ViewState[this.ID + ".StartIndex"];
            return ((s == null) ? (CurrentPage - 1) * PageSize : Convert.ToInt32(s));


        }
        set
        {
            ViewState[this.ID + ".StartIndex"] = value.ToString();
        }
    }

    //private int _CurrentPage;
    public int CurrentPage
    {
        get
        {
            try
            {
                if (ViewState["CurrentPage"]==null)
                {
                    return 1;
                }
                return Convert.ToInt32(ViewState["CurrentPage"]);
                //return System.Convert.ToInt32(this.lbCurrentPage.Text);
            }
            catch (System.Exception)
            {
                return 1;
            }
        }
        set
        {
            //_CurrentPage = value;
            //this.lbCurrentPage.Text = value.ToString();
            ViewState["CurrentPage"] = value;
            txtDestinationPage.Text = value.ToString();
        }
    }
    //private int _TotalPage = 1;
    public int TotalPage
    {
        get
        {
            try
            {
                if (ViewState["TotalPage"] != null)
                {
                    return Convert.ToInt32(ViewState["TotalPage"]);
                }
                return 1;
                //return System.Convert.ToInt32(this.lbTotalPage.Text);
            }
            catch (System.Exception)
            {
                return 1;
            }
        }
        set
        {
            int totalPage = value ;
            if (value<=0)
            {
                totalPage = 1;
            }
            //_TotalPage = value;
            ViewState["TotalPage"] = totalPage;
            lbTotalPage.Text = totalPage.ToString();
        }
    }

    //private int _TotalRecord = 1;
    public int TotalRecord
    {
        get
        {
            try
            {
                if (ViewState["TotalRecord"]!=null)
                {
                    return Convert.ToInt32(ViewState["TotalRecord"]);
                }
                else
                {
                    return 0;
                }
                //return System.Convert.ToInt32(this.lbRecordCount.Text);
            }
            catch (System.Exception)
            {
                return 0;
            }

        }
        set
        {
            //_TotalRecord = value;
            ViewState["TotalRecord"] = value;
            this.lbRecordCount.Text = string.Format("共&nbsp;<font color='#5D7B9D'><b>{0}</b></font>&nbsp;条记录",value.ToString());

            if (value - (value / this.PageSize) * this.PageSize > 0)
                this.TotalPage = value / this.PageSize + 1;
            else
                this.TotalPage = value / this.PageSize;
            if (CurrentPage>TotalPage)
            {
                CurrentPage = TotalPage;
            }
            if (CurrentPage<1)
            {
                CurrentPage = 1;
            }

            InitComponentsState();
        }
    }

    #endregion propertys ......
}
