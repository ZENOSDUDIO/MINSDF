using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Xml.Serialization;
using System.IO;
using System.Data.Common;
using System.Data;

namespace SGM.ECount.BLL
{
    public class StorageRecordBLL : BaseGenericBLL<StorageRecord>
    {
        public StorageRecordBLL()
            : base("StorageRecord")
        {}

        public void ImportStorage(List<S_StorageRecord> list)
        {
            Type[] types = new Type[] { typeof(S_StorageRecord) };
            XmlSerializer xs = new XmlSerializer(typeof(List<S_StorageRecord>), types);
            string itemStr = string.Empty;
            using (StringWriter sw=new StringWriter())
            {
                xs.Serialize(sw, list);
                itemStr = sw.ToString();
            }
                DbParameter paramItems = Context.CreateDbParameter("@storageItems", System.Data.DbType.Xml, itemStr, System.Data.ParameterDirection.Input);

            Context.ExecuteNonQuery("sp_ImportStorage", CommandType.StoredProcedure, paramItems);
        }
    }
}
