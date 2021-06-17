using OPUSERP.Areas.HRPMSRetirementAndTermination.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.RetirementAndTermination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSRetirementAndTermination.Models
{
    public class PRLApplicationViewModel
    {
        [Required]
        [Display(Name ="Employee ID")]
        public int? employeeId { get; set; }
        public string applicationName { get; set; }
        public string applicationDate { get; set; }
        public string type { get; set; }
        public string fileTitle { get; set; }
        public string uploadFile { get; set; }

        public int? resignId { get; set; }
        public DateTime? resignDate { get; set; }
        public DateTime? lastWorkingDate { get; set; }
        public string resignReason { get; set; }


        public IEnumerable<PRLApplication> pRLApplications { get; set; }
        public IEnumerable<ResignInformation> resignInformation { get; set; }
        public PRLApplication pRLApplicationsById { get; set; }
        public PRLApplicationLn flang { get; set; }
    }
}
