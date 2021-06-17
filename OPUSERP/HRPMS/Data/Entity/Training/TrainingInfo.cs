using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("TrainingInfo", Schema = "HR")]
    public class TrainingInfo : Base
    {
        public string name { get; set; }

        public int? planDetailsId { get; set; }
        public PlanDetails planDetails { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endDate { get; set; }

        public int? organizationId { get; set; } //  For venue purpose
        public Organization organization { get; set; }

        public int maxParticipate { get; set; }

        public int numberOfFreeSeat { get; set; }
    }
}
