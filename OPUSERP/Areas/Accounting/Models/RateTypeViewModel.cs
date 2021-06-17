using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class RateTypeViewModel
    {
        public int rateTypeId { get; set; }
        public string rateTypeName { get; set; }

        public IEnumerable<RateType> rateTypes { get; set; }
    }
}
