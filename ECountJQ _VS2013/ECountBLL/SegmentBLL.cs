using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{

    public class SegmentBLL : BaseGenericBLL<Segment>
    {
        public SegmentBLL()
            : base("Segment")
        {

        }

        public List<Segment> GetSegments()
        {
            return _context.Segment.Include("Workshop.Plant").Where(s => s.Available == true).ToList();
        }

        public Segment GetSegmentbykey(Segment info)
        {
            info = this.Context.Segment.Include("Workshop.Plant").Include("Workshop").FirstOrDefault(s => s.SegmentID == info.SegmentID);
            return info;
        }

        public List<Segment> GetSegmentbyWorkshop(Workshop workshop)
        {
            List<Segment> segList = _context.Segment.Include("Workshop").Where(p => p.Workshop.WorkshopID == workshop.WorkshopID).ToList();
            if (segList != null && segList.Count > 0)
            {
                return segList;
            }
            return new List<Segment>();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="workshopID"></param>
        /// <returns></returns>
        public List<Segment> GetSegmentbyWorkshopID(int workshopID)
        {

            List<Segment> segList = _context.Segment.Include("Workshop").Where(p => p.Workshop.WorkshopID == workshopID).ToList();

            if (segList != null && segList.Count > 0)
            {
                return segList;
            }
            return new List<Segment>();
        }

        public List<Segment> GetSegmentsByWorkshopCodes(List<string> workshopCodes)
        {
            if (workshopCodes.Count <= 0)
            {
                return new List<Segment>();
            }
            string workShopCode = workshopCodes[0];
            IQueryable<Segment> segmentQry = Context.Segment.Where(s => s.Workshop.WorkshopCode == workShopCode);
            for (int i = 1; i < workshopCodes.Count; i++)
            {
                string code = workshopCodes[i];
                IQueryable<Segment> tmpQry = Context.Segment.Where(s => s.Workshop.WorkshopCode == code);
                segmentQry = segmentQry.Union(tmpQry);
            }
            return segmentQry.ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="workshopID"></param>
        /// <returns></returns>
        public List<ViewPlantWorkshopSegment> GetSegmentbyPlantID(int plantID)
        {

            IQueryable<ViewPlantWorkshopSegment> segList =
                from p in _context.ViewPlantWorkshopSegment
                where p.PlantID == plantID
                select p;
            if (segList != null)
            {
                return segList.ToList<ViewPlantWorkshopSegment>();
            }
            return new List<ViewPlantWorkshopSegment>();
        }

        public void UpdateSegment(Segment segment)
        {
            this.UpdateObject(segment);
        }

        public void DeleteSegment(Segment segment)
        {
            Segment segmentInfo = this.GetObjectByKey(segment);
            segmentInfo.Available = false;
            this.UpdateSegment(segmentInfo);
        }

        public void AddSegment(Segment segment)
        {
            this.AddObject(segment);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partID"></param>
        /// <returns></returns>
        //public List<PartSegment> GetPartSegmentByPartID(int partID)
        //{
        //    return _context.PartSegment.Include("Part").Include("Segment").Where(p => p.Part.PartID == partID).ToList();
        //}

        public IQueryable<Segment> QuerySegmentByPage(Segment segment)
        {
            var segmentQry = this.QuerySegments(segment).OrderBy(p => p.SegmentCode);
            return segmentQry;
        }

        public IQueryable<Segment> QuerySegments(Segment segment)
        {
            IQueryable<Segment> segmentQry = _context.Segment.Include("Workshop").Include("Workshop.Plant").Where(p => p.Available == true);
            if (segment != null)
            {
                Workshop workshop = new Workshop();
                if (!string.IsNullOrEmpty(segment.SegmentCode))
                {
                    segmentQry = segmentQry.Where(p => p.SegmentCode == segment.SegmentCode);
                }
                if (segment.Workshop != null && segment.Workshop.Plant != null)
                {
                    segmentQry = segmentQry.Where(p => p.Workshop.Plant.PlantCode == segment.Workshop.Plant.PlantCode);
                    if (!string.IsNullOrEmpty(segment.Workshop.WorkshopCode))
                    {
                        segmentQry = segmentQry.Where(p => p.Workshop.WorkshopCode == segment.Workshop.WorkshopCode);
                    }
                }
            }

            return segmentQry;
        }
    }
}
