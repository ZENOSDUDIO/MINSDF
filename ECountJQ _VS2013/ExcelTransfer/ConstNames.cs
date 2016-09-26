namespace ECount.Infrustructure.Data.Integration
{
    public static class ContextState
    {
        public const string DatabaseConnectionString = "connectionstring";
        public const string DatabaseTableName = "tablename";
        public const string DatabaseTruncateTable = "truncatetable";
        public const string FlatFileDelimiter = "delimiter";
        public const string FlatFileSkipFirstLine = "skipfirstline";
        public const string FlatFileSkipLastLine = "skiplastline";
        internal const string IntegrationMode = "providemode";
        public const string InvalidData = "invaliddata";
        public const string PerTransferCount = "pertransfercount";
        public const string SchemaFilePath = "schemafilepath";
        public const string SourcePath = "sourcePath";
        public const string TransferMode = "transfermode";
    }

    public static class ExceptionPolicyNames
    {
        public const string DefaultPolicy = "Default Policy";
    }

    public static class LogCategory
    {
        public const string General = "General";
        public const string Trace = "Trace";
    }

    public static class LogPriority
    {
        public const int High = 3;
        public const int Highest = 4;
        public const int Low = 1;
        public const int Lowest = 0;
        public const int Normal = 2;
    }
}