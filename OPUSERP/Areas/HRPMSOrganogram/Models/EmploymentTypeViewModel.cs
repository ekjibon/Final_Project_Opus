using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSOrganogram.Models
{
    public class EmploymentTypeViewModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Organization Type Name")]
        public string nameEN { get; set; }

        public string nameBN { get; set; }
        public string remarks { get; set; }

        public EmploymentTypeLn fLang { get; set; }

        public IEnumerable<EmploymentType> employmentTypes { get; set; }
    }
}
