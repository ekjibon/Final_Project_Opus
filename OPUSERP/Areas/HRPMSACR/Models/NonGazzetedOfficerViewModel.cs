using OPUSERP.Areas.HRPMSACR.Models.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class NonGazzetedOfficerViewModel
    {
        [Required]
        public int? employeeId { get; set; }
        public string designation { get; set; }

        public string joiningDate { get; set; }
        public string isSuccedded { get; set; }
        public string employeeType { get; set; }
        public string employeeName { get; set; }
        public string dateOfBirth { get; set; }
        public string educationQualification { get; set; }
        public string language { get; set; }
        public string training { get; set; }
        public string serviceDurationFromDate { get; set; }
        public string serviceDurationToDate { get; set; }

        public string fromDate { get; set; }
        
        public int toDate { get; set; }

        public NonGazzetedLn fLang { get; set; }

    }
}
