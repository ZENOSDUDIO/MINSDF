using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class Part
    {
        public Part()
        {
            _CycleCountTimes = DefaultValue.INT;
            _Available = true;
        }
        public string PlantName
        {
            get
            {
                return this.Plant.PlantName;
            }
        }

        //public string PartCategoryName
        //{
        //    get
        //    {
        //        return this.PartCategory.CategoryName;
        //    }
        //}

        //public string PartStatusName
        //{
        //    get
        //    {
        //        return this.PartStatus.StatusName;
        //    }
        //}

        //public string CycleCountLevelName
        //{
        //    get
        //    {
        //        return this.CycleCountLevel.LevelName;
        //    }
        //}
    }
}
