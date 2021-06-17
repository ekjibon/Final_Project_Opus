using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Disbarse
{
    [Table("DisbarseMaster", Schema = "SCM")]
    public class DisbarseMaster : Base
    {
        public string disbarseNo { get; set; }
        public DateTime? disbarseDate { get; set; }
        public int? statusId { get; set; }
        public string remarks { get; set; }
        public int? userId { get; set; }
    }
}
