using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class LanguageViewModel
    {
        public string employeeID { get; set; }

        public string languageId { get; set; }

        [Required]
        [Display(Name ="Language")]
        public string language { get; set; }

        [Display(Name = "Proficiency")]
        public string proficiency { get; set; }
        public string reading { get; set; }
        public string writing { get; set; }
        public string speaking { get; set; }
        public string action { get; set; }
        public LanguageLn fLang { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<EmployeeLanguage> employeeLanguages { get; set; }

        public IEnumerable<Language> languages { get; set; }
    }
}
