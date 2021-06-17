using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SlabIncomeTaxAssign", Schema = "Payroll")]
    public class SlabIncomeTaxAssign : Base
    {
        public int slabTypeId { get; set; }
        public SlabType slabType { get; set; }

        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }             

       
    }
}
