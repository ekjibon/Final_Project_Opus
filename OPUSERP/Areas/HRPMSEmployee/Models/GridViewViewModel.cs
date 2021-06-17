using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Models.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class GridViewViewModel
    {
        public string employeeNameCode { get; set; }

        [Required]
        [Display(Name = "Employee Id")]
        public string employeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string employeeName { get; set; }

        public string employeeInfo { get; set; }

        public string addressInfo { get; set; }

        public string educational { get; set; }

        public string spouseInfo { get; set; }

        public string childrenInfo { get; set; }

        public string drivingInfo { get; set; }

        public string passportInfo { get; set; }

        public string foreignTour { get; set; }

        public string trainingInfo { get; set; }

        public string membershipInfo { get; set; }

        public string languageInfo { get; set; }

        public string empPhotograph { get; set; }

        public string awardInfo { get; set; }

        public string publicationInfo { get; set; }

        public string promotionInfo { get; set; }

        public string transferInfo { get; set; }

        public GridViewLn fLang { get; set; }
    }
}
