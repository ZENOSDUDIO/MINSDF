using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SGM.ECountJQ.UPG.BLL
{
    internal class DBHelper
    {
        public static Database CreateDatabase()
        {
            return DatabaseFactory.CreateDatabase("SGM.ECountJQ.UPG");
        }
    }
}
