using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Data.Entity
{
    [Table("TaskHours", Schema = "TMS")]
    public class TaskHours:Base
    {
        public int? taskInformationId { get; set; }
        public TaskInformation taskInformation { get; set; }

        public decimal? hours { get; set; }

        public DateTime? date { get; set; }
    }
}
