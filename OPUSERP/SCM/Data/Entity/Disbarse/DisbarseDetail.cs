using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.IOU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Disbarse
{
    [Table("DeisbarseDetails", Schema = "SCM")]
    public class DisbarseDetail:Base
    {
        public int? disbarseId { get; set; }
        public DisbarseMaster Disbarse { get; set; }

        public int? IOUId { get; set; }
        public IOUMaster IOU { get; set; }

        public int? statusId { get; set; }
        public string issueBy { get; set; }
        public DateTime? issueDate { get; set; }
        public int? userId { get; set; }
    }
}
