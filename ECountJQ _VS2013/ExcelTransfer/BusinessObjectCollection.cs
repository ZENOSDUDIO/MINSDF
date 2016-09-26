using System.Data;
using ECount.Infrustructure.Data.Integration;

namespace ECount.Infrustructure.Data
{

    public class BusinessObjectCollection<T> : DataItemCollection where T : BusinessObject
    {
        public string _TableName;
        public BusinessObjectCollection(string tablename)
        {
            _TableName = tablename;
        }

        public new T this[int index]
        {
            get
            {
                return base[index] as T;
            }
            set
            {
                base[index] = value;
            }
        }

        public DataTable ToDataTable()
        {
            if (Count == 0)
                return null;

            DataTable dt = DataTableStorage.GetEmptyDataTable(this[0].Schema);
            dt.TableName = _TableName;
            foreach (DataItem item in this)
            {
                DataTableStorage.PrepareDataForDestination(item, dt);
            }

            return dt;
        }
    }
}
