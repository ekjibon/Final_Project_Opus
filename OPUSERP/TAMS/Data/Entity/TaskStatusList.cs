using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Data.Entity
{
    [Table("TaskStatusList", Schema = "TMS")]
    public class TaskStatusList:Base
    {
        public int? taskInformationId { get; set; }
        public TaskInformation taskInformation { get; set; }

        public string status { get; set; }

        public string remarks { get; set; }

        public DateTime? date { get; set; }
    }
}
