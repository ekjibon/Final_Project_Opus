using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models.Lang
{
    public class TransferLogLn
    {
        public string title { get; set; }
        public string membertitle { get; set; }

        public string sl { get; set; }

        public string workStation { get; set; }
        public string organization { get; set; }
        public string address { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

        public string grade { get; set; }

        public string designation { get; set; }

        public string action { get; set; }
    }
}
