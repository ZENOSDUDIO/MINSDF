using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using SGM.ECountJQ.UPG.BLL.DBBase;

namespace SGM.ECountJQ.UPG.BLL
{
    [MapTable("View_StocktakeNotification", ConnName = "SGM.ECountJQ.UPG")]
    public class DiffAnalyse
    {
        #region Properties

        /// <summary>
        /// PN
        /// </summary>
        [DataObjectField(false, false, true, 100)]
        public string PartCode { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartChineseName { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        public string PartPlantCode { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        public string Workshops { get; set; }

        /// <summary>
        /// 工段
        /// </summary>
        public string Segments { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string RequestUser { get; set; }

        /// <summary>
        /// 申请类别
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 返修
        /// </summary>
        public string RepairAmount { get; set; }

        /// <summary>
        /// 外协
        /// </summary>
        public string CSMTAmount { get; set; }

        /// <summary>
        /// "RDC
        /// </summary>
        public string RDCAmount { get; set; }

        /// <summary>
        /// SGM
        /// </summary>
        public string SGMAmount { get; set; }

        /// <summary>
        /// UNIT PRICE
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 盘点级别
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 盘点日期
        /// </summary>
        public string PlanDate { get; set; }

        /// <summary>
        /// PA
        /// </summary>
        public string WorkLocation { get; set; }

        /// <summary>
        /// JIT
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 其中HOLD
        /// </summary>
        public string HOLD { get; set; }

        /// <summary>
        /// HOLD
        /// </summary>
        public string SysHOLD { get; set; }

        /// <summary>
        /// 分析
        /// </summary>
        public string UnRecorded { get; set; }

        /// <summary>
        /// WIP
        /// </summary>
        public string Wip { get; set; }

        /// <summary>
        /// M080
        /// </summary>
        public string M080 { get; set; }

        /// <summary>
        /// M60(block)
        /// </summary>
        public string Sys_Block_CSMT { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string Specs { get; set; }

        /// <summary>
        /// 应商名称
        /// </summary>
        public string SupplierName { get; set; }

        #endregion

        public string HelloWorld()
        {

            return "HelloWorld";
        }

        public DataTable GetAnalyseResult(List<int> details)
        {
            //string detailsStr = null;
            //if (details != null && details.Count > 0)
            //{
            //    string[] detailArray = details.Select(d => d.ToString()).ToArray();
            //    detailsStr = string.Join(",", detailArray);
            //}
            //DbParameter paramDetails = Context.CreateDbParameter("@details", DbType.String, detailsStr, ParameterDirection.Input);
            //DataTable dt = Context.LoadDataTable("sp_GetDiffAnalyseResult", CommandType.StoredProcedure, paramDetails);



            return null;
        }

        #region Fill

        private static void Fill(DiffAnalyse entity, IDataReader dr)
        {
            if (dr == null) return;

            entity.PartCode = Convert.IsDBNull(dr["PartCode"]) ? string.Empty : dr["PartCode"].ToString();
            entity.PartChineseName = Convert.IsDBNull(dr["PartChineseName"]) ? string.Empty : dr["PartChineseName"].ToString();
            entity.PartPlantCode = Convert.IsDBNull(dr["PartPlantCode"]) ? string.Empty : dr["PartPlantCode"].ToString();
            entity.Workshops = Convert.IsDBNull(dr["Workshops"]) ? string.Empty : dr["Workshops"].ToString();
            entity.Segments = Convert.IsDBNull(dr["Segments"]) ? string.Empty : dr["Segments"].ToString();
            entity.RequestUser = Convert.IsDBNull(dr["RequestUser"]) ? string.Empty : dr["RequestUser"].ToString();
            entity.TypeName = Convert.IsDBNull(dr["TypeName"]) ? string.Empty : dr["TypeName"].ToString();
            entity.RepairAmount = Convert.IsDBNull(dr["RepairAmount"]) ? string.Empty : dr["RepairAmount"].ToString();
            entity.CSMTAmount = Convert.IsDBNull(dr["CSMTAmount"]) ? string.Empty : dr["CSMTAmount"].ToString();
            entity.RDCAmount = Convert.IsDBNull(dr["RDCAmount"]) ? string.Empty : dr["RDCAmount"].ToString();
            entity.SGMAmount = Convert.IsDBNull(dr["SGMAmount"]) ? string.Empty : dr["SGMAmount"].ToString();
            entity.Price = Convert.IsDBNull(dr["Price"]) ? string.Empty : dr["Price"].ToString();
            entity.LevelName = Convert.IsDBNull(dr["LevelName"]) ? string.Empty : dr["LevelName"].ToString();
            entity.PlanDate = Convert.IsDBNull(dr["PlanDate"]) ? string.Empty : dr["PlanDate"].ToString();
            entity.WorkLocation = Convert.IsDBNull(dr["WorkLocation"]) ? string.Empty : dr["WorkLocation"].ToString();
            entity.CategoryName = Convert.IsDBNull(dr["CategoryName"]) ? string.Empty : dr["CategoryName"].ToString();
            entity.HOLD = Convert.IsDBNull(dr["HOLD"]) ? string.Empty : dr["HOLD"].ToString();
            entity.SysHOLD = Convert.IsDBNull(dr["SysHOLD"]) ? string.Empty : dr["SysHOLD"].ToString();
            entity.UnRecorded = Convert.IsDBNull(dr["UnRecorded"]) ? string.Empty : dr["UnRecorded"].ToString();
            entity.Wip = Convert.IsDBNull(dr["Wip"]) ? string.Empty : dr["Wip"].ToString();
            entity.M080 = Convert.IsDBNull(dr["M080"]) ? string.Empty : dr["M080"].ToString();
            entity.Sys_Block_CSMT = Convert.IsDBNull(dr["Sys_Block_CSMT"]) ? string.Empty : dr["Sys_Block_CSMT"].ToString();
            entity.Specs = Convert.IsDBNull(dr["Specs"]) ? string.Empty : dr["Specs"].ToString();
            entity.SupplierName = Convert.IsDBNull(dr["SupplierName"]) ? string.Empty : dr["SupplierName"].ToString();
        }

        private static void Fill(List<DiffAnalyse> list, IDataReader dr)
        {
            while (dr.Read())
            {
                DiffAnalyse entity = new DiffAnalyse();
                Fill(entity, dr);
                list.Add(entity);
            }
        }

        #endregion
    }
}
