using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class EmployeeTypeViewModel
    {
        public int empTypeId { get; set; }
        [Required]
        [Display(Name = "Employee Type")]
        public string empType { get; set; }
        public string empTypeBn { get; set; }

        public string shortName { get; set; }

        public EmployeeTypeLn fLang { get; set; }

        public IEnumerable<EmployeeType> employeeTypes { get; set; }
    }
}
