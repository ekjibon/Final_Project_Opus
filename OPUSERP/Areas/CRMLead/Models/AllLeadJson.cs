using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class AllLeadJson
    {
        public string leadName { get; set; }
        public string leadOwner { get; set; }
        public string sectorName { get; set; }
        public int sectorId { get; set; }
        public string action { get; set; }
    }
}
