using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalarySlab", Schema = "Payroll")]
    public class SalarySlab : Base
    {        
        public int salaryGradeId { get; set; }
        public SalaryGrade salaryGrade { get; set; }
        [MaxLength(100)]
        public string slabName { get; set; }
        
        public decimal slabAmount { get; set; }
        public DateTime? effectDate { get; set; }

    }
}
