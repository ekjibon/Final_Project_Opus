﻿using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class DepartmentViewModel
    {
        public string departmentId { get; set; }
        [Required]
        [Display(Name = "Department Code")]
        public string deptCode { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string deptName { get; set; }
        public string deptNameBn { get; set; }
        public string shortName { get; set; }
        public DateTime? startDate { get; set; }

        public DepartmentInfoLn fLang { get; set; }

        public IEnumerable<Department> departments { get; set; }
    }
}