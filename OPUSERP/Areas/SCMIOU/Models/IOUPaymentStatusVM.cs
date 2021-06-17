using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMIOU.Models
{
    public class IOUPaymentStatusVM
    {
        public string iouNo { get; set; }

        public string projectName { get; set; }

        public string attentionTo { get; set; }

        public string iouDate { get; set; }

        public string paymentDate { get; set; }

        public string adjustmentDate { get; set; }

        public decimal? paymentAmount { get; set; }

        public decimal? adjustmentAmount { get; set; }

        public decimal? returnAmount { get; set; }

        public decimal? balanceAmount { get; set; }

    }
}
