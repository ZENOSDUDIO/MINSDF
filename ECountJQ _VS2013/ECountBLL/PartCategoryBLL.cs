using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;

namespace SGM.ECount.BLL
{
    public class PartCategoryBLL : BaseGenericBLL<PartCategory>
    {
        public PartCategoryBLL()
            : base("PartCategory")
        {
        }

        public List<PartCategory> GetPartCategories()
        {
            return _context.PartCategory.ToList();
        }

        public List<PartCategory> GetPartCategories(PartCategory info)
        {
            IQueryable<PartCategory> queryList = _context.PartCategory;
            if (queryList != null)
            {
                if (!string.IsNullOrEmpty(info.CategoryName))
                {
                    queryList = queryList.Where(p => p.CategoryName == info.CategoryName);
                }
            }
            return queryList.ToList();
        }

        public PartCategory GetPartCategoryByKey(PartCategory info)
        {
            return GetObjectByKey(info);
        }


        public PartCategory AddPartCategory(PartCategory model)
        {
            AddObject(model);
            return model;
        }

        public void DeletePartCategory(PartCategory model)
        {
            DeleteObject(model, true);
        }

        public void DeletePartCategorys(List<string> ids)
        {
            StringBuilder sbSql = new StringBuilder();
            foreach (string id in ids)
            {
                sbSql.Append(string.Format("Delete PartCategory where CategoryID='{0}';", id));
            }
            using (Context.Connection)
            {
                Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, true);
            }
        }    

        public void UpdatePartCategory(PartCategory model)
        {
            UpdateObject(model, true);
        }
    }
}
