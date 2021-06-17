using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("WorkProgressActivityTypes", Schema = "SCM")]
    public class WorkProgressActivityType:Base
    {
        public string activityName { get; set; }
        public int? shortOrder { get; set; }
    }
}
