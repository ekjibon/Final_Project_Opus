using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("AdvancePayment", Schema = "Payroll")]
    public class AdvancePayment : Base
    {
        public int advanceAdjustmentId { get; set; }
        public AdvanceAdjustment advanceAdjustment { get; set; }       

        public int salaryPeriodId { get; set; }
        public decimal adjustedAmount { get; set; }
       
    }
}
