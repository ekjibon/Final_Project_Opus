using OPUSERP.SCM.Data.Entity.Disbarse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMIOU.Models
{
    public class DisbarseViewModel
    {
        public string disbarseNo { get; set; }
        public DateTime? disbarseDate { get; set; }
        public string iouNo { get; set; }
        public string status { get; set; }

        public IEnumerable<DisbarseMaster> disbarseMaster { get; set; }
    }
}
