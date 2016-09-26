using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.Common.Utility
{
    public struct Consts
    {
        public const string CACHE_KEY_PRIORITY = "Priorities";
        public const string CACHE_KEY_STOCKTAKETYPES = "StocktakeTypes";
        public const string CACHE_KEY_STORELOCATION_TYPES = "StoreLocationTypes";
        public const string CACHE_KEY_PLANT = "Plant";
        public const string CACHE_KEY_CYCLE_COUNT_LEVEL = "CycleCountLevel";
        public const string CACHE_KEY_PART_CATEGORY = "PartCategory";
        public const string CACHE_KEY_PART_STATUS = "PartStatus";
        public const string CACHE_KEY_SUPPLIER = "Supplier"; 
        public const string CACHE_KEY_STOCKTAKE_STATUS = "StocktakeStatus";
        public const string CACHE_KEY_USER_GROUPS = "UserGroup";
        public const string CACHE_KEY_STORE_LOCATION = "StoreLocation";
        public const string CACHE_KEY_BIZ_PARAMS = "BizParams";
        public const string BIZ_PARAMS_CYCLECOUNTED = "CycleCounted";
        public const string BIZ_PARAMS_CYCLEDTIMES = "CycledTimes";
        public const int STOCKTAKE_NEW_REQUEST = 1;
        public const int STOCKTAKE_NEW_NOTIFICATION = 2;
        public const int STOCKTAKE_PUBLISHED = 3;
        public const int STOCKTAKE_COUNTING = 4;
        public const int STOCKTAKE_COMPLETE = 5;
        public const int STOCKTAKE_NEW_ANALYSIS = 6;
        public const int STOCKTAKE_ANALYZING = 7;
        public const int STOCKTAKE_ANALYSIS_COMPLETE = 8;
        public const string DROPDOWN_UNSELECTED_TEXT = "--";

    }
}
