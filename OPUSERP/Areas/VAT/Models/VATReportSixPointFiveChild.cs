using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportSixPointFiveChild
    {
        public string itemName { get; set; }
        public decimal? qnt { get; set; }
        public decimal? price { get; set; }
        public decimal? vat { get; set; }
        public string comment { get; set; }
    }
}
