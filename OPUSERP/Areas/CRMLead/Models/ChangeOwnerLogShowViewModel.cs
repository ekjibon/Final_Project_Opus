using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class ChangeOwnerLogShowViewModel
    {
        public string leadName { get; set; }
        public string nextOwner { get; set; }
        public int id { get; set; }
        public string peviousOwner { get; set; }
        public string createdBy { get; set; }
        public DateTime? createdAt { get; set; }
        public int SLNo { get; set; }
    }
}
