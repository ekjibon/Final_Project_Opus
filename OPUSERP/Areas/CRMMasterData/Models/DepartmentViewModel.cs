//using UMBRELLA.Areas.MasterData.Models.Lang;

using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
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

        //public DepartmentInfoLn fLang { get; set; }

        public IEnumerable<CRMDepartment> departments { get; set; }
    }
}
