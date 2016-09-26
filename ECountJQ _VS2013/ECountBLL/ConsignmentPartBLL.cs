using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using Microsoft.Data.Extensions;
using System.Data.Metadata.Edm;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;

namespace SGM.ECount.BLL
{
    public class ConsignmentPartBLL : BaseGenericBLL<ConsignmentPartRecord>
    {
        public ConsignmentPartBLL()
            : base("ConsignmentPartRecord")
        {

        }

        public IQueryable<ConsignmentPartRecord> QueryRecords(ConsignmentPartRecord filter)
        {
            IQueryable<ConsignmentPartRecord> query = Context.ConsignmentPartRecord.Include("Part.Plant").Include("Part.Supplier").Include("Supplier").Where(r => r.Available == true);
            if (filter != null)
            {
                if (filter.Part != null)
                {
                    if (filter.Part!=null&&filter.Part.PartID!=DefaultValue.INT)
                    {
                        int partID = filter.Part.PartID;
                        query = query.Where(p => p.Part.PartID == partID);
                    }
                    if (filter.Part.Plant != null && filter.Part.Plant.PlantID != DefaultValue.INT)
                    {
                        int plantID = filter.Part.Plant.PlantID;
                        query = query.Where(r => r.Part.Plant.PlantID == plantID);
                    }
                    if (!string.IsNullOrEmpty(filter.Part.PartCode))
                    {
                        string partCode = filter.Part.PartCode;
                        query = query.Where(r => r.Part.PartCode == partCode);
                    }
                }
                if (filter.Part != null && filter.Part.Supplier != null && !string.IsNullOrEmpty(filter.Part.Supplier.DUNS))
                {
                    string duns = filter.Part.Supplier.DUNS;
                    query = query.Where(r => r.Part.Supplier.DUNS == duns);
                }
                if (filter.Supplier != null)
                {
                    if (!string.IsNullOrEmpty(filter.Supplier.DUNS))
                    {
                        string duns = filter.Supplier.DUNS;
                        query = query.Where(r => r.Supplier.DUNS == duns);
                    }
                    if (!string.IsNullOrEmpty(filter.Supplier.SupplierName))
                    {
                        string supplierName = filter.Supplier.SupplierName;
                        query = query.Where(r => r.Supplier.SupplierName == supplierName);
                    }
                }
            }
            return query.OrderBy(r => r.Part.Plant.PlantCode).ThenBy(r => r.Part.PartCode);
        }

        public List<View_ConsignmentPart> GetViewConsignment(View_ConsignmentPart record)
        {
            IQueryable<View_ConsignmentPart> query = Context.View_ConsignmentPart.Where(c => c.Available == true);

            if (record != null)
            {
                if (!string.IsNullOrEmpty(record.PlantCode))
                {
                    query = query.Where(c => c.PlantCode == record.PlantCode);
                }

                if (!string.IsNullOrEmpty(record.PartCode))
                {
                    query = query.Where(c => c.PartCode == record.PartCode);
                }
                if (!string.IsNullOrEmpty(record.DUNS))
                {
                    query = query.Where(c => c.DUNS == record.DUNS);
                }
                if (!string.IsNullOrEmpty(record.CDUNS))
                {
                    query = query.Where(c => c.CDUNS == record.CDUNS);
                }
                if (!string.IsNullOrEmpty(record.SupplierName))
                {
                    query = query.Where(c => c.SupplierName == record.SupplierName);
                }
            }

            return query.ToList();
        }

        public List<ConsignmentPartRecord> GetRecordsByPage(ConsignmentPartRecord filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            return GetQueryByPage(QueryRecords(filter), pageSize, pageNumber, out pageCount,out itemCount).ToList();
        }

        public ConsignmentPartRecord GetConsignmentPartRecordbykey(ConsignmentPartRecord record)
        {
            record = GetObjectByKey(record);
            if (record.Part != null)
            {
                record.Part.PlantReference.Load();
                record.Part.SupplierReference.Load();
            }

            return record;
        }

        public void UpdateConsignmentPartRecord(ConsignmentPartRecord record)
        {
            record.DateModified = DateTime.Now;
            //record.PartReference.EntityKey = Context.CreateEntityKey(_context.DefaultContainerName + "." + "Part", record.Part);
            //record.SupplierReference.EntityKey = Context.CreateEntityKey(_context.DefaultContainerName + "." + "Supplier", record.Supplier);

            UpdateObject(record);
        }


        public ConsignmentPartRecord AddConsignmentPartRecord(ConsignmentPartRecord record)
        {
            record =AddObject(record);
            return record;
        }

        public void DeleteConsignmentPartRecord(ConsignmentPartRecord record)
        {
            ConsignmentPartRecord tmpRecord = GetConsignmentPartRecordbykey(record);
            tmpRecord.Available = false;
            UpdateConsignmentPartRecord(tmpRecord);
        }

        public void ImportConsignmentRecord(List<View_ConsignmentPart> list)
        {
            Type[] types = new Type[] { typeof(View_ConsignmentPart) };
            XmlSerializer xs = new XmlSerializer(typeof(List<View_ConsignmentPart>), types);
            string itemStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, list);
                itemStr = sw.ToString();
            }
            DbParameter paramItems = Context.CreateDbParameter("@partItems", System.Data.DbType.Xml, itemStr, System.Data.ParameterDirection.Input);

            Context.ExecuteNonQuery("sp_ImportPartConsignmentRecord", CommandType.StoredProcedure, paramItems);
        }
    }
}
