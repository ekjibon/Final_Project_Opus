using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class ReferenceViewModel
    {
        public int? candidateId { get; set; }
        public int? refID { get; set; }
        public string refName { get; set; }
        public string refOrganization { get; set; }
        public string refDesignation { get; set; }
        public string refEmail { get; set; }
        public string refContact { get; set; }

        public string employeeNameCode { get; set; }

        public IEnumerable<RcrtReference> rcrtReferences { get; set; }
    }
}
