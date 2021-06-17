using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Data.Entity.FDR
{
    [Table("FDREncashment", Schema = "ACCOUNT")]
    public class FDREncashment : Base
    {
        public int? fDRInvestmentDetailId { get; set; }
        public FDRInvestmentDetail fDRInvestmentDetail { get; set; }

        public DateTime? encashDate { get; set; }
        public decimal? provisionAmount { get; set; }
        public decimal? receiveAmount { get; set; }
        public decimal? exciseDuty { get; set; }
        public decimal? percentDiff { get; set; }
        public decimal? diffAmount { get; set; }
        public decimal? advanceTax { get; set; }
        public int? typeOfDiff { get; set; }
        public decimal? prematurePercent { get; set; }

        public string accountName { get; set; }

        public int? principleBankId { get; set; }
        public Bank principleBank { get; set; }

        public string principleBankAccountNumber { get; set; }

        public int? interestBankId { get; set; }
        public Bank interestBank { get; set; }

        public string interestBankAccountNumber { get; set; }

        public decimal? totalAmount { get; set; }
        public string encashStatus { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
        
    }
}
