using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.SOR;

namespace OPUSERP.Areas.VMS.Models
{
    public class SORMasterViewModel
    {
        public string sorNumber { get; set; }

        public string sorTitle { get; set; }

        public string duration { get; set; }

        public int? numberOfItems { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

        public decimal? grandTotal { get; set; } 

        public IEnumerable<SORDetails> sORDetails { get; set; }
        public IEnumerable<SORMaster> sORMasters { get; set; }
    }
}
