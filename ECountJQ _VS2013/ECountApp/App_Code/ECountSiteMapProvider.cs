using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SGM.ECount.DataModel;
using System.Threading;
using SGM.Common.Exception;

/// <summary>
/// Summary description for ECountSiteMapProvider
/// </summary>
public class ECountSiteMapProvider : XmlSiteMapProvider
{
   
    public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
    {
        bool bVisible = false;
        try
        {
            ECountIdentity identity = Thread.CurrentPrincipal.Identity as ECountIdentity;
            if (identity != null && identity.UserInfo != null)
            {
                List<Operation> operations = identity.UserInfo.UserGroup.Operations.ToList();
                if (string.IsNullOrEmpty(node.Url))
                {
                    bVisible = true;
                }
                else
                {
                    if (node.Url.ToLower().Contains("logout.aspx"))
                    {
                        bVisible = true;
                    }
                    else
                    {
                        if (operations.Exists(o => string.Equals(o.CommandName, node.Url)))
                        {
                            bVisible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionHandler.HandleUIException(ex);
        }
            return bVisible;
    }
   
    //private SiteMapNode CheckNodeRoles(SiteMapNode node)
    //{
    //    if (node.HasChildNodes)
    //    {
    //        foreach (SiteMapNode item in node.ChildNodes)
    //        {
    //             node = CheckNodeRoles(node);
    //        }
    //    }
    //    else
    //    {
    //        if (!string.IsNullOrEmpty(node.Url))
    //        {
    //            List<string> roles = GetRolesByUrl(node.Url);
    //            if (roles.Count>0)
    //                node.Roles.Add(roles);
    //        }
    //    }
    //    return node;
    //}

    //private List<string> GetRolesByUrl(string url)
    //{
    //    List<string> roles = new List<string>();
    //    if (!string.IsNullOrEmpty(url))
    //    {
    //        SGM.ECount.DataModel.Operation op = new SGM.ECount.DataModel.Operation();
    //        op.CommandName = url;
    //        op = basePage.Service.GetOperationByKey(op);
    //        if (op == null)
    //        {
    //            roles = null;
    //        }
    //        else
    //        {
    //            foreach (var item in op.UserGroups)
    //            {
    //                roles.Add(item.GroupName);
    //            }
    //        }
    //    }
    //    return roles;
    //}
    
}
