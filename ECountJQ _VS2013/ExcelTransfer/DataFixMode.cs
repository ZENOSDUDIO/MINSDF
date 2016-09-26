namespace ECount.Infrustructure.Data.Integration
{
    internal static class DataFixMode
    {
        /// <summary>
        /// If record is bad data then try to fixed, and skipped when fixing failure
        /// </summary>
        public const string Fix = "fix";

        /// <summary>
        /// If record is bad data always raising exception
        /// </summary>
        public const string RaiseError = "raiseerror";

        /// <summary>
        /// If record is bad data then skipped
        /// </summary>
        public const string Skip = "skip";

        public static bool IsValid(string mode)
        {
            if (mode.Equals(Skip) ||
                mode.Equals(Fix) ||
                mode.Equals(RaiseError))
                return true;
            return false;
        }
    }
}