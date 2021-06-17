using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("EmployeesBonusStructure", Schema = "Payroll")]
    public class EmployeesBonusStructure : Base
    {
        public int? bonousStructureId { get; set; }
        public BonousStructure bonousStructure { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
       

        
    }
}
