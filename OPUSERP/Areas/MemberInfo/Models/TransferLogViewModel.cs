using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class TransferLogViewModel
    {
        public string employeeID { get; set; }

        public string transfarID { get; set; }

        public string workStation { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }

        public int grade { get; set; }

        public string designation { get; set; }

        public string department { get; set; }

        public string employeeNameCode { get; set; }

        public TransferLogLn fLang { get; set; }

        public IEnumerable<SalaryGrade> salaryGrade { get; set; }

        public IEnumerable<MemberTransferLog> transferLogs { get; set; }
        
    }
}
