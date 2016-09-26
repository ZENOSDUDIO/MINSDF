using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SGM.ECount.DataModel
{
    [DataContract, XmlRoot("StocktakeRequest")]
    public class NewStocktakeRequest
    {
        [DataMember, XmlElement("IsStatic")]
        public bool IsStatic { get; set; }

        [DataMember, XmlElement("IsCycleCount")]
        public bool IsCycleCount { get; set; }

        [DataMember, XmlElement("RequestID")]
        public long? RequestID { get; set; }

        [DataMember, XmlElement("RequestBy")]
        public int? RequestBy { get; set; }

        [DataMember, XmlElement("PlantID")]
        public int? PlantID { get; set; }

        [DataMember, XmlArray("Details")]
        public List<NewStocktakeDetails> Details { get; set; }


        public StocktakeRequest ConvertToRequest()
        {
            StocktakeRequest request = new StocktakeRequest() { IsStatic = this.IsStatic, RequestBy = new User { UserID = this.RequestBy.Value }, Plant = new Plant { PlantID = PlantID.Value } };
            if (RequestID != null)
            {
                request.RequestID = RequestID.Value;
            }
            request.StocktakeDetails = new System.Data.Objects.DataClasses.EntityCollection<StocktakeDetails>();
            foreach (NewStocktakeDetails item in this.Details)
            {
                StocktakeDetails details = new StocktakeDetails()
                {
                    Part = new Part { PartID = int.Parse(item.PartID) },
                    StocktakeType = new StocktakeType { TypeID = item.StocktakeTypeID },
                    StocktakePriority = new StocktakePriority { PriorityID = item.StocktakePriority },
                    Description = item.Description,
                    PreDynamicNoticeCode = item.PreDynamicNoticeCode,
                    PreDynamicNotiTime = item.PreDynamicNotiTime,
                    PreStaticNoticeCode = item.PreStaticNoticeCode,
                    PreStaticNotiTime = item.PreStaticNotiTime
                };
                request.StocktakeDetails.Add(details);
            }
            return request;
        }

    }
    [DataContract, XmlRoot("StocktakeDetails")]
    public class NewStocktakeDetails
    {
        [DataMember, XmlElement("DetailsID")]
        public string DetailsID { get; set; }

        [DataMember, XmlElement("PartID")]
        public string PartID { get; set; }

        [DataMember, XmlElement("StocktakeTypeID")]
        public int StocktakeTypeID { get; set; }

        [DataMember, XmlElement("StocktakePriorityID")]
        public int StocktakePriority { get; set; }

        [DataMember, XmlElement("Description")]
        public string Description { get; set; }

        [DataMember, XmlElement("NotifyComments")]
        public string NotifyComments { get; set; }


        [DataMember, XmlElement("PreStaticNotiTime")]
        public DateTime? PreStaticNotiTime { get; set; }

        [DataMember, XmlElement("PreDynamicNotiTime")]
        public DateTime? PreDynamicNotiTime { get; set; }

        [DataMember, XmlElement("PreStaticNoticeCode")]
        public string PreStaticNoticeCode { get; set; }

        [DataMember, XmlElement("PreDynamicNoticeCode")]
        public string PreDynamicNoticeCode { get; set; }
    }

    public partial class ViewStockTakeRequest
    {
        public ViewStockTakeRequest()
        {
            _RequestID = 0;
            //_IsCycleCount = false;
            _UserName = DefaultValue.STRING;
            _GroupName = DefaultValue.STRING;
        }
        public void CreateViewByPart(Part part)
        {
            this.PartID = part.PartID;
            this.PartCode = part.PartCode;
            this.PartChineseName = PartChineseName;
            this.PartEnglishName = part.PartEnglishName;
            this.PartStatus = part.PartStatus.StatusID;
            this.StatusName = part.PartStatus.StatusName;
            this.PlantID = part.Plant.PlantID;
            this.PlantName = part.Plant.PlantName;
            if (part.Supplier != null)
            {
                this.DUNS = part.Supplier.DUNS;
            }
            this.FollowUp = part.FollowUp;
            this.CategoryName = part.PartCategory.CategoryName;
            this.LevelName = part.CycleCountLevel.LevelName;
            this.CategoryID = part.PartCategory.CategoryID;
            this.CycleCountLevel = part.CycleCountLevel.LevelID;

        }
    }

    public partial class View_StocktakeDetails
    {
        public View_StocktakeDetails()
        {
            _RequestID = 0;
            //_IsCycleCount = false;
            _RequestUser = DefaultValue.STRING;
            _GroupName = DefaultValue.STRING;
        }
        public void CreateViewByPart(Part part)
        {
            this.PartID = part.PartID;
            this.PartCode = part.PartCode;
            this.PartChineseName = part.PartChineseName;
            this.PartEnglishName = part.PartEnglishName;
            if (part.PartStatus!=null)
            {
                this.PartStatus = part.PartStatus.StatusID;
                this.StatusName = part.PartStatus.StatusName; 
            }
            this.PlantID = part.Plant.PlantID;
            this.PartPlantName = part.Plant.PlantName;
            this.PartPlantCode = part.Plant.PlantCode;
            this.Workshops = part.Workshops;
            this.WorkLocation = part.WorkLocation;
            this.Segments = part.Segments;
            if (part.Supplier != null)
            {
                this.DUNS = part.Supplier.DUNS;
            }
            this.FollowUp = part.FollowUp;

            if (part.PartCategory!=null)
            {
                this.CategoryName = part.PartCategory.CategoryName;
                this.CategoryID = part.PartCategory.CategoryID; 
            }
            this.LevelName = part.CycleCountLevel.LevelName;
            this.CycleCountLevel = part.CycleCountLevel.LevelID;
            this.PreDynamicNoticeCode = part.PreDynamicNotiCode;
            this.PreDynamicNotiTime = part.PreDynamicNotiTime;
            this.PreStaticNoticeCode = part.PreStaticNotiCode;
            this.PreStaticNotiTime = part.PreStaticNotiTime;

        }

        [DataMember]
        public string CSMTDUNS { get; set; }
        [DataMember]
        public decimal? Wip { get; set; }
        [DataMember]
        public string UnRecorded { get; set; }
        [DataMember]
        public decimal? M080 { get; set; }
    }

}
