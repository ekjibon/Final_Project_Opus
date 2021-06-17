using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class PFLoanPaymentViewModel
    {
        public int PfloanpaymentId { get; set; }
        public int employeeInfoId { get; set; }
        public decimal paymentAmount { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? pFLoanId { get; set; }

        public int? installmentno { get; set; }

        public IEnumerable<PFLoanPayment> pFLoanPayments { get; set; }
     
        public string visualEmpCodeName { get; set; }

    }
}
