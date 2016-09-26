using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;

namespace SGM.ECount.BLL
{
   public class DifferenceAnalyseDetailsBLL : BaseGenericBLL<DifferenceAnalyseDetails>
    {
        public DifferenceAnalyseDetailsBLL()
            : base("DifferenceAnalyseDetails")
        {

        }

        public List<DifferenceAnalyseDetails> GetDifferenceAnalyseDetails()
        {
            return _context.DifferenceAnalyseDetails.Include("UserGroup").Where(d => d.Available == true).OrderBy(d=>d.UserGroup.GroupName).ToList();
        }

        public DifferenceAnalyseDetails GetDiffAnalyseDetailstbyID(int detailsID)
        {
            return _context.DifferenceAnalyseDetails.Include("UserGroup").FirstOrDefault(d => d.DetailsID == detailsID);
        }

        public void UpdateDiffAnalyseDetail(DifferenceAnalyseDetails detail)
        {
            this.UpdateObject(detail);
        }

        public void AddDiffAnalyseDetail(DifferenceAnalyseDetails detail)
        {
            this.AddObject(detail);
        }

        public void DeleteDiffAnalyseDetail(DifferenceAnalyseDetails detail)
        {
            DifferenceAnalyseDetails detailInfo = this.GetObjectByKey(detail);
            detailInfo.Available = false;
            this.UpdateDiffAnalyseDetail(detailInfo);
        }

        public bool ExistDifferenceAnalyse(DifferenceAnalyseDetails model)
        {
            IQueryable<DifferenceAnalyseDetails> qryResult = Context.DifferenceAnalyseDetails;
            if (!string.IsNullOrEmpty(model.Description))
            {
                qryResult = qryResult.Where(p => p.Description == model.Description && p.Available == true && p.UserGroup.GroupID == model.UserGroup.GroupID && p.DetailsID != model.DetailsID);
            }

            if (qryResult.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
