using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class RVMasterViewModel
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public decimal? Amount { get; set; }
   
        public IEnumerable<RVDetailViewModel> rVDetailViewModels{ get; set; }

        public IEnumerable<RVDetailViewModel> cashOpeningBalance { get; set; }

        public Company Company { get; set; }
        
    }
}
