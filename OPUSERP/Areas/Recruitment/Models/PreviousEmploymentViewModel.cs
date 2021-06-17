using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class PreviousEmploymentViewModel
    {
        public int? candidateId { get; set; }
        public int? previousEmploymentID { get; set; }
        public string companyName { get; set; }
        public int? organizationType { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public string companyBusiness { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string companyLocation { get; set; }
        public string responsibilities { get; set; }

        public IEnumerable<HRPMSOrganizationType> hRPMSOrganizationTypes { get; set; }
        public IEnumerable<RcrtPreviousEmployment> rcrtPreviousEmployments { get; set; }
    }
}
