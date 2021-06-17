using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class TrialBalanceSPViewModel
    {
        public int? Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
       
        public decimal? DRAmount { get; set; }
        public decimal? CRAmount { get; set; }
       
    }
}
