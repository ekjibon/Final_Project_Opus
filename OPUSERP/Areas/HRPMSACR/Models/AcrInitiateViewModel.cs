using OPUSERP.Areas.HRPMSACR.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class AcrInitiateViewModel
    {
        
        public int employeeId { get; set; }
        public string acrType { get; set; }
        public int? isArchive { get; set; }
        public string acrDate { get; set; }
        public string archivedate { get; set; }
        public string lateReason { get; set; }
        //public int acrInitiateId { get; set; }

        public ACRLn flang { get; set; }

        public IEnumerable<AcrInitiate> acrInitiates { get; set; }

        public AcrInitiate acrInitiate { get; set; }


    }
}
