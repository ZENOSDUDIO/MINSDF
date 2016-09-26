using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;

namespace SGM.ECount.BLL
{
    public class UserBLL : BaseGenericBLL<User>
    {
        public UserBLL()
            : base("User")
        {

        }
        public bool Validate(string userName, string password)
        {
            ECountContext _context = new ECountContext();
            User user = _context.User.Single(u => string.Compare(userName, u.UserName, true) == 0);
            if (user == null || user.Password != password)
            {
                return false;
            }
            return true;
        }

        public List<User> GetUsers()
        {
            IQueryable<User> qryResult = Context.User.Include("UserGroup").Include("Workshop").Include("Segment").Include("Plant").Where(u => u.Available == true);
            return qryResult.ToList();
        }

        public User GetUserByName(string userName)
        {
            return Context.User.Include("UserGroup").Include("UserGroup.StoreLocationType").Include("UserGroup.Operations").Include("Plant").Include("Workshop").Include("Segment").FirstOrDefault(u => u.UserName == userName);
        }

        public User GetUserbyKey(User user)
        {
            return Context.User.FirstOrDefault(u => u.UserID == user.UserID);
        }

        public User GetUserInfo(User user)
        {
            User result = _context.User.Include("Plant").Include("UserGroup").Include("UserGroup.Operations").Include("Workshop").Include("Workshop.Plant").Include("Segment").Include("UserGroup.StoreLocationType").Where(u => u.Available == true||u.Available==false&&u.RetryTimes>=5).FirstOrDefault(u => u.UserID == user.UserID);

            return result;
        }


        public User AddUser(User user)
        {
            return AddObject(user, true);
        }

        public void UpdateUser(User user)
        {
            UpdateObject(user, true);
        }

        public void DeleteUsers(List<string> ids)
        {
            StringBuilder sbSql = new StringBuilder();
            foreach (string id in ids)
            {
                sbSql.Append(string.Format("Update dbo.[User] set Available= 0,RetryTimes=0 where UserID={0};", id));
            }
            Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, false);
        }

        public void AddToGroup(UserGroup userGroup, User user)
        {
            user = GetUserInfo(user);
            if (user.UserGroup == null)
            {
                user.UserGroup = new UserGroup();
            }
            user.UserGroup.GroupID = userGroup.GroupID;
            UpdateUser(user);
        }

        public bool ExistUser(User user)
        {
            //valid or blocked user exists 
            int iCount = Context.User.Count(o => o.UserName == user.UserName&&o.UserID!= user.UserID&&(o.Available==true||o.Available==false&&o.RetryTimes>=5));
            if (iCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<User> QueryUsersByPage(User user)
        {
            var userQry = this.QueryUsers(user).Where(u => u.Available == true||u.RetryTimes>=5).OrderBy(u => u.UserName);

            return userQry;
        }

        public IQueryable<User> QueryUsers(User user)
        {
            IQueryable<User> userQry = _context.User.Include("UserGroup").Include("Plant").Include("Workshop").Include("Segment");

            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.UserName))
                {
                    userQry = userQry.Where(u => u.UserName == user.UserName);
                }
                if (user.UserGroup != null)//((user.UserGroup != null) && (user.UserGroup.GroupID != null))
                {
                    userQry = userQry.Where(u => u.UserGroup.GroupID == user.UserGroup.GroupID);
                }
            }
            return userQry;
        }

    }

}