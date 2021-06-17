using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VMS.Models
{
    public class LogBookReportViewModel
    {
        public string reqNo { get; set; }
        public string reqDate { get; set; }
        public string reqBy { get; set; }

        public string journeyDate { get; set; }
        public string timetoleave { get; set; }
        public string journeyTime { get; set; }
        public string journeyFrom { get; set; }
        public string journeyDestination { get; set; }
        public string reason { get; set; }
        public decimal? odometerValuejourneyStart { get; set; }
        public decimal? odometerValuejourneyEnd { get; set; }
        public decimal? petrolOpeningStock { get; set; }
        public decimal? petrolSupply { get; set; }
        public decimal? petrolUse { get; set; }
        public string comments { get; set; }
        public decimal? totalmeter { get; set; }
        public decimal? pertrolStock { get; set; }
    }
}
