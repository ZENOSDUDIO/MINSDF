using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;
using SGM.ECount.DataModel;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using SGM.ECount.Contract.Service;
using System.Threading;

/// <summary>
/// ECountIdentity for authentication
/// </summary>
public class ECountIdentity : IIdentity
{
    FormsAuthenticationTicket _ticket;

    public ECountIdentity(FormsAuthenticationTicket ticket)
    {
        _ticket = ticket;
        
    }
    private User _userInfo;
    public User UserInfo
    {
        get
        {
            if (_userInfo == null)
            {
                _userInfo = System.Web.HttpContext.Current.Session["ECountUser"] as User;
                if (_userInfo == null)
                {
                    IECountService proxy = Utils.GetService();
                    _userInfo = proxy.GetUserbyName(_ticket.Name);
                    System.Web.HttpContext.Current.Session["ECountUser"] = _userInfo;
                }
            }
            return _userInfo;
        }
    }

    public void RefreshUserProfile()
    {
        IECountService proxy = Utils.GetService();
        _userInfo = proxy.GetUserbyName(_ticket.Name);
        System.Web.HttpContext.Current.Session["ECountUser"] = _userInfo;
    }



    #region IIdentity Members

    private string _authenticationType;
    public string AuthenticationType
    {
        get { return "ECountAuth"; }
    }

    public bool IsAuthenticated
    {
        get { return Name != null; }
    }

    public string Name
    {
        get { return Ticket.Name; }
    }

    public FormsAuthenticationTicket Ticket
    {
        get
        {
            return _ticket;
        }
    }

    #endregion
}
