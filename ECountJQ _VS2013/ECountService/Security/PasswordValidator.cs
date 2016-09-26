using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using SGM.ECount.BLL;

namespace SGM.ECount.Service.Security
{
    class PasswordValidator:UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            UserBLL userBll = new UserBLL();
            if (userBll.Validate(userName, password))
            { }
            else
            { }
        }
    }
}
