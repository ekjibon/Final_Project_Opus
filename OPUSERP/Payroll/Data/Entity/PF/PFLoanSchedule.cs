using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("PFLoanSchedule", Schema = "Payroll")]
    public class PFLoanSchedule : Base
    {
        public int? pFLoanId { get; set; }
        public PFLoan pFLoan { get; set; }
     

        public int salaryPeriodId { get; set; }
       
       

        public decimal restAmount { get; set; }
        public decimal installmentAmount { get; set; }
        public int noOfInstallment { get; set; }
      
        [DefaultValue(0)]
        public int? isComplete { get; set; }

        public int? PFLoanPaymentId { get; set; }
        public PFLoanPayment PFLoanPayment { get; set; }
   
        
    }
}
