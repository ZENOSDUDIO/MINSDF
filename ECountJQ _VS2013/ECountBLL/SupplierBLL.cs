using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Collections;
using System.Data;

namespace SGM.ECount.BLL
{
    public class SupplierBLL : BaseGenericBLL<Supplier>
    {
        public SupplierBLL()
            : base("Supplier")
        {

        }
        public Supplier GetSupplierbykey(Supplier info)
        {
            info = GetObjectByKey(info);
            return info;
        }

        public IQueryable<Supplier> QuerySupplier(Supplier filter)
        {
            IQueryable<Supplier> supplier = Context.Supplier.Where(s=> s.Available == true);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.DUNS))
                {
                    supplier = from s in supplier where s.DUNS.Contains(filter.DUNS) select s;
                }
                if (!string.IsNullOrEmpty(filter.SupplierName))
                {
                    supplier = from s in supplier where s.SupplierName.Contains(filter.SupplierName) select s;
                }
            }
            supplier = supplier.OrderBy(s => s.DUNS);
            return supplier;
        }

        public int GetSuppliersCount(Supplier info)
        {
            IQueryable<Supplier> supplier = Context.Supplier.Where(s=> s.Available ==true);
            if (info != null)
            {
                if (!string.IsNullOrEmpty(info.DUNS))
                {
                    supplier = from s in supplier where s.DUNS == info.DUNS select s;
                }
                if (!string.IsNullOrEmpty(info.SupplierName))
                {
                    supplier = from s in supplier where s.SupplierName == info.SupplierName select s;
                }
            }
            return supplier.ToList().Count;
        }

        public List<Supplier> QuerySupplierByPage(Supplier filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<Supplier> supplier = this.QuerySupplier(filter);
            return base.GetQueryByPage(supplier, pageSize, pageNumber, out pageCount,out itemCount).ToList(); 
        }

        public void DeleteSupplier(Supplier model)
        {
            this.DeleteObject(model);
        }

        public void DeleteSuppliers(List<int> ids)
        {
            StringBuilder sbSql = new StringBuilder();
            foreach (int id in ids)
            {
                sbSql.Append(string.Format("Update  Supplier set Available = 0 where SupplierID={0};", id));
            }
            Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text);
        }  

        public Supplier AddSupplier(Supplier model)
        {
            model.Available = true;
            this.AddObject(model);
            return model;
        }

        public void UpdateSupplier(Supplier model)
        {
            model.Available = true;
            this.UpdateObject(model);
        }
    }
}
