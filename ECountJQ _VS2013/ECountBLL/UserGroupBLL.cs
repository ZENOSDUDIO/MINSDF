using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Extensions;
using System.Data.Objects;
using System.Data.Common;

namespace SGM.ECount.BLL
{
    public class UserGroupBLL : BaseGenericBLL<UserGroup>
    {
        public UserGroupBLL()
            : base("UserGroup")
        {
        }
        public string[] GetUserGroupsbyUser(string userName)
        {
            //ECountContext e = new ECountContext();
            return new string[] { Context.User.SingleOrDefault(u => u.UserName == userName).UserGroup.GroupName };
        }

        public List<UserGroup> GetUserGroups()
        {
            return Context.UserGroup.Include("Workshop").Include("Plant").Include("StoreLocationType").Include("Operations").Where(u => u.Available == true).ToList();
        }

        public UserGroup GetUserGroupByKey(UserGroup info)
        {
            return Context.UserGroup.Include("Workshop").Include("Plant").Include("StoreLocationType").Include("Operations").Where(u => u.Available == true).FirstOrDefault(o => o.GroupID == info.GroupID);
        }

        public static bool IsUserInRole(string userName, string roleName)
        {
            ECountContext _context = new ECountContext();
            int count = _context.User.Count(u => u.UserName == userName && u.UserGroup.GroupName == roleName);
            return (count > 0) ? true : false;
        }

        public void AddUserGroup(UserGroup userGroup)
        {
            Operation[] operations = userGroup.Operations.ToArray();
            userGroup.Operations.Clear();
            for (int i = 0; i < operations.Length; i++)
            {
                operations[i] = Context.AttachExistedEntity(operations[i]);
            }
            userGroup.CreateCollection(g => g.Operations, operations);
            this.AddObject(userGroup);
            //foreach (var item in userGroup.Operations)
            //{
            //    Context.AttachExistedEntity(item);
            //}
            //AddObject(userGroup, true);
        }

        public void UpdateUserGroup(UserGroup userGroup)
        {

            UserGroup ug = Context.UserGroup.Include("Operations").FirstOrDefault(u => u.GroupID == userGroup.GroupID);
            
            ug.GroupName = userGroup.GroupName;
            ug.AnalyzeAll = userGroup.AnalyzeAll;
            ug.CurrentDynamicStocktake = userGroup.CurrentDynamicStocktake;
            ug.CurrentStaticStocktake = userGroup.CurrentStaticStocktake;
            //ug.DUNS = userGroup.DUNS;
            ug.FillinAllLocation = userGroup.FillinAllLocation;
            ug.MaxDynamicStocktake = userGroup.MaxDynamicStocktake;
            ug.MaxStaticStocktake = userGroup.MaxStaticStocktake;
            ug.ShowAllLocation = userGroup.ShowAllLocation;
            ug.SysAdmin = userGroup.SysAdmin;
            

            StoreLocationType locationType = null;
            if (userGroup.StoreLocationType!=null)
            {
                StoreLocationTypeBLL locationTypeBll = new StoreLocationTypeBLL(Context);
                locationType = locationTypeBll.GetStoreLocationTypeByKey(userGroup.StoreLocationType);
            }
            ug.StoreLocationType = locationType;

            List<Operation> ops = ug.Operations.ToList();
            for (int i = ops.Count - 1; i >= 0; i--)
            {
                ug.Operations.Remove(ops[i]);
            }
            foreach (var item in userGroup.Operations)
            {
                Operation operation = Context.Operation.FirstOrDefault(o => o.OperationID == item.OperationID);
                ug.Operations.Add(operation);
            }
            Context.SaveChanges();

        }

        private class OperationEqualityComparer : IEqualityComparer<Operation>
        {
            public static readonly OperationEqualityComparer Instance = new OperationEqualityComparer();
            private OperationEqualityComparer()
            { }

            #region IEqualityComparer<Operation> Members

            public bool Equals(Operation x, Operation y)
            {
                if (!(x == null || y == null) && x.OperationID == y.OperationID)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Operation obj)
            {
                return obj.OperationID.GetHashCode();
            }

            #endregion
        }

        public bool ExistUserGroup(UserGroup userGroup)
        {
            int iCount = Context.UserGroup.Count(o => o.GroupID == userGroup.GroupID);
            if (iCount > 0)
            {
                return true;
            }
            else
                return false;
        }

        public List<UserGroup> QueryUserGroups(UserGroup info)
        {
            IQueryable<UserGroup> qryResult = Context.UserGroup;
            qryResult = qryResult.Where(p => p.Available == true);
            return qryResult.OrderBy(s => s.GroupName).ToList();
        }

        public void DeleteUserGroups(List<string> ids)
        {
            StringBuilder sbSql = new StringBuilder();
            foreach (string id in ids)
            {
                sbSql.Append(string.Format("Update UserGroup set Available = 0 where GroupID={0};", id));
            }
            using (Context.Connection)
            {
                Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, false);
            }
        }
    }
}
