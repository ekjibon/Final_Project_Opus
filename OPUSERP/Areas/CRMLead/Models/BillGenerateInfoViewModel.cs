using OPUSERP.CRM.Data.Entity.Lead;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class BillGenerateInfoViewModel
    {       


        public string leadName { get; set; }
        public string agreementNo { get; set; }
        public string invoiceNo { get; set; }
        public DateTime? billDate { get; set; }
        public string ratingType { get; set; }
        public decimal? billAmount { get; set; }
        public string vatType { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? collectedAmount { get; set; }
        public decimal? dueAmount { get; set; }

    }
}
