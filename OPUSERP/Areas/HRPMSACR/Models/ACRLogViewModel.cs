using OPUSERP.Areas.HRPMSACR.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class ACRLogViewModel
    {
        public int? ID { get; set; }
        public int employeeId { get; set; }

        public DateTime? startDate { get; set; }

        public string supervisorName { get; set; }

        public string supervisorDesignation { get; set; }

        public DateTime? endDate { get; set; }

        public string signatoryName { get; set; }

        public string signatoryDesignation { get; set; }

        public string remarks { get; set; }

        public ACRLn fLang2 { get; set; }


        public IEnumerable<AcrInfo> acrInfos { get; set; }
    }
}
