using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{
    public class PlantBLL:BaseGenericBLL<Plant>
    {
        public PlantBLL()
            : base("Plant")
        {

        }

        public PlantBLL(ECountContext context)
            : base(context,"Plant")
        {

        }

        public List<Plant> GetPlants()
        {
            return _context.Plant.Include("Workshop").Where(p=>p.Available==true).ToList();
        }

        public Plant GetPlantbykey(Plant info)
        {
            info = GetObjectByKey(info);
            return info;
        }

        //public List<Workshop> GetWorkShopByPlant(Plant plant)
        //{ }

        public void DeletePlant(Plant plant)
        {
            Plant plantInfo = this.GetObjectByKey(plant);
            plantInfo.Available = false;
            plantInfo.Workshop.Load();
            foreach (var item in plantInfo.Workshop)
            {

                item.Available = false;
                item.Segment.Load();
                foreach (var segment in item.Segment)
                {
                    segment.Available = false;
                }
            }
            UpdatePlant(plantInfo);
        }
        public Plant AddPlant(Plant plant)
        {
            plant = this.AddObject(plant);
            return plant;
        }

        public void UpdatePlant(Plant plant)
        {
            this.UpdateObject(plant);
        }
        public List<Plant> QueryPlant(Plant condition)
        {
            return this.QueryPlant(condition, true);
        }
        public List<Plant> QueryPlant(Plant condition,bool isExact)
        {
            IQueryable<Plant> query = Context.Plant.AsQueryable<Plant>();
            if (!string.IsNullOrEmpty(condition.PlantCode))
            {
                if (isExact)
                {

                query = query.Where(p => string.Compare(p.PlantCode, condition.PlantCode) == 0);
                }
                else
                {
                    query = query.Where(p => p.PlantCode.Contains(condition.PlantCode));
                }
            }
            if (!string.IsNullOrEmpty(condition.PlantName))
            {
                if (isExact)
                {
                    query = query.Where(p => string.Compare(p.PlantName, condition.PlantName) == 0);
                }
                else
                {
                    query = query.Where(p => p.PlantName.Contains(condition.PlantName));
                }
            }
            return query.ToList();
        }
    }
}
