using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("PlanDetails", Schema = "HR")]
    public class PlanDetails:Base
    {
        public string name { get; set; }

        public int? planId { get; set; }
        public Plan plan { get; set; }

        public int? trainingOfferId { get; set; }
        public TrainingOffer trainingOffer { get; set; }

        public string numberOfTraining { get; set; }

    }
}
