using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VMS.Models
{
    public class ServiceReportViewModel
    {
        public string vehicleName { get; set; }

        public string supplierName { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

        public decimal? subTotal { get; set; }

        public Int32? SrlNo { get; set; }
        public string workorderDate { get; set; }

        public string issueDate { get; set; }

        public string billCommitteeDate { get; set; }
        public string itemName { get; set; }
        
    }
}
