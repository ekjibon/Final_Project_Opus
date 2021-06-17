using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.TrainingNew
{
    [Table("TrainingResourcePerson", Schema = "HR")]
    public class TrainingResourcePerson: Base
    {
        public int? trainingInfoNewId { get; set; }
        public TrainingInfoNew trainingInfoNew { get; set; }

        public int? resourcePersonId { get; set; }
        public ResourcePerson resourcePerson { get; set; }

        public string comments { get; set; }

        public string remarks { get; set; }
    }
}
