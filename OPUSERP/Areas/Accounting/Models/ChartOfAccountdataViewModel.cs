using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class ChartOfAccountdataViewModel
    {

        public string accountName { get; set; }
        public string accountCode { get; set; }
        public string accountGroup { get; set; }
        public string natureName { get; set; }

    }
}
