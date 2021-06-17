using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models
{
    public class RequisitionMasterForStatusVM
    {
        public string reqNo { get; set; }

        public string csNo { get; set; }

        public string projectName { get; set; }

        public DateTime? reqDate { get; set; }

        public decimal? reqValue { get; set; }

        public decimal? csValue { get; set; }

        public string subject { get; set; }

        public string action { get; set; }
    }
}
