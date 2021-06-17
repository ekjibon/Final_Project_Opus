using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class RequisitionApprovalViewModel
    {
        public int? Id { get; set; }
        public string requisitionNumber { get; set; }
        public DateTime? requisitionDate { get; set; }
        public string nameEnglish { get; set; }
        public int? isApproved { get; set; }
        public decimal? NetTotal { get; set; }
        public string distributorType { get; set; }
    }
}
