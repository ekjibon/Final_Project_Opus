using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models.Lang
{
    public class TransferPosting
    {
        public string workStation { get; set; }
        public string trFromDate { get; set; }
        public string trToDate { get; set; }
        public string duration { get; set; }
        public string grade { get; set; }
        public string designation { get; set; }
        public string action { get; set; }
    }
}
