using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportsixpointtenSaleViewModel
    {

        public string invoiceNo { get; set; }
        public DateTime? invoiceDate { get; set; }
        public string saler { get; set; }
        public string salerAddress { get; set; }
        public string salerId { get; set; }

        public string officerName { get; set; }
        public string designation { get; set; }

        public decimal? amount { get; set; }
       




    }
}
