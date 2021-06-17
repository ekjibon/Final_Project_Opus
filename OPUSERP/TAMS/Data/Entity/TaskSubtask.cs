using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Data.Entity
{
    [Table("TaskSubtask", Schema = "TMS")]
    public class TaskSubtask:Base
    {
        public int? taskInformationId { get; set; }
        public TaskInformation taskInformation { get; set; }

        public string name { get; set; }

        public string remarks { get; set; }

        public string details { get; set; }

        public DateTime? satrtDate { get; set; }

        public DateTime? endDate { get; set; }

        public int? status { get; set; }

        public string statusDetails { get; set; }
    }
}
