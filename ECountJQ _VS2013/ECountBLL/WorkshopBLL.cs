using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{

    public class WorkshopBLL : BaseGenericBLL<Workshop>
    {
        public WorkshopBLL()
            : base("Workshop")
        {

        }

        public WorkshopBLL(ECountContext context)
            : base(context, "Workshop")
        {

        }

        public Workshop GetWorkshopbykey(Workshop info)
        {
            //info = GetObjectByKey(info);
            info = this.Context.Workshop.Include("Plant").FirstOrDefault(w => w.WorkshopID == info.WorkshopID);
            return info;
        }

        public List<Workshop> GetWorkshops()
        {
            return _context.Workshop.Include("Plant").Where(w=>w.Available==true).ToList();
        }


        public List<Workshop> GetWorkshopbyPlant(Plant plant)
        {
            ECountContext _context = new ECountContext();
            List<Workshop> wsList = _context.Workshop.Include("Plant").Where(p => p.Plant.PlantID == plant.PlantID && p.Available ==true).ToList();
            if (wsList != null && wsList.Count > 0)
            {
                return wsList;
            }
            return new List<Workshop>();
        }

        public List<Workshop> GetWorkshopbyPlantID(int plantID)
        {
            ECountContext _context = new ECountContext();
            List<Workshop> wsList = _context.Workshop.Include("Plant").Where(p => p.Plant.PlantID == plantID && p.Available == true).ToList();
            if (wsList != null && wsList.Count > 0)
            {
                return wsList;
            }
            return new List<Workshop>();
        }

        public void UpdateWorkshop(Workshop workshop)
        {
            this.UpdateObject(workshop);
        }  

        public void DeleteWorkshop(Workshop workshop)
        {
            Workshop workshopInfo = this.GetObjectByKey(workshop);
            workshopInfo.Available = false;
            workshopInfo.Segment.Load();
            foreach (var segment in workshopInfo.Segment)
            {
                segment.Available = false;
            }
            this.UpdateObject(workshopInfo);
            
        }

        public void AddWorkshop(Workshop workshop)
        {
            this.AddObject(workshop);
        }
    }
}
