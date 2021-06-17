using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class ActivityLogShowViewModel
    {
        public string leadName { get; set; }
        public string actionName { get; set; }
        public int id { get; set; }
        public string actionDetails { get; set; }
        public string createdBy { get; set; }
        public DateTime? createdAt { get; set; }
        public int SLNo { get; set; }
    }
}
