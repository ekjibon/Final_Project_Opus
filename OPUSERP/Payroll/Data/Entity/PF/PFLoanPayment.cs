using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("PFLoanPayment", Schema = "Payroll")]
    public class PFLoanPayment : Base
    {
 
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; } 
        public int? pFLoanId { get; set; }
        public PFLoan pFLoan { get; set; }

      

        public decimal paymentAmount { get; set; }
        public DateTime? paymentDate { get; set; }
      
    }
}
