using System.Collections.Generic;

namespace ECount.Infrustructure.Data.Integration
{
    public class DataItemCollection : List<DataItem>
    {
        public new void Add(DataItem item)
        {
            item.Container = this;
            //item.DoValidate();
            base.Add(item);
        }
    }
}