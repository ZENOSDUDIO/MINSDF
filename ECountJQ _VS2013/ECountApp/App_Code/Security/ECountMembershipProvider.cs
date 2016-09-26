using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Threading;

/// <summary>
/// Summary description for ECountMembershipProvider
/// </summary>
public class ECountMembershipProvider:MembershipProvider
{
    public ECountMembershipProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override string ApplicationName
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        ECountBasePage p = new ECountBasePage();
        SGM.ECount.DataModel.User user = p.Service.GetUserbyName(username);
        if (user == null || user.Password != oldPassword || newPassword.IndexOf(username) != -1)
        {
            return false;
        }
        else
        {
            //SGM.ECount.DataModel.User newUser = new SGM.ECount.DataModel.User();
            //newUser.UserID = user.UserID;
            //newUser.UserName = user.UserName;
            //newUser.UserGroup = user.UserGroup;
            //newUser.Password = newPassword;
            //newUser.Available = user.Available;
            //newUser.ConsignmentDUNS = user.ConsignmentDUNS;
            //newUser.CreateDate = user.CreateDate;
            //newUser.DUNS = user.DUNS;
            //newUser.Plant = user.Plant;
            //newUser.RepairDUNS = user.RepairDUNS;
            //newUser.Segment = user.Segment;
            //newUser.Workshop = user.Workshop;
            //p.Service.UpdateUser(newUser);
            user.Password = newPassword;
            user.LastModified = DateTime.Now;
            p.Service.UpdateUser(user);
            return true;
        }
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        throw new NotImplementedException();
    }

    public override bool EnablePasswordReset
    {
        get { return true; }
    }

    public override bool EnablePasswordRetrieval
    {
        get { throw new NotImplementedException(); }
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public override int MaxInvalidPasswordAttempts
    {
        get { throw new NotImplementedException(); }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
        get { throw new NotImplementedException(); }
    }

    public override int MinRequiredPasswordLength
    {
        get { throw new NotImplementedException(); }
    }

    public override int PasswordAttemptWindow
    {
        get { throw new NotImplementedException(); }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
        get { throw new NotImplementedException(); }
    }

    public override string PasswordStrengthRegularExpression
    {
        get { throw new NotImplementedException(); }
    }

    public override bool RequiresQuestionAndAnswer
    {
        get { throw new NotImplementedException(); }
    }

    public override bool RequiresUniqueEmail
    {
        get { throw new NotImplementedException(); }
    }

    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user)
    {
        throw new NotImplementedException();
    }

    public override bool ValidateUser(string username, string password)
    {
        ECountBasePage p = new ECountBasePage();
        SGM.ECount.DataModel.User user = p.Service.GetUserbyName(username);
        if (user == null || !user.Available)
            return false;
        if (user.Password != password)
        {
            if (user.RetryTimes >= 5)
            {
                user.Available = false;
                p.Service.UpdateUser(user);
            }
            else
            {
                user.RetryTimes += 1;
                p.Service.UpdateUser(user);
            }
            return false;
        }
        else
        {
            System.Web.HttpContext.Current.Session["ECountUser"] = user;
            //ECountIdentity identity = new ECountIdentity(user);
            //ECountPrincipal principal = new ECountPrincipal(identity);
            //Thread.CurrentPrincipal = principal;
            //System.Web.HttpContext.Current.User = principal;
            user.RetryTimes = 0;
            user.LastLogon = DateTime.Now;
            p.Service.UpdateUser(user);
            return true;
        }
    }
}
