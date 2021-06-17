using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Stock
{
    [Table("ItemReqMaster", Schema = "SCM")]
    public class ItemReqMaster:Base
    {
        public string reqNo { get; set; }
        public DateTime? reqDate { get; set; }
        
    }
}
