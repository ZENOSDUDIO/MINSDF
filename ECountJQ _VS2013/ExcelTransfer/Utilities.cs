using System;
using System.Data;
using System.IO;
using ECount.Infrustructure.Utilities;

namespace ECount.Infrustructure.Data.Integration
{
    internal static class Utilities
    {
        /// <summary>
        /// in case trim 
        /// </summary>
        /// <param name="itemfield"></param>
        public static void AdjustDataItemFieldValue(DataItemField itemfield)
        {
            if (itemfield.Value == null)
                return;

            if (itemfield.Type.Equals(typeof(int).ToString()))
            {
                int val1;
                if (StringUtil.TryParseInt32(itemfield.Value.ToString(), out val1))
                    itemfield.Value = val1;
                else
                    throw new InvalidDataException(
                        string.Format("fields value [{0}] can not parse as type of schema: name [{1}] index[{2}] type [{3}]",
                                      itemfield.Value,
                                      itemfield.Name, itemfield.Index, itemfield.Type));
            }
            else if (itemfield.Type.Equals(typeof(double).ToString()))
            {
                double val2;
                if (StringUtil.TryParseDouble(itemfield.Value.ToString(), out val2))
                    itemfield.Value = val2;
                else
                    throw new InvalidDataException(
                        string.Format("fields value [{0}] can not parse as type of schema: name [{1}] index[{2}] type [{3}]",
                                      itemfield.Value,
                                      itemfield.Name, itemfield.Index, itemfield.Type));
            }
            else if (itemfield.Type.Equals(typeof(DateTime).ToString()))
            {
                DateTime val3;
                if (StringUtil.TryParseDateTime(itemfield.Value.ToString(), out val3))
                    itemfield.Value = val3;
                else
                    throw new InvalidDataException(
                        string.Format("fields value [{0}] can not parse as type of schema: name [{1}] index[{2}] type [{3}]",
                                      itemfield.Value,
                                      itemfield.Name, itemfield.Index, itemfield.Type));
            }
            else if (itemfield.Type.Equals(typeof(string).ToString()) && itemfield.Value != null)
                itemfield.Value = itemfield.Value.ToString().Trim();
            else
            {
                // TODO: handle more types
            }
        }

        public static DbType GetDbType()
        {
            return DbType.AnsiString;
        }
    }
}