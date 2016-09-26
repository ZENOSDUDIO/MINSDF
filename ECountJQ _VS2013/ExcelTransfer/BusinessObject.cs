using ECount.Infrustructure.Data.Integration;

namespace ECount.Infrustructure.Data
{
    public class BusinessObject : DataItem
    {
        public string _TableName;
        public string[] _Keys;
        public BusinessObject(string tablename)
            : base(null)
        {
            _TableName = tablename;
        }
    }
}
