using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    public class RcrtEmpMembership : Base
    {
        public int candidateId { get; set; }
        public CandidateInfo candidate { get; set; }

        public string nameOrganization { get; set; }
        public string membershipNo { get; set; }

        public int? membershipId { get; set; }
        public Membership membership { get; set; }

        public string remarks { get; set; }
    }
}
