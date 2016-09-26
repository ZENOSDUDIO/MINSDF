using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{
    public class OperationBLL:BaseGenericBLL<Operation>
    {
        public OperationBLL()
            : base("Operation")
        {

        }
        //public string[] GetRoles(string operationName)
        //{
        //    ECountContext e = new ECountContext();
        //    Operation operation = e.Operation.Single(o => o.OperationName == operationName);
        //    if (operation!=null)
        //    {
        //        return operation.UserGroups.Select(u=>u.GroupName).ToArray();
        //    }
        //    return new string[0];
        //}

        public List<UserGroup> GetUserGroupbyOperation(Operation operation)
        {
            Operation tmpOperation = Context.Operation.Include("UserGroups").FirstOrDefault(o => o.CommandName == operation.CommandName);  // GetObjectByKey(operation);
            return tmpOperation.UserGroups.ToList();
        }

        public List<Operation> GetOperations()
        {
            // return GetObjects().ToList();
           // Temporary usage to get menus only
            ECountContext _context = new ECountContext();
            List<Operation> opList = _context.Operation.Where(p => p.ParentOperationID == null).ToList();
            if (opList != null && opList.Count > 0)
            {
                return opList;
            }
            return new List<Operation>();
        }

        public Operation GetOperationbyKey(Operation operation)
        {
            //return GetObjectByKey(operation);
            return Context.Operation.Include("UserGroups").FirstOrDefault(op => op.CommandName == operation.CommandName);
        }

        public List<Operation> GetOperationsbyUserGroup(UserGroup group)
        {
            // return GetObjectByKey(operation);
            UserGroup userGroup = new UserGroupBLL().GetUserGroupByKey(group);
            userGroup.Operations.Load();
            return userGroup.Operations.ToList();
            //return Context.UserGroup.Where(o => o.GroupID == group.GroupID).Select(o => o.Operations).ToList();
        }

        public List<Operation> GetOperationsbyOperation(Operation operation)
        {
            // return GetObjectByKey(operation);
            // ECountContext _context = new ECountContext();
            List<Operation> opList = Context.Operation.Where(p => p.ParentOperationID == operation.OperationID).ToList();
            if (opList != null && opList.Count > 0)
            {
                return opList;
            }
            return new List<Operation>();
        }

        public Operation GetOperationbyCommandName(string commandName)
        {
            Operation operation = Context.Operation.Include("UserGroups").FirstOrDefault(o => string.Compare(o.CommandName, commandName, true)==0);
            return operation;
        }
    }
}
