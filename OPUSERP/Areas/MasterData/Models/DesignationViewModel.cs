//using UMBRELLA.Areas.MasterData.Models.Lang;

using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MasterData.Models
{
    public class DesignationViewModel
    {
        //[Display(Name = "Employee Type")]
        //public int? empType { get; set; }

        public int designationId { get; set; }

        [Required]
        [Display(Name = "Designation Code")]
        public string designationCode { get; set; }

        [Required]
        [Display(Name = "Designation Name")]
        public string designationName { get; set; }

        public string designationNameBn { get; set; }

        [Required]
        [Display(Name = "Short Name")]
        public string shortName { get; set; }

        //public DesignationLn fLang { get; set; }

        public IEnumerable<Designation> designations { get; set; }

    }
}
