using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data.Objects;
using SGM.ECount.BLL;
using System.Transactions;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Data.Extensions;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using System.Linq.Expressions;
using SGM.Common.Utility;

namespace SGM.ECount.BLL
{
    public class PartBLL : BaseGenericBLL<Part>
    {
        public PartBLL()
            : base("Part")
        {

        }
        public List<Part> GetPartsbyUser(User user)
        {
            IQueryable<Part> partsQry = _context.Part.Include("CycleCountLevel").Include("PartCategory").Include("PartStatus");
            if (user.Plant!=null)
            {
                partsQry=partsQry.Where(p => p.Plant.PlantID == user.Plant.PlantID);
            }

            List<Part> partList = partsQry.ToList();
            if (partList != null && partList.Count > 0)
            {
                return partList;
            }
            return new List<Part>();
        }


        public IQueryable<ViewPart> QueryParts(Part part, IQueryable<ViewPart> partQry)
        {
            if (part != null)
            {
                if (!string.IsNullOrEmpty(part.PartCode))
                {
                    partQry = partQry.Where(p => p.PartCode == part.PartCode);
                }
                if (!string.IsNullOrEmpty(part.PartChineseName))
                {
                    partQry = partQry.Where(p => p.PartChineseName == part.PartChineseName);
                }
                //if (part.PartGroup != null && part.PartGroup.GroupID != DefaultValue.INT)
                //{
                //    int groupID = part.PartGroup.GroupID;
                //    partQry = partQry.Where(p => p.GroupID == groupID);
                //}

                if (part.UpdateBy != null && !string.IsNullOrEmpty(part.UpdateBy.UserName))
                {
                    partQry = partQry.Where(p => p.UserName == part.UpdateBy.UserName);
                }

                if (part.Plant != null)
                {
                    if (part.Plant.PlantID > 0)
                    {
                        partQry = partQry.Where(p => p.PlantID == part.Plant.PlantID);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(part.Plant.PlantCode))
                        {
                            partQry = partQry.Where(p => p.PlantCode == part.Plant.PlantCode);
                        }
                    }
                }
                //if (part.PartSegments != null)
                //{
                //    List<PartSegment> partSegments = part.PartSegments.ToList();
                //    if (partSegments.Count > 0)
                //    {
                //        //by segment 
                //        if (partSegments[0].Segment.SegmentID != DefaultValue.INT)
                //        {
                //            int segmentID = partSegments[0].Segment.SegmentID;
                //            partQry = from q in partQry join ps in _context.ViewPlantWorkshopSegment on q.PlantID equals ps.PlantID where ps.SegmentID == segmentID select q;
                //        }
                //        //by workshop
                //        if (partSegments[0].Segment != null && partSegments[0].Segment.Workshop != null && partSegments[0].Segment.Workshop.WorkshopID != DefaultValue.INT)
                //        {
                //            int workshopID = partSegments[0].Segment.Workshop.WorkshopID;
                //            partQry = from q in partQry join ps in _context.ViewPlantWorkshopSegment on q.PlantID equals ps.PlantID where ps.WorkShopID == workshopID select q;
                //        }
                //    }
                //}
                if (!string.IsNullOrEmpty(part.Workshops))
                {
                    string workshop = part.Workshops;
                    //partQry = partQry.Where(p => p.Workshops == workshop || p.Workshops.Contains("," + workshop) || p.Workshops.Contains(workshop + ","));

                    partQry = partQry.Where(p => ("," + p.Workshops + ",").Contains("," + workshop + ","));

                }
                if (!string.IsNullOrEmpty(part.Segments))
                {
                    string segment = part.Segments;
                    partQry = partQry.Where(p => p.Segments == segment || p.Segments.Contains("," + segment) || p.Segments.Contains(segment + ","));
                }
                if (!string.IsNullOrEmpty(part.WorkLocation))
                {
                    partQry = partQry.Where(p => p.WorkLocation == part.WorkLocation || p.WorkLocation.Contains("," + part.WorkLocation) || p.WorkLocation.Contains(part.WorkLocation + ","));
                }
                if (part.Supplier != null && !string.IsNullOrEmpty(part.Supplier.DUNS))
                {
                    partQry = partQry.Where(p => p.DUNS == part.Supplier.DUNS);
                }
                if (!string.IsNullOrEmpty(part.FollowUp))
                {
                    partQry = partQry.Where(p => p.FollowUp == part.FollowUp);
                }
                if (!string.IsNullOrEmpty(part.Specs))
                {
                    partQry = partQry.Where(p => p.Specs == part.Specs);
                }
                if (part.PartCategory != null && part.PartCategory.CategoryID != DefaultValue.INT)
                {
                    partQry = partQry.Where(p => p.CategoryID == part.PartCategory.CategoryID);
                }
                if (part.PartStatus != null && part.PartStatus.StatusID != DefaultValue.INT)
                {
                    partQry = partQry.Where(p => p.PartStatus == part.PartStatus.StatusID);
                }
                if (part.CycleCountLevel != null && part.CycleCountLevel.LevelID != DefaultValue.INT)
                {
                    partQry = partQry.Where(p => p.CycleCountLevel == part.CycleCountLevel.LevelID);
                }
                partQry = partQry.Where(p => p.Available == true);
            }
            return partQry;
        }

        public IQueryable<ViewPart> QueryParts(Part part)
        {
            IQueryable<ViewPart> partQry = Context.ViewPart;

            return QueryParts(part, partQry);
        }

        public IQueryable<ViewPart> QueryPartsByGroup(PartGroup group)
        {
            var groupedPartQry = Context.GroupPartRelation.Where(gp => gp.PartGroup.GroupID == group.GroupID).Select(gp => gp.Part);
            var partQry = from gp in groupedPartQry join p in Context.ViewPart on gp.PartID equals p.PartID select p;
            return partQry;
        }

        private class ViewPartEqualityComparer : IEqualityComparer<ViewPart>
        {
            public static readonly ViewPartEqualityComparer Instance = new ViewPartEqualityComparer();
            private ViewPartEqualityComparer()
            { }

            #region IEqualityComparer<ViewPart> Members

            public bool Equals(ViewPart x, ViewPart y)
            {
                if (!(x == null || y == null) && x.PartID == y.PartID)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(ViewPart obj)
            {
                return obj.PartID.GetHashCode();
            }

            #endregion
        }

        public IQueryable<ViewPart> QueryFilteredParts(Part part, List<ViewPart> excludedParts)
        {
            IQueryable<ViewPart> partQry = QueryParts(part);
            if (excludedParts != null && excludedParts.Count > 0)
            {
                foreach (var item in excludedParts)
                {
                    partQry = partQry.Where(p => p.PartID != item.PartID);
                }
                //List<ViewPart> tmpList = partQry.ToList();
                //partQry = tmpList.Except(excludedParts, ViewPartEqualityComparer.Instance).AsQueryable();

            }
            return partQry;
        }


        public IQueryable<ViewPart> QueryFilteredParts(View_StocktakeDetails filter, List<ViewPart> filterParts)
        {
            Part part = new Part
            {
                PartCode = filter.PartCode,
                PartChineseName = filter.PartChineseName,
                FollowUp = filter.FollowUp,
                Specs = filter.Specs
            };
            if (filter.PlantID != null)
            {
                part.Plant = new Plant
                {
                    PlantID = filter.PlantID.Value
                };
            }

            var partQry = QueryParts(part);
            //exclude parts in given request 
            if (filter != null && filter.RequestID != null)
            {
                List<View_StocktakeDetails> requestDetails = Context.View_StocktakeDetails.Where(d => d.RequestID == filter.RequestID).ToList(); ;
                var exceptParts = requestDetails.Select(d => new ViewPart { PartID = d.PartID.Value });
                List<ViewPart> exceptPartList = exceptParts.ToList();

                for (int i = filterParts.Count - 1; i >= 0; i--)
                {
                    ViewPart tmpPart = filterParts[i];
                    int index = exceptPartList.FindIndex(p => p.PartID == tmpPart.PartID);
                    if (index >= 0)
                    {
                        filterParts.RemoveAt(i);
                        exceptPartList.RemoveAt(index);
                    }
                }
                foreach (var item in filterParts)
                {
                    exceptPartList.Add(item);
                }

                //partQry = (from p in partQry join d in Context.View_StocktakeDetails on p.PartID equals d.PartID where d.RequestID != filter.RequestID select p).Distinct();
                //var removedPartsQry = filterParts.AsQueryable();
                //removedPartsQry = (from f in removedPartsQry join d in Context.View_StocktakeDetails on f.PartID equals d.PartID where d.RequestID == filter.RequestID select f).Distinct();
                //partQry = partQry.Union(removedPartsQry);

                //var addedPartsQry = filterParts.AsQueryable().Except(removedPartsQry);
                //List<ViewPart> addeParts = addedPartsQry.ToList();
                if (exceptPartList != null && exceptPartList.Count > 0)
                {
                    foreach (var item in exceptPartList)
                    {
                        int partID = item.PartID;
                        partQry = partQry.Where(p => p.PartID != partID);
                    }
                }
                //if (filterParts != null && filterParts.Count > 0)
                //{
                //    partQry = partQry.Union(filterParts).Distinct();
                //}
            }
            else
            {
                if (filterParts != null && filterParts.Count > 0)
                {
                    foreach (var item in filterParts)
                    {
                        int partID = item.PartID;
                        partQry = partQry.Where(p => p.PartID != partID);
                    }
                }
            }

            return partQry;
        }

        public IQueryable<ViewPart> QueryUngroupedPartsByPage(Part part, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            var partQry = this.QueryUngroupedParts(part).OrderBy(p => p.PartCode);
            return this.GetQueryByPage(partQry, pageSize, pageNumber, out pageCount, out itemCount);
        }

        public IQueryable<ViewPart> QueryFilteredPartsByPage(Part part, List<ViewPart> excludedParts, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<ViewPart> partQry = QueryFilteredParts(part, excludedParts).OrderBy(p => p.PartCode);

            return this.GetQueryByPage(partQry, pageSize, pageNumber, out pageCount, out itemCount);

        }


        public IQueryable<ViewPart> QueryFilteredPartsByPage(View_StocktakeDetails filter, List<ViewPart> filteredParts, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<ViewPart> partQry = QueryFilteredParts(filter, filteredParts).OrderBy(p => p.PartCode);

            return this.GetQueryByPage(partQry, pageSize, pageNumber, out pageCount, out itemCount);

        }

        public IQueryable<ViewPart> QueryUngroupedParts(Part part)
        {
            IQueryable<ViewPart> partQry = QueryParts(part);
            partQry = partQry.Where(p => p.GroupID == null);
            return partQry;
        }

        public IQueryable<ViewPart> QueryPartByPage(Part part, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            var partQry = this.QueryParts(part).OrderBy(p => p.PartCode);
            return this.GetQueryByPage(partQry, pageSize, pageNumber, out pageCount, out itemCount);
        }

        public IQueryable<ViewPart> QueryPartCodeScope(Part part, string startCode, string endCode)//, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            var partQry = this.QueryParts(part);
            if (!string.IsNullOrEmpty(startCode))
            {
                partQry = partQry.Where(p => string.Compare(p.PartCode, startCode) >= 0);
            }
            if (!string.IsNullOrEmpty(endCode))
            {
                partQry = partQry.Where(p => string.Compare(p.PartCode, endCode) <= 0);
            }
            return partQry.OrderBy(p => p.PartCode);
            //return this.GetQueryByPage(partQry, pageSize, pageNumber, out pageCount, out itemCount);
        }



        public List<ViewPart> QueryUnRequestedPartByPage(StocktakeRequest request, Part filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            itemCount = DefaultValue.INT;
            //var partQry = this.QueryParts(filter);
            ObjectParameter p = new ObjectParameter("itemCount", itemCount);
            pageCount = (itemCount - 1) / pageSize + 1;
            Context.CommandTimeout = 120;
            int? plantID = null;
            if (filter.Plant != null)
            {
                plantID = filter.Plant.PlantID;
            }
            long? requestID = null;
            if (request != null)
            {
                requestID = request.RequestID;
            }
            List<ViewPart> result = Context.GetUnrequestedParts(requestID, pageSize, pageNumber, p, filter.PartCode, filter.PartChineseName, plantID, filter.FollowUp, filter.Specs).ToList();
            itemCount = (int)p.Value;
            return result;
            //partQry = this.QueryParts(filter, partQry);
            //partQry = from p in partQry join q in qry on p.PartID equals q.PartID select p;
            //var detailsQry = (from d in Context.View_StocktakeDetails join p in Context.ViewPart on d.PartID equals p.PartID where d.RequestID == request.RequestID select p.PartID).ToList().AsQueryable();
            //var pidQry = partQry.Select(p => p.PartID).ToList().AsQueryable();
            //pidQry = pidQry.Except(detailsQry);

            //partQry = partQry.Where(BuildContainsExpression<ViewPart, int>(p => p.PartID, pidQry.ToList()));

            //partQry = from p in partQry join i in pidQry on p.PartID equals i select p;
            //var qry = from d in Context.Part
            //          select new
            //          {
            //              Part = d,
            //              Details = Context.StocktakeDetails.Where(dt => dt.StocktakeRequest.RequestID == request.RequestID).FirstOrDefault(dt => dt.Part.PartID == d.PartID)
            //          };
            //var partQry1 = this.QueryParts(filter);
            //if (request != null)
            //{
            //    partQry = from v in partQry join p in qry on v.PartID equals p.Part.PartID where p.Details == null select v;//from v in partQry join p in Context.Part.Include("StocktakeDetails") on v.PartID equals p.PartID where p.StocktakeDetails.Count == 0 select v;
            //    //var detailsQry = Context.StocktakeDetails.Where(d => d.StocktakeRequest.RequestID != request.RequestID).Select(d => d.Part).Distinct();
            //    //var detailsPartQry = (from d in Context.StocktakeDetails join p in partQry1 on d.Part.PartID equals p.PartID where d.StocktakeRequest.RequestID != request.RequestID select p).Distinct();
            //    //var detailsQry = Context.View_StocktakeDetails.Where(d => d.RequestID != request.RequestID).Select(d => d.PartID.Value ).Distinct();
            //    //var detailsPartQry = from p in Context.ViewPart join d in detailsQry on p.PartID equals d select p;
            //    //partQry = partQry.Union(detailsPartQry);

            //}

            //if (request != null)
            //{
            //    var detailsQry = Context.StocktakeDetails.AsQueryable();
            //    long requestID = request.RequestID;
            //    //details of request
            //    detailsQry = from d in detailsQry where d.StocktakeRequest.RequestID == requestID select d;

            //    //get parts not in this request
            //    var qry = from p in partQry join d in detailsQry on p.PartID equals d.Part.PartID into detailParts select new { p, cnt=detailParts.Count() };
            //    partQry = qry.Where(pd => pd.cnt == 0).Select(pd => pd.p);
            //    //partQry = (from v in partQry
            //               //join p in pQry
            //               //    on v.PartID equals p.PartID
            //               //select v);
            //}
            // partQry = partQry.OrderBy(p => p.PartCode);
            //var result = (from p in partQry join d in Context.StocktakeDetails on p.PartID equals d.Part.PartID into joinedParts select new { p, Count=joinedParts.Count() }).Where(pd=>pd.Count==0);//from joinedPart in joinedParts.DefaultIfEmpty() select new { p, joinedPart });//
            //partQry = result.Select(pd => pd.p).OrderBy(p => p.PartCode);// result.Where(pd => pd.joinedPart == null).Select(pd => pd.p).OrderBy(p => p.PartCode); 

            //return (ObjectQuery<ViewPart>)this.GetQueryByPage(partQry, pageSize, pageNumber, out pageCount, out itemCount);
        }


        public List<Part> GetParts()
        {
            return _context.Part.Where(p => p.Available == true).Include("CycleCountLevel").Include("PartCategory").Include("PartStatus").Include("Plant").ToList();
        }

        public ViewPart GetViewPartByKey(string partID)
        {
            //Part part = new Part { PartID = new Guid(partID) };
            int pid = int.Parse(partID);
            return _context.ViewPart.Where(v => v.PartID == pid).ToList()[0];
        }

        public List<ViewPart> GetViewPartsByPartIDs(List<string> partids)
        {
            List<ViewPart> vps = new List<ViewPart>();
            foreach (string partid in partids)
            {
                vps.Add(GetViewPartByKey(partid));
            }
            return vps;
        }

        public Part GetPartbyKey(Part part)
        {
            //if (!part.PlantReference.IsLoaded)
            //{
            //    part.PlantReference.Load();
            //}
            part = GetObjectByKey(part);
            part.Groups.Load();

            return part;
            //return _context.Part.Include("Plant").Include("CycleCountLevel").Include("PartCategory").Include("PartStatus").FirstOrDefault(p => p.PartID == part.PartID);
        }

        public IQueryable<PartGroup> GetGroupsByPart(Part part)
        {
            return Context.GroupPartRelation.Where(gp => gp.Part.PartID == part.PartID).Select(gp => gp.PartGroup);
        }

        public IQueryable<Part> GetPartsOfSameGroups(Part part)
        {
            var qry = (from g in Context.GroupPartRelation.Where(gp => gp.Part.PartID == part.PartID).Select(gp => gp.PartGroup) join gp in Context.GroupPartRelation on g.GroupID equals gp.PartGroup.GroupID select gp.Part).Distinct();
            return qry;
        }

        public void DeletePart(Part part)
        {            
            part = GetPartbyKey(part);
            part.Groups.Clear();
            part.Available = false;
            //part.PartGroup = null;

            UpdatePart(part);
        }

        public List<Part> GetRelatedParts(Part part)
        {
            part = this.GetPartbyKey(part);
            List<PartGroup> groups = GetGroupsByPart(part).ToList();
            var query = _context.Part.Include("CycleCountLevel").Include("PartCategory").Include("PartStatus").Include("Plant").Include("Groups").Include("Supplier").Where(p => p.PartID == part.PartID && p.ConsignmentPartRecords.Count != 0 && p.Available == true);
            if (groups == null || groups.Count == 0)
            {
                return query.ToList();
            }
            else
            {
                int partID = part.PartID;
                string partCode = part.PartCode;
                int groupID = groups[0].GroupID;

                IQueryable<Part> groupParts = GetPartsOfSameGroups(part);
                //    Context.GroupPartRelation.Where(gp => gp.PartGroup.GroupID == groupID).Select(gp => gp.Part);
                //for (int i = 1; i < groups.Count; i++)
                //{
                //    groupID = groups[i].GroupID;
                //    var tmpParts = Context.GroupPartRelation.Where(gp => gp.PartGroup.GroupID == groupID&&gp.Part.PartID!=partID).Select(gp => gp.Part);
                //    groupParts.Union(tmpParts);
                //}
                //groupParts = groupParts.Distinct();
                // groups.SelectMany(g => g.Parts).Distinct();
                ////var groupParts = Context.GroupPartRelation.Where(gp => gp.Part.PartID == partID).Select(gp => gp.Part).Distinct();
                
                groupParts = (from p in Context.Part.Include("CycleCountLevel").Include("PartCategory").Include("PartStatus").Include("Plant").Include("Groups").Include("Supplier")
                              join gp in groupParts on p.PartID equals gp.PartID
                             select p);
                query = query.Union(groupParts).Distinct();
                return query.ToList();
            }
            ////var query = from p in _context.Part
            //            where (
            //            p.PartGroup.GroupID == part.PartGroup.GroupID
            //            || p.PartCode == part.PartCode
            //            && p.ConsignmentPartRecords.Count != 0
            //            )
            //            select new Part { Available=p.Available, CycleCountLevel=p.CycleCountLevel, PartCode=p.PartCode, PartGroup=p.PartGroup, Supplier=p.Supplier, Plant=p.Plant, PartStatus=p.PartStatus, PartCategory= p.PartCategory, CycleCountTimes=p.CycleCountTimes, DateModified=p.DateModified, Description=p.Description, FollowUp=p.FollowUp, PartChineseName=p.PartChineseName, PartEnglishName=p.PartEnglishName, PartID=p.PartID, Specs=p.Specs, UpdateBy=p.UpdateBy, WorkLocation=p.WorkLocation};
            ////List<Part> relatedParts = query.Cast<Part>().ToList();
            //List<Part> relatedParts = query.ToList();
            //return relatedParts;
        }

        public Part UpdatePart(Part part)
        {
            List<PartGroup> groups = part.Groups.Select(g => g.PartGroup).ToList();
            part.Groups.Clear();
            part.DateModified = DateTime.Now;
            if (part.EntityState != EntityState.Detached)
            {
                _context.Detach(part);
            }
            this.UpdateObject(part, false);
            _context.SaveChanges();

            string partGroupStr = Utils.SerializeToString( groups);
            return Context.AddPartToGroups(partGroupStr, part.PartID).ToList()[0];//this.GetPartbyKey(part);
            //return part;
        }

        private Part UpdatePart2(Part part)
        {
            UpdateObject(part);
            return part;
        }

        public Part AddPart2(Part part)
        {
            part = this.AddObject(part, true);
            return part;
        }

        public Part AddPart(Part part)
        {
            List<PartGroup> groups = part.Groups.Select(g=>g.PartGroup).ToList();
            part.Groups.Clear();
            part = this.AddObject(part);
           
            string partGroupStr = Utils.SerializeToString( groups);
            return Context.AddPartToGroups(partGroupStr, part.PartID).ToList()[0];

        }

        public List<ViewPart> GetPartsToCycleCount(int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            IQueryable<ViewPart> queryResult = this.GetPartsByPlantToCycleCount();//this.GetPartsToCycleCount();
            pageCount = 0;
            itemCount = 0;
            List<ViewPart> partsToCycleCount = new List<ViewPart>();
            if (queryResult != null)
            {
                queryResult = this.GetQueryByPage(queryResult, pageSize, pageNumber, out pageCount, out itemCount);
                partsToCycleCount.AddRange(queryResult);
            }
            return partsToCycleCount;
        }

        public IQueryable<ViewPart> GetPartsToCycleCount()
        {
            DateTime tmpDate = new DateTime(DateTime.Now.Year, 12, 31);

            int totalWeeksInYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(tmpDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int currentWeekInYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            CycleCountLevelBLL levelBLL = new CycleCountLevelBLL();
            List<CycleCountLevel> levelList = levelBLL.GetCycleCountLevel();
            List<ViewPart> partsToCycleCount = new List<ViewPart>();
            IQueryable<ViewPart> queryResult = null;
            foreach (var level in levelList)
            {

                int weeksPerCycle = totalWeeksInYear / level.times.Value;
                int restWeeksOfCurrentCycle = weeksPerCycle - (currentWeekInYear - 1) % weeksPerCycle;
                int cycledTimes = ((currentWeekInYear - 1) / weeksPerCycle) + 1;
                int levelId = level.LevelID;
                //query parts to be cycle counted
                IQueryable<ViewPart> partsToCycleCountQry = Context.ViewPart.Where(p => p.CycleCountLevel.Value == levelId && p.CycleCountTimes < cycledTimes).OrderBy(p => p.PartCode).ThenBy(p => p.PlantCode).ThenBy(p => p.DUNS);
                // get number of parts are not cycle counted yet
                int numberOfPartsToCycleCount = partsToCycleCountQry.Count();
                if (numberOfPartsToCycleCount > 0)
                {
                    if (restWeeksOfCurrentCycle > 1)//not last week of current cycle,average numbers
                    {
                        partsToCycleCountQry = partsToCycleCountQry.Take(numberOfPartsToCycleCount / restWeeksOfCurrentCycle);
                    }
                    if (queryResult == null)
                    {
                        queryResult = partsToCycleCountQry;
                    }
                    else
                    {
                        queryResult = queryResult.Union(partsToCycleCountQry);
                    }
                }
            }
            if (queryResult != null)
            {
                queryResult = queryResult.OrderBy(p => p.PartCode).ThenBy(p => p.PlantCode).ThenBy(p => p.DUNS);
            }
            return queryResult;
        }

        public IQueryable<ViewPart> GetPartsByPlantToCycleCount()
        {
            PlantBLL plantBll = new PlantBLL();
            List<Plant> plantList = plantBll.GetPlants();

            DateTime tmpDate = new DateTime(DateTime.Now.Year, 12, 31);

            int totalWeeksInYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(tmpDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            int currentWeekInYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            CycleCountLevelBLL levelBLL = new CycleCountLevelBLL();
            List<CycleCountLevel> levelList = levelBLL.GetCycleCountLevel();
            List<ViewPart> partsToCycleCount = new List<ViewPart>();
            IQueryable<ViewPart> queryResult = null;
            foreach (var level in levelList)
            {

                int weeksPerCycle = totalWeeksInYear / level.times.Value;
                int restWeeksOfCurrentCycle = weeksPerCycle - (currentWeekInYear - 1) % weeksPerCycle;
                int cycledTimes = ((currentWeekInYear - 1) / weeksPerCycle) + 1;
                int levelId = level.LevelID;
                foreach (var plant in plantList)
                {
                    int plantID = plant.PlantID;
                    //query parts to be cycle counted
                    IQueryable<ViewPart> partsToCycleCountQry = Context.ViewPart.Where(p => p.CycleCount == true && p.CycleCountLevel.Value == levelId && p.CycleCountTimes < cycledTimes && p.PlantID == plantID&&p.Available == true).OrderBy(p => p.PartCode).ThenBy(p => p.DUNS);
                    // get number of parts are not cycle counted yet
                    int numberOfPartsToCycleCount = partsToCycleCountQry.Count();
                    if (numberOfPartsToCycleCount > 0)
                    {
                        if (restWeeksOfCurrentCycle > 1)//not last week of current cycle,average numbers
                        {
                            partsToCycleCountQry = partsToCycleCountQry.Take(numberOfPartsToCycleCount / restWeeksOfCurrentCycle);
                        }
                        if (queryResult == null)
                        {
                            queryResult = partsToCycleCountQry;
                        }
                        else
                        {
                            queryResult = queryResult.Union(partsToCycleCountQry);
                        }
                    }
                }
            }
            if (queryResult != null)
            {
                queryResult = queryResult.OrderBy(p => p.PartCode).ThenBy(p => p.PlantCode).ThenBy(p => p.DUNS);
            }
            return queryResult;
        }

        public List<ViewPart> QueryPartsByKey(List<ViewPart> list)
        {
            Type[] types = new Type[] { typeof(ViewPart) };
            XmlSerializer xs = new XmlSerializer(typeof(List<ViewPart>), types);
            string itemStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, list);
                itemStr = sw.ToString();
            }
            DbParameter paramItems = Context.CreateDbParameter("@parts", System.Data.DbType.Xml, itemStr, System.Data.ParameterDirection.Input);
            //DbDataReader reader = 
            DataTable dt = Context.LoadDataTable("sp_GetPartsByKey", CommandType.StoredProcedure, paramItems);
            DataSet ds = new DataSet();
            list = new List<ViewPart>();
            //while (reader.Read())
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new ViewPart
                {
                    PartID = int.Parse(dt.Rows[i]["PartID"].ToString()),
                    PartCode = dt.Rows[i]["PartCode"].ToString(),
                    PlantCode = dt.Rows[i]["PlantCode"].ToString(),
                    PlantID = int.Parse(dt.Rows[i]["PlantID"].ToString()),
                    DUNS = dt.Rows[i]["DUNS"].ToString(),
                    CycleCountLevel = int.Parse(dt.Rows[i]["CycleCountLevel"].ToString()),
                    PartStatus = int.Parse(dt.Rows[i]["PartStatus"].ToString()),
                    SupplierID = int.Parse(dt.Rows[i]["SupplierID"].ToString()),
                    PartChineseName = dt.Rows[i]["PartChineseName"].ToString(),
                    WorkLocation = dt.Rows[i]["WorkLocation"].ToString(),
                    FollowUp = dt.Rows[i]["FollowUp"].ToString(),
                    CategoryID = int.Parse(dt.Rows[i]["CategoryID"].ToString()),
                    Specs = dt.Rows[i]["Specs"].ToString(),
                    Workshops = dt.Rows[i]["Workshops"].ToString(),
                    Segments = dt.Rows[i]["Segments"].ToString(),
                });
            }
            return list;
        }
        public IQueryable<ViewPart> QueryPartofScope(string startCode, string endCode)
        {
            var partQry = Context.ViewPart.AsQueryable();
            if (!string.IsNullOrEmpty(startCode))
            {
                partQry = partQry.Where(p => string.Compare(p.PartCode, startCode) >= 0);
            }
            if (!string.IsNullOrEmpty(endCode))
            {
                partQry = partQry.Where(p => string.Compare(p.PartCode, endCode) <= 0);
            }
            return partQry.OrderBy(p => p.PartCode);
          
        }

        public List<ViewPart> QueryPartsByCodePlant(List<ViewPart> list)
        {
            Type[] types = new Type[] { typeof(ViewPart) };
            XmlSerializer xs = new XmlSerializer(typeof(List<ViewPart>), types);
            string itemStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, list);
                itemStr = sw.ToString();
            }
            DbParameter paramItems = Context.CreateDbParameter("@parts", System.Data.DbType.Xml, itemStr, System.Data.ParameterDirection.Input);
            //DbDataReader reader = 
            DataTable dt = Context.LoadDataTable("sp_GetPartsByCodePlant", CommandType.StoredProcedure, paramItems);
            DataSet ds = new DataSet();
            list = new List<ViewPart>();
            //while (reader.Read())
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new ViewPart { PartID = int.Parse(dt.Rows[i]["PartID"].ToString()), PartCode = dt.Rows[i]["PartCode"].ToString(), PlantCode = dt.Rows[i]["PlantCode"].ToString(), DUNS = dt.Rows[i]["DUNS"].ToString() });
            }
            return list;
        }
        public void ImportPart(List<ViewPart> list)
        {
            Type[] types = new Type[] { typeof(ViewPart) };
            XmlSerializer xs = new XmlSerializer(typeof(List<ViewPart>), types);
            string itemStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, list);
                itemStr = sw.ToString();
            }
            DbParameter paramItems = Context.CreateDbParameter("@partItems", System.Data.DbType.Xml, itemStr, System.Data.ParameterDirection.Input);

            Context.ExecuteNonQuery("sp_ImportPart", CommandType.StoredProcedure, paramItems);
        }
    }
}
