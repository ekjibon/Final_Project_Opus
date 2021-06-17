using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLibrary.Models.Lang
{
    public class BorrowerInfoLn
    {
        public string title { get; set; }
        public string id { get; set; }
        public string borrowerName { get; set; }
        public string borrowDate { get; set; }
        public string returnDate { get; set; }
        public string designation { get; set; }

        public string action { get; set; }
    }
}
