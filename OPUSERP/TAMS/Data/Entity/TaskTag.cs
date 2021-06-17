using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Data.Entity
{
    [Table("TaskTag", Schema = "TMS")]
    public class TaskTag:Base
    {
        public int? taskInformationId { get; set; }
        public TaskInformation taskInformation { get; set; }

        public string tag { get; set; }
    }
}
