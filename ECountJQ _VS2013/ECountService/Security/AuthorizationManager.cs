using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Principal;
using SGM.ECount.BLL;

namespace SGM.ECount.Service.Security
{
    public class AuthorizationManager:ServiceAuthorizationManager
    {
        public override bool CheckAccess(OperationContext operationContext, ref System.ServiceModel.Channels.Message message)
        {
            base.CheckAccess(operationContext, ref message);
            IPrincipal principal = GetPrincipal(operationContext);
            bool authorized = false;
            string[] roles=GetRolesForAction(operationContext.IncomingMessageHeaders.Action);
            for (int i = 0; i < roles.Length; i++)
            {
                if (principal.IsInRole(roles[i]))
                {
                    authorized = true;
                    break;
                }
            }
            return authorized;
        }
        private string[] GetRolesForAction(string action)
        {
            //OperationBLL bll = new OperationBLL();
            //return bll.GetRoles(action);            
            return null;
        }


        private IPrincipal GetPrincipal(OperationContext operationContext)
        {
            return operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] as IPrincipal;
        }
    }
}
