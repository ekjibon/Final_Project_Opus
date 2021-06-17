using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("TrainingInfoDetails", Schema = "HR")]
    public class TrainingInfoDetails:Base
    {
        public int? trainingInfoId { get; set; }
        public TrainingInfo trainingInfo { get; set; }

        public string name { get; set; }

        public string startTimestamp { get; set; }

        public string endTimestamp { get; set; }

        public int? trainerId { get; set; }
        public Trainer trainer { get; set; }

        public string content { get; set; }
    }
}
