using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Workflow.Data.Entity
{
    [Table("ActionInfo", Schema = "WMS")]
    public class ActionInfo:Base
    {
        public string description { get; set; }
    }
}
