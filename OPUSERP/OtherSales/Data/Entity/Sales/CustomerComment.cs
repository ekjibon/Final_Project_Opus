using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    public class CustomerComment: Base
    {
        public int? resourceId { get; set; }
        public Resource resource { get; set; }

        [MaxLength(250)]
        public string titles { get; set; }
        public string comments { get; set; }
    }
}
