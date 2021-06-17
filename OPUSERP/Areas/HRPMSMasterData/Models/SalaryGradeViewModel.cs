using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class SalaryGradeViewModel
    {
        public int salaryGradeId { get; set; }
        [Required]
        [Display(Name ="Grade Name")]
        public string gradeName { get; set; }

        [Required]
        [Display(Name = "Basic Amount")]
        public decimal basicAmount { get; set; }

        [Required]
        [Display(Name = "Pay Scale")]
        public string payScale { get; set; }

        [Required]
        [Display(Name = "Current Basic")]
        public decimal currentBasic { get; set; }

        public SalaryGradeLn fLang { get; set; }

        public IEnumerable<SalaryGrade> salaryGrades { get; set; }
    }
}
