using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.PF
{
    [Table("FdrInvestment", Schema = "Payroll")]
    public class FdrInvestment:Base
    {
        public string Ftno { get; set; }
        public string FromBank { get; set; }
        public string FromBranch { get; set; }
        public string AccountNumber { get; set; }
        public string ToBank { get; set; }
        public string ToBranch { get; set; }
        public string ChequeNo { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Rate { get; set; } 
        public DateTime? OpenDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public int? Tenure { get; set; }
        public decimal? Interest { get; set; }

    }
}
