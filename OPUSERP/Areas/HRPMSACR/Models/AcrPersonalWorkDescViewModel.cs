using OPUSERP.Areas.HRPMSACR.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class AcrPersonalWorkDescViewModel
    {
        public int acrInitiateId { get; set; }
        public string description { get; set; }
        public int acrpersonalworkdescId { get; set; }
        //public ACRLn flang { get; set; }

        public IEnumerable<AcrPersonalWorkDescription> acrPersonalWorkDescriptions { get; set; }

        public AcrPersonalWorkDescription acrPersonalWorkDescription { get; set; }

        public int noOfChildren { get; set; }

        public AcrInitiate acrInitiate { get; set; }
    }
}
