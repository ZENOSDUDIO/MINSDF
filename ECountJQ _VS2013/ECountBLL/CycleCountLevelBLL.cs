using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;
using System.Globalization;
using System.Data.EntityClient;

namespace SGM.ECount.BLL
{
    public class CycleCountLevelBLL : BaseGenericBLL<CycleCountLevel>
    {
        public CycleCountLevelBLL()
            : base("CycleCountLevel")
        {

        }
        public List<CycleCountLevel> GetCycleCountLevel()
        {
            return GetObjects().Where(l => l.Available == true).ToList();
        }

        public List<CycleCountLevel> GetCycleCountLevel(CycleCountLevel info)
        {
            IQueryable<CycleCountLevel> query = _context.CycleCountLevel.Where(l => l.Available == true);

            if (info != null && !string.IsNullOrEmpty(info.LevelName))
            {
                query = query.Where(p => p.LevelName == info.LevelName);
            }

            return query.ToList();
        }


        public CycleCountLevel GetCycleCountLevelByKey(CycleCountLevel info)
        {
            return GetObjectByKey(info);
        }


        public CycleCountLevel AddCycleCountLevel(CycleCountLevel model)
        {
            AddObject(model);
            return model;
        }

        public void DeleteCycleCountLevel(CycleCountLevel level)
        {
            level = GetCycleCountLevelByKey(level);
            level.Available = false;
            UpdateCycleCountLevel(level);
            //DeleteObject(level, true);
        }

        public void DeleteCycleCountLevels(List<string> ids)
        {
            StringBuilder sbSql = new StringBuilder();
            foreach (string id in ids)
            {
                //sbSql.Append(string.Format("Delete CycleCountLevel where LevelID='{0}';", id));
                sbSql.Append(string.Format("UPDATE CycleCountLevel SET Available=0 where LevelID='{0}';", id));
            }

            Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, true);

        }

        public void UpdateCycleCountLevel(CycleCountLevel level)
        {
            UpdateObject(level, true);
        }

        public int GetCycledTimes(CycleCountLevel level)
        {
            DateTime tmpDate = new DateTime(DateTime.Now.Year, 12, 31);

            int totalWeeksInYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(tmpDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int currentWeekInYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int weeksPerCycle = totalWeeksInYear / level.times.Value;
            int cycledTimes = ((currentWeekInYear - 1) / weeksPerCycle) + 1;
            return cycledTimes;
        }

    }
}
