using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class CFSMasterViewModel
    {

        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public IEnumerable<CFSDetailViewModel> cFSDetailViewModels{ get; set; }
       
        public Company Company { get; set; }
        
    }
}
