using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public partial class StorageRecord
    {
        public S_StorageRecord MakeSerializable()
        {
            return new S_StorageRecord { Available = this.Available, SLOCID = this.StoreLocation.LocationID, QI = this.QI, Block = this.Block, PartID = this.Part.PartID };
        }
    }
    
    public class S_StorageRecord
    {
        public int PartID { get; set; }
        public int SLOCID { get; set; }
        public decimal? Available { get; set; }
        public decimal? QI { get; set; }
        public decimal? Block { get; set; }
        public decimal? Price { get; set; }
        public int DetailsID { get; set; }
    }
}
