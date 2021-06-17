using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportsixpointThreeSumViewModel
    {
        public string registeredPerson { get; set; }
        public string registeredPersonBIN { get; set; }
        public string invoiceNo { get; set; }
        public DateTime? issueDate { get; set; }

        public string customer { get; set; }
        public string customerBIN { get; set; }
        public string address { get; set; }

        public string officerName { get; set; }
        public string designation { get; set; }

        public string details { get; set; }
        public string unitName { get; set; }
        public decimal? quantity { get; set; }
        public decimal? unitPrice { get; set; }

        public decimal? totalValue { get; set; }
        public decimal? sdAmount { get; set; }
        public decimal? vatRate { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? totalWithVAT { get; set; }
        
    }
}
