using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Threading;

/// <summary>
/// Summary description for ECountPrincipal
/// </summary>
public class ECountPrincipal:IPrincipal
{
    ECountIdentity _identity;
    public ECountPrincipal(ECountIdentity identity)
    {
        _identity = identity;
    }

    #region IPrincipal Members

    public IIdentity Identity
    {
        get { return _identity; }
    }

    public bool IsInRole(string role)
    {
        return _identity.UserInfo.UserGroup.GroupName == role;
    }

    #endregion
}
