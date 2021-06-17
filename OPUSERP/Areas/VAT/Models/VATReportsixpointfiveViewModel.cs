//using JSON.Data.Entity.Inventory;
//using JSON.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportsixpointfiveViewModel
    {
        public string PersonName { get; set; }
        public string bin { get; set; }
        public string fromStore { get; set; }
        public string fromStoreAddress { get; set; }
        public string toStore { get; set; }
        public string chalanNo { get; set; }
        public DateTime? chalanDate { get; set; }

        public decimal? totalAmount { get; set; }
        public decimal? totalPrice { get; set; }
        public decimal? totalVAT { get; set; }

        public IEnumerable<VATReportSixPointFiveChild> vATReportSixPointFives { get; set; }

    }
}
