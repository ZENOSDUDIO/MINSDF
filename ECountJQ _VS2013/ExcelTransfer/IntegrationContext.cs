using System.Collections;
using System.Text;

namespace ECount.Infrustructure.Data.Integration
{
    internal class IntegrationContext : IContext,System.IDisposable
    {
        private DataItemCollection _data = new DataItemCollection();
        private bool _haltTransferWhenHasSkippedData = false;
        private int _rowIndex = 0;
        private DataSchema _schema = null;
        private DataItemCollection _skippeddata = new DataItemCollection();
        private IDictionary _state;
        public IntegrationStatus Status = IntegrationStatus.Running;

        public IntegrationContext(IDictionary state)
        {
            _state = state;
        }

        public int SuccessCount
        {
            get
            {
                if (_data == null)
                    return 0;
                return _data.Count;
            }
        }

        public int SkippedCount
        {
            get
            {
                if (_skippeddata == null)
                    return 0;
                return _skippeddata.Count;
            }
        }

        public DataItemCollection SkippedData
        {
            get { return _skippeddata; }
            internal set { _skippeddata = value; }
        }

        public DataItemCollection Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public DataSchema Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        public bool HaltTransferWhenHasSkippedData
        {
            get { return _haltTransferWhenHasSkippedData; }
            set { _haltTransferWhenHasSkippedData = value; }
        }

        public string Message
        {
            get
            {
                if (SkippedCount > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("共有{0}条数据验证失败.&", SkippedCount);
                    foreach (DataItem item in SkippedData)
                    {
                        sb.AppendFormat("第{0}行数据验证失败, 详细信息:{1}|", item.RowIndex, item.ValidationMessage);
                    }
                    return sb.ToString().TrimEnd('|');
                }
                return string.Empty;
            }
        }

        #region IContext Members

        public IDictionary State
        {
            get { return _state; }
            set { _state = value; }
        }

        #endregion

        public void AddData(DataItem item)
        {
            // schema validation passed, but we need do custom rule validation here
            bool valid = true;
            if (Schema.Rules != null)
            {
                Schema.IndexRules();
                foreach (DataSchemaRule rule in Schema.Rules)
                {
                    if (!valid)
                        break;
                    // TODO: add param value of code here, by alex@20080820
                    valid = rule.ExecuteCommand(item.ValidationResults, item, _state);
                }
            }
            if (!valid)
            {
                AddSkippedData(item);
                return;
            }

            item.RowIndex = ++_rowIndex;
            _data.Add(item);
        }

        public void AddSkippedData(DataItem item)
        {
            item.RowIndex = ++_rowIndex;
            _skippeddata.Add(item);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Data.Clear();
            this.SkippedData.Clear();
        }

        #endregion
    }

    internal enum IntegrationStatus
    {
        Success,
        Cancel,
        Failure,
        Completed,
        Running
    }
}