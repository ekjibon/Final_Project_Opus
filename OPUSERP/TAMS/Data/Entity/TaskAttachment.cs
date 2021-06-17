using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Data.Entity
{
    [Table("TaskAttachment", Schema = "TMS")]
    public class TaskAttachment:Base
    {
        public int? taskInformationId { get; set; }
        public TaskInformation taskInformation { get; set; }

        public string fileTitle { get; set; }

        public string url { get; set; }
    }
}
