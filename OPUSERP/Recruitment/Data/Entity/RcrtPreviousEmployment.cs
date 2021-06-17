using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("RcrtPreviousEmployment", Schema = "RCRT")]
    public class RcrtPreviousEmployment : Base
    {
        public int candidateId { get; set; }
        public CandidateInfo candidate { get; set; }

        public int? organizationTypeId { get; set; }
        public HRPMSOrganizationType organizationType { get; set; }

        public string companyName { get; set; }

        public string position { get; set; }

        public string department { get; set; }

        public string companyBusiness { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public string companyLocation { get; set; }

        public string responsibilities { get; set; }
    }
}
