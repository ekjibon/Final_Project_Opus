using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class CFSDetailViewModel
    {
        public string fsLineName { get; set; }
        public string noteName { get; set; }
        public string noteHead { get; set; }
        public decimal? Amount { get; set; }
        
    }
}
