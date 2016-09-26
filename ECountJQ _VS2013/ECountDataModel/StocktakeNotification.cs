using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SGM.ECount.DataModel
{
    public partial class StocktakeNotification
    {
        [DataMember]
        public List<View_StocktakeDetails> DetailsView
        {
            get;
            set;
        }
        public S_StocktakeNotification MakeSerializable()
        {
            S_StocktakeNotification result = new S_StocktakeNotification
            {
                CreatedBy = this.Creator.UserID,
                IsStatic = this.IsStatic.Value,
                NotificationCode = this.NotificationCode,
                NotificationID = this.NotificationID
            };
            if (this.Plant!=null)
            {
                result.PlantID = this.Plant.PlantID;
            }
            result.Details = new List<NewStocktakeDetails>();
            foreach (var item in this.DetailsView)
            {
                NewStocktakeDetails detail = new NewStocktakeDetails
                {
                    Description = item.Description,
                    DetailsID = item.DetailsID.ToString(),
                    PartID = item.PartID.ToString(),
                    NotifyComments = item.NotifyComments
                };
                if (item.Priority!=null)
                {
                    detail.StocktakePriority = item.Priority.Value; 
                }
                if (item.StocktakeType!=null)
                {
                    detail.StocktakeTypeID = item.StocktakeType.Value; 
                }
                
                result.Details.Add(detail);
            }
            return result;
        }
    }

    /// <summary>
    /// Serializable StocktakeNotification
    /// </summary>
    [DataContract, XmlRoot("StocktakeNotification")]
    public class S_StocktakeNotification
    {

        [DataMember, XmlElement("NotificationID")]
        public long? NotificationID { get; set; }

        [DataMember, XmlElement("NotificationCode")]
        public string NotificationCode { get; set; }

        [DataMember, XmlElement("IsStatic")]
        public bool IsStatic { get; set; }

        [DataMember, XmlElement("CreatedBy")]
        public int CreatedBy { get; set; }

        [DataMember, XmlElement("PlantID")]
        public int? PlantID { get; set; }

        [DataMember, XmlArray]
        public List<NewStocktakeDetails> Details
        { get; set; }
    }

    //public partial class StocktakeItem
    //{
    //    public S_StocktakeItem MakeSerializable()
    //    {
    //        S_StocktakeItem item = new S_StocktakeItem { ItemID = this.ItemID, EndCSN = this.EndCSN, Line = this.Line.Value, Machining = this.Machining, QI = this.QI, StartCSN = this.StartCSN, Store = this.Store,Block=this.Block };
    //        return item;
    //    }

    //}

    [XmlRoot("StocktakeItem")]
    public class S_StocktakeItem
    { 
        [XmlElement]
        public long ItemID { get; set; }
        [XmlElement]
        public decimal? Line { get; set; }
        [XmlElement]
        public decimal? Machining { get; set; }
        [XmlElement]
        public decimal? Store { get; set; }
        [XmlElement]
        public string StartCSN { get; set; }
        [XmlElement]
        public string EndCSN { get; set; }
        [XmlElement]
        public decimal? Block { get; set; }
        [XmlElement]
        public decimal? Available { get; set; }
        [XmlElement]
        public decimal? QI { get; set; }
        [XmlElement]
        public int? BlockAdjust { get; set; }
        [XmlElement]
        public int? AvailableAdjust { get; set; }
        [XmlElement]
        public int? QIAdjust { get; set; }
        [XmlElement]
        public string FillinTime { get; set; }
        [XmlElement]
        public int? FillinBy { get; set; }
    }

    [XmlRoot("SupplierStocktakeItem")]
    public class S_SupplierStocktakeItem
    {
        [XmlElement]
        public long ItemID { get; set; }
        [XmlElement]
        public decimal? Available { get; set; }
        [XmlElement]
        public decimal? QI { get; set; }
        [XmlElement]
        public decimal? Block { get; set; }
        [XmlElement]
        public int? AvailableAdjust { get; set; }
        [XmlElement]
        public int? QIAdjust { get; set; }
        [XmlElement]
        public int? BlockAdjust { get; set; }
        [XmlElement]
        public string FillinTime { get; set; }
        [XmlElement]
        public int? FillinBy { get; set; }
    }

    public partial class View_DifferenceAnalyse
    {
        [DataMember]
        public string DiffFilter { get; set; }
        [DataMember]
        public string TimesFilter { get; set; }
        [DataMember]
        public string ProfitLossFilter { get; set; }
        [DataMember]
        public string DiffSumFilter { get; set; }
    }

}
